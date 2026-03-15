using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for ZenGardenPlantInfo </summary>

public class ZenGardenPlantSerializer : IBinarySerializer<ZenGardenPlantInfo>
{
// Read data from BinaryStream

public ZenGardenPlantInfo ReadBin(Stream reader)
{

ZenGardenPlantInfo info = new()
{
PlantType = (PlantTypeID)reader.ReadUInt32(),
GardenLocation = (GardenLocationID)reader.ReadUInt32(),
PlantPos = SexyPoint.Read(reader),
FaceDirection = (PlantFaceDirectionID)reader.ReadUInt32()
};

uint reserved = reader.ReadUInt32();

info.LastTimeWatered = reader.ReadUnixTime64();
info.PlantColor = (PlantColorID)reader.ReadUInt32();
info.TimesFertilized = reader.ReadUInt32();
info.TimesWatered = reader.ReadUInt32();
info.WateringNeedTimes = reader.ReadUInt32();
info.PlantNeeds = (ZenGardenPlantNeeds)reader.ReadUInt32();

uint reserved2 = reader.ReadUInt32();

info.LastHappyTime = reader.ReadUnixTime64();
info.LastTimeFertilized = reader.ReadUnixTime64();
info.LastChocolateTime = reader.ReadUnixTime64();

uint reserved3 = reader.ReadUInt32();
uint reserved4 = reader.ReadUInt32();

return info;
}

// Write data to BinaryStream

public void WriteBin(Stream writer, ZenGardenPlantInfo info)
{
writer.WriteUInt32( (uint)info.PlantType);
writer.WriteUInt32(( uint)info.GardenLocation);

info.PlantPos.Write(writer);

writer.WriteUInt32( (uint)info.FaceDirection);
writer.WriteUInt32(0); // Reserved1

writer.WriteUnixTime64(info.LastTimeWatered);
writer.WriteUInt32( (uint)info.PlantColor);
writer.WriteUInt32(info.TimesFertilized);
writer.WriteUInt32(info.TimesWatered);
writer.WriteUInt32(info.WateringNeedTimes);
writer.WriteUInt32( (uint)info.PlantNeeds);

writer.WriteUInt32(0); // Reserved2

writer.WriteUnixTime64(info.LastHappyTime);
writer.WriteUnixTime64(info.LastTimeFertilized);
writer.WriteUnixTime64(info.LastChocolateTime);

writer.WriteUInt32(0); // Reserved3
writer.WriteUInt32(0); // Reserved4
}

}

}