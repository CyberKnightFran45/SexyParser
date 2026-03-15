using System;

namespace SexyParsers.PvZSave
{
/// <summary> Records Item States for Zen Garden </summary>

public class ZenItemsRecord
{
/// <summary> Days since left Marigold was Purchased from Store </summary>

public DateTime? MarigoldLastPurchased_L{ get; set; }

/// <summary> Days since center Marigold was Purchased from Store </summary>

public DateTime? MarigoldLastPurchased{ get; set; }

/// <summary> Days since right Marigold was Purchased from Store </summary>

public DateTime? MarigoldLastPurchased_R{ get; set; }

/// <summary> <c>Gold Watering Can</c> Sale State </summary>

public bool GoldCan{ get; set; }

/// <summary> Amount of Fertilizer </summary>

public int? Fertilizer{ get; set; }

/// <summary> Amount of Bug Spray </summary>

public int? BugSpray{ get; set; }

/// <summary> <c>Phonograph</c> Sale State </summary>

public bool Phonograph{ get; set; }

/// <summary> <c>Gardening Glove</c> Sale State </summary>

public bool GardenGlove{ get; set; }

/// <summary> <c>Mushroom Garden</c> Sale State </summary>

public bool MushroomGarden{ get; set; }

/// <summary> <c>Wheel Barrow</c> Sale State </summary>

public bool WheelBarrow{ get; set; }

/// <summary> Last time when Stinky the Snail awoken </summary>

public DateTime SnailLastAwakeTime{ get; set; }

/// <summary> Creates a new instance of <c>ZenItemsRecord</c> </summary>

public ZenItemsRecord()
{
}

}

}