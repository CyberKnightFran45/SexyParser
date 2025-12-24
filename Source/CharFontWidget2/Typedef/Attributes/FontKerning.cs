using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Kerning. </summary>


public class FontKerning
{
/// <summary> Char offset in the kerning. </summary>

public ushort Offset{ get; set; }

/// <summary> Char index in the kerning. </summary>

public char Index{ get; set; }

public FontKerning()
{
}

public FontKerning(ushort offset, char index)
{
Offset = offset;
Index = index;
}

// Read FontKerning

public static FontKerning Read(Stream reader)
{
ushort offset = reader.ReadUInt16();
char index = reader.ReadChar16();

return new(offset, index);
}

// Get Kernings

public static List<FontKerning> ReadAll(Stream reader)
{
int count = reader.ReadInt32();

if(count < 0)
return null;

List<FontKerning> kernings = new(count);

for(int i = 0; i < count; i++)
kernings.Add(Read(reader) );	

return kernings;
}

// Write FontKerning

public void Write(Stream writer)
{
writer.WriteUInt16(Offset);
writer.WriteChar16(Index);
}

// Save Kernings

public static void WriteAll(Stream writer, List<FontKerning> kernings)
{
int count = kernings is null ? -1 : kernings.Count;
writer.WriteInt32(count);

for(int i = 0; i < count; i++)
kernings[i].Write(writer);	

}

}

}