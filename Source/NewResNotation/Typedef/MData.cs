using System.IO;
using System.Runtime.InteropServices;

namespace SexyParsers.NewResNotation
{
/// <summary> Defines the Data contained in a NEWTON File. </summary>

[StructLayout(LayoutKind.Explicit, Size = 45, Pack = 1)]

public readonly struct MData
{
[FieldOffset(0)]
public readonly uint Slot;

[FieldOffset(4)]
public readonly uint Width;

[FieldOffset(8)]
public readonly uint Height;

[FieldOffset(12)]
public readonly uint AtlasX;

[FieldOffset(16)]
public readonly uint AtlasY;

[FieldOffset(20)]
public readonly uint AtlasHeight;

[FieldOffset(24)]
public readonly uint AtlasWidth;

[FieldOffset(28)]
public readonly uint Columns;

[FieldOffset(32)]
public readonly uint Rows;

[FieldOffset(36)]
public readonly bool IsAtlas;

[FieldOffset(37)]
public readonly int X;

[FieldOffset(41)]
public readonly int Y;

// Read MData

public static MData Read(Stream reader)
{
using var rOwner = reader.ReadPtr(45);
var rawData = rOwner.AsSpan();

return MemoryMarshal.Read<MData>(rawData);
}

}

}