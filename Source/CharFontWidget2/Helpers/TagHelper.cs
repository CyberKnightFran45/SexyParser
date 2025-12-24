using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Loads or Saves the Tags for a CFW2. </summary>

public static class TagHelper
{
// Get Tag List

public static List<string> LoadTags(Stream reader)
{
int count = reader.ReadInt32();

if(count < 0)
return null;

List<string> tags = new(count);

for(int i = 0; i < count; i++)
{
using var tOwner = reader.ReadStringByLen32();
tags.Add(tOwner.ToString() );
}

return tags;
}

// Save Tag List

public static void SaveTags(Stream writer, List<string> tags)
{
int count = tags is null ? 0 : tags.Count;
writer.WriteInt32(count);

for(int i = 0; i < count; i++)
writer.WriteStringByLen32(tags[i] );

}

}

}