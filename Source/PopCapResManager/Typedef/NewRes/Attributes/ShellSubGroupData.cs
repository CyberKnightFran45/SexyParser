using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a SubGroup. </summary>

public class ShellSubGroupData
{
/** <summary> Gets or Sets the SubGroup Type. </summary>
<returns> The SubGroup Type. </returns> */

[JsonPropertyName("type") ]

public SubGroupType Type{ get; set; }

/** <summary> Gets or Sets the SubGroup ID. </summary>
<returns> The SubGroup ID. </returns> */

[JsonPropertyName("id") ]

public string ID{ get; set; } = string.Empty;

/** <summary> Gets or Sets the SubGroup Res. </summary>
<returns> The SubGroup Res. </returns> */

[JsonPropertyName("res") ]

public string Res{ get; set; }

/** <summary> Gets or Sets the SubGroup Parent. </summary>
<returns> The SubGroup Parent. </returns> */

[JsonPropertyName("parent") ]

public string Parent{ get; set; }

/** <summary> Gets or Sets a List of SubGroups. </summary>
<returns> The SubGroups list. </returns> */

[JsonPropertyName("subgroups") ]

public List<SubGroupWrapper> SubGroups{ get; set; }

/** <summary> Gets or Sets a List of Resources. </summary>
<returns> The SubGroups list. </returns> */

[JsonPropertyName("resources") ]

public List<MSubGroupWrapper> Resources{ get; set; }

// ctor

public ShellSubGroupData()
{
Type = SubGroupType.simple;
}

// ctor 2

public ShellSubGroupData(string id)
{
Type = SubGroupType.composite;

ID = id;
SubGroups = new();
}

// ctor 3

public ShellSubGroupData(ExtraInfo composite)
{
Type = SubGroupType.simple;
ID = composite.ID;

Parent = composite.Parent;
Resources = new();
}

// ctor 4

public ShellSubGroupData(ExtraInfo composite, string res)
{
Type = SubGroupType.simple;
ID = composite.ID;

Parent = composite.Parent;
Res = res;

Resources = new();
}

// Add Res

public void AddRes(MSubGroupWrapper res)
{
Resources ??= new();

Resources.Add(res);
}

// Add SubGroup

public void AddSubGroup(SubGroupWrapper subGroup)
{
SubGroups ??= new();

SubGroups.Add(subGroup);
}

}

}