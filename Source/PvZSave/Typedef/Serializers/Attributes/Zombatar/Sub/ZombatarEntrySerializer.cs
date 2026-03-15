using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for ZombatarEntry </summary>

public class ZombatarEntrySerializer : IBinarySerializer<ZombatarEntry>
{
// Read data from BinaryStream

public ZombatarEntry ReadBin(Stream reader)
{
int reserved = reader.ReadInt32();

return new()
{
SkinColor = reader.ReadUInt32(),

ClothesType = reader.ReadInt32(),
ClothesColor = reader.ReadUInt32(),

TidbitsType = reader.ReadInt32(),
TidbitsColor = reader.ReadUInt32(),

AccessoriesType = reader.ReadInt32(),
AccessoriesColor = reader.ReadUInt32(),

FacialHairType = reader.ReadInt32(),
FacialHairColor = reader.ReadUInt32(),

HairType = reader.ReadInt32(),
HairColor = reader.ReadUInt32(),

EyewearType = reader.ReadInt32(),
EyewearColor = reader.ReadUInt32(),

HatType = reader.ReadInt32(),
HatColor = reader.ReadUInt32(),

BackdropType = reader.ReadUInt32(),
BackdropColor = reader.ReadUInt32()
};

}

// Write data to BinaryStream

public void WriteBin(Stream writer, ZombatarEntry info)
{
writer.WriteInt32(-1); // Reserved

writer.WriteUInt32(info.SkinColor);

writer.WriteInt32(info.ClothesType);
writer.WriteUInt32(info.ClothesColor);

writer.WriteInt32(info.TidbitsType);
writer.WriteUInt32(info.TidbitsColor);

writer.WriteInt32(info.AccessoriesType);
writer.WriteUInt32(info.AccessoriesColor);

writer.WriteInt32(info.FacialHairType);
writer.WriteUInt32(info.FacialHairColor);

writer.WriteInt32(info.HairType);
writer.WriteUInt32(info.HairColor);

writer.WriteInt32(info.EyewearType);
writer.WriteUInt32(info.EyewearColor);

writer.WriteInt32(info.HatType);
writer.WriteUInt32(info.HatColor);

writer.WriteUInt32(info.BackdropType);
writer.WriteUInt32(info.BackdropColor);
}

}

}