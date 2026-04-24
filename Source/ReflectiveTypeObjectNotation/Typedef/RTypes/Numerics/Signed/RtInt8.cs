namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 8-bits signed integer in the RtSystem. </summary>

internal static class RtInt8
{
// Write signed byte

private static void Write(sbyte v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.INT8);
pos++;

buffer.SetInt8(pos, v);
pos++;
}

// Pack Int8

internal static void Pack(sbyte v, NativeBuffer buffer, ref ulong pos)
{
uint u = BinaryHelper.EncodeZigZag(v);
int packedSize = BinaryHelper.ComputeVarIntLength(u);

if(packedSize == 1)
RtVarInt.WriteZigZag32(u, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}