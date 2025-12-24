using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Represents a Char with fixed layout for binary serialization. </summary>

public class CharItem
{
/// <summary> Char index in the font. </summary>

public char Index{ get;  set; }

/// <summary> Char value representation. </summary>

public char Value{ get; set; }

public CharItem()
{	
}

public CharItem(char index, char value)
{
Index = index;
Value = value;
}

/// <summary> Reads a CharItem from a stream. </summary>

public static CharItem Read(Stream reader)
{
char index = reader.ReadChar16();
char val = reader.ReadChar16();

return new(index, val);
}

/// <summary> Reads a list of CharItems from a stream. </summary>

public static List<CharItem> ReadAll(Stream reader)
{
int count = reader.ReadInt32();

if(count < 0)
return null;

List<CharItem> charItems = new(count);

for(uint i = 0; i < count; i++)
charItems.Add(Read(reader) );	

return charItems;
}

/// <summary> Writes this CharItem to a stream. </summary>

public void Write(Stream writer)
{
writer.WriteChar16(Index);
writer.WriteChar16(Value);
}

// Save CharItems

public static void WriteAll(Stream writer, List<CharItem> items)
{
int count = items is null ? -1 : items.Count;
writer.WriteInt32(count);

for(int i = 0; i < count; i++)
items[i].Write(writer);	

}

}

}