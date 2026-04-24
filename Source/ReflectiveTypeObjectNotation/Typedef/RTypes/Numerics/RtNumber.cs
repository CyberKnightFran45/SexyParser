namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a Number in the RtSystem. </summary>

internal static class RtNumber
{
// Write flags

private static void WriteFlags(byte flags, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, flags);

pos++;
}

// Write zero

private static void WriteZero(NativeBuffer buffer, ref ulong pos)
{
WriteFlags(RTypeId.INT32_ZERO, buffer, ref pos);
}

#region ==============  SIGNED INT ENCODER ==============

// Pack signed integer

private static void PackSigned(long v, NativeBuffer buffer, ref ulong pos)
{

if(v >= sbyte.MinValue && v <= sbyte.MaxValue)
RtInt8.Pack( (sbyte)v, buffer, ref pos);
    
else if(v >= short.MinValue && v <= short.MaxValue)
RtInt16.Pack( (short)v, buffer, ref pos);

else if(v >= int.MinValue && v <= int.MaxValue)
RtInt32.Pack( (int)v, buffer, ref pos);

else
RtInt64.Pack(v, buffer, ref pos);

}

/** <summary> Checks the Type of a signed Integer, then write its RTON representation </summary>

<param name = "buffer"> RTON buffer. </param>
<param name = "v"> The Value. </param> */

private static void WriteSignedInt(long v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0)
{
WriteZero(buffer, ref pos);

return;
}

PackSigned(v, buffer, ref pos);
}

#endregion


#region ==============  UNSIGNED INT ENCODER ==============

// Pack unsigned integer

private static void PackUnsigned(ulong v, NativeBuffer buffer, ref ulong pos)
{

if(v <= byte.MaxValue)
RtUInt8.Pack( (byte)v, buffer, ref pos);
    
else if(v <= ushort.MaxValue)
RtUInt16.Pack( (ushort)v, buffer, ref pos);
    
else if(v <= uint.MaxValue)
RtUInt32.Pack( (uint)v, buffer, ref pos);
   
else if(v <= long.MaxValue)
RtUInt64.Pack(v, buffer, ref pos);

else
RtUInt64.Write(v, buffer, ref pos);

}

/** <summary> Checks the Type of an Unsigned Integer, then Write it to RTON </summary>

<param name = "writer"> The Stream where the RTON Data will be Written. </param>
<param name = "v"> The Value to be Written. </param> */

private static void WriteUnsignedInt(ulong v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0)
{
WriteZero(buffer, ref pos);

return;
}

PackUnsigned(v, buffer, ref pos);
}

#endregion


#region ==============  FLOAT-POINT  ==============

/** <summary> Checks if Value is a Single or Double, then Write it to RTON </summary>

<param name = "writer"> The RTON Writer. </param>
<param name = "v"> The Value to Write. </param> */

private static void WriteDecimal(double v, NativeBuffer buffer, ref ulong pos)
{

if(v == 0.0d)
WriteFlags(RTypeId.FLOAT32_ZERO, buffer, ref pos);

else if( (double)(float)v == v)
RtFloat32.Write( (float)v, buffer, ref pos);

else
RtFloat64.Write(v, buffer, ref pos);

}

#endregion

/** <summary> Evaluates a JSON Number and writes it to RTON. </summary>

<param name = "reader"> The JSON Reader. </param>
<param name = "writer"> The RTON Writer. </param> */

internal static void Write(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{

if(reader.IsDecimal() )
WriteDecimal(reader.GetDouble(), buffer, ref pos);

else if(reader.IsNumberNegative() )
WriteSignedInt(reader.GetInt64(), buffer, ref pos);

else
WriteUnsignedInt(reader.GetUInt64(), buffer, ref pos);

}

}

}