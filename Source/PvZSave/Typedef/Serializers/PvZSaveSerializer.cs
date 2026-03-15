using System;
using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for PvZUserdata </summary>

public class PvZSaveSerializer : IBinarySerializer<PvZUserdata>
{
// SurvivalData serializer

private static readonly SurvivalInfoSerializer survivalSerializer = new();

// MinigamesInfo serializer

private static readonly MinigamesInfoSerializer minigamesSerializer = new();

// LimboPage serializer

private static readonly LimboPageInfoSerializer limboSerializer = new();

// PuzzleData serializer

private static readonly PuzzleInfoSerializer puzzleSerializer = new();

// ShopData serializer

private static readonly DaveShopInfoSerializer shopSerializer = new();

// AnimInfo serializer

private static readonly AnimPlayInfoSerializer animSerializer = new();

// ZenGardenInfo serializer

private static readonly ZenGardenInfoSerializer zenSerializer = new();

// AchievementsInfo serializer

private static readonly AchievementsInfoSerializer achievementsSerializer = new();

// ZombatarData serializer

private static readonly ZombatarDataSerializer zombatarSerializer = new();

// Read Userdata

public PvZUserdata ReadBin(Stream reader)
{

PvZUserdata userdata = new()
{
Version = (PvZVersion)reader.ReadUInt32(),
CurrentLevel = (CurrentLevelID)reader.ReadUInt32(),
Coins = reader.ReadInt32() * 10,
AdventuresCompleted = reader.ReadUInt32(),
SurvivalData = survivalSerializer.ReadBin(reader),
MinigamesCompleted = minigamesSerializer.ReadBin(reader),
MinigamesCompleted_Limbo = limboSerializer.ReadBin(reader),
TreeWeight = reader.ReadUInt32(),
PuzzleData = puzzleSerializer.ReadBin(reader),
Limbo_Upsell = reader.ReadBool32(),
Limbo_Intro = reader.ReadBool32()
};

using var unknown = reader.ReadPtr(112); // Unknown section: 0x130 - 0x19F

userdata.ItemsBought = shopSerializer.ReadBin(reader);

using var unknown2 = reader.ReadPtr(216); // Unknown section: 0x218 - 0x2EF

userdata.AlmanacUnlockedAnim = reader.ReadBool32();
userdata.SnailLastChocolateTime = reader.ReadUnixTime32();
userdata.SnailPos = SexyPoint.Read(reader);
userdata.MinigamesUnlocked = reader.ReadBool32();
userdata.PuzzlesUnlocked = reader.ReadBool32();
userdata.AnimsPlayed = animSerializer.ReadBin(reader);
userdata.HasTaco = reader.ReadBool32();
userdata.ZenGardenData = zenSerializer.ReadBin(reader);

// NOTE: some Fields might not be Present in save

if(reader.Position < reader.Length)
{
userdata.Achievements = achievementsSerializer.ReadBin(reader);
userdata.ZombatarData = zombatarSerializer.ReadBin(reader);
}

return userdata;
}

// Write Userdata to BinaryStream

public void WriteBin(Stream writer, PvZUserdata data)
{
writer.WriteUInt32( (uint)data.Version);
writer.WriteUInt32( (uint)data.CurrentLevel);

writer.WriteInt32(data.Coins / 10);
writer.WriteUInt32(data.AdventuresCompleted);

survivalSerializer.WriteBin(writer, data.SurvivalData);
minigamesSerializer.WriteBin(writer, data.MinigamesCompleted);
limboSerializer.WriteBin(writer, data.MinigamesCompleted_Limbo);

writer.WriteUInt32(data.TreeWeight);

puzzleSerializer.WriteBin(writer, data.PuzzleData);

writer.WriteBool32(data.Limbo_Upsell);
writer.WriteBool32(data.Limbo_Intro);

writer.Fill(112);

shopSerializer.WriteBin(writer, data.ItemsBought);

writer.Fill(216);

writer.WriteBool32(data.AlmanacUnlockedAnim);
writer.WriteUnixTime32(data.SnailLastChocolateTime);

data.SnailPos.Write(writer);

writer.WriteBool32(data.MinigamesUnlocked);
writer.WriteBool32(data.PuzzlesUnlocked);

animSerializer.WriteBin(writer, data.AnimsPlayed);

writer.WriteBool32(data.HasTaco);

zenSerializer.WriteBin(writer, data.ZenGardenData);
achievementsSerializer.WriteBin(writer, data.Achievements);
zombatarSerializer.WriteBin(writer, data.ZombatarData);
}

}

}