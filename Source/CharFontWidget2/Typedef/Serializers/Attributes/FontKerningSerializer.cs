using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> FontKerning Serializer </summary>

public class FontKerningSerializer : IBinarySerializer<FontKerning>
{
/// <summary> Reads a CharItem from a stream </summary>

public FontKerning ReadBin(Stream reader)
{
ushort offset = reader.ReadUInt16();
char index = reader.ReadChar16();

return new(offset, index);
}

/// <summary> Writes a CharItem to a stream </summary>

public void WriteBin(Stream writer, FontKerning kerning)
{
writer.WriteUInt16(kerning.Offset);
writer.WriteChar16(kerning.Index);
}

}

}