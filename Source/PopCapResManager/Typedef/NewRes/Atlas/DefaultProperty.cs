using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a DefaultProperty for SpriteData. </summary>

public class DefaultProperty
{
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

// ctor

public DefaultProperty()
{
}

}

}