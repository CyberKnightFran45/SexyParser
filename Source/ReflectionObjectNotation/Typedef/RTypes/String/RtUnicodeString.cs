using System.IO;
using System.Text.Json;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Represents a Unicode String in the RtSystem. </summary>

public static class RtUnicodeString
{
/** <summary> Decodes a Unicode String from a RTON File. </summary>

<param name = "reader"> The RTON Reader. </param>

<returns> The Decoded String. </returns> */

private static string Decode(Stream reader)
{
int expectedLen = reader.ReadVarInt();
using var uOwner = reader.ReadStringByVarLen();

return uOwner.Substring(0, expectedLen);
}

/** <summary> Reads a Unicode String from RTON and writes it to JSON. </summary>

<param name = "reader"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "writer"> The Stream where the JSON Data will be Written. </param>
<param name = "isPropertyName"> Wheter to Write String. </param> */

public static void Read(Stream reader, Utf8JsonWriter writer, bool isPropertyName)
{
string str = Decode(reader);

RtString.Decode(writer, str, isPropertyName);
}

/** <summary> Reads a CachedString from a RTON and Write it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param>
<param name = "isIndexed"> Determines if the UnicodeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Wheter to write string as a PropertyName or not. </param> */

public static void ReadCached(Stream reader, Utf8JsonWriter writer, bool isIndexed,
bool isPropertyName)
{
string str;

if(isIndexed)
{
int strIndex = reader.ReadVarInt();

str = ReferenceStrings.Get(strIndex, true);
}

else
{
str = Decode(reader);

ReferenceStrings.Add(str, true);
}

RtString.Decode(writer, str, isPropertyName);
}

/** <summary> Writes a CachedString to RTON, by indexing or adding it. </summary>

<param name = "writer"> The RTON writer. </param>
<param name = "str"> The String to be Written. </param> */

public static void Write(Stream writer, string str)
{

if(ReferenceStrings.Contain(str, true) )
{
writer.WriteByte(RTypeId.UNICODE_STRING_INDEX);

int strIndex = ReferenceStrings.IndexOf(str, true);
writer.WriteVarInt(strIndex);
}

else
{
writer.WriteByte(RTypeId.UNICODE_STRING_VALUE);

writer.WriteVarInt(str.Length);
writer.WriteStringByVarLen(str);

ReferenceStrings.Add(str, true);
}

}

}

}