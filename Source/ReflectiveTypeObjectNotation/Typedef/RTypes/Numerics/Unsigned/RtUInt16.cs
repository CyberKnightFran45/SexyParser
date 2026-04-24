namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 16-bits unsigned integer in the RtSystem. </summary>

internal static class RtUInt16
{
// Write ushort

private static void Write(ushort v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.UINT16);
pos++;

buffer.SetUInt16(pos, v);
pos += 2;
}

// Pack UInt16

internal static void Pack(ushort v, NativeBuffer buffer, ref ulong pos)
{
int packedSize = RtVarInt.ComputeSize(v);

if(packedSize < 2)
RtVarInt.WriteVarLong(v, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}