using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Kerning </summary>

public class FontKerning
{
/// <summary> Char offset </summary>

public ushort Offset{ get; set; }

/// <summary> Char index </summary>

public char Index{ get; set; }

// ctor

public FontKerning()
{
}

// ctor 2

public FontKerning(ushort offset, char index)
{
Offset = offset;
Index = index;
}

}

}