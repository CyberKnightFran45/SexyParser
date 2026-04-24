namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a 8-bits unsigned integer in the RtSystem. </summary>

internal static class RtUInt8
{
// Write byte

private static void Write(byte v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.UINT8);
pos++;

buffer.SetUInt8(pos, v);
pos++;
}

// Pack Int8

internal static void Pack(byte v, NativeBuffer buffer, ref ulong pos)
{

if(v > 127)
Write(v, buffer, ref pos);

else
RtVarInt.WriteVarInt(v, buffer, ref pos);

}

}

}