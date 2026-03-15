using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> CharItem Serializer </summary>

public class CharItemSerializer : IBinarySerializer<CharItem>
{
/// <summary> Reads a CharItem from a stream </summary>

public CharItem ReadBin(Stream reader)
{
char index = reader.ReadChar16();
char val = reader.ReadChar16();

return new(index, val);
}

/// <summary> Writes a CharItem to a stream </summary>

public void WriteBin(Stream writer, CharItem item)
{
writer.WriteChar16(item.Index);
writer.WriteChar16(item.Value);
}

}

}