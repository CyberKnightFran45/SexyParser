namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 64-bits unsigned integer in the RtSystem. </summary>

internal static class RtUInt64
{
// Write ulong

internal static void Write(ulong v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.UINT64);
pos++;

buffer.SetUInt64(pos, v);
pos += 8;
}

// Pack UInt64

internal static void Pack(ulong v, NativeBuffer buffer, ref ulong pos)
{
int packedSize = RtVarInt.ComputeSize(v);

if(packedSize < 8)
RtVarInt.WriteVarLong(v, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}