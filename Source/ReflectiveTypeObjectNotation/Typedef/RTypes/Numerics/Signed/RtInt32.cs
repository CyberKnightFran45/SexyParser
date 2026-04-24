namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 32-bits signed integer in the RtSystem. </summary>

internal static class RtInt32
{
// Write int

private static void Write(int v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.INT32);
pos++;

buffer.SetInt32(pos, v);
pos += 4;
}

// Pack Int32

internal static void Pack(int v, NativeBuffer buffer, ref ulong pos)
{
uint u = BinaryHelper.EncodeZigZag(v);
int packedSize = BinaryHelper.ComputeVarIntLength(u);

if(packedSize < 4)
RtVarInt.WriteZigZag32(u, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}