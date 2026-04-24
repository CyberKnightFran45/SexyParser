namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 16-bits signed integer in the RtSystem. </summary>

internal static class RtInt16
{
// Write short

private static void Write(short v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.INT16);
pos++;

buffer.SetInt16(pos, v);
pos += 2;
}

// Pack Int16

internal static void Pack(short v, NativeBuffer buffer, ref ulong pos)
{
uint u = BinaryHelper.EncodeZigZag(v);
int packedSize = BinaryHelper.ComputeVarIntLength(u);

if(packedSize < 2)
RtVarInt.WriteZigZag32(u, buffer, ref pos);

else
Write(v, buffer, ref pos);

}

}

}