using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Char inside a Font. </summary>

public class FontChar
{
/** <summary> Gets or Sets the Char index in the Font. </summary>
<returns> The Char Index. </returns> */

public char Index{ get; set; }

/** <summary> Gets or Sets a Rectangle for the Font as a Image. </summary>
<returns> The ImageRect. </returns> */
		
public SexyRect ImageRect{ get; set; }

/** <summary> Gets or Sets the Image Offset. </summary>
<returns> The ImageOffset. </returns> */
		
public SexyPoint ImageOffset{ get; set; }

/** <summary> Gets or Sets the Offset of the First Kerning. </summary>
<returns> The FirstKerning. </returns> */

public ushort FirstKerning{ get; set; }

/** <summary> Gets or Sets the Numbers of Kernings in the Font. </summary>
<returns> The KerningsCount. </returns> */

public ushort KerningsCount{ get; set; }

/** <summary> Gets or Sets the Width of the Font Character. </summary>
<returns> The Font Width. </returns> */

public int Width{ get; set; }

/** <summary> Gets or Sets the Order of the Font Character. </summary>
<returns> The Char Order </returns> */
		
public int Order{ get; set; }

/// <summary> Creates a new <c>FontCharacter</c> </summary>

public FontChar()
{
}

// Get FontChar

public static FontChar Read(Stream reader)
{

return new()
{
Index = reader.ReadChar16(),
ImageRect = SexyRect.Read(reader),
ImageOffset = SexyPoint.Read(reader),
FirstKerning = reader.ReadUInt16(),
KerningsCount = reader.ReadUInt16(),
Width = reader.ReadInt32(),
Order = reader.ReadInt32()
};

}

/// <summary> Reads a list of Chars from a stream. </summary>

public static List<FontChar> ReadAll(Stream reader)
{
int count = reader.ReadInt32();

if(count < 0)
return null;

List<FontChar> chars = new(count);

for(uint i = 0; i < count; i++)
chars.Add(Read(reader) );	

return chars;
}

// Write FontChar

public void Write(Stream writer)
{
writer.WriteChar16(Index);

ImageRect.Write(writer);
ImageOffset.Write(writer);

writer.WriteUInt16(FirstKerning);
writer.WriteUInt16(KerningsCount);
writer.WriteInt32(Width);
writer.WriteInt32(Order);
}

// Save CharItems

public static void WriteAll(Stream writer, List<FontChar> chars)
{
int count = chars is null ? -1 : chars.Count;
writer.WriteInt32(count);

for(int i = 0; i < count; i++)
chars[i].Write(writer);	

}

}

}