using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Layer </summary>

public class FontLayer
{
/// <summary> Layer name </summary>

public string LayerName{ get; set; }

/// <summary> Tags required by this Layer </summary>

public List<string> RequiredTags{ get; set; }

/// <summary> Tags to exclude </summary>

public List<string> TagsToExclude{ get; set; }

/// <summary> Font Kernings </summary>

public List<FontKerning> Kernings{ get; set; }

/// <summary> Chars for this Layer </summary>

public List<FontChar> Chars{ get; set; }

/// <summary> Color multiplier </summary>

public SexyColor ColorMul{ get; set; }

/// <summary> Color addend </summary>

public SexyColor ColorSum{ get; set; }

/// <summary> Image file name </summary>

public string ImageFile{ get; set; }

/// <summary> Draw Mode </summary>

public int DrawMode{ get; set; }

/// <summary> Layer Offset </summary>

public SexyPoint Offset{ get; set; }

/// <summary> Layer Spacing </summary>

public int Spacing{ get; set; }

/// <summary> Range that PointSize must follow </summary>

public Limit<int> PxRange{ get; set; }

/// <summary> Layer Point size </summary>

public int Px{ get; set; }

/// <summary> Font Ascent </summary>

public int FontAscent{ get; set; }

/// <summary> Ascent Padding </summary>

public int AscentPadding{ get; set; }

/// <summary> Layer Height </summary>

public int Height{ get; set; }

/// <summary> Default Height </summary>

public int DefaultHeight{ get; set; }

/// <summary> Line Spacing Offset </summary>

public int LineSpacingOffset{ get; set; }

/// <summary> Base order </summary>

public int BaseOrder{ get; set; }

/// <summary> Creates a new <c>FontLayer</c> </summary>

public FontLayer()
{
}

}

}