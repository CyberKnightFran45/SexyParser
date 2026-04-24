using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a Unicode String in the RtSystem. </summary>

internal static class RtUnicodeString
{
/** <summary> Reads a Unicode String from RTON and writes it to JSON. </summary>

<param name = "buffer"> RTON buffer </param>
<param name = "writer"> JSON writer </param>
<param name = "isPropertyName"> Wheter to Write String. </param> */

internal static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, bool isPropertyName)
{
string str = RtStringCacheL2.Read(buffer, ref pos);

JsonHelper.WriteString(writer, str, isPropertyName);
}

// Decode index string

private static string DecodeIndexed(NativeBuffer buffer, ref ulong pos)
{
int strIndex = buffer.GetVarInt(pos, out int varLen);
pos += (ulong)varLen;

return ReferenceStrings.Get(strIndex, true);
}

// Decode raw value and add it to cache

private static string DecodeRaw(NativeBuffer buffer, ref ulong pos)
{
string str = RtStringCacheL2.Read(buffer, ref pos);
ReferenceStrings.Add(str, true);

return str;
}

/** <summary> Reads a CachedString from a RTON and Write it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isIndexed"> Determines if the UnicodeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

internal static void ReadCached(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer,
                              bool isIndexed, bool isPropertyName)
{
var str = isIndexed ? DecodeIndexed(buffer, ref pos) : DecodeRaw(buffer, ref pos);

JsonHelper.WriteString(writer, str, isPropertyName);
}

// Encode index string

private static void EncodeIndexed(string str, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.UNICODE_STRING_INDEX);
pos++;

int strIndex = ReferenceStrings.IndexOf(str, true);
pos += (ulong)buffer.SetVarInt(pos, strIndex);
}

// Encode raw value and add it to cache

private static void EncodeRaw(string str, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.UNICODE_STRING_CACHE);
pos++;

RtStringCacheL2.Write(str, buffer, ref pos);

ReferenceStrings.Add(str, true);
}

/** <summary> Writes a CachedString to RTON, by indexing or adding it. </summary>

<param name = "writer"> The RTON writer. </param>
<param name = "str"> The String to be Written. </param> */

internal static void Write(string str, NativeBuffer buffer, ref ulong pos)
{

if(ReferenceStrings.Contain(str, true) )
EncodeIndexed(str, buffer, ref pos);

else
EncodeRaw(str, buffer, ref pos);

}

}

}