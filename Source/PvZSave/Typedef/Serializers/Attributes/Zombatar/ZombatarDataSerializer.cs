using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Serializer for ZombatarInfo </summary>

public class ZombatarDataSerializer : IBinarySerializer<ZombatarInfo>
{
// Entry serializer

private static readonly ZombatarEntrySerializer entrySerializer = new();

// Read data from BinaryStream

public ZombatarInfo ReadBin(Stream reader)
{
bool agreement = reader.ReadBool();
var entries = BinaryList.ReadObjects(reader, entrySerializer);

using var unknown = reader.ReadPtr(20); // Unknown section: 0x00 - 0x13

bool ignoreDialog = reader.ReadBool();

return new(agreement, entries, ignoreDialog);
}

// Write data to BinaryStream

public void WriteBin(Stream writer, ZombatarInfo info)
{
writer.WriteBool(info.AgreementAccepted);
BinaryList.WriteObjects(writer, info.Zombatars, entrySerializer);

writer.Fill(20);

writer.WriteBool(info.HasCreatedZombatar);
}

}

}