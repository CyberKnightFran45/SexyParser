using System;
using System.Globalization;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a ID reference in the RtSystem </summary>

internal static class RtIdString
{
// RTID prefix

private const string RTID_PREFIX = "RTID(";

// Prefix length

private const int PREFIX_LEN = 5; 

// Min RTID len

private const int MIN_RTID_LEN = 6; 

/// <summary> Match pattern for null references </summary>

private const string NULL_PATTERN = "RTID()";

/// <summary> Match pattern for UID strings </summary>

private const string UID_PATTERN = "RTID({0:d}.{1:d}.{2:X8}@{3})";

/// <summary> Match pattern for Alia refs </summary>

private const string ALIAS_PATTERN = "RTID({0}@{1})";

/// <summary> The Reference pointed is <b>null</b> </summary>

private const byte NULL_REFERENCE = 0x00;

/// <summary> The Reference pointed is a <b>UID</b> </summary>

private const byte UID_REFERENCE = 0x02;

/// <summary> The Reference pointed is an <b>Alias</b> </summary>

private const byte ALIAS_REFERENCE = 0x03;

// Get Uid String for RTID

private static string ReadUid(NativeBuffer buffer, ref ulong pos)
{
string refStr = RtString.ReadL2(buffer, ref pos);

int subType = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

int type = buffer.GetVarInt(pos, out int varLenB);
pos += (ulong)varLenB;

uint hash = buffer.GetUInt32(pos);
pos += 4;

return string.Format(UID_PATTERN, type, subType, hash, refStr);
}

// Get Alias String for RTID

private static string ReadAlias(NativeBuffer buffer, ref ulong pos)
{
string refStr = RtString.ReadL2(buffer, ref pos);
string aliasStr = RtString.ReadL2(buffer, ref pos);

return string.Format(ALIAS_PATTERN, aliasStr, refStr);
}

// Read id flags

private static byte ReadIdFlags(NativeBuffer buffer, ref ulong pos, bool isNull)
{

if(isNull)
return NULL_REFERENCE;

byte idFlags = buffer.GetUInt8(pos);
pos++;

return idFlags;
}

// Read id string

private static string Decode(byte flags, NativeBuffer buffer, ref ulong pos)
{
string str;

switch(flags)
{
case NULL_REFERENCE:
str = NULL_PATTERN;
break;

case UID_REFERENCE:
str = ReadUid(buffer, ref pos);
break;

case ALIAS_REFERENCE:
str = ReadAlias(buffer, ref pos);
break;

default:
TraceLogger.WriteError($"Unknown RtID type: {flags:X2} @ {pos}");

str = null;
break;
};

return str;
}

/** <summary> Reads a RtID String and Writes it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isPropertyName"> Wheter to write string as a Property or not. </param> */

internal static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer,
                          bool isPropertyName, bool isNull)
{
byte idFlags = ReadIdFlags(buffer, ref pos, isNull);
string str = Decode(idFlags, buffer, ref pos);

RtString.WriteJson(writer, str, isPropertyName);
}

// Check if str follow RTID bounds

private static bool FollowsBound(ReadOnlySpan<char> str)
{

return str.Length >= MIN_RTID_LEN &&
       str.StartsWith(RTID_PREFIX) &&
       str[^1] == ')';

}

// Check if str has single '@'

private static bool HasSingleAt(ReadOnlySpan<char> str)
{
int first = str.IndexOf('@');

if(first < 0)
return false;

return str[ (first + 1).. ].IndexOf('@') < 0;
}

// Check if str is a valid class name (C++ naming rule)

private static bool IsValidReference(ReadOnlySpan<char> str)
{

if(str.IsEmpty)
return false;

if(str.Length >= 2 && str[0] == ':' && str[1] == ':')
return false; // Name can't start with "::"

if(str.Length >= 2 && str[^2] == ':' && str[^1] == ':')
return false; // Name can't end with "::"

char first = str[0];
bool firstIsValid = char.IsLetter(first) || first == '_';

if(!firstIsValid)
return false;

for(int i = 1; i < str.Length; i++)
{
char c = str[i];

if(c == ':')
{

if(i + 1 >= str.Length || str[i + 1] != ':')
return false; // Must be "::"

i++;
continue;
}

bool isValid = char.IsLetterOrDigit(c) || c == '_';

if(!isValid)
return false;

}

return true;
}

// Check alias str

private static bool IsValidAlias(ReadOnlySpan<char> str)
{

foreach(char c in str)
{
bool isValid = char.IsLetterOrDigit(c) || c == '_' || c == '-';

if(!isValid)
return false;

}

return true;
}

// Check if Rtid is Alias

private static bool TryParseAlias(ReadOnlySpan<char> input,
                                  out ReadOnlySpan<char> aliasStr,
                                  out ReadOnlySpan<char> refStr)
{
aliasStr = default;
refStr = default;

if(!FollowsBound(input) )
return false;

var inner = input[PREFIX_LEN .. ^1];

if(!HasSingleAt(inner) )
return false;

int at = inner.IndexOf('@');

if(at <= 0 || at == inner.Length - 1)
return false;

var beforeAt = inner[.. at];

if(beforeAt.IsEmpty || !IsValidAlias(beforeAt) )
return false;

var afterAt = inner[ (at + 1) ..];

if(!IsValidReference(afterAt) )
return false;

aliasStr = beforeAt;
refStr = afterAt;

return true;
}

// Convert hex str to int

private static bool TryParseHex(ReadOnlySpan<char> hex, out uint result)
{
return uint.TryParse(hex, NumberStyles.HexNumber, null, out result);
}

// Check if Rtid is UID

private static bool TryParseUid(ReadOnlySpan<char> input,
                                out int type,
								out int subType,
                                out uint hash,
								out ReadOnlySpan<char> refStr)
{
type = 0;
subType = 0;

hash = 0;
refStr = default;

if(!FollowsBound(input) )
return false;

var inner = input[PREFIX_LEN .. ^1];

if(!HasSingleAt(inner) )
return false;

int at = inner.IndexOf('@');

if(at <= 0 || at >= inner.Length - 1)
return false;

var beforeAt = inner[.. at];
var afterAt = inner[ (at + 1) ..];

if(!IsValidReference(afterAt) )
return false;

int dot1 = beforeAt.IndexOf('.');

if(dot1 <= 0)
return false;

int after1 = dot1 + 1;
int dot2Rel = beforeAt[after1 .. ].IndexOf('.');

if(dot2Rel < 0)
return false;

int dot2 = after1 + dot2Rel;
int after2 = dot2 + 1;

if(beforeAt[after2.. ].IndexOf('.') >= 0)
return false;

var p1 = beforeAt[.. dot1];
var p2 = beforeAt[after1 .. dot2];
var p3 = beforeAt[after2 ..];

if(p3.Length != 8)
return false;

if(!TryParseHex(p1, out var sub1) )
return false;

if(!TryParseHex(p2, out var sub2) )
return false;

if(sub1 > int.MaxValue || sub2 > int.MaxValue)
return false;

if(!TryParseHex(p3, out var sub3) )
return false;

type = (int)sub1;
subType = (int)sub2;

hash = sub3;
refStr = afterAt;

return true;
}

// Write RTID identifier

private static void WriteIdentifier(byte flags, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.ID_STRING);
pos++;

buffer.SetUInt8(pos, flags);
pos++;
}

// Write UID

private static void WriteUid(NativeBuffer buffer, ref ulong pos,
                             ReadOnlySpan<char> refStr,
                             int subType, int type, uint hash)
{
WriteIdentifier(UID_REFERENCE, buffer, ref pos);

RtString.WriteL2(refStr, buffer, ref pos);

pos += (ulong)buffer.SetVarInt(pos, subType);
pos += (ulong)buffer.SetVarInt(pos, type);

buffer.SetUInt32(pos, hash);
pos += 4;
}

// Write Alias

private static void WriteAlias(NativeBuffer buffer, ref ulong pos,
                               ReadOnlySpan<char> refStr,
                               ReadOnlySpan<char> aliasStr)
{
WriteIdentifier(ALIAS_REFERENCE, buffer, ref pos);

RtString.WriteL2(refStr, buffer, ref pos);
RtString.WriteL2(aliasStr, buffer, ref pos);
}

// Write RTID string

internal static bool TryWrite(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{

if(str.SequenceEqual(NULL_PATTERN) )
{
WriteIdentifier(NULL_REFERENCE, buffer, ref pos);

return true;
}

else if(TryParseUid(str, out int type, out int subType, out uint hash, out var uidRef) )
{
WriteUid(buffer, ref pos, uidRef, subType, type, hash);

return true;
}

else if(TryParseAlias(str, out var alias, out var aliasRef) )
{
WriteAlias(buffer, ref pos, aliasRef, alias);

return true;
}

return false;
}

}

}