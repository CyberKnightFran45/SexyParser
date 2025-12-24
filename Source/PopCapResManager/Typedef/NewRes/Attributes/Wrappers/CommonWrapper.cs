using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a Wrapper for CommonData. </summary>

public class CommonWrapper
{
/** <summary> Gets or Sets the Data Type. </summary>
<returns> The Data Type. </returns> */

[JsonPropertyName("type") ]

public string Type{ get; set; }

/** <summary> Gets or Sets the Data Type. </summary>
<returns> The Data Type. </returns> */

[JsonPropertyName("data") ]

public Dictionary<string, CommonDataWrapper> Data = new();

// ctor

public CommonWrapper()
{
}

}

}