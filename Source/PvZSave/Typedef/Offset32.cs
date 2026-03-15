using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Helper for handling Offsets as Int32 </summary>

public static class Offset32
{
// Base offset

private const int OFFSET = 1000;

// Read

public static int? Read(Stream reader)
{
int v = reader.ReadInt32();

return v == 0 ? null : v - OFFSET;
}

// Write

public static void Write(Stream writer, int? v) => writer.WriteInt32(v is null ? 0 : (int)v + OFFSET);
}

}