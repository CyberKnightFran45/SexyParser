using System.Numerics;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents a variant-length integer in the RtSystem. </summary>

internal static class RtVarInt
{
// Compute size

internal static int ComputeSize(ulong u)
{

if(u == 0)
return 1;

int bits = 64 - BitOperations.LeadingZeroCount(u);

return (bits + 6) / 7;
}

// Encode ZigZag (32-bits)

internal static uint EncodeZigZag32(int v) => (uint)( (v << 1) ^ (v >> 31) );

// Encode ZigZag (64-bits)

internal static ulong EncodeZigZag64(long v) => (ulong)( (v << 1) ^ (v >> 63) );

// Encode raw var int

private static int EncodeRaw32(uint val, NativeBuffer buffer, ulong pos) 
{
int bytesWritten = 0;

while(val >= 0x80) 
{
buffer.SetUInt8(pos + (ulong)bytesWritten, (byte)(val | 0x80) );
val >>= 7;

bytesWritten++;
}

buffer.SetUInt8(pos + (ulong)bytesWritten, (byte)val);
	
return bytesWritten + 1;
}

// Write VarInt or ZigZag (Core)

private static void WriteCore32(uint val, byte flags, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, flags);
pos++;

pos += (ulong)EncodeRaw32(val, buffer, pos);
}

// Write raw VarInt

internal static void WriteVarInt(uint val, NativeBuffer buffer, ref ulong pos)
{
WriteCore32(val, RTypeId.VARINT, buffer, ref pos);
}

// Write raw ZigZag

internal static void WriteZigZag32(uint val, NativeBuffer buffer, ref ulong pos)
{
WriteCore32(val, RTypeId.ZIGZAG32, buffer, ref pos);
}

// Encode raw VarLong

private static int EncodeRaw64(ulong val, NativeBuffer buffer, ulong pos) 
{
int bytesWritten = 0;

while(val >= 0x80) 
{
buffer.SetUInt8(pos + (ulong)bytesWritten, (byte)(val | 0x80) );
val >>= 7;

bytesWritten++;
}

buffer.SetUInt8(pos + (ulong)bytesWritten, (byte)val);
    
return bytesWritten + 1;
}

// Write VarLong or ZigZag64 (Core)

private static void WriteCore64(ulong val, byte flags, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, flags);
pos++;

pos += (ulong)EncodeRaw64(val, buffer, pos);
}

// Write raw varlong

internal static void WriteVarLong(ulong val, NativeBuffer buffer, ref ulong pos)
{
WriteCore64(val, RTypeId.VARLONG, buffer, ref pos);
}

// Write ZigZag64

internal static void WriteZigZag64(ulong val, NativeBuffer buffer, ref ulong pos)
{
WriteCore64(val, RTypeId.ZIGZAG64, buffer, ref pos);
}

}

}