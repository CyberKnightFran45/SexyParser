using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a SubGroup. </summary>

public class SubGroupData
{
/** <summary> Gets or Sets the SubGroup ID. </summary>
<returns> The SubGroup ID. </returns> */

[JsonPropertyName("id") ]

public string ID{ get; set; }

/** <summary> Gets or Sets the SubGroup Type. </summary>
<returns> The SubGroup Type. </returns> */

[JsonPropertyName("type") ]

public string Type{ get; set; }

/** <summary> Gets or Sets the SubGroup Parent. </summary>
<returns> The SubGroup Parent. </returns> */

[JsonPropertyName("parent") ]

public string Parent{ get; set; }

/** <summary> Gets or Sets the SubGroup Res. </summary>
<returns> The SubGroup Res. </returns> */

[JsonPropertyName("res") ]

public string Res{ get; set; }

/** <summary> Gets or Sets a List of SubGroups. </summary>
<returns> The SubGroups list. </returns> */

[JsonPropertyName("subgroups") ]

public List<SubGroupWrapper> SubGroups{ get; set; } = new();

/** <summary> Gets or Sets a List of Resources. </summary>
<returns> The SubGroups list. </returns> */

[JsonPropertyName("resources") ]

public List<MSubGroupWrapper> Resources{ get; set; } = new();

// ctor

public SubGroupData()
{
}

}

}