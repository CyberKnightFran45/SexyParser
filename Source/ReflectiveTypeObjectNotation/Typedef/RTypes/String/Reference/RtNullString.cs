using System;
using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a null String in the RtSystem. </summary>

internal static class RtNullString
{
// Null string representation

private const string NULL_STR_REF = "*";

// Writes a null string

internal static bool TryWrite(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{

if(!str.SequenceEqual(NULL_STR_REF) )
return false;

buffer.SetUInt8(pos, RTypeId.NULL_STRING);
pos++;

return true;
}

// Read null string

internal static void Read(Utf8JsonWriter writer, bool isPropertyName)
{
JsonHelper.WriteString(writer, NULL_STR_REF, isPropertyName);
}

}

}