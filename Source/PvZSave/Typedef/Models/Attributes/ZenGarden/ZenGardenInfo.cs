using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Stores info about Player's Zen Garden </summary>

public class ZenGardenInfo
{
/// <summary> Stinky the Snail Conscious State (Awake or Sleeping) </summary>

public SnailConsciousState SnailState{ get; set; }

/// <summary> List of Plants and their Info </summary>

public List<ZenGardenPlantInfo> ZenPlants{ get; set; }

/// <summary> Creates a new instance of <c>ZenGardenInfo</c> </summary>

public ZenGardenInfo()
{
ZenPlants = new();
}

public static readonly JsonSerializerContext Context = new ZenGardenContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(ZenGardenPlantInfo))]
[JsonSerializable(typeof(List<ZenGardenPlantInfo>))]

public partial class ZenGardenContext : JsonSerializerContext
{
}

}