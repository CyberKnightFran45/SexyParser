using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a Wrapper for CommonData. </summary>

public class CommonDataWrapper
{
/** <summary> Gets or Sets the Data Type. </summary>
<returns> The Data Type. </returns> */

[JsonPropertyName("type") ]

public ResType Type{ get; set; }

/** <summary> Gets or Sets a SourcePath for this SubGroup. </summary>

<remarks> This field can be an Array or a String. </remarks>

<returns> The SourcePath. </returns> */

[JsonPropertyName("path") ]

public object Path{ get; set; }

/** <summary> Wheter to force Original Vector SymbolSize. </summary>
<returns> true or false. </returns> */

[JsonPropertyName("forceOriginalVectorSymbolSize") ]

public bool? ForceOriginalVSize{ get; set; }

/** <summary> Gets or Sets a SourcePath for this SubGroup. </summary>

<remarks> This field can be an Array or a String. </remarks>

<returns> The SourcePath. </returns> */

[JsonPropertyName("srcpath") ]

public object SourcePath{ get; set; }

// ctor

public CommonDataWrapper()
{
}

}

}