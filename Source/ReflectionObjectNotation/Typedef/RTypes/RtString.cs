using System;
using System.IO;
using System.Text.Json;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Represents a String in the RtSystem. </summary>

public static class RtString
{
/** <summary> Evaluates a Json String and writes it as RTON. </summary>

<param name = "reader"> The JSON reader. </param>
<param name = "writer"> The RTON writer. </param> */

public static void Encode(NativeJsonReader reader, Stream writer)
{
string str = reader.GetString();

bool isNative = EncodeHelper.IsASCII(str);
bool isRtid = RtidString.IsValid(str);

if(isRtid)
RtidString.Write(writer, str);

else if(isNative)
RtNativeString.Write(writer, str);

else
RtUnicodeString.Write(writer, str);

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