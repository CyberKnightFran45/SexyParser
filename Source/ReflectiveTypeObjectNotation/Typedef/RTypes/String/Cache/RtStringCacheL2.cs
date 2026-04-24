using System;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a String Cache in the RtSystem (L2 type) </summary>

internal static class RtStringCacheL2
{
// Read cache string

internal static string Read(NativeBuffer buffer, ref ulong pos)
{
int rawLen = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

int strLen = buffer.GetVarInt(pos, out int varLenB);
pos += (ulong)varLenB;

using var uOwner = buffer.GetString(pos, rawLen);
pos += (ulong)rawLen;

return uOwner.Substring(0, strLen);
}

// Write cache string

internal static void Write(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{
var rawLen = BinaryHelper.GetEncodedLength(str, EncodingType.UTF8);
pos += (ulong)buffer.SetVarInt(pos, rawLen);

pos += (ulong)buffer.SetVarInt(pos, str.Length);
pos += buffer.SetString(pos, str);
}

}

}