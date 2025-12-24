using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents Info related to Resources from PvZ 2 </summary>

public class ResInfo
{
/** <summary> Gets or Sets the Type of Path to use. </summary>
<returns> The PathType. </returns> */

[JsonPropertyName("expand_path") ]

public PathType ExpandPath{ get; set; }

/** <summary> Gets or Sets a Dictionary of Groups. </summary>
<returns> The Groups. </returns> */

[JsonPropertyName("groups") ]

public Dictionary<string, GroupMap> Groups{ get; set; } = new();

// ctor

public ResInfo()
{
}

// ctor2

public ResInfo(PathType pStyle)
{
ExpandPath = pStyle;
}

// Convert SubGroupData to Atlas

private static MSubGroupData ConvertAtlasData(SubGroupData subGroup, bool isArray)
{
var resources = subGroup.Resources ?? [];

var childrenByParentId = resources
.Where(r => r.Parent is not null)
.GroupBy(r => r.Parent!)
.ToDictionary(g => g.Key, g => g.ToList() );

Dictionary<string, AtlasWrapper> packet = new();

foreach(var res in resources.Where(r => r.IsAtlas == true) )
{
string currID = res.ID;

AtlasWrapper atlas = new()
{
Type = res.Type,
Path = isArray ? res.Path : (res.Path as string)?.Split('\\'),
Dimensions = new((uint)res.Height!, (uint)res.Width!),
Data = new Dictionary<string, SpriteData>()
};

if(childrenByParentId.TryGetValue(currID, out var children) )
{

foreach(var child in children)
{
var atlasData = child.IsAtlas as dynamic;

atlasData[child.ID] = new SpriteData
{
Type = child.Type,
Path = isArray ? child.Path : (child.Path as string)?.Split('\\'),

Default = new()
{
AtlasX = (uint)child.AtlasX!,
AtlasY = (uint)child.AtlasY!,
AtlasHeight = (uint)child.AtlasHeight!,
AtlasWidth = (uint)child.AtlasWidth!,
X = child.X ?? 0,
Y = child.Y ?? 0,
Columns = child.Columns,
Rows = child.Rows
}

};

}

}

packet[currID] = atlas;
}

return new(subGroup.Res, packet);
}

// Convert SubGroupData for Common Wrappers

private static MSubGroupData ConvertCommonData(SubGroupData subGroup, bool isArray)
{
Dictionary<string, CommonDataWrapper> wrapperData = new();

foreach(var e in subGroup.Resources ?? [])
{

CommonDataWrapper wrapper = new()
{
Type = e.Type,
Path = isArray ? e.Path : (e.Path as string)?.Split('\\'),

SourcePath = e.SourcePath != null
? (isArray ? e.SourcePath : (e.SourcePath as string)?.Split('\\'))
: null,

ForceOriginalVSize = e.ForceOriginalVSize
};

wrapperData[e.ID] = wrapper;
}

CommonWrapper packet = new()
{
Type = "File",
Data = wrapperData
};

return new(packet);
}

/// <summary> Converts a ResGroup to a ResInfo. </summary>
/// <param name="resGroup"> The ResGroup to convert. </param>
/// <param name="pStyle"> The PathType to use for the conversion. </param>
/// <returns> A ResInfo object containing the converted data. </returns>

public static ResInfo FromResGroup(ResGroup resGroup, PathType pStyle)
{
bool isArray = pStyle == PathType.Array;
ResInfo resInfo = new(pStyle);

foreach(var grp in resGroup.Groups)
{

if(grp.SubGroups is not null)
{
Dictionary<string, MSubGroupData> subGroupDict = new();

foreach(var subGroupEntry in grp.SubGroups)
{
var target = resGroup.Groups.First(m => m.ID == subGroupEntry.ID);

var converted = subGroupEntry.Res is not null and not "0"
? ConvertAtlasData(target, isArray)
: ConvertCommonData(target, isArray);

subGroupDict[subGroupEntry.ID] = converted;
}

resInfo.Groups[grp.ID] = new()
{
IsComposite = true,
SubGroup = subGroupDict
};

}

if(grp.Parent is null && grp.Resources is not null)
{
	
resInfo.Groups[grp.ID] = new()
{
IsComposite = false,

SubGroup = new()
{
[grp.ID] = ConvertCommonData(grp, isArray)
}

};

}

}

return resInfo;
}

// Split Res into smaller files

public void Split(string outputDir)
{
string fileExt = ".json";

foreach(var groupEntry in Groups)
{
var grp = groupEntry.Value;

foreach(var subEntry in grp.SubGroup)
{
var subGroup = subEntry.Value;
var filePath = Path.Combine(outputDir, $"[{groupEntry.Key}]", subGroup.Type, subEntry.Key, fileExt);

using var subStream = FileManager.OpenWrite(filePath);
JsonSerializer.SerializeObject(subGroup, subStream);
}

}

}

/// <summary> Reconstructs a ResInfo object from a directory of split JSON files.</summary>
/// <param name="inputDir">The directory where the split files are located.</param>
/// <param name="pStyle">The PathType used for the original split.</param>
/// <returns>A rehydrated ResInfo instance.</returns>

public static ResInfo Merge(string inputDir)
{
ResInfo resInfo = new();
var jsonFiles = Directory.EnumerateFiles(inputDir, "*.json", SearchOption.AllDirectories);

foreach(var file in jsonFiles)
{

var parts = file.Replace(inputDir, "").TrimStart(Path.DirectorySeparatorChar)
.Split(Path.DirectorySeparatorChar);

if(parts.Length != 3)
continue;

string groupID = parts[0].Trim('[', ']');
string type = parts[1];

string subGroupID = Path.GetFileNameWithoutExtension(parts[2]);

using var subStream = FileManager.OpenRead(file);
var subGroupData = JsonSerializer.DeserializeObject<MSubGroupData>(subStream, MSubGroupData.Context);

if(!resInfo.Groups.TryGetValue(groupID, out var groupMap) )
{

groupMap = new()
{
IsComposite = true,
SubGroup = new()
};

resInfo.Groups[groupID] = groupMap;
}

groupMap.SubGroup[subGroupID] = subGroupData;
}

foreach(var grp in resInfo.Groups.Values)
grp.IsComposite = grp.SubGroup.Count > 1;

return resInfo;
}

public static readonly JsonSerializerContext Context = new OldResContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(MSubGroupData) ) ]
[JsonSerializable(typeof(Dictionary<string, MSubGroupData>) ) ]

[JsonSerializable(typeof(GroupMap) ) ]
[JsonSerializable(typeof(Dictionary<string, GroupMap>) ) ]

[JsonSerializable(typeof(ResInfo) ) ]

public partial class OldResContext : JsonSerializerContext
{
}

}