using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents an Atlas Wrapper. </summary>

public class AtlasWrapper
{
/** <summary> Gets the Atlas Type. </summary>
<returns> The Atlas Type. </returns> */

[JsonPropertyName("type") ]

public ResType Type{ get; set; }

/** <summary> Gets or Sets a Path for this Atlas. </summary>

<remarks> This field can be an Array or a String. </remarks>

<returns> The Path. </returns> */

[JsonPropertyName("path") ]

public object Path{ get; set; }

/** <summary> Gets or Sets the Atlas Dimensions. </summary>
<returns> The Atlas Dimensions. </returns> */

[JsonPropertyName("dimension") ]

public Dimension Dimensions{ get; set; }

/** <summary> Gets or Sets the Atlas Data. </summary>
<returns> The Atlas Data. </returns> */

[JsonPropertyName("data") ]

public object Data{ get; set; }

// ctor

public AtlasWrapper()
{
}

}

}