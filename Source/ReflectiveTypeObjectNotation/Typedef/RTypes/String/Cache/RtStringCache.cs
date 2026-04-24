using System;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a String Cache in the RtSystem. </summary>

internal static class RtStringCache
{
// Read cache string

internal static string Read(NativeBuffer buffer, EncodingType encoding, ref ulong pos)
{
using var nOwner = buffer.GetStringByVarLen(pos, out int varLen, encoding);

pos += (ulong)varLen;
pos += nOwner.Size;

return nOwner.ToString();
}

// Write cache string

internal static void Write(ReadOnlySpan<char> str, NativeBuffer buffer,
                           EncodingType encoding, ref ulong pos)
{
pos += buffer.SetStringByVarLen(pos, str, encoding);
}

}

}