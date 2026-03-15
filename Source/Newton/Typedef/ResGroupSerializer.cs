using System.IO;
using PopCapResManager;

namespace SexyParsers.Newton
{
/// <summary> ResGroup Serializer </summary>

public class ResGroupSerializer : IBinarySerializer<MResGroup>
{
// SubGroup serializer

private static readonly SubGroupSerializer subSerializer = new();

// Read data

public MResGroup ReadBin(Stream reader)
{

return new()
{
SlotCount = reader.ReadUInt32(),
Groups = BinaryList.ReadObjects(reader, subSerializer)
};

}

// Write data

public void WriteBin(Stream writer, MResGroup resGroup)
{
writer.WriteUInt32(resGroup.SlotCount);

BinaryList.WriteObjects(writer, resGroup.Groups, subSerializer);
}

}

}