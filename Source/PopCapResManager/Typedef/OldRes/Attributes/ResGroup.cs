using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a Resource Group. </summary>

public class ResGroup
{
/** <summary> Gets the Resource Version. </summary>
<returns> The Res Version. </returns> */

[JsonPropertyName("version") ]

public uint Version{ get; set; } = 1;

/** <summary> Gets the Content Version. </summary>
<returns> The Content Version. </returns> */

[JsonPropertyName("content_version") ]

public uint ContentVer{ get; set; } = 1;

/** <summary> Gets or Sets the Number of Slots. </summary>
<returns> The Slot Count. </returns> */

[JsonPropertyName("slot_count") ]

public uint SlotCount{ get; set; }

/** <summary> Gets or Sets the Groups of this Res. </summary>
<returns> The ResGroups. </returns> */

[JsonPropertyName("groups") ]

public List<SubGroupData> Groups{ get; set; } = new();

// ctor

public ResGroup()
{
}

}

}