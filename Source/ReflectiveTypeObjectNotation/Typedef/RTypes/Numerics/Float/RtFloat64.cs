namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents double precision float-point in the RtSystem. </summary>

internal static class RtFloat64
{
// Write float

internal static void Write(double v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.FLOAT64);
pos++;

buffer.SetDouble(pos, v);
pos += 8;
}

}

}