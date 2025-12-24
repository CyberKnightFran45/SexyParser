using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents some SpriteData. </summary>

public class SpriteData
{
/** <summary> Gets or Sets the Sprite Type. </summary>
<returns> The Sprite Type. </returns> */

[JsonPropertyName("type") ]

public ResType Type{ get; set; }

/** <summary> Gets or Sets a Path for this Atlas. </summary>

<remarks> This field can be an Array or a String. </remarks>

<returns> The Path. </returns> */

[JsonPropertyName("path") ]

public object Path{ get; set; }

/** <summary> Gets or Sets the Default SpriteData. </summary>
<returns> The Default SpriteData. </returns> */

[JsonPropertyName("default") ]

public DefaultProperty Default{ get; set; }

// ctor

public SpriteData()
{
}

}

}