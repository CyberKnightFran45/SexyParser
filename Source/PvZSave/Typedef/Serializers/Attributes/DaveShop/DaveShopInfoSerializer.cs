using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for DaveShopInfo </summary>

public class DaveShopInfoSerializer : IBinarySerializer<DaveShopInfo>
{
// PlantStore serializer

private static readonly PlantStoreRecordSerializer plantStoreSerializer = new();

// PlantStore serializer

private static readonly ZenItemsRecordSerializer zenItemsSerializer = new();

// Read data from BinaryStream

public DaveShopInfo ReadBin(Stream reader)
{

DaveShopInfo info = new()
{
PlantRecord = plantStoreSerializer.ReadBin(reader)
};

uint reserved = reader.ReadUInt32();
 
info.ZenItems = zenItemsSerializer.ReadBin(reader);
info.ExtraSeedSlots = reader.ReadUInt32();
info.PoolCleaner = reader.ReadBool32();
info.RoofCleaner = reader.ReadBool32();
info.RakeUses = reader.ReadUInt32();
info.AquariumGarden = reader.ReadBool32();
info.Chocolate = Offset32.Read(reader);
info.TreeOfWisdom = reader.ReadBool32();
info.TreeFood = Offset32.Read(reader);
info.WallNutAid = reader.ReadBool32();

return info;
}

// Write data to BinaryStream

public void WriteBin(Stream writer, DaveShopInfo info)
{
plantStoreSerializer.WriteBin(writer, info.PlantRecord);
writer.WriteUInt32(0); // Reserved

zenItemsSerializer.WriteBin(writer, info.ZenItems);
writer.WriteUInt32(info.ExtraSeedSlots);

writer.WriteBool32(info.PoolCleaner);
writer.WriteBool32(info.RoofCleaner);

writer.WriteUInt32(info.RakeUses);
writer.WriteBool32(info.AquariumGarden);

Offset32.Write(writer, info.Chocolate);
writer.WriteBool32(info.TreeOfWisdom);

Offset32.Write(writer, info.TreeFood);
writer.WriteBool32(info.WallNutAid);
}

}

}