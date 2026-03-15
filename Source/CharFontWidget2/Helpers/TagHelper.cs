using System.Collections.Generic;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Loads or Saves the Tags for a CFW2 </summary>

public static class TagHelper
{
// Repeat flags

private const RepeatCountFlags REPEAT_FLAGS = RepeatCountFlags.UInt32;

// String flags

private const StrLenType STR_FLAGS = StrLenType.UInt32;

// Get Tag List

public static List<string> LoadTags(Stream reader) => BinaryList.ReadStrings(reader, REPEAT_FLAGS, STR_FLAGS);

// Save Tag List

public static void SaveTags(Stream writer, List<string> tags) => BinaryList.WriteStrings(writer, tags, REPEAT_FLAGS, STR_FLAGS);
}

}