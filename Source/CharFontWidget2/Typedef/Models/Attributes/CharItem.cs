using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Char with fixed layout for binary serialization </summary>

public class CharItem
{
/// <summary> Char index in the font </summary>

public char Index{ get;  set; }

/// <summary> Char representation </summary>

public char Value{ get; set; }

// ctor

public CharItem()
{	
}

// ctor 2

public CharItem(char index, char val)
{
Index = index;
Value = val;
}

}

}