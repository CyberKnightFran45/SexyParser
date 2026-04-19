using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a NativeString in the RtSystem. </summary>

internal static class RtNativeString
{
/// <summary> The Encoding used </summary>

private const EncodingType ENCODING = EncodingType.US_ASCII;

// Decode native string

private static string Decode(NativeBuffer buffer, ref ulong pos)
{
using var nOwner = buffer.GetStringByVarLen(pos, out int varLen, ENCODING);

pos += (ulong)varLen;
pos += nOwner.Size;

return nOwner.ToString();
}

/** <summary> Reads a NativeString from RTON and Writes it to JSON. </summary>

<param name = "reader"> The RTON Reader. </param>
<param name = "writer"> The JSON Writer. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

internal static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, bool isPropertyName)
{
string str = Decode(buffer, ref pos);

RtString.WriteJson(writer, str, isPropertyName);
}

// Decode index string

private static string DecodeIndexed(NativeBuffer buffer, ref ulong pos)
{
int strIndex = buffer.GetVarInt(pos, out int varLen);
pos += (ulong)varLen;

return ReferenceStrings.Get(strIndex, false);
}

// Decode raw value and add it to cache

private static string DecodeRaw(NativeBuffer buffer, ref ulong pos)
{
string str = Decode(buffer, ref pos);
ReferenceStrings.Add(str, false);

return str;
}

/** <summary> Reads a CachedString from RTON and Write it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isIndexed"> Determines if the NativeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

internal static void ReadCached(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer,
                              bool isIndexed, bool isPropertyName)
{
var str = isIndexed ? DecodeIndexed(buffer, ref pos) : DecodeRaw(buffer, ref pos);

RtString.WriteJson(writer, str, isPropertyName);
}

// Encode index string

private static void EncodeIndexed(string str, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.NATIVE_STRING_INDEX);
pos++;

int strIndex = ReferenceStrings.IndexOf(str, false);
pos += (ulong)buffer.SetVarInt(pos, strIndex);
}

// Encode raw value and add it to cache

private static void EncodeRaw(string str, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.NATIVE_STRING_VALUE);
pos++;

pos += buffer.SetStringByVarLen(pos, str, ENCODING);

ReferenceStrings.Add(str, false);
}

/** <summary> Writes a CachedString to RTON, by indexing or adding it. </summary>

<param name = "writer"> The Stream where the RTON Data will be Written. </param>
<param name = "str"> The String to be Written. </param> */

internal static void Write(string str, NativeBuffer buffer, ref ulong pos)
{

if(ReferenceStrings.Contain(str, false) )
EncodeIndexed(str, buffer, ref pos);

else
EncodeRaw(str, buffer, ref pos);

}

}

}