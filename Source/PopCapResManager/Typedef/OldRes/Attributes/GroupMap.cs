using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a Map of Resource. </summary>

public class GroupMap
{
/** <summary> Determines if the Resource is Composite. </summary>
<returns> true or false </returns> */

[JsonPropertyName("is_composite") ]

public bool IsComposite{ get; set; }

/** <summary> Gets or Sets a Dictionary of SubGroups. </summary>
<returns> The SubGroups. </returns> */

[JsonPropertyName("subgroup") ]

public Dictionary<string, MSubGroupData> SubGroup{ get; set; } = new();

// ctor

public GroupMap()
{
}

}

}