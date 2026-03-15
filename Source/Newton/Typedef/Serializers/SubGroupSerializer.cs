using System;
using System.IO;
using PopCapResManager;

namespace SexyParsers.Newton
{
/// <summary> SubGroup Serializer </summary>

public class SubGroupSerializer : IBinarySerializer<ShellSubGroupData>
{
// Composite serializer

private static readonly CompositeGroupSerializer compositeSerializer = new();

// Res serializer

private static readonly SimpleResSerializer resSerializer = new();

// Read data

public ShellSubGroupData ReadBin(Stream reader)
{
var type = (SubGroupType)reader.ReadByte();

if(!Enum.IsDefined(type) )
{
TraceLogger.WriteError($"Invalid SubGroup Type {type} at Pos: {reader.Position}");

return null;
}

uint flags = reader.ReadUInt32();

int subGroupsCount = reader.ReadInt32();
int resCount = reader.ReadInt32();

if(subGroupsCount <= 0 && resCount <= 0)
{
TraceLogger.WriteError("ResGroup must have SubGroups or one Resource at least.");

return null;
}

byte version = reader.ReadUInt8();

if(version != 1)
TraceLogger.WriteWarn($"Unknown Res version v{version} at Pos {reader.Position} (Expected: v1)");

bool hasParent = reader.ReadBool();
using var idOwner = reader.ReadStringByLen32();

string parent = null;

if(hasParent)
{
using var parentOwner = reader.ReadStringByLen32();
parent = parentOwner.ToString();
}

ShellSubGroupData subGroup = new()
{
Type = type,
Res = flags != 0x00 ? $"{flags}" : null,
ID = idOwner.ToString(),
Parent = parent
};

if(type == SubGroupType.composite)
subGroup.SubGroups = BinaryList.ReadObjects(reader, compositeSerializer, subGroupsCount);

else
subGroup.Resources = BinaryList.ReadObjects(reader, resSerializer, resCount);

return subGroup;
}

// Write data

public void WriteBin(Stream writer, ShellSubGroupData subGroup)
{
writer.WriteByte( (byte)subGroup.Type);

var resFlags = subGroup.Res is null ? 0 : Convert.ToUInt32(subGroup.Res);
writer.WriteUInt32(resFlags);

writer.WriteInt32(subGroup.SubGroups.Count);
writer.WriteInt32(subGroup.Resources.Count);

writer.WriteByte(0x01); // Version

bool hasParent = subGroup.Parent is not null;
writer.WriteBool(hasParent);

writer.WriteStringByLen32(subGroup.ID);

if(hasParent)
writer.WriteStringByLen32(subGroup.Parent);

if(subGroup.Type == SubGroupType.composite)
BinaryList.WriteObjects(writer, subGroup.SubGroups, compositeSerializer, false);

else
BinaryList.WriteObjects(writer, subGroup.Resources, resSerializer, false);

}

}

}