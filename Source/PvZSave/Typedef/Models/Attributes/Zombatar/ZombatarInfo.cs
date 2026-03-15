using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PvZSave
{
/// <summary> Stores info about Player's Zombatars </summary>

public class ZombatarInfo
{
/// <summary> Determines if User accepted Zombatar license agreement </summary>

public bool AgreementAccepted{ get; set; }

/// <summary> List of Zombatars created </summary>

public List<ZombatarEntry> Zombatars{ get; set; }

/// <summary> Determines if User has Created at Least one Zombatar </summary>

public bool HasCreatedZombatar{ get; set; }

/// <summary> Creates a new instance of <c>ZombatarInfo</c> </summary>

public ZombatarInfo()
{
Zombatars = new();
}

/// <summary> Creates a new instance of <c>ZombatarInfo</c> </summary>

public ZombatarInfo(bool agreement, List<ZombatarEntry> entries, bool ignoreDialog)
{
AgreementAccepted = agreement;

Zombatars = entries;
HasCreatedZombatar = ignoreDialog;
}

public static readonly JsonSerializerContext Context = new ZombatarContext(JsonSerializer.Options);
}

// Context for Serialization

[JsonSerializable(typeof(ZombatarEntry))]
[JsonSerializable(typeof(List<ZombatarEntry>))]

public partial class ZombatarContext : JsonSerializerContext
{
}

}