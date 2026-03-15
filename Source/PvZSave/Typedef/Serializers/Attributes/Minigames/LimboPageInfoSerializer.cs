using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for LimboPageInfo </summary>

public class LimboPageInfoSerializer : IBinarySerializer<LimboPageInfo>
{
// Read data from BinaryStream

public LimboPageInfo ReadBin(Stream reader)
{

return new()
{
Art_WallNut = reader.ReadBool32(),
SunnyDay = reader.ReadBool32(),
Unsodded = reader.ReadBool32(),
BigTime = reader.ReadBool32(),
Art_Sunflower = reader.ReadBool32(),
Air_Raid = reader.ReadBool32(),
IceLevel = reader.ReadBool32(),
Limbo_ZenGarden = reader.ReadBool32(),
HighGravity = reader.ReadBool32(),
GraveDanger = reader.ReadBool32(),
CanUDigIt = reader.ReadBool32(),
Dark_Stormy_Night = reader.ReadBool32(),
BungeeBlitz = reader.ReadBool32(),
Squirrel = reader.ReadBool32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, LimboPageInfo info)
{
writer.WriteBool32(info.Art_WallNut);
writer.WriteBool32(info.SunnyDay);
writer.WriteBool32(info.Unsodded);
writer.WriteBool32(info.BigTime);
writer.WriteBool32(info.Art_Sunflower);
writer.WriteBool32(info.Air_Raid);
writer.WriteBool32(info.IceLevel);
writer.WriteBool32(info.Limbo_ZenGarden);
writer.WriteBool32(info.HighGravity);
writer.WriteBool32(info.GraveDanger);
writer.WriteBool32(info.CanUDigIt);
writer.WriteBool32(info.Dark_Stormy_Night);
writer.WriteBool32(info.BungeeBlitz);
writer.WriteBool32(info.Squirrel);
}

}

}