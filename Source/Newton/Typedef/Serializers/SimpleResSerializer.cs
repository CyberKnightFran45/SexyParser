using System;
using System.IO;
using PopCapResManager;

namespace SexyParsers.Newton
{
/// <summary> SimpleRes Serializer </summary>

public class SimpleResSerializer : IBinarySerializer<MSubGroupWrapper>
{
// Read Nullable int

private static int? ReadNullableInt(Stream reader, int offset = 0)
{
int v = reader.ReadInt32();

return v != offset ? v : null;
}

// Set Coord Value

private static int? SetCoordValue(int? coord) => coord != int.MaxValue && coord != 0 ? coord : null; 

// Read data

public MSubGroupWrapper ReadBin(Stream reader)
{
var type = (ResType)reader.ReadByte();

if(!Enum.IsDefined(type) )
{
TraceLogger.WriteError($"Invalid ResType {type} at Pos: {reader.Position}");

return null;
}

uint slot = reader.ReadUInt32();

var width = ReadNullableInt(reader);
var height = ReadNullableInt(reader);

var aX = reader.ReadInt32();
var aY = reader.ReadInt32();
var aH = ReadNullableInt(reader);
var aW = ReadNullableInt(reader);

var cols = ReadNullableInt(reader, 1);
var rows = ReadNullableInt(reader, 1);

bool isAtlas = reader.ReadBool();

var x = ReadNullableInt(reader);
var y = ReadNullableInt(reader);

byte contentVer = reader.ReadUInt8();

if(contentVer != 1)
TraceLogger.WriteWarn($"Unknown Content version v{contentVer} at Pos {reader.Position} (Expected: v1)");

byte version = reader.ReadUInt8();

if(version != 1)
TraceLogger.WriteWarn($"Unknown Res version v{version} at Pos {reader.Position} (Expected: v1)");

bool hasParent = reader.ReadBool();

using var idOwner = reader.ReadStringByLen32();
using var pathOwner = reader.ReadStringByLen32();

string parent = null;

if(hasParent)
{
using var parentOwner = reader.ReadStringByLen32();
parent = parentOwner.ToString();
}

bool isSprite = aW != 0 && aH != 0;

MSubGroupWrapper res = new()
{
Type = type,
Slot = slot,
Width = width,
Height = height,
X = SetCoordValue(x),
Y = SetCoordValue(y),
AtlasX = isSprite ? aX : null,
AtlasY = isSprite ? aY : null,
AtlasWidth = aW,
AtlasHeight = aH,
Columns = cols,
Rows = rows,
ID = idOwner.ToString(),
Path = pathOwner.ToString(),
Parent = parent
};

switch(type)
{
case ResType.PopAnim:
res.ForceOriginalVSize = true;
break;

case ResType.RenderEffect:
res.SourcePath = $"res\\common\\{res.Path}";
break;

default:
res.IsAtlas = isAtlas;

res.Runtime = isAtlas;
break;
}

return res;
}

// Get Coord Value

private static int GetCoordValue(int? coord, int? aw, int? ah)
{

if(coord is null)
return (aw is not null && aw != 0 && ah is not null && ah != 0) ? 0 : int.MaxValue;

return coord.Value;
}

// Write data

public void WriteBin(Stream writer, MSubGroupWrapper res)
{
writer.WriteByte( (byte)res.Type);
writer.WriteUInt32(res.Slot);

writer.WriteInt32(res.Width ?? 0);
writer.WriteInt32(res.Height ?? 0);

int resX = GetCoordValue(res.X, res.AtlasWidth, res.AtlasHeight);
writer.WriteInt32(resX);

int resY = GetCoordValue(res.Y, res.AtlasWidth, res.AtlasHeight);
writer.WriteInt32(resY);

writer.WriteInt32(res.AtlasX ?? 0);
writer.WriteInt32(res.AtlasY ?? 0);
writer.WriteInt32(res.AtlasWidth ?? 0);
writer.WriteInt32(res.AtlasHeight ?? 0);

writer.WriteInt32(res.Columns ?? 1);
writer.WriteInt32(res.Rows ?? 1);

writer.WriteBool(res.IsAtlas ?? false);

writer.WriteByte(0x01); // Content version
writer.WriteByte(0x01); // Version

bool hasParent = res.Parent is not null;
writer.WriteBool(hasParent);

writer.WriteStringByLen32(res.ID);

string path = res.Path?.ToString();
writer.WriteStringByLen32(path);

if(hasParent)
writer.WriteStringByLen32(res.Parent);

}

}

}