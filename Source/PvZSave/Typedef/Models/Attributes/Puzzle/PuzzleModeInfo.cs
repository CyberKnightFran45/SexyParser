using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Records for Puzzle Mode </summary>

public class PuzzleModeInfo
{
/// <summary> Progress for Vasebreaker </summary>

public PuzzleRecord Vasebreaker{ get; set; }

/// <summary> Progress for I, Zombie </summary>

public PuzzleRecord IZombie{ get; set; }

/// <summary> Creates a new instance of <c>PuzzleModeInfo</c> </summary>

public PuzzleModeInfo()
{
Vasebreaker = new();
IZombie = new();
}

public static readonly JsonSerializerContext Context = new PuzzleDataContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(PuzzleRecord))]

public partial class PuzzleDataContext : JsonSerializerContext
{
}

}