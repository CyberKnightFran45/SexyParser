using System;
using System.IO;
using PopCapResManager;

namespace SexyParsers.Newton
{
/// <summary> CompositeGroup Serializer </summary>

public class CompositeGroupSerializer : IBinarySerializer<SubGroupWrapper>
{
// Read data

public SubGroupWrapper ReadBin(Stream reader)
{
uint flags = reader.ReadUInt32();
using var idOwner = reader.ReadStringByLen32();

return new()
{
Res = flags == 0 ? null : $"{flags}",
ID = idOwner.ToString()
};

}

// Write data

public void WriteBin(Stream writer, SubGroupWrapper composite)
{
var flags = composite.Res is null ? 0 : Convert.ToUInt32(composite.Res);

writer.WriteUInt32(flags);
writer.WriteStringByLen32(composite.ID);
}

}

}