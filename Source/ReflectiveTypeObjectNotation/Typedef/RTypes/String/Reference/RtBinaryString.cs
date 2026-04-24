using System;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a Binary reference in the RtSystem </summary>

internal static class RtBinaryString
{
// Binary prefix

private const string BINARY_PREFIX = "$BINARY(\"";

// Prefix length

private const int PREFIX_LEN = 9;

// Min binary length

private const int MIN_STR_LEN = 14;

/// <summary> Match pattern for binary references </summary>

private const string PATTERN = "$BINARY(\"{0}\", {1})";

// Read binary string

private static string Decode(NativeBuffer buffer, ref ulong pos)
{
byte flags = buffer.GetUInt8(pos);
pos++;

string refStr = RtStringCache.Read(buffer, EncodingType.UTF8, ref pos);

int refAddress = buffer.GetVarInt(pos, out int varLen);
pos += (ulong)varLen;

return string.Format(PATTERN, refStr, refAddress);
}

/** <summary> Reads a Binary String and Writes its JSON equivalent </summary>

<param name = "reader"> RTON buffer </param>
<param name = "writer"> JSON writer </param>
<param name = "isPropertyName"> Wheter to write string as a Property or not. </param> */

internal static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, bool isPropertyName)
{
string str = Decode(buffer, ref pos);

JsonHelper.WriteString(writer, str, isPropertyName);
}

// Check if str follows BINARY bounds

private static bool FollowsBound(ReadOnlySpan<char> str)
{

return str.Length >= MIN_STR_LEN &&
       str.StartsWith(BINARY_PREFIX) &&
       str[^1] == ')';

}

// Get separator index

private static int GetSeparatorIndex(ReadOnlySpan<char> str)
{
int sepIndex = -1;

for(int i = str.Length - 2; i >= PREFIX_LEN; i--)
{

if(str[i] == '"')
{
sepIndex = i;

break;
}

}

return sepIndex;
}

// Write binary string

internal static bool TryWrite(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{

if(!FollowsBound(str) )
return false;

int sepIndex = GetSeparatorIndex(str);

if(sepIndex < PREFIX_LEN)
return false;

int refStrStart = PREFIX_LEN;
int refStrLen = sepIndex - PREFIX_LEN;

int addrStart = sepIndex + 3;
int addrLen = str.Length - 1 - addrStart;

if(addrLen <= 0)
return false;

var rawAddress = str.Slice(addrStart, addrLen);

if(!int.TryParse(rawAddress, out int refAddress) )
return false;

buffer.SetUInt8(pos, RTypeId.BINARY_STRING);
pos++;

buffer.SetUInt8(pos, 0x00);
pos++;

var refStr = str.Slice(refStrStart, refStrLen);
RtStringCache.Write(refStr, buffer, EncodingType.UTF8, ref pos);

pos += (ulong)buffer.SetVarInt(pos, refAddress);

return true;
}

}

}