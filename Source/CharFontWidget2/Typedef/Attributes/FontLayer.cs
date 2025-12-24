using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Layer. </summary>

public class FontLayer
{
/** <summary> Gets or Sets the Name of the FontLayer. </summary>
<returns> The Layer Name. </returns> */

public string LayerName{ get; set; }

/** <summary> Gets or Sets a List of Tags Required for this Layer. </summary>
<returns> The RequiredTags. </returns> */

public List<string> RequiredTags{ get; set; }

/** <summary> Gets or Sets the Tags to Exclude from this Layer. </summary>
<returns> The TagsToExclude. </returns> */

public List<string> TagsToExclude{ get; set; }

/** <summary> Gets or Sets the FontKernings for this Layer. </summary>
<returns> The FontKernings. </returns> */

public List<FontKerning> Kernings{ get; set; }

/** <summary> Gets or Sets the FontCharacters for this FontLayer. </summary>
<returns> The FontCharacters. </returns> */

public List<FontChar> Chars{ get; set; }

/** <summary> Gets or Sets the Color Multiples for this Layer </summary>
<returns> The ColorMultiples. </returns> */

public SexyColor ColorMul{ get; set; }

/** <summary> Gets or Sets the Color Addends for this Layer </summary>
<returns> The ColorAddends. </returns> */

public SexyColor ColorSum{ get; set; }

/** <summary> Gets or Sets the Image File for this Layer </summary>
<returns> The ImageFile. </returns> */

public string ImageFile{ get; set; }

/** <summary> Gets or Sets the Draw Mode for this Layer </summary>
<returns> The DrawMode. </returns> */

public int DrawMode{ get; set; }

/** <summary> Gets or Sets the Layer Offset. </summary>
<returns> The LayerOffset. </returns> */

public SexyPoint Offset{ get; set; }

/** <summary> Gets or Sets the Layer Spacing. </summary>
<returns> The Layer Spacing. </returns> */

public int Spacing{ get; set; }

/** <summary> Gets or Sets a Range that PointSize must follow on this Layer. </summary>
<returns> The PointSize Range. </returns> */

public Limit<int> PxRange{ get; set; }

/** <summary> Gets or Sets the Point Size of this Layer. </summary>
<returns> The PointSize. </returns> */

public int Px{ get; set; }

/** <summary> Gets or Sets Ascent of this FontLayer. </summary>
<returns> The Font Ascent. </returns> */

public int FontAscent{ get; set; }

/** <summary> Gets or Sets Ascent Padding of this Layer. </summary>
<returns> The AscentPadding. </returns> */

public int AscentPadding{ get; set; }

/** <summary> Gets or Sets Height of this FontLayer. </summary>
<returns> The Layer Height. </returns> */

public int Height{ get; set; }

/** <summary> Gets or Sets default Height of this Layer. </summary>
<returns> The DefaultHeight. </returns> */

public int DefaultHeight{ get; set; }

/** <summary> Gets or Sets LineSpacing Offset of this Layer. </summary>
<returns> The LineSpacingOffset. </returns> */

public int LineSpacingOffset{ get; set; }
		
/** <summary> Gets or Sets base Order of this Layer. </summary>
<returns> The BaseOrder. </returns> */

public int BaseOrder{ get; set; }

/// <summary> Creates a new <c>FontLayer</c> </summary>

public FontLayer()
{
}

// Get FontLayer from BinaryStream

public static FontLayer Read(Stream reader)
{
FontLayer layer = new();

using var nOwner = reader.ReadStringByLen32();
layer.LayerName = nOwner.ToString();

layer.RequiredTags = TagHelper.LoadTags(reader);
layer.TagsToExclude = TagHelper.LoadTags(reader);
layer.Kernings = FontKerning.ReadAll(reader);
layer.Chars = FontChar.ReadAll(reader);
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

// Get FontLayers

public static List<FontLayer> ReadAll(Stream reader)
{
int count = reader.ReadInt32();

if(count < 0)
return null;

List<FontLayer> layers = new(count);

for(int i = 0; i < count; i++)
layers.Add(Read(reader) );	

return layers;
}

// Write FontLayer to BinaryStream

public void Write(Stream writer)
{
writer.WriteStringByLen32(LayerName);

TagHelper.SaveTags(writer, RequiredTags);
TagHelper.SaveTags(writer, TagsToExclude);
FontKerning.WriteAll(writer, Kernings);
FontChar.WriteAll(writer, Chars);

ColorMul.Write(writer);
ColorSum.Write(writer);

writer.WriteStringByLen32(ImageFile);
writer.WriteInt32(DrawMode);

Offset.Write(writer);
writer.WriteInt32(Spacing);

writer.WriteInt32(PxRange.MinValue);
writer.WriteInt32(PxRange.MaxValue);

writer.WriteInt32(Px);
writer.WriteInt32(FontAscent);
writer.WriteInt32(AscentPadding);
writer.WriteInt32(Height);
writer.WriteInt32(DefaultHeight);
writer.WriteInt32(LineSpacingOffset);
writer.WriteInt32(BaseOrder);
}

// Save FontLayers

public static void WriteAll(Stream writer, List<FontLayer> layers)
{
int count = layers is null ? -1 : layers.Count;
writer.WriteInt32(count);

for(int i = 0; i < count; i++)
layers[i].Write(writer);	

}

}

}