using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for ZenGardenInfo </summary>

public class ZenGardenInfoSerializer : IBinarySerializer<ZenGardenInfo>
{
// ZombatarData serializer

private static readonly ZenGardenPlantSerializer zenPlantSerializer = new();

// Read data from BinaryStream

public ZenGardenInfo ReadBin(Stream reader)
{
var snailState = (SnailConsciousState)reader.ReadUInt32();
uint reserved  = reader.ReadUInt32();
uint reserved2 = reader.ReadUInt32();
var zenPlants = BinaryList.ReadObjects(reader, zenPlantSerializer);

return new()
{
SnailState = snailState,
ZenPlants = zenPlants
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, ZenGardenInfo info)
{
writer.WriteUInt32( (uint)info.SnailState);
writer.WriteUInt32(0); // Reserved
writer.WriteUInt32(0); // Reserved2

BinaryList.WriteObjects(writer, info.ZenPlants, zenPlantSerializer);
}

}

}