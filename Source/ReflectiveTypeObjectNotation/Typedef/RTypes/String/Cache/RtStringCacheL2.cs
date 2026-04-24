using System;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a String Cache in the RtSystem (L2 type) </summary>

internal static class RtStringCacheL2
{
// Read cache string

internal static string Read(NativeBuffer buffer, ref ulong pos)
{
uint rawLen = buffer.GetVarInt(pos, out int varLenA);
pos += (ulong)varLenA;

uint strLen = buffer.GetVarInt(pos, out int varLenB);
pos += (ulong)varLenB;

using var uOwner = buffer.GetString(pos, (int)rawLen);
pos += rawLen;

return uOwner.Substring(0, (int)strLen);
}

// Write cache string

internal static void Write(ReadOnlySpan<char> str, NativeBuffer buffer, ref ulong pos)
{
var rawLen = (uint)BinaryHelper.GetEncodedLength(str, EncodingType.UTF8);
pos += (ulong)buffer.SetVarInt(pos, rawLen);

var strLen = (uint)str.Length;
pos += (ulong)buffer.SetVarInt(pos, strLen);

pos += buffer.SetString(pos, str);
}

}

}