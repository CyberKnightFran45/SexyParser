using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Widget. </summary>

public class FontWidget
{
/// <summary> Font Ascent </summary>

public int FontAscent{ get; set; }

/// <summary> Ascent Padding </summary>

public int AscentPadding{ get; set; }

/// <summary> Widget Height </summary>

public int WidgetHeight{ get; set; }

/// <summary> Line Spacing Offset </summary>

public int LineSpacingOffset{ get; set; }

/// <summary> Determines if this Widget was Initialized or not </summary>

public bool IsInitialized{ get; set; }

/// <summary> Default Point size </summary>

public int DefaultPx{ get; set; }

/// <summary> Collection of Chars for this Widget </summary>

public List<CharItem> Chars{ get; set; }

/// <summary> Collection of Layers for this Widget </summary>

public List<FontLayer> Layers{ get; set; }

/// <summary> Source file name </summary>

public string SourceFile{ get; set; }

/// <summary> Header name on Exception </summary>

public string ErrorHeader{ get; set; }

/// <summary> Widget Point size </summary>

public int Px{ get; set; }

/// <summary> Collection of Tags for this Widget </summary>

public List<string> Tags{ get; set; }

/// <summary> Widget scale </summary>

public double Scale{ get; set; }

/// <summary> Wheter to Force ScaledImages to be White. </summary>

public bool ForceScaledImageToWhite{ get; set; }

/// <summary> Wheter to Activate all Layers or not. </summary>

public bool ActivateAllLayers{ get; set; }

/// <summary> Creates a new <c>FontWidget</c> </summary>

public FontWidget()
{
}

public static readonly JsonSerializerContext Context = new Cfw2Context(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(CharItem))]
[JsonSerializable(typeof(List<CharItem>) ) ]

[JsonSerializable(typeof(FontKerning) ) ]
[JsonSerializable(typeof(List<FontKerning>) ) ]

[JsonSerializable(typeof(SexyRect) ) ]
[JsonSerializable(typeof(SexyPoint) ) ]

[JsonSerializable(typeof(FontChar) ) ]
[JsonSerializable(typeof(List<FontChar>) ) ]
[JsonSerializable(typeof(SexyColor) ) ]
[JsonSerializable(typeof(Limit<int>) ) ]

[JsonSerializable(typeof(FontLayer) ) ]
[JsonSerializable(typeof(List<FontLayer>) ) ]

[JsonSerializable(typeof(FontWidget) ) ]

public partial class Cfw2Context : JsonSerializerContext
{
}

}