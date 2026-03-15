using System;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a ID reference in the RtSystem. </summary>

public static class RtidString
{
/// <summary> The Reference pointed is <b>null</b> </summary>

private const byte NULL_REFERENCE = 0x00;

/// <summary> The Reference pointed is a <b>UID</b> </summary>

private const byte UID_REFERENCE = 0x02;

/// <summary> The Reference pointed is an <b>Alias</b> </summary>

private const byte ALIAS_REFERENCE = 0x03;

// Invalid Alias Chars

private static readonly char[] InvalidAliasChars = [' ', '.'];

// Get Uid String for RTID

private static string ReadUid(NativeBuffer buffer, ref ulong pos)
{
const string PATTERN = "RTID({0:d}.{1:d}.{2:X8}@{3})";

int expectedLen = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

using var uOwner = buffer.GetStringByVarLen(pos, out int varLenB);
pos += uOwner.Size + (ulong)varLenB;

string refStr = uOwner.Substring(0, expectedLen);

int subType = buffer.GetVarInt(pos, out int varLenC);
pos += (ulong)varLenC;

int type = buffer.GetVarInt(pos, out int varLenD);
pos += (ulong)varLenD;

uint hash = buffer.GetUInt32(pos);
pos += 4;

return string.Format(PATTERN, type, subType, hash, refStr);
}

// Get Alias String for RTID

private static string ReadAlias(NativeBuffer buffer, ref ulong pos)
{
const string PATTERN = "RTID({0}@{1})";

int expectedRefLen = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

using var rOwner = buffer.GetStringByVarLen(pos, out int varLenB);
pos += rOwner.Size + (ulong)varLenB;

string refStr = rOwner.Substring(0, expectedRefLen);

int expectedAliasLen = buffer.GetVarInt(pos, out int varLenC);
pos += (ulong)varLenC;

using var aOwner = buffer.GetStringByVarLen(pos, out int varLenD);
pos += aOwner.Size + (ulong)varLenD;

string aliasStr = aOwner.Substring(0, expectedAliasLen);

return string.Format(PATTERN, aliasStr, refStr);
}

/** <summary> Reads a RtID String and Writes it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isPropertyName"> Wheter to write string as a Property or not. </param> */

public static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer,
                        bool isPropertyName, bool isNull)
{
byte idFlags;

if(isNull)
idFlags = NULL_REFERENCE;

else
{
idFlags = buffer.GetUInt8(pos);
pos++;
}

string str = idFlags switch
{
NULL_REFERENCE => "RTID()",
UID_REFERENCE => ReadUid(buffer, ref pos),
ALIAS_REFERENCE => ReadAlias(buffer, ref pos),
_ => null
};

RtString.Decode(writer, str, isPropertyName);
}

// Check if string is RTID

public static bool IsValid(ReadOnlySpan<char> str)
{

if(str.Length < 6)
return false;

if(!str.StartsWith("RTID(") )
return false;

if(str[^1] != ')')
return false;

if(str.Length == 6)
return true;

var inner = str[5..^1]; 
int atIndex = inner.IndexOf('@');

return atIndex > 0 && atIndex < inner.Length - 1;
}

// Check if Rtid is Alias

private static bool TryParseAlias(ReadOnlySpan<char> input, out ReadOnlySpan<char> aliaz,
                                  out ReadOnlySpan<char> reference)
{
aliaz = default;
reference = default;

var inner = input[5..^1];
int at = inner.LastIndexOf('@');

if(at <= 0 || at == inner.Length - 1)
return false;

var beforeAt = inner[..at];

if(beforeAt.IsEmpty || beforeAt.IndexOfAny(InvalidAliasChars) >= 0)
return false;

var afterAt = inner[(at + 1)..];

if(afterAt.IsEmpty || afterAt.IndexOf(' ') >= 0)
return false;

aliaz = beforeAt;
reference = afterAt;

return true;
}

// Convert hex str to int

private static bool TryParseHex(ReadOnlySpan<char> hex, out uint v)
{
v = 0;

try
{
v = Convert.ToUInt32(hex.ToString(), 16);

return true;
}

catch
{
return false;
}

}

// Check if Rtid is UID

private static bool TryParseUid(ReadOnlySpan<char> input, out int type, out int subType,
                                out uint hash, out ReadOnlySpan<char> reference)
{
type = subType = 0; 
hash = 0;
reference = default;

var inner = input[5..^1];
int at = inner.LastIndexOf('@');

if(at <= 0 || at == inner.Length - 1)
return false;

var beforeAt = inner[..at];
var afterAt = inner[(at + 1)..];

if(afterAt.IsEmpty || afterAt.IndexOf(' ') >= 0)
return false;

var parts = beforeAt.Split('.');
var enumerator = parts.GetEnumerator();

if(!enumerator.MoveNext() )
return false;

if(!TryParseHex(beforeAt[enumerator.Current], out uint sub1) )
return false;

if(!enumerator.MoveNext() )
return false;

if(!TryParseHex(beforeAt[enumerator.Current], out uint sub2) )
return false;

if(!enumerator.MoveNext() ) return false;

var p3 = beforeAt[enumerator.Current];

if(p3.Length != 8 || !TryParseHex(p3, out uint sub3) )
return false;

if(enumerator.MoveNext() )
return false; // ID must have three parts only

type = (int)sub1;
subType = (int)sub2;
hash = sub3;

reference = afterAt;

return true;
}

// Write RTID string

public static void Write(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.ID_STRING);
pos++;

if(TryParseUid(str, out int type, out int subType, out uint hash, out var uidRef) )
{
buffer.SetUInt8(pos, UID_REFERENCE);
pos++;

pos += (ulong)buffer.SetVarInt(pos, uidRef.Length);
pos += buffer.SetStringByVarLen(pos, uidRef);

pos += (ulong)buffer.SetVarInt(pos, subType);
pos += (ulong)buffer.SetVarInt(pos, type);

buffer.SetUInt32(pos, hash);
pos += 4;
}

else if(TryParseAlias(str, out var aliaz, out var aliasRef) )
{
buffer.SetUInt8(pos, ALIAS_REFERENCE);
pos++;

pos += (ulong)buffer.SetVarInt(pos, aliasRef.Length);
pos += buffer.SetStringByVarLen(pos, aliasRef);

pos += (ulong)buffer.SetVarInt(pos, aliaz.Length);
pos += buffer.SetStringByVarLen(pos, aliaz);
}

else
{
buffer.SetUInt8(pos, NULL_REFERENCE);
pos++;
}

}

}

}