using System.IO;
using System.Text;
using System.Text.Json;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Represents a NativeString in the RtSystem. </summary>

public static class RtNativeString
{
/// <summary> The Encoding used </summary>

private static readonly EncodingType encoding = EncodingType.US_ASCII;

/** <summary> Reads a NativeString from RTON and Writes it to JSON. </summary>

<param name = "reader"> The RTON Reader. </param>
<param name = "writer"> The JSON Writer. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

public static void Read(Stream reader, Utf8JsonWriter writer, bool isPropertyName)
{
using var nOwner = reader.ReadStringByVarLen(encoding);

RtString.Decode(writer, nOwner.AsSpan(), isPropertyName);
}

/** <summary> Reads a CachedString from RTON and Write it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isIndexed"> Determines if the NativeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

public static void ReadCached(Stream reader, Utf8JsonWriter writer, bool isIndexed,
                              bool isPropertyName)
{
string str;

if(isIndexed)
{
int strIndex = reader.ReadVarInt();

str = ReferenceStrings.Get(strIndex, false);
}

else
{
using var sOwner = reader.ReadStringByVarLen(encoding);
str = sOwner.ToString();

ReferenceStrings.Add(str, false);
}

RtString.Decode(writer, str, isPropertyName);
}

/** <summary> Writes a CachedString to RTON, by indexing or adding it. </summary>

<param name = "writer"> The Stream where the RTON Data will be Written. </param>
<param name = "str"> The String to be Written. </param> */

public static void Write(Stream writer, string str)
{

if(ReferenceStrings.Contain(str, false) )
{
writer.WriteByte(RTypeId.NATIVE_STRING_INDEX);

int strIndex = ReferenceStrings.IndexOf(str, false);
writer.WriteVarInt(strIndex);
}

else
{
writer.WriteByte(RTypeId.NATIVE_STRING_VALUE);
writer.WriteStringByVarLen(str, encoding);

ReferenceStrings.Add(str, false);
}

}

}

}