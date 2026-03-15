using System;
using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Represents a Data Container for a PvZ Profile </summary>

public class PvZUserdata
{
/// <summary> Identifier for Game version </summary>

public PvZVersion Version{ get; set; }

/// <summary> Current Level </summary>

public CurrentLevelID CurrentLevel{ get; set; }

/// <summary> Amount of Coins divided by 10 </summary>

public int Coins{ get; set; }

/// <summary> Number of times Adventure Mode completed </summary>

public uint AdventuresCompleted{ get; set; }

/// <summary> Flags attained in each Survival mode </summary>

public SurvivalModeInfo SurvivalData{ get; set; }

/// <summary> Trophies obtained in Minigames section </summary>

public MinigamesInfo MinigamesCompleted{ get; set; }

/// <summary> Trophies obtained in Minigames section </summary>

public LimboPageInfo MinigamesCompleted_Limbo{ get; set; }

/// <summary> Height of Tree of Wisdom in feet </summary>

public uint TreeWeight{ get; set; }

/// <summary> Puzzle records </summary>

public PuzzleModeInfo PuzzleData{ get; set; }

/// <summary> Upsell Trophy (Limbo Page) </summary>

public bool Limbo_Upsell{ get; set; }

/// <summary> Intro Trophy (Limbo Page) </summary>

public bool Limbo_Intro{ get; set; }

/// <summary> Items bought in Dave's Shop </summary>

public DaveShopInfo ItemsBought{ get; set; }

/// <summary> Play Anim when Almanac is Unlocked </summary>

public bool AlmanacUnlockedAnim{ get; set; }

/// <summary> Last time when Stinky the Snail was fed with Chocolate </summary>

public DateTime SnailLastChocolateTime{ get; set; }

/// <summary> Position of Stinky the Snail </summary>

public SexyPoint SnailPos{ get; set; }

/// <summary> Determines if Minigames are Unlocked or not </summary>

public bool MinigamesUnlocked{ get; set; }

/// <summary> Determines if Puzzles are Unlocked or not </summary>

public bool PuzzlesUnlocked{ get; set; }

/// <summary> Animations Played </summary>

public AnimPlayInfo AnimsPlayed{ get; set; }

/// <summary> Has <c>Taco</c> </summary>

public bool HasTaco{ get; set; }

/// <summary> Zen Garden Data </summary>

public ZenGardenInfo ZenGardenData{ get; set; }

/// <summary> Achievements unlocked </summary>

public AchievementsInfo Achievements{ get; set; }

/// <summary> Zombatar Data </summary>

public ZombatarInfo ZombatarData{ get; set; }

/// <summary> Creates a new instance of <c>PvZUserdata</c> </summary>

public PvZUserdata()
{
SurvivalData = new();

MinigamesCompleted = new();
MinigamesCompleted_Limbo = new();

PuzzleData = new();
ItemsBought = new();

AnimsPlayed = new();
ZenGardenData = new();

Achievements = new();
ZombatarData = new();
}

public static readonly JsonSerializerContext Context = new PvZUserdataContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(DateTime), TypeInfoPropertyName = "DateTime")]
[JsonSerializable(typeof(SexyPoint))]

[JsonSerializable(typeof(SurvivalModeInfo))]

[JsonSerializable(typeof(MinigamesInfo))]
[JsonSerializable(typeof(LimboPageInfo))]
[JsonSerializable(typeof(DaveShopInfo))]

[JsonSerializable(typeof(AnimPlayInfo))]
[JsonSerializable(typeof(ZenGardenInfo))]
[JsonSerializable(typeof(ZombatarInfo))]

public partial class PvZUserdataContext : JsonSerializerContext
{
}

}