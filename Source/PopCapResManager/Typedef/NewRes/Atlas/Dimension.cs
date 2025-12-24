using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a 2D Dimension. </summary>

public class Dimension
{
/** <summary> Gets or Sets the Width. </summary>
<returns> The Width. </returns> */

[JsonPropertyName("width") ]

public uint Width{ get; set; }

/** <summary> Gets or Sets the Width. </summary>
<returns> The Width. </returns> */

[JsonPropertyName("height") ]

public uint Height{ get; set; }

// ctor
	
public Dimension()
{
}

// ctor2
	
public Dimension(uint width, uint height)
{
Width = width;
Height = height;
}

}

}