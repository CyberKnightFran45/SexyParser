namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a Number in the RtSystem. </summary>

public static class RtNumber
{
// Write Float 32

private static void WriteFloat32(float v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0.0f)
{
buffer.SetUInt8(pos, RTypeId.FLOAT32_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.FLOAT32);
pos++;

buffer.SetFloat(pos, v);
pos += 4;
}

}

// Write Float 64

private static void WriteFloat64(double v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0.0d)
{
buffer.SetUInt8(pos, RTypeId.FLOAT64_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.FLOAT64);
pos++;

buffer.SetDouble(pos, v);
pos += 8;
}

}

/** <summary> Checks if Value is a Single or Double, then Write it to RTON </summary>

<param name = "writer"> The RTON Writer. </param>
<param name = "v"> The Value to Write. </param> */

private static void WriteDecimal(double v, NativeBuffer buffer, ref ulong pos)
{

if(v <= float.MaxValue)
WriteFloat32( (float)v, buffer, ref pos);

else
WriteFloat64(v, buffer, ref pos);

}

// Write Int16

private static void WriteInt16(short v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0)
{
buffer.SetUInt8(pos, RTypeId.INT16_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.INT16);
pos++;

buffer.SetInt16(pos, v);
pos += 2;
}

}

// Write UInt16

private static void WriteUInt16(ushort v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0)
{
buffer.SetUInt8(pos, RTypeId.UINT16_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.UINT16);
pos++;

buffer.SetUInt16(pos, v);
pos += 2;
}

}

// Write Int32

private static void WriteInt32(int v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0)
{
buffer.SetUInt8(pos, RTypeId.INT32_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.INT32);
pos++;

buffer.SetInt32(pos, v);
pos += 4;
}

}

// Write UInt32

private static void WriteUInt32(uint v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0)
{
buffer.SetUInt8(pos, RTypeId.UINT32_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.UINT32);
pos++;

buffer.SetUInt32(pos, v);
pos += 4;
}

}

// Write Int64

private static void WriteInt64(long v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0L)
{
buffer.SetUInt8(pos, RTypeId.INT64_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.INT64);
pos++;

buffer.SetInt64(pos, v);
pos += 8;
}

}

// Write UInt64

private static void WriteUInt64(ulong v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0uL)
{
buffer.SetUInt8(pos, RTypeId.UINT64_ZERO);
pos++;
}

else
{
buffer.SetUInt8(pos, RTypeId.UINT64);
pos++;

buffer.SetUInt64(pos, v);
pos += 8;
}

}

// Write Var Int

private static void WriteVarInt(int v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.VAR_INT32);
pos++;

pos += (ulong)buffer.SetVarInt(pos, v);
}

/** <summary> Checks the Type of an Unsigned Integer, then Write it to RTON </summary>

<param name = "writer"> The Stream where the RTON Data will be Written. </param>
<param name = "v"> The Value to be Written. </param> */

private static void WriteUnsigned(double v, NativeBuffer buffer, ref ulong pos)
{

if(v >= 16384 && v <= short.MaxValue)
WriteInt16( (short)v, buffer, ref pos);

else if(v > short.MaxValue && v <= ushort.MaxValue)
WriteUInt16( (ushort)v, buffer, ref pos);

else if(v > ushort.MaxValue && v <= int.MaxValue)
WriteInt32( (int)v, buffer, ref pos);

else if(v > int.MaxValue && v <= uint.MaxValue)
WriteUInt32( (uint)v, buffer, ref pos);

else if(v > uint.MaxValue && v <= long.MaxValue)
WriteInt64( (long)v, buffer, ref pos);

else if(v > long.MaxValue && v <= ulong.MaxValue)
WriteUInt64( (ulong)v, buffer, ref pos);

else
WriteVarInt( (int)v, buffer, ref pos);

}

// Write ZigZag

private static void WriteZigZag(int v, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.ZIGZAG32);
pos++;

pos += (ulong)buffer.SetZigZag(pos, v);
}

/** <summary> Checks the Type of a signed Integer, then write it to RTON. </summary>

<param name = "writer"> The RTON writer. </param>
<param name = "v"> The Value to be Written. </param> */

private static void WriteSigned(double v, NativeBuffer buffer, ref ulong pos)
{

if(v < short.MinValue && v >= short.MinValue)
WriteInt16( (short)v, buffer, ref pos);

else if(v < short.MinValue && v >= int.MinValue)
WriteInt32( (int)v, buffer, ref pos);

else if(v < int.MinValue && v >= long.MinValue)
WriteInt64( (long)v, buffer, ref pos);

else
WriteZigZag( (int)v, buffer, ref pos);

}

/** <summary> Evaluates a JSON Number and writes it to RTON. </summary>

<param name = "reader"> The JSON Reader. </param>
<param name = "writer"> The RTON Writer. </param> */

public static void Write(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
double v = reader.GetDouble();

if(v % 1 == 0)
{

if(v >= 0)
WriteUnsigned(v, buffer, ref pos);

else
WriteSigned(v, buffer, ref pos);

}

else
WriteDecimal(v, buffer, ref pos);

}

}

}