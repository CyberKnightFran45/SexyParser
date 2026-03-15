using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for MinigamesInfo </summary>

public class MinigamesInfoSerializer : IBinarySerializer<MinigamesInfo>
{
// Read data from BinaryStream

public MinigamesInfo ReadBin(Stream reader)
{

return new()
{
ZomBotany = reader.ReadBool32(),
WallNut_Bowling = reader.ReadBool32(),
SlotMachine = reader.ReadBool32(),
Its_Raining_Seeds = reader.ReadBool32(),
Beghouled = reader.ReadBool32(),
Invisighoul = reader.ReadBool32(),
Seeing_Stars = reader.ReadBool32(),
Zombiquarium = reader.ReadBool32(),
BeghouledTwist = reader.ReadBool32(),
LittleTrouble = reader.ReadBool32(),
PortalCombat = reader.ReadBool32(),
ColumnAsUCM = reader.ReadBool32(),
Bobsled_Bonanza = reader.ReadBool32(),
ZombiesOnSpeed = reader.ReadBool32(),
WhackAZombie = reader.ReadBool32(),
LastStand = reader.ReadBool32(),
ZomBotany_2 = reader.ReadBool32(),
WallNut_Bowling_2 = reader.ReadBool32(),
PogoParty = reader.ReadBool32(),
FinalBoss = reader.ReadBool32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, MinigamesInfo info)
{
writer.WriteBool32(info.ZomBotany);
writer.WriteBool32(info.WallNut_Bowling);
writer.WriteBool32(info.SlotMachine);
writer.WriteBool32(info.Its_Raining_Seeds);
writer.WriteBool32(info.Beghouled);
writer.WriteBool32(info.Invisighoul);
writer.WriteBool32(info.Seeing_Stars);
writer.WriteBool32(info.Zombiquarium);
writer.WriteBool32(info.BeghouledTwist);
writer.WriteBool32(info.LittleTrouble);
writer.WriteBool32(info.PortalCombat);
writer.WriteBool32(info.ColumnAsUCM);
writer.WriteBool32(info.Bobsled_Bonanza);
writer.WriteBool32(info.ZombiesOnSpeed);
writer.WriteBool32(info.WhackAZombie);
writer.WriteBool32(info.LastStand);
writer.WriteBool32(info.ZomBotany_2);
writer.WriteBool32(info.WallNut_Bowling_2);
writer.WriteBool32(info.PogoParty);
writer.WriteBool32(info.FinalBoss);
}

}

}