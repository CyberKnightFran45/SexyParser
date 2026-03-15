using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for AnimPlayInfo </summary>

public class AnimPlayInfoSerializer : IBinarySerializer<AnimPlayInfo>
{
// Read data from BinaryStream

public AnimPlayInfo ReadBin(Stream reader)
{

return new()
{
LastLevelUnlocked_Minigames = reader.ReadBool32(),
LastLevelUnlocked_Vasebreaker = reader.ReadBool32(),
LastLevelUnlocked_IZombie = reader.ReadBool32(),
LastLevelUnlocked_Survival = reader.ReadBool32(),
LastLevelUnlocked_Limbo = reader.ReadBool32(),
AdventureComplete = reader.ReadBool32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, AnimPlayInfo info)
{
writer.WriteBool32(info.LastLevelUnlocked_Minigames);
writer.WriteBool32(info.LastLevelUnlocked_Vasebreaker);
writer.WriteBool32(info.LastLevelUnlocked_IZombie);
writer.WriteBool32(info.LastLevelUnlocked_Survival);
writer.WriteBool32(info.LastLevelUnlocked_Limbo);
writer.WriteBool32(info.AdventureComplete);
}

}

}