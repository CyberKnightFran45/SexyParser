namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a String in the RtSystem. </summary>

internal static class RtString
{
/** <summary> Evaluates a Json String and writes it as RTON. </summary>

<param name = "reader"> JSON reader. </param>
<param name = "buffer"> RTON buffer. </param> */

internal static void Write(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
string str = reader.GetString();

if(RtNullString.TryWrite(str, buffer, ref pos) )
{
// Null string is written if success
}

else if(RtIdNullString.TryWrite(str, buffer, ref pos) )
{
// Null RTID is written if success
}

else if(RtIdString.TryWrite(str, buffer, ref pos) )
{
// RTID is written if success
}

else if(RtBinaryString.TryWrite(str, buffer, ref pos) )
{
// Binary string is written if success
}

else if(EncodeHelper.IsASCII(str) )
RtNativeString.Write(str, buffer, ref pos);

else
RtUnicodeString.Write(str, buffer, ref pos);

}

}

}