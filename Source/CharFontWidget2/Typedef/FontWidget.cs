using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Font Widget. </summary>

public class FontWidget
{
/** <summary> Gets or Sets Ascent of this FontWidget. </summary>
<returns> The Font Ascent. </returns> */

public int FontAscent{ get; set; }

/** <summary> Gets or Sets Ascent Padding of this Widget. </summary>
<returns> The AscentPadding. </returns> */

public int AscentPadding{ get; set; }

/** <summary> Gets or Sets the Height of this FontWidget. </summary>
<returns> The Widget Height. </returns> */

public int WidgetHeight{ get; set; }

/** <summary> Gets or Sets LineSpacing Offset of this Widget. </summary>
<returns> The LineSpacingOffset. </returns> */

public int LineSpacingOffset{ get; set; }

/** <summary> Determines if this Widget was Initialized or not. </summary>
<returns> <b>true</b> or <b>false</b> </returns> */

public bool IsInitialized{ get; set; }

/** <summary> Gets or Sets a default PointSize for this Widget. </summary>
<returns> The Default PointSize. </returns> */

public int DefaultPx{ get; set; }

/** <summary> Gets or Sets the Characters for this FontWidget. </summary>
<returns> A Collection of CharItems. </returns> */

public List<CharItem> Chars{ get; set; }

/** <summary> Gets or Sets the Layers of this Widget. </summary>
<returns> The FontLayers. </returns> */

public List<FontLayer> Layers{ get; set; }

/** <summary> Gets or Sets the source File for this FontWidget. </summary>
<returns> The SourceFile. </returns> */

public string SourceFile{ get; set; }

/// <summary> Unknown Field, don't know its Purpose. </summary>

public string ErrorHeader{ get; set; }

/** <summary> Gets or Sets the Point Size of this Widget. </summary>
<returns> The PointSize. </returns> */

public int Px{ get; set; }

/** <summary> Gets or Sets a List of Tags for this Widget. </summary>
<returns> The Collection of Tags. </returns> */

public List<string> Tags{ get; set; }

/** <summary> Gets or Sets the Scale of this Widget. </summary>
<returns> The Widget Scale. </returns> */

public double Scale{ get; set; }

/** <summary> Wheter to Force ScaledImages to be White. </summary>
<returns> <b>true</b> or <b>false</b>. </returns> */

public bool ForceScaledImageToWhite{ get; set; }

/** <summary> Wheter to Activate all Layers of this Widget. </summary>
<returns> <b>true</b> or <b>false</b>. </returns> */

public bool ActivateAllLayers{ get; set; }

/// <summary> Creates a new <c>FontWidget</c> </summary>

public FontWidget()
{
}

public static readonly JsonSerializerContext Context = new Cfw2Context(JsonSerializer.Options);

// Get FontWidget from BinaryStream

public static FontWidget ReadBin(Stream reader)
{

FontWidget widget = new()
{
FontAscent = reader.ReadInt32(),
AscentPadding = reader.ReadInt32(),
WidgetHeight = reader.ReadInt32(),
LineSpacingOffset = reader.ReadInt32(),
IsInitialized = reader.ReadBool(),
DefaultPx = reader.ReadInt32(),
Chars = CharItem.ReadAll(reader),
Layers = FontLayer.ReadAll(reader)
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

public void WriteBin(Stream writer)
{
writer.WriteInt32(FontAscent);
writer.WriteInt32(AscentPadding);
writer.WriteInt32(WidgetHeight);
writer.WriteInt32(LineSpacingOffset);
writer.WriteBool(IsInitialized);
writer.WriteInt32(DefaultPx);

CharItem.WriteAll(writer, Chars);
FontLayer.WriteAll(writer, Layers);

writer.WriteStringByLen32(SourceFile);
writer.WriteStringByLen32(ErrorHeader);
writer.WriteInt32(Px);

TagHelper.SaveTags(writer, Tags);

writer.WriteDouble(Scale);
writer.WriteBool(ForceScaledImageToWhite);
writer.WriteBool(ActivateAllLayers);
}

}

// Context for serialization

[JsonSerializable(typeof(CharItem))]
[JsonSerializable(typeof(List<CharItem>) ) ]

[JsonSerializable(typeof(FontKerning) ) ]
[JsonSerializable(typeof(List<FontKerning>) ) ]

[JsonSerializable(typeof(SexyRect) ) ]
[JsonSerializable(typeof(SexyPoint) ) ]

[JsonSerializable(typeof(FontChar) ) ]
[JsonSerializable(typeof(List<FontChar>) ) ]
[JsonSerializable(typeof(SexyColor) ) ]
[JsonSerializable(typeof(Limit<int>) ) ]

[JsonSerializable(typeof(FontLayer) ) ]
[JsonSerializable(typeof(List<FontLayer>) ) ]

[JsonSerializable(typeof(FontWidget) ) ]

public partial class Cfw2Context : JsonSerializerContext
{
}

}