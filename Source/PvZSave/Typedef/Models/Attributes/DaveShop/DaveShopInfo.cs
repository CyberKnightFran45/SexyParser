using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Records which Items were Bough in Dave's Shop </summary>

public class DaveShopInfo
{
/// <summary> Plant Record </summary>

public PlantStoreRecord PlantRecord{ get; set; }

/// <summary> Items from Zen Garden </summary>

public ZenItemsRecord ZenItems{ get; set; }

/// <summary> Number of Extra SeedSlots </summary>

public uint ExtraSeedSlots{ get; set; }

/// <summary> Has <c>Pool Cleaner</c></summary>

public bool PoolCleaner{ get; set; }

/// <summary> Has <c>Roof Cleaner</c> </summary>

public bool RoofCleaner{ get; set; }

/// <summary> Number of Rake uses </summary>

public uint RakeUses{ get; set; }

/// <summary> Has <c>Aquarium Garden</c> </summary>

public bool AquariumGarden{ get; set; }

/// <summary> Amount of Chocolate </summary>

public int? Chocolate{ get; set; }

/// <summary> Has <c>Tree of Wisdom</c> </summary>

public bool TreeOfWisdom{ get; set; }

/// <summary> Amount of Tree Food </summary>

public int? TreeFood{ get; set; }

/// <summary> Has Upgrade: <c>Wall-nut First Aid</c> </summary>

public bool WallNutAid{ get; set; }

/// <summary> Creates a new instance of <c>DaveShopInfo</c> </summary>

public DaveShopInfo()
{
PlantRecord = new();
ZenItems = new();
}

public static readonly JsonSerializerContext Context = new DaveShopContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(PlantStoreRecord))]
[JsonSerializable(typeof(ZenItemsRecord))]

public partial class DaveShopContext : JsonSerializerContext
{
}

}