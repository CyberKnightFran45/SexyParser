using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for AchievementsInfo </summary>

public class AchievementsInfoSerializer : IBinarySerializer<AchievementsInfo>
{
// Read data from BinaryStream

public AchievementsInfo ReadBin(Stream reader)
{

return new()
{
HomeSecurity = reader.ReadBool16(),
NobelPeas = reader.ReadBool16(),
Better_Off_Dead = reader.ReadBool16(),
ChinaShop = reader.ReadBool16(),
Spudow = reader.ReadBool16(),
Explodonator = reader.ReadBool16(),
Morticulturalist = reader.ReadBool16(),
DontPInPool = reader.ReadBool16(),
Roll_Some_Heads = reader.ReadBool16(),
Grounded = reader.ReadBool16(),
Zombologist = reader.ReadBool16(),
PennyPincher = reader.ReadBool16(),
SunnyDays = reader.ReadBool16(),
PopcornParty = reader.ReadBool16(),
GoodMorning = reader.ReadBool16(),
No_Fungus_Among_Us = reader.ReadBool16(),
BeyondGrave = reader.ReadBool16(),
Inmortal = reader.ReadBool16(),
ToweringWisdom = reader.ReadBool16(),
MustacheMode = reader.ReadBool16()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, AchievementsInfo info)
{
writer.WriteBool16(info.HomeSecurity);
writer.WriteBool16(info.NobelPeas);
writer.WriteBool16(info.Better_Off_Dead);
writer.WriteBool16(info.ChinaShop);
writer.WriteBool16(info.Spudow);
writer.WriteBool16(info.Explodonator);
writer.WriteBool16(info.Morticulturalist);
writer.WriteBool16(info.DontPInPool);
writer.WriteBool16(info.Roll_Some_Heads);
writer.WriteBool16(info.Grounded);
writer.WriteBool16(info.Zombologist);
writer.WriteBool16(info.PennyPincher);
writer.WriteBool16(info.SunnyDays);
writer.WriteBool16(info.PopcornParty);
writer.WriteBool16(info.GoodMorning);
writer.WriteBool16(info.No_Fungus_Among_Us);
writer.WriteBool16(info.BeyondGrave);
writer.WriteBool16(info.Inmortal);
writer.WriteBool16(info.ToweringWisdom);
writer.WriteBool16(info.MustacheMode);
}

}

}