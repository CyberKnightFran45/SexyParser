using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a Resource Group. </summary>

public class MResGroup
{
/** <summary> Gets the Resource Version. </summary>
<returns> The Res Version. </returns> */

[JsonPropertyName("version") ]

public uint Version{ get; set; } = 1;

/** <summary> Gets the Content Version. </summary>
<returns> The Content Version. </returns> */

[JsonPropertyName("content_version") ]

public uint ContentVer{ get; set; } = 1;

/** <summary> Gets or Sets the Number of Slots. </summary>
<returns> The Slot Count. </returns> */

[JsonPropertyName("slot_count") ]

public uint SlotCount{ get; set; }

/** <summary> Gets or Sets the Groups of this Res. </summary>
<returns> The ResGroups. </returns> */

[JsonPropertyName("groups") ]

public List<ShellSubGroupData> Groups{ get; set; } = new();

// ctor

public MResGroup()
{
}

// Add Group

public void AddGroup(ShellSubGroupData grp) => Groups.Add(grp);

// Rewrite the slots for the ResGroup

public void RewriteSlot()
{
Dictionary<string, uint> map = new();

foreach(var grp in Groups)
{

if(grp.Resources is null)
continue;

foreach(var res in grp.Resources)
{

if(string.IsNullOrEmpty(res.ID))
continue;

if(!map.TryGetValue(res.ID, out uint assignedSlot) )
{
assignedSlot = SlotCount++;
map[res.ID] = assignedSlot;
}

res.Slot = assignedSlot;
}

}

}


// Handle Null Tokens

[MethodImpl(MethodImplOptions.AggressiveInlining)]

private static int? GetNullableInt(JToken token)
{

if (token == null) return null;

int val = (int)token;

return val != 0 ? val : null;
}

// Generate FileInfo

private static ShellSubGroupData GenFileInfo(ExtraInfo composite, MSubGroupData image, bool isArray)
{
ShellSubGroupData fileInfo = new(composite);

var jPacket = image.Packet as dynamic;

if(jPacket.Data is not JObject data) 
return fileInfo;

foreach(var property in data.Properties() )
{
string key = property.Name;

if(property.Value is not JObject jVal) 
continue;

MSubGroupWrapper res = new()
{
ID = key,
Path = isArray ? jVal["path"]! : string.Join('\\', jVal["path"]!),
Type = Enum.TryParse<ResType>(jVal["type"]!.ToString(), out var resType) ? resType : ResType.File,

SourcePath = jVal["srcpath"] is not null ? isArray ? jVal["srcpath"]! 
: string.Join('\\', jVal["srcpath"]!) : null,

ForceOriginalVSize =  jVal["forceOriginalVectorSymbolSize"]?.ToObject<bool?>()
};

fileInfo.AddRes(res);
}

return fileInfo;
}

// Generate ImageInfo

private static ShellSubGroupData GenImgInfo(ExtraInfo composite, MSubGroupData image, bool isArray)
{
ShellSubGroupData imgInfo = new(composite, image.Type);
var list = (JObject)image.Packet!;

foreach(var property in list.Properties() )
{
string key = property.Name;
var valueObj = (JObject)property.Value;

var pathToken = valueObj["path"];
var dimToken = valueObj["dimension"];
string type = (string)valueObj["type"]!;

MSubGroupWrapper resource = new()
{
ID = key,
Path = isArray ? pathToken! : string.Join('\\', pathToken!),
Type = Enum.TryParse<ResType>(valueObj["type"]!.ToString(), out var resType) ? resType : ResType.Image,
IsAtlas = true,
Runtime = true,
Width = (uint)dimToken!["width"]!,
Height = (uint)dimToken!["height"]!
};

imgInfo.AddRes(resource);

var dataToken = (JObject)valueObj["data"];

if(dataToken is null)
continue;

foreach(var subProperty in dataToken.Properties() )
{
string subKey = subProperty.Name;
var subValue = (JObject)subProperty.Value;

var subPath = subValue["path"];
var defToken = subValue["default"];

MSubGroupWrapper subRes = new()
{
ID = subKey,
Path = isArray ? subPath! : string.Join('\\', subPath!),
Type = Enum.TryParse<ResType>(valueObj["type"]!.ToString(), out var subType) ? subType : ResType.Image,
Parent = key,
AtlasX = (uint)defToken!["ax"]!,
AtlasY = (uint)defToken["ay"]!,
AtlasWidth = (uint)defToken["aw"]!,
AtlasHeight = (uint)defToken["ah"]!,
X = GetNullableInt(defToken["x"]),
Y = GetNullableInt(defToken["y"]),
Columns = (uint?)defToken["cols"],
Rows = (uint?)defToken["rows"]
};

imgInfo.AddRes(subRes);
}

}

return imgInfo;
}

// Generate CompositeInfo

private static ShellSubGroupData GenComposite(string id, GroupMap composite)
{
ShellSubGroupData compositeInfo = new(id);

foreach(var kvp in composite.SubGroup)
{
SubGroupWrapper subGroup = new(kvp.Key, kvp.Value.Type);
compositeInfo.AddSubGroup(subGroup);
}

return compositeInfo;
}

// Convert ResGroup from ResInfo

public static MResGroup FromResInfo(ResInfo resInfo)
{
bool isArray = resInfo.ExpandPath == PathType.Array;
MResGroup resGroup = new();

foreach(var composite in resInfo.Groups)
{
string compositeName = composite.Key;
GroupMap grp = composite.Value;

if(grp.IsComposite)
{
var compositeInfo = GenComposite(compositeName, grp);
resGroup.AddGroup(compositeInfo);

foreach(var subGroupEntry in grp.SubGroup)
{
var subGroup = subGroupEntry.Value;
ExtraInfo extra = new(subGroupEntry.Key, subGroup.Type);

var subInfo = subGroup.Type is not null and not "0"
? GenImgInfo(extra, subGroup, isArray)
: GenFileInfo(extra, subGroup, isArray);

resGroup.AddGroup(subInfo);
}

}

else
{

foreach(var subGroupEntry in grp.SubGroup)
{
var subGroup = subGroupEntry.Value;
ExtraInfo extra = new(subGroupEntry.Key, subGroup.Type);

var fileInfo = GenFileInfo(extra, subGroup, isArray);
resGroup.AddGroup(fileInfo);
}

}

}

resGroup.RewriteSlot();

return resGroup;
}

// Split Res into smaller files

public void Split(string outputDir)
{
string fileExt = ".json";

foreach(var grp in Groups)
{
string parentName = grp.Parent ?? string.Empty;
var parentDir = Path.Combine(outputDir, grp.Type.ToString(), $"[{grp.Res}]", parentName, grp.ID);

if(grp.Type == SubGroupType.composite)
{

foreach(var subGroup in grp.SubGroups ?? [])
{
string filePath = Path.Combine(parentDir, subGroup.ID, fileExt);
using var subStream = FileManager.OpenWrite(filePath);

JsonSerializer.SerializeObject(subGroup, subStream, SubGroupWrapper.Context);
}

}

else
{

foreach(var res in grp.Resources ?? [])
{

string resName = res.Path switch
{
string[] arr => arr.LastOrDefault(),
string str => Path.GetFileNameWithoutExtension(str),
_ => $"Res#{res.Slot}"
};

var filePath = Path.Combine(parentDir, resName, fileExt);
using var resStream = FileManager.OpenWrite(filePath);

JsonSerializer.SerializeObject(res, resStream, MSubGroupWrapper.Context);
}

}

}

}

// Merge res

public static MResGroup Merge(string inputDir)
{
MResGroup resGroup = new();

foreach(var typeDir in Directory.EnumerateDirectories(inputDir) )
{

if(!Enum.TryParse<SubGroupType>(Path.GetFileName(typeDir), out var type) )
continue;

foreach(var groupDir in Directory.EnumerateDirectories(typeDir) )
{
string resName = Path.GetFileName(groupDir).Trim('[', ']');

foreach(var parentDir in Directory.EnumerateDirectories(groupDir) )
{
string parentName = Path.GetFileName(parentDir);

foreach(var resDir in Directory.EnumerateDirectories(parentDir) )
{
string groupId = Path.GetFileName(resDir);

ShellSubGroupData grp = new()
{
Type = type,
ID = groupId,
Parent = parentName,
Res = resName
};

if(type == SubGroupType.composite)
{
grp.SubGroups = new();

foreach(var file in Directory.EnumerateFiles(resDir, "*.json") )
{
using var subStream = FileManager.OpenRead(file);
var subGroup = JsonSerializer.DeserializeObject<SubGroupWrapper>(subStream, SubGroupWrapper.Context);

grp.AddSubGroup(subGroup);
}

}

else
{
grp.Resources = new();

foreach(var file in Directory.EnumerateFiles(resDir, "*.json") )
{
using var wStream = FileManager.OpenRead(file);
var wrapper = JsonSerializer.DeserializeObject<MSubGroupWrapper>(wStream, MSubGroupWrapper.Context);

grp.AddRes(wrapper);
}

}

resGroup.AddGroup(grp);
}

}

}

}

resGroup.RewriteSlot();

return resGroup;
}


public static readonly JsonSerializerContext Context = new NewResContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(SubGroupWrapper) ) ]
[JsonSerializable(typeof(List<SubGroupWrapper>) ) ]

[JsonSerializable(typeof(MSubGroupWrapper) ) ]
[JsonSerializable(typeof(List<MSubGroupWrapper>) ) ]

[JsonSerializable(typeof(ShellSubGroupData) ) ]
[JsonSerializable(typeof(List<ShellSubGroupData>) ) ]

[JsonSerializable(typeof(MResGroup) ) ]

public partial class NewResContext : JsonSerializerContext
{
}

}