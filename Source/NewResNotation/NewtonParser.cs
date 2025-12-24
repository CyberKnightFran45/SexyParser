using System;
using System.IO;
using SexyParsers.PopCapResManager;

namespace SexyParsers.NewResNotation
{
/// <summary> Initializes Parsing Tasks for NEWTON Files. </summary>

public class NewtonParser
{
/// <summary> The Expected Version Number </summary>

private const byte VERSION = 1;

// Get Coord Value

private static int GetCoordValue(int? coord, uint? aw, uint? ah)
{

if(coord is null)
return (aw is not null && aw != 0 && ah is not null && ah != 0) ? 0 : int.MaxValue;

return coord.Value;
}

// Encodes a ResGroup to NEWTON

public static void Encode(MResGroup resGroup, Stream writer)
{
writer.WriteUInt32(resGroup.SlotCount);

int groupsCount = resGroup.Groups.Count;
writer.WriteInt32(groupsCount);

for(int groupIndex = 0; groupIndex < groupsCount; ++groupIndex)
{
var groupEntry = resGroup.Groups[groupIndex];
writer.WriteByte( (byte)groupEntry.Type);

var subgroupsCount = (uint)groupEntry.SubGroups?.Count;
var resCount = (uint)groupEntry.Resources?.Count;

var resFlags = groupEntry.Res is null ? 0 : Convert.ToUInt32(groupEntry.Res);
writer.WriteUInt32(resFlags);

writer.WriteUInt32(subgroupsCount);
writer.WriteUInt32(resCount);

writer.WriteByte(VERSION);

bool hasParent = groupEntry.Parent is not null;
writer.WriteBool(hasParent);

writer.WriteStringByLen32(groupEntry.ID);

if(hasParent)
writer.WriteStringByLen32(groupEntry.Parent);

if(groupEntry.Type == SubGroupType.composite)
{

for(int i = 0; i < subgroupsCount; i++)
{
var current = groupEntry!.SubGroups[i]!;
var subResFlags = current.Res is null ? 0 : Convert.ToUInt32(current.Res);

writer.WriteUInt32(subResFlags);
writer.WriteStringByLen32(current.ID);
}

}

else
{

for(var resIndex = 0; resIndex < resCount; ++resIndex) 
{
var resEntry = groupEntry.Resources![resIndex]!;

writer.WriteByte( (byte)resEntry.Type);
writer.WriteUInt32(resEntry.Slot);

writer.WriteUInt32(resEntry.Width ?? 0);
writer.WriteUInt32(resEntry.Height ?? 0);

int resX = GetCoordValue(resEntry.X, resEntry.AtlasWidth, resEntry.AtlasHeight);
writer.WriteInt32(resX);

int resY = GetCoordValue(resEntry.Y, resEntry.AtlasWidth, resEntry.AtlasHeight);
writer.WriteInt32(resY);

writer.WriteUInt32(resEntry.AtlasX ?? 0);
writer.WriteUInt32(resEntry.AtlasY ?? 0);
writer.WriteUInt32(resEntry.AtlasWidth ?? 0);
writer.WriteUInt32(resEntry.AtlasHeight ?? 0);

writer.WriteUInt32(resEntry.Columns ?? 1);
writer.WriteUInt32(resEntry.Rows ?? 1);

writer.WriteBool(resEntry.IsAtlas ?? false);

writer.WriteByte(0x01);
writer.WriteByte(0x01);

bool resHasParent = resEntry.Parent is not null;
writer.WriteBool(resHasParent);

writer.WriteStringByLen32(resEntry.ID);

string path = resEntry.Path?.ToString();
writer.WriteStringByLen32(path);

if(resHasParent)
writer.WriteStringByLen32(resEntry.Parent);

}

}

}

}

/** <summary> Converts a ResGroup to NEWTON. </summary>

<param name = "inputPath"> The Path where the ResGroup to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded NEWTON File will be Saved. </param> */

public static void EncodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("NewRes Encoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".newton");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");	

TraceLogger.WriteActionStart("Loading ResGroup...");

using var inFile = FileManager.OpenRead(inputPath);
var resGroup = JsonSerializer.DeserializeObject<MResGroup>(inFile, MResGroup.Context);

TraceLogger.WriteActionEnd();

using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionStart("Encoding data...");
Encode(resGroup, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("Newton Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Decode NEWTON as ResGroup

public static MResGroup Decode(Stream reader)
{
MResGroup resGroup = new();

uint slotCount = reader.ReadUInt32();
resGroup.SlotCount = slotCount;

uint groupsCount = reader.ReadUInt32();

for(int groupIndex = 0; groupIndex < groupsCount; ++groupIndex)
{
ShellSubGroupData grp = new();
var groupType = (SubGroupType)reader.ReadByte();

if(!Enum.IsDefined(groupType) )
{
TraceLogger.WriteError($"Invalid SubGroup Type {groupType} at Pos: {reader.Position}");
return null;
}

grp.Type = groupType;

uint resFlags = reader.ReadUInt32();
grp.Res = resFlags != 0x00 ? $"{resFlags}" : null;

var subGroupsCount = reader.ReadUInt32();
var resCount = reader.ReadUInt32();

if(subGroupsCount == 0 && resCount == 0)
{
TraceLogger.WriteError("ResGroup must have SubGroups or one Resource at least.");
return null;
}

byte inputVer = reader.ReadUInt8();

if(inputVer != VERSION)
TraceLogger.WriteWarn($"Unknown Res version v{inputVer} at Pos {reader.Position} (Expected: v{VERSION})");

bool hasParent = reader.ReadBool();

using var idOwner = reader.ReadStringByLen32();
grp.ID = idOwner.ToString();

if(hasParent)
{
using var parentOwner = reader.ReadStringByLen32();
grp.Parent = parentOwner.ToString();
}

// Composite

if(groupType == SubGroupType.composite)
{

if(resCount > 0)
TraceLogger.WriteWarn($"Composite group '{grp.ID}' contains direct resources, which is not allowed.");

grp.SubGroups = new();

for(int subGroupsIndex = 0; subGroupsIndex < subGroupsCount; ++subGroupsIndex)
{
SubGroupWrapper subgroup = new();

var subResFlags = reader.ReadUInt32();
subgroup.Res = subResFlags == 0 ? null : $"{subResFlags}";

using var subIdOwner = reader.ReadStringByLen32();
subgroup.ID = subIdOwner.ToString();

grp.AddSubGroup(subgroup);
}

}

// Simple Res

else if(groupType == SubGroupType.simple)
{

if(subGroupsCount > 0)
TraceLogger.WriteWarn($"Resource is being treated as a SubGroup, but it should only contain a single file.");

grp.Resources = new();

for(var resIndex = 0; resIndex < resCount; ++resIndex)
{
MSubGroupWrapper resEntry = new();
var resType = (ResType)reader.ReadByte();

if(!Enum.IsDefined(resType) )
{
TraceLogger.WriteError($"Invalid ResType {resType} at Pos: {reader.Position}");
return null;
}

resEntry.Type = resType;

var resWrapper = MData.Read(reader);
bool isSprite = resWrapper.AtlasWidth != 0 && resWrapper.AtlasWidth != 0;

resEntry.Slot = resWrapper.Slot;
resEntry.Width = resWrapper.Width != 0 ? resWrapper.Width : null;
resEntry.Height = resWrapper.Height != 0 ? resWrapper.Height : null;
resEntry.X = resWrapper.X != int.MaxValue && resWrapper.X != 0 ? resWrapper.X : null; 
resEntry.Y = resWrapper.Y != int.MaxValue && resWrapper.Y != 0 ? resWrapper.Y : null;
resEntry.AtlasX = isSprite ? resWrapper.AtlasX : null;
resEntry.AtlasY = isSprite ? resWrapper.AtlasY : null;
resEntry.AtlasWidth = resWrapper.AtlasWidth != 0 ? resWrapper.AtlasWidth : null;
resEntry.AtlasHeight = resWrapper.AtlasHeight != 0 ? resWrapper.AtlasHeight : null;
resEntry.Columns = resWrapper.Columns != 1 ? resWrapper.Columns : null;
resEntry.Rows = resWrapper.Rows != 1 ? resWrapper.Rows : null; 

using var unk = reader.ReadPtr(2);
bool resHasParent = reader.ReadBool();

using var rIdOwner = reader.ReadStringByLen32();
resEntry.ID = rIdOwner.ToString();

using var pathOwner = reader.ReadStringByLen32();
resEntry.Path = pathOwner.ToString();

if(resHasParent)
{
using var parentOwner = reader.ReadStringByLen32();
resEntry.Parent = parentOwner.ToString();
}

switch(resType)
{
case ResType.PopAnim:
{
resEntry.ForceOriginalVSize = true;
break;
}

case ResType.RenderEffect:
{
resEntry.SourcePath = $"res\\common\\{resEntry.Path}";
break;
}

default:
{

if(resWrapper.IsAtlas)
{
resEntry.IsAtlas = true;
resEntry.Runtime = true;
}

break;
}

}

grp.AddRes(resEntry);
}

}

resGroup.AddGroup(grp);
}

return resGroup;
}

/** <summary> Converts a NETWON Stream to ResGroup. </summary>

<param name = "inputPath"> The Path where the NEWTON File to Encode is Located. </param>
<param name = "outputPath"> The Location where the ResGroup File will be Saved. </param> */

public static void DecodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("NewRes Decoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".json");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

using FileStream inFile = FileManager.OpenRead(inputPath);

TraceLogger.WriteActionStart("Decoding data...");
var resGroup = Decode(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving json...");

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(resGroup, outFile, MResGroup.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("Newton Decoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}