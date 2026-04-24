namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents single precision float-point in the RtSystem. </summary>

internal static class RtFloat32
{
// Write float

internal static void Write(float v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.FLOAT32);
pos++;

buffer.SetFloat(pos, v);
pos += 4;
}

}

}