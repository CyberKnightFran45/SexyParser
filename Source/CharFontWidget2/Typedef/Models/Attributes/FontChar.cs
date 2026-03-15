using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Char </summary>

public class FontChar
{
/// <summary> Char index </summary>

public char Index{ get; set; }

/// <summary> Rectangle for the font Image </summary>

public SexyRect ImageRect{ get; set; }

/// <summary> Image offset </summary>

public SexyPoint ImageOffset{ get; set; }

/// <summary> Offset to first Kerning </summary>

public ushort FirstKerning{ get; set; }

/// <summary> Numbers of Kernings </summary>

public ushort KerningsCount{ get; set; }

/// <summary> Char width </summary>

public int Width{ get; set; }

/// <summary> Font order </summary>

public int Order{ get; set; }

/// <summary> Creates a new <c>FontChar</c> </summary>

public FontChar()
{
}

}

}