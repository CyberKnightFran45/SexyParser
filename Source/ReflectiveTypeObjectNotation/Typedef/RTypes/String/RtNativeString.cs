using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a NativeString in the RtSystem. </summary>

public static class RtNativeString
{
/// <summary> The Encoding used </summary>

private const EncodingType ENCODING = EncodingType.US_ASCII;

/** <summary> Reads a NativeString from RTON and Writes it to JSON. </summary>

<param name = "reader"> The RTON Reader. </param>
<param name = "writer"> The JSON Writer. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

public static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, bool isPropertyName)
{
using var nOwner = buffer.GetStringByVarLen(pos, out int varLen, ENCODING);
pos += nOwner.Size + (ulong)varLen;

RtString.Decode(writer, nOwner.AsSpan(), isPropertyName);
}

/** <summary> Reads a CachedString from RTON and Write it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isIndexed"> Determines if the NativeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

public static void ReadCached(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer,
                              bool isIndexed, bool isPropertyName)
{
string str;

if(isIndexed)
{
int strIndex = buffer.GetVarInt(pos, out int varLen);
pos += (ulong)varLen;

str = ReferenceStrings.Get(strIndex, false);
}

else
{
using var sOwner = buffer.GetStringByVarLen(pos, out int varLen, ENCODING);
pos += sOwner.Size + (ulong)varLen;

str = sOwner.ToString();

ReferenceStrings.Add(str, false);
}

RtString.Decode(writer, str, isPropertyName);
}

/** <summary> Writes a CachedString to RTON, by indexing or adding it. </summary>

<param name = "writer"> The Stream where the RTON Data will be Written. </param>
<param name = "str"> The String to be Written. </param> */

public static void Write(string str, NativeBuffer buffer, ref ulong pos)
{

if(ReferenceStrings.Contain(str, false) )
{
buffer.SetUInt8(pos, RTypeId.NATIVE_STRING_INDEX);
pos++;

int strIndex = ReferenceStrings.IndexOf(str, false);
pos += (ulong)buffer.SetVarInt(pos, strIndex);
}

else
{
buffer.SetUInt8(pos, RTypeId.NATIVE_STRING_VALUE);
pos++;

pos += buffer.SetStringByVarLen(pos, str, ENCODING);

ReferenceStrings.Add(str, false);
}

}

}

}