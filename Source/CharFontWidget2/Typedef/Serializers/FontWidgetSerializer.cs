using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> FontWidget Serializer </summary>

public class FontWidgetSerializer : IBinarySerializer<FontWidget>
{
// Char serializer

private static readonly CharItemSerializer charSerializer = new();

// Layer serializer

private static readonly FontLayerSerializer layerSerializer = new();

// Read FontWidget from BinaryStream

public FontWidget ReadBin(Stream reader)
{

FontWidget widget = new()
{
FontAscent = reader.ReadInt32(),
AscentPadding = reader.ReadInt32(),
WidgetHeight = reader.ReadInt32(),
LineSpacingOffset = reader.ReadInt32(),
IsInitialized = reader.ReadBool(),
DefaultPx = reader.ReadInt32(),
Chars = BinaryList.ReadObjects(reader, charSerializer),
Layers = BinaryList.ReadObjects(reader, layerSerializer)
};

using var sOwner = reader.ReadStringByLen32();
widget.SourceFile = sOwner.ToString();

using var eOwner = reader.ReadStringByLen32();
widget.ErrorHeader = eOwner.ToString();

widget.Px = reader.ReadInt32();
widget.Tags = TagHelper.LoadTags(reader);
widget.Scale = reader.ReadDouble();
widget.ForceScaledImageToWhite = reader.ReadBool();
widget.ActivateAllLayers = reader.ReadBool();

return widget;
}

// Save FontWidget to BinaryStream

public void WriteBin(Stream writer, FontWidget widget)
{
writer.WriteInt32(widget.FontAscent);
writer.WriteInt32(widget.AscentPadding);
writer.WriteInt32(widget.WidgetHeight);

writer.WriteInt32(widget.LineSpacingOffset);
writer.WriteBool(widget.IsInitialized);
writer.WriteInt32(widget.DefaultPx);

BinaryList.WriteObjects(writer, widget.Chars, charSerializer);
BinaryList.WriteObjects(writer, widget.Layers, layerSerializer);

writer.WriteStringByLen32(widget.SourceFile);
writer.WriteStringByLen32(widget.ErrorHeader);
writer.WriteInt32(widget.Px);

TagHelper.SaveTags(writer, widget.Tags);

writer.WriteDouble(widget.Scale);
writer.WriteBool(widget.ForceScaledImageToWhite);
writer.WriteBool(widget.ActivateAllLayers);
}

}

}