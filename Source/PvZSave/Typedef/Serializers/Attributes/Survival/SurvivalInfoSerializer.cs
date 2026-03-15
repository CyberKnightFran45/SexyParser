using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for SurvivalModeInfo </summary>

public class SurvivalInfoSerializer : IBinarySerializer<SurvivalModeInfo>
{
// Record serializer

private static readonly SurvivalRecordSerializer recordSerializer = new();

// Read data from BinaryStream

public SurvivalModeInfo ReadBin(Stream reader)
{

return new()
{
Progress = recordSerializer.ReadBin(reader),
Progress_Hard = recordSerializer.ReadBin(reader),
Progress_Endless = recordSerializer.ReadBin(reader)
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, SurvivalModeInfo info)
{
recordSerializer.WriteBin(writer, info.Progress);
recordSerializer.WriteBin(writer, info.Progress_Hard);
recordSerializer.WriteBin(writer, info.Progress_Endless);
}

}

}