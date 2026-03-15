using System;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a String in the RtSystem. </summary>

public static class RtString
{
/** <summary> Evaluates a Json String and writes it as RTON. </summary>

<param name = "reader"> The JSON reader. </param>
<param name = "writer"> The RTON writer. </param> */

public static void Encode(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
string str = reader.GetString();

bool isNative = EncodeHelper.IsASCII(str);
bool isRtid = RtidString.IsValid(str);

if(isRtid)
RtidString.Write(str, buffer, ref pos);

else if(isNative)
RtNativeString.Write(str, buffer, ref pos);

else
RtUnicodeString.Write(str, buffer, ref pos);

}

/** <summary> Writes a String to JSON as a PropertyName or a Value. </summary>

<param name = "writer"> The Json writer. </param>
<param name = "str"> The String to Write. </param>
<param name = "isPropertyName"> Wether to write a PropertyName or not. </param> */

public static void Decode(Utf8JsonWriter writer, ReadOnlySpan<char> str, bool isPropertyName)
{

if(isPropertyName)
writer.WritePropertyName(str);

else
writer.WriteStringValue(str);

}

}

}