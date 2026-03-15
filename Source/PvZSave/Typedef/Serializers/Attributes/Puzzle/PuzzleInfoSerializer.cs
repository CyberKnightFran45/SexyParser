using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Records for Puzzle Mode </summary>

public class PuzzleInfoSerializer : IBinarySerializer<PuzzleModeInfo>
{
// Record serializer

private static readonly PuzzleRecordSerializer recordSerializer = new();

// Read data from BinaryStream

public PuzzleModeInfo ReadBin(Stream reader)
{

return new()
{
Vasebreaker = recordSerializer.ReadBin(reader),
IZombie = recordSerializer.ReadBin(reader)
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, PuzzleModeInfo info)
{
recordSerializer.WriteBin(writer, info.Vasebreaker);
recordSerializer.WriteBin(writer, info.IZombie);
}

}

}