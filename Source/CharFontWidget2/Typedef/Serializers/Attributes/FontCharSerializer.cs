using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> FontChar Serializer </summary>

public class FontCharSerializer : IBinarySerializer<FontChar>
{
// Read FontChar

public FontChar ReadBin(Stream reader)
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

// Write FontChar

public void WriteBin(Stream writer, FontChar c)
{
writer.WriteChar16(c.Index);

c.ImageRect.Write(writer);
c.ImageOffset.Write(writer);

writer.WriteUInt16(c.FirstKerning);
writer.WriteUInt16(c.KerningsCount);
writer.WriteInt32(c.Width);
writer.WriteInt32(c.Order);
}

}

}