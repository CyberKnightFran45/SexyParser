using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for PuzzleRecord </summary>

public class PuzzleRecordSerializer : IBinarySerializer<PuzzleRecord>
{
// Read data from BinaryStream

public PuzzleRecord ReadBin(Stream reader)
{

return new()
{
Puzzle1 = reader.ReadBool32(),
Puzzle2 = reader.ReadBool32(),
Puzzle3 = reader.ReadBool32(),
Puzzle4 = reader.ReadBool32(),
Puzzle5 = reader.ReadBool32(),
Puzzle6 = reader.ReadBool32(),
Puzzle7 = reader.ReadBool32(),
Puzzle8 = reader.ReadBool32(),
Puzzle9 = reader.ReadBool32(),
EndlessStreak = reader.ReadUInt32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, PuzzleRecord info)
{
writer.WriteBool32(info.Puzzle1);
writer.WriteBool32(info.Puzzle2);
writer.WriteBool32(info.Puzzle3);
writer.WriteBool32(info.Puzzle4);
writer.WriteBool32(info.Puzzle5);
writer.WriteBool32(info.Puzzle6);
writer.WriteBool32(info.Puzzle7);
writer.WriteBool32(info.Puzzle8);
writer.WriteBool32(info.Puzzle9);

writer.WriteUInt32(info.EndlessStreak);
}

}

}