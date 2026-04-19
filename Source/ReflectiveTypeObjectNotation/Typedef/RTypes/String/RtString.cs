using System;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a String in the RtSystem. </summary>

internal static class RtString
{
// Read rton string

internal static string ReadL2(NativeBuffer buffer, ref ulong pos)
{
int rawLen = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

int strLen = buffer.GetVarInt(pos, out int varLenB);
pos += (ulong)varLenB;

using var uOwner = buffer.GetString(pos, rawLen);
pos += (ulong)rawLen;

return uOwner.Substring(0, strLen);
}

// Write rton string

internal static void WriteL2(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{
var rawLen = BinaryHelper.GetEncodedLength(str, EncodingType.UTF8);
pos += (ulong)buffer.SetVarInt(pos, rawLen);

pos += (ulong)buffer.SetVarInt(pos, str.Length);
pos += buffer.SetString(pos, str);
}
	
/** <summary> Evaluates a Json String and writes it as RTON. </summary>

<param name = "reader"> The JSON reader. </param>
<param name = "writer"> The RTON writer. </param> */

internal static void WriteRton(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
string str = reader.GetString();

if(RtIdString.TryWrite(str, buffer, ref pos) )
{
// RTID is written if success
}

else if(EncodeHelper.IsASCII(str) )
RtNativeString.Write(str, buffer, ref pos);

else
RtUnicodeString.Write(str, buffer, ref pos);

}

/** <summary> Writes a String to JSON as a PropertyName or a Value. </summary>

<param name = "writer"> The Json writer. </param>
<param name = "str"> The String to Write. </param>
<param name = "isPropertyName"> Wether to write a PropertyName or not. </param> */

internal static void WriteJson(Utf8JsonWriter writer, ReadOnlySpan<char> str, bool isPropertyName)
{

if(isPropertyName)
writer.WritePropertyName(str);

else
writer.WriteStringValue(str);

}

}

}