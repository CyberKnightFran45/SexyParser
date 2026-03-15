using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for PlantStoreRecord </summary>

public class PlantStoreRecordSerializer : IBinarySerializer<PlantStoreRecord>
{
// Read data from BinaryStream

public PlantStoreRecord ReadBin(Stream reader)
{

return new()
{
GatlingPea = reader.ReadBool32(),
TwinSunflower = reader.ReadBool32(),
GloomShroom = reader.ReadBool32(),
Cattail = reader.ReadBool32(),
WinterMelon = reader.ReadBool32(),
GoldMagnet = reader.ReadBool32(),
Spikerock = reader.ReadBool32(),
CobCannon = reader.ReadBool32(),
Imitater = reader.ReadBool32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, PlantStoreRecord info)
{
writer.WriteBool32(info.GatlingPea);
writer.WriteBool32(info.TwinSunflower);
writer.WriteBool32(info.GloomShroom);
writer.WriteBool32(info.Cattail);
writer.WriteBool32(info.WinterMelon);
writer.WriteBool32(info.GoldMagnet);
writer.WriteBool32(info.Spikerock);
writer.WriteBool32(info.CobCannon);
writer.WriteBool32(info.Imitater);
}

}

}