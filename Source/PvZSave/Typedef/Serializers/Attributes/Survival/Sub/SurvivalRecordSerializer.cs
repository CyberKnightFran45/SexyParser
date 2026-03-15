using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for SurvivalRecord </summary>

public class SurvivalRecordSerializer : IBinarySerializer<SurvivalRecord>
{
// Read record from BinaryStream

public SurvivalRecord ReadBin(Stream reader)
{

return new()
{
Day = reader.ReadUInt32(),
Night = reader.ReadUInt32(),
Pool = reader.ReadUInt32(),
Fog = reader.ReadUInt32(),
Roof = reader.ReadUInt32()
};

}

// Write record to BinaryStream

public void WriteBin(Stream writer, SurvivalRecord info)
{
writer.WriteUInt32(info.Day);
writer.WriteUInt32(info.Night);
writer.WriteUInt32(info.Pool);
writer.WriteUInt32(info.Fog);
writer.WriteUInt32(info.Roof);
}

}

}