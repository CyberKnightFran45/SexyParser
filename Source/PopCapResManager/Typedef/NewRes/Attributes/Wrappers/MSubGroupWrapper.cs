using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a SubGroup Wrapper. </summary>

public class MSubGroupWrapper
{
/** <summary> Gets or Sets the SubGroup Type. </summary>
<returns> The SubGroup Type. </returns> */

[JsonPropertyName("type") ]

public ResType Type{ get; set; }

/** <summary> Gets or Sets the SubGroup Slot. </summary>
<returns> The SubGroup Slot. </returns> */

[JsonPropertyName("slot") ]

public uint Slot{ get; set; }

/** <summary> Gets or Sets the SubGroup ID. </summary>
<returns> The SubGroup ID. </returns> */

[JsonPropertyName("id") ]

public string ID{ get; set; } = string.Empty;

/** <summary> Gets or Sets the SubGroup Path. </summary>

<remarks> This field can be an Array or a String. </remarks>

<returns> The SubGroup Path. </returns> */

[JsonPropertyName("path") ]

public object Path{ get; set; }

/** <summary> Determines if the SubGroup is an Atlas Image or not. </summary>
<returns> true or false. </returns> */

[JsonPropertyName("atlas") ]

public bool? IsAtlas{ get; set; }

/** <summary> Wheter to use this Wrapper in Runtime. </summary>
<returns> true or false. </returns> */

[JsonPropertyName("runtime") ]

public bool? Runtime{ get; set; }

/** <summary> Gets or Sets the X Coordinate. </summary>
<returns> The Value of X. </returns> */

[JsonPropertyName("x") ]

public int? X{ get; set; }

/** <summary> Gets or Sets the Y Coordinate. </summary>
<returns> The Value of Y. </returns> */

[JsonPropertyName("y") ]

public int? Y{ get; set; }

/** <summary> Gets or Sets the Number of Columns </summary>
<returns> The Cols Number. </returns> */

[JsonPropertyName("cols") ]

public uint? Columns{ get; set; }

/** <summary> Gets or Sets the Number of Rows </summary>
<returns> The Rows Number. </returns> */

[JsonPropertyName("rows") ]

public uint? Rows{ get; set; }

/** <summary> Gets or Sets the Parent of this SubGroup </summary>
<returns> The Parent SubGroup. </returns> */

[JsonPropertyName("parent") ]

public string Parent{ get; set; }

/** <summary> Gets or Sets the X Coordinate for an Atlas SubGroup </summary>
<returns> The Value of X for Atlas. </returns> */

[JsonPropertyName("ax") ]

public uint? AtlasX{ get; set; }

/** <summary> Gets or Sets the Y Coordinate for an Atlas SubGroup </summary>
<returns> The Value of Y for Atlas. </returns> */

[JsonPropertyName("ay") ]

public uint? AtlasY{ get; set; }

/** <summary> Gets or Sets the Width of an Atlas SubGroup </summary>
<returns> The Atlas Width. </returns> */

[JsonPropertyName("aw") ]

public uint? AtlasWidth{ get; set; }

/** <summary> Gets or Sets the Height of an Atlas SubGroup </summary>
<returns> The Atlas Height. </returns> */

[JsonPropertyName("ah") ]

public uint? AtlasHeight{ get; set; }

/** <summary> Gets or Sets the Width of this SubGroup </summary>
<returns> The Width. </returns> */

[JsonPropertyName("width") ]

public uint? Width{ get; set; }

/** <summary> Gets or Sets the Height of this SubGroup </summary>
<returns> The Height. </returns> */

[JsonPropertyName("height") ]

public uint? Height{ get; set; }

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

public MSubGroupWrapper()
{
}

public static readonly JsonSerializerContext Context = new MSubWrapperContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(MSubGroupWrapper) ) ]

public partial class MSubWrapperContext : JsonSerializerContext
{
}

}