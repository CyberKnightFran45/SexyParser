namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 64-bits signed integer in the RtSystem. </summary>

internal static class RtInt64
{
// Write long

private static void Write(long v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.INT64);
pos++;

buffer.SetInt64(pos, v);
pos += 8;
}

// Pack Int32

internal static void Pack(long v, NativeBuffer buffer, ref ulong pos)
{
ulong u = BinaryHelper.EncodeZigZag64(v);
int packedSize = BinaryHelper.ComputeVarIntLength(u);

if(packedSize < 8)
RtVarInt.WriteZigZag64(u, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}