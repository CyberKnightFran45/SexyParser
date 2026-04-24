namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a variant-length integer in the RtSystem. </summary>

internal static class RtVarInt
{
// Write VarInt or ZigZag (Core)

private static void WriteCore32(uint val, byte flags, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, flags);
pos++;

pos += (ulong)buffer.SetVarInt(pos, val);
}

// Write raw VarInt

internal static void WriteVarInt(uint val, NativeBuffer buffer, ref ulong pos)
{
WriteCore32(val, RTypeId.VARINT, buffer, ref pos);
}

// Write raw ZigZag

internal static void WriteZigZag32(uint val, NativeBuffer buffer, ref ulong pos)
{
WriteCore32(val, RTypeId.ZIGZAG32, buffer, ref pos);
}

// Write VarLong or ZigZag64 (Core)

private static void WriteCore64(ulong val, byte flags, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, flags);
pos++;

pos += (ulong)buffer.SetVarInt64(pos, val);
}

// Write raw varlong

internal static void WriteVarLong(ulong val, NativeBuffer buffer, ref ulong pos)
{
WriteCore64(val, RTypeId.VARLONG, buffer, ref pos);
}

// Write ZigZag64

internal static void WriteZigZag64(ulong val, NativeBuffer buffer, ref ulong pos)
{
WriteCore64(val, RTypeId.ZIGZAG64, buffer, ref pos);
}

}

}