namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 32-bits unsigned integer in the RtSystem. </summary>

internal static class RtUInt32
{
// Write uint

private static void Write(uint v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.UINT32);
pos++;

buffer.SetUInt32(pos, v);
pos += 4;
}

// Pack UInt32

internal static void Pack(uint v, NativeBuffer buffer, ref ulong pos)
{
int packedSize = BinaryHelper.ComputeVarIntLength(v);

if(packedSize < 4)
RtVarInt.WriteVarInt(v, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}