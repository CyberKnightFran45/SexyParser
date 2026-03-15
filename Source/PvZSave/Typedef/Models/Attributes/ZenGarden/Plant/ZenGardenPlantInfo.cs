using System;
using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Info for a Zen Garden Plant </summary>

public class ZenGardenPlantInfo
{
/// <summary> Plant Type </summary>

public PlantTypeID PlantType{ get; set; }

/// <summary> Garden Location </summary>

public GardenLocationID GardenLocation{ get; set; }

/// <summary> Position of Plant </summary>

public SexyPoint PlantPos{ get; set; }

/// <summary> Plant face direction </summary>

public PlantFaceDirectionID FaceDirection{ get; set; }

/// <summary> Last time plant was watered </summary>

public DateTime LastTimeWatered{ get; set; }

/// <summary> Plant Color </summary>

public PlantColorID PlantColor{ get; set; }

/// <summary> Number of Times fertilized </summary>

public uint TimesFertilized{ get; set; }

/// <summary> Number of Times watered </summary>

public uint TimesWatered{ get; set; }

/// <summary> Number of times plant needs to be watered </summary>

public uint WateringNeedTimes{ get; set; }

/// <summary> Plant Needs </summary>

public ZenGardenPlantNeeds PlantNeeds{ get; set; }

/// <summary> Last time Phonograph/Bug Spray was used (Plant is Happy) </summary>

public DateTime LastHappyTime{ get; set; }

/// <summary> Last time fertilized </summary>

public DateTime LastTimeFertilized{ get; set; }

/// <summary> Last time plant was fed with Chocolate </summary>

public DateTime LastChocolateTime{ get; set; }

/// <summary> Creates a new instance of <c>ZenGardenPlantInfo</c> </summary>

public ZenGardenPlantInfo()
{
}

public static readonly JsonSerializerContext Context = new ZenGardenPlantContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(SexyPoint))]

public partial class ZenGardenPlantContext : JsonSerializerContext
{
}

}