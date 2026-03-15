using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> FontLayer Serializer </summary>

public class FontLayerSerializer : IBinarySerializer<FontLayer>
{
// Kerning serializer

private static readonly FontKerningSerializer kerningSerializer = new();

// Char serializer

private static readonly FontCharSerializer charSerializer = new();

// Get FontLayer from BinaryStream

public FontLayer ReadBin(Stream reader)
{
FontLayer layer = new();

using var nOwner = reader.ReadStringByLen32();
layer.LayerName = nOwner.ToString();

layer.RequiredTags = TagHelper.LoadTags(reader);
layer.TagsToExclude = TagHelper.LoadTags(reader);
layer.Kernings = BinaryList.ReadObjects(reader, kerningSerializer);
layer.Chars = BinaryList.ReadObjects(reader, charSerializer);
layer.ColorMul = SexyColor.Read(reader);
layer.ColorSum = SexyColor.Read(reader);

using var iOwner = reader.ReadStringByLen32();
layer.ImageFile = iOwner.ToString();

layer.DrawMode = reader.ReadInt32();
layer.Offset = SexyPoint.Read(reader);
layer.Spacing = reader.ReadInt32();

layer.PxRange = new()
{
MinValue = reader.ReadInt32(),
MaxValue = reader.ReadInt32()
};

layer.Px = reader.ReadInt32();
layer.FontAscent = reader.ReadInt32();
layer.AscentPadding = reader.ReadInt32();
layer.Height = reader.ReadInt32();
layer.DefaultHeight = reader.ReadInt32();
layer.LineSpacingOffset = reader.ReadInt32();
layer.BaseOrder = reader.ReadInt32();

return layer;
}

// Write FontLayer to BinaryStream

public void WriteBin(Stream writer, FontLayer layer)
{
writer.WriteStringByLen32(layer.LayerName);

TagHelper.SaveTags(writer, layer.RequiredTags);
TagHelper.SaveTags(writer, layer.TagsToExclude);

BinaryList.WriteObjects(writer, layer.Kernings, kerningSerializer);
BinaryList.WriteObjects(writer, layer.Chars, charSerializer);

layer.ColorMul.Write(writer);
layer.ColorSum.Write(writer);

writer.WriteStringByLen32(layer.ImageFile);
writer.WriteInt32(layer.DrawMode);

layer.Offset.Write(writer);
writer.WriteInt32(layer.Spacing);

writer.WriteInt32(layer.PxRange.MinValue);
writer.WriteInt32(layer.PxRange.MaxValue);

writer.WriteInt32(layer.Px);
writer.WriteInt32(layer.FontAscent);
writer.WriteInt32(layer.AscentPadding);
writer.WriteInt32(layer.Height);
writer.WriteInt32(layer.DefaultHeight);
writer.WriteInt32(layer.LineSpacingOffset);
writer.WriteInt32(layer.BaseOrder);
}

}

}