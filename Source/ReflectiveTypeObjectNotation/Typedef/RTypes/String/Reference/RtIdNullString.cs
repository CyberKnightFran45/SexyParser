using System;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a null ID reference in the RtSystem </summary>

internal static class RtIdNullString
{
/// <summary> Match pattern for null references </summary>

private const string PATTERN = "RTID(0)";

/** <summary> Reads a null RtID String and Writes it to JSON. </summary>

<param name = "writer"> The JSON writer. </param>
<param name = "isPropertyName"> Wheter to write string as a Property or not. </param> */

internal static void Read(Utf8JsonWriter writer, bool isPropertyName)
{

if(isPropertyName)
JsonHelper.WriteString(writer, PATTERN, true);

else
writer.WriteNullValue();

}

// Write null RTID (direct)

internal static void Write(NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.ID_STRING_NULL);

pos++;
}

// Try write null RTID

internal static bool TryWrite(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{

if(!str.SequenceEqual(PATTERN) )
return false;

Write(buffer, ref pos);

return true;
}

}

}