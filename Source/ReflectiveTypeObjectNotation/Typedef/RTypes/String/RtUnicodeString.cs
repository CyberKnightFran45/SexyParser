using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a Unicode String in the RtSystem. </summary>

public static class RtUnicodeString
{
/** <summary> Decodes a Unicode String from a RTON File. </summary>

<param name = "reader"> The RTON Reader. </param>

<returns> The Decoded String. </returns> */

private static string Decode(NativeBuffer buffer, ref ulong pos)
{
int expectedLen = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

using var uOwner = buffer.GetStringByVarLen(pos, out int varLenB);
pos += uOwner.Size + (ulong)varLenB;

return uOwner.Substring(0, expectedLen);
}

/** <summary> Reads a Unicode String from RTON and writes it to JSON. </summary>

<param name = "reader"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "writer"> The Stream where the JSON Data will be Written. </param>
<param name = "isPropertyName"> Wheter to Write String. </param> */

public static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, bool isPropertyName)
{
string str = Decode(buffer, ref pos);

RtString.Decode(writer, str, isPropertyName);
}

/** <summary> Reads a CachedString from a RTON and Write it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isIndexed"> Determines if the UnicodeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

public static void ReadCached(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer,
                              bool isIndexed, bool isPropertyName)
{
string str;

if(isIndexed)
{
int strIndex = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

str = ReferenceStrings.Get(strIndex, true);
}

else
{
str = Decode(buffer, ref pos);

ReferenceStrings.Add(str, true);
}

RtString.Decode(writer, str, isPropertyName);
}

/** <summary> Writes a CachedString to RTON, by indexing or adding it. </summary>

<param name = "writer"> The RTON writer. </param>
<param name = "str"> The String to be Written. </param> */

public static void Write(string str, NativeBuffer buffer, ref ulong pos)
{

if(ReferenceStrings.Contain(str, true) )
{
buffer.SetUInt8(pos, RTypeId.UNICODE_STRING_INDEX);
pos++;

int strIndex = ReferenceStrings.IndexOf(str, true);
pos += (ulong)buffer.SetVarInt(pos, strIndex);
}

else
{
buffer.SetUInt8(pos, RTypeId.UNICODE_STRING_VALUE);
pos++;

pos += (ulong)buffer.SetVarInt(pos, str.Length);
pos += buffer.SetStringByVarLen(pos, str);

ReferenceStrings.Add(str, true);
}

}

}

}