using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Records Item States for Zen Garden </summary>

public class ZenItemsRecordSerializer : IBinarySerializer<ZenItemsRecord>
{
// Read data from BinaryStream

public ZenItemsRecord ReadBin(Stream reader)
{

return new()
{
MarigoldLastPurchased_L = TimeOffset32.Read(reader),
MarigoldLastPurchased = TimeOffset32.Read(reader),
MarigoldLastPurchased_R = TimeOffset32.Read(reader),
GoldCan = reader.ReadBool32(),
Fertilizer = Offset32.Read(reader),
BugSpray = Offset32.Read(reader),
Phonograph = reader.ReadBool32(),
GardenGlove = reader.ReadBool32(),
MushroomGarden = reader.ReadBool32(),
WheelBarrow = reader.ReadBool32(),
SnailLastAwakeTime = reader.ReadUnixTime32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, ZenItemsRecord info)
{
TimeOffset32.Write(writer, info.MarigoldLastPurchased_L);
TimeOffset32.Write(writer, info.MarigoldLastPurchased);
TimeOffset32.Write(writer, info.MarigoldLastPurchased_R);

writer.WriteBool32(info.GoldCan);

Offset32.Write(writer, info.Fertilizer);
Offset32.Write(writer, info.BugSpray);

writer.WriteBool32(info.Phonograph);
writer.WriteBool32(info.GardenGlove);
writer.WriteBool32(info.MushroomGarden);
writer.WriteBool32(info.WheelBarrow);

writer.WriteUnixTime32(info.SnailLastAwakeTime);
}

}

}