using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Records for Flags in Survival Mode </summary>

public class SurvivalModeInfo
{
/// <summary> Progress for Survival </summary>

public SurvivalRecord Progress{ get; set; }

/// <summary> Progress for Survival (Hard) </summary>

public SurvivalRecord Progress_Hard{ get; set; }

/// <summary> Progress for Survival (Endless) </summary>

public SurvivalRecord Progress_Endless{ get; set; }

/// <summary> Creates a new instance of <c>SurvivalModeInfo</c> </summary>

public SurvivalModeInfo()
{
Progress = new();

Progress_Hard = new();
Progress_Endless = new();
}

public static readonly JsonSerializerContext Context = new SurvivalDataContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(SurvivalRecord))]

public partial class SurvivalDataContext : JsonSerializerContext
{
}

}