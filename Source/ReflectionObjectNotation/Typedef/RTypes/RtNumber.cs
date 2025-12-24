using System.IO;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Represents a Number in the RtSystem. </summary>

public static class RtNumber
{
// Write Float 32

private static void WriteFloat32(Stream writer, float v)
{

if(v == 0.0f)
writer.WriteByte(RTypeId.FLOAT32_ZERO);

else
{
writer.WriteByte(RTypeId.FLOAT32);

writer.WriteFloat(v);
}

}

// Write Float 64

private static void WriteFloat64(Stream writer, double v)
{

if(v == 0.0d)
writer.WriteByte(RTypeId.FLOAT64_ZERO);

else
{
writer.WriteByte(RTypeId.FLOAT64);

writer.WriteDouble(v);
}

}

/** <summary> Checks if Value is a Single or Double, then Write it to RTON </summary>

<param name = "writer"> The RTON Writer. </param>
<param name = "v"> The Value to Write. </param> */

private static void WriteDecimal(Stream writer, double v)
{

if(v <= float.MaxValue)
WriteFloat32(writer, (float)v);

else
WriteFloat64(writer, v);

}

// Write Int16

private static void WriteInt16(Stream writer, short v)
{

if(v == 0)
writer.WriteByte(RTypeId.INT16_ZERO);

else
{
writer.WriteByte(RTypeId.INT16);

writer.WriteInt16(v);
}

}

// Write UInt16

private static void WriteUInt16(Stream writer, ushort v)
{

if(v == 0)
writer.WriteByte(RTypeId.UINT16_ZERO);

else
{
writer.WriteByte(RTypeId.UINT16);

writer.WriteUInt16(v);
}

}

// Write Int32

private static void WriteInt32(Stream writer, int v)
{

if(v == 0)
writer.WriteByte(RTypeId.INT32_ZERO);

else
{
writer.WriteByte(RTypeId.INT32);

writer.WriteInt32(v);
}

}

// Write UInt32

private static void WriteUInt32(Stream writer, uint v)
{

if(v == 0)
writer.WriteByte(RTypeId.UINT32_ZERO);

else
{
writer.WriteByte(RTypeId.UINT32);

writer.WriteUInt32(v);
}

}

// Write Int64

private static void WriteInt64(Stream writer, long v)
{

if(v == 0L)
writer.WriteByte(RTypeId.INT64_ZERO);

else
{
writer.WriteByte(RTypeId.INT64);

writer.WriteInt64(v);
}

}

// Write UInt64

private static void WriteUInt64(Stream writer, ulong v)
{

if(v == 0uL)
writer.WriteByte(RTypeId.UINT64_ZERO);

else
{
writer.WriteByte(RTypeId.UINT64);

writer.WriteUInt64(v);
}

}

// Write Var Int

private static void WriteVarInt(Stream writer, int v)
{
writer.WriteByte(RTypeId.VAR_INT32);

writer.WriteVarInt(v);
}

/** <summary> Checks the Type of an Unsigned Integer, then Write it to RTON </summary>

<param name = "writer"> The Stream where the RTON Data will be Written. </param>
<param name = "v"> The Value to be Written. </param>
<param name = "endian"> The endian Order of the RTON Data. </param> */

private static void WriteUnsigned(Stream writer, double v)
{

if(v >= 16384 && v <= short.MaxValue)
WriteInt16(writer, (short)v);

else if(v > short.MaxValue && v <= ushort.MaxValue)
WriteUInt16(writer, (ushort)v);

else if(v > ushort.MaxValue && v <= int.MaxValue)
WriteInt32(writer, (int)v);

else if(v > int.MaxValue && v <= uint.MaxValue)
WriteUInt32(writer, (uint)v);

else if(v > uint.MaxValue && v <= long.MaxValue)
WriteInt64(writer, (long)v);

else if(v > long.MaxValue && v <= ulong.MaxValue)
WriteUInt64(writer, (ulong)v);

else
WriteVarInt(writer, (int)v);

}

// Write ZigZag

private static void WriteZigZag(Stream writer, int v)
{
writer.WriteByte(RTypeId.ZIGZAG32);

writer.WriteZigZag32(v);
}

/** <summary> Checks the Type of a signed Integer, then write it to RTON. </summary>

<param name = "writer"> The RTON writer. </param>
<param name = "v"> The Value to be Written. </param> */

private static void WriteSigned(Stream writer, double v)
{

if(v < short.MinValue && v >= short.MinValue)
WriteInt16(writer, (short)v);

else if(v < short.MinValue && v >= int.MinValue)
WriteInt32(writer, (int)v);

else if(v < int.MinValue && v >= long.MinValue)
WriteInt64(writer, (long)v);

else
WriteZigZag(writer, (int)v);

}

/** <summary> Evaluates a JSON Number and writes it to RTON. </summary>

<param name = "reader"> The JSON Reader. </param>
<param name = "writer"> The RTON Writer. </param> */

public static void Write(NativeJsonReader reader, Stream writer)
{
double v = reader.GetDouble();

if(v % 1 == 0)
{

if(v >= 0)
WriteUnsigned(writer, v);

else
WriteSigned(writer, v);

}

else
WriteDecimal(writer, v);

}

}

}