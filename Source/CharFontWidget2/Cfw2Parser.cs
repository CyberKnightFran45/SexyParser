using System;
using System.IO;

namespace SexyParsers.CharFontWidget2
{
/// <summary> Initializes Parsing Tasks for CFW2 Files. </summary>

public static class Cfw2Parser
{
/// <summary> The Expected Version Number (Major and Minor) </summary>

private const long VERSION = 0;

// Get CFW2 Stream

public static void EncodeStream(FontWidget widget, Stream writer)
{
TraceLogger.WriteActionStart("Writting header...");

writer.WriteInt64(VERSION);
writer.WriteInt64(VERSION);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encoding data...");
widget.WriteBin(writer);

TraceLogger.WriteActionEnd();
}

/** <summary> Encodes a Json FontWidget as a CFW2 File. </summary>

<param name = "inputPath"> The Path to the FontWidget (must be a JSON File). </param>
<param name = "outputPath"> The Location where the Encoded File will be Saved. </param> */

public static void EncodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("Cfw2 Encoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".cfw2");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Loading Font data...");

using var inFile = FileManager.OpenRead(inputPath);
var widget = JsonSerializer.DeserializeObject<FontWidget>(inFile, FontWidget.Context);

TraceLogger.WriteActionEnd();

using FileStream outFile = FileManager.OpenWrite(outputPath);
EncodeStream(widget, outFile);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("Cfw2 Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}
	
// Get FontWidget from BinaryStream

public static FontWidget DecodeStream(Stream input) 
{
TraceLogger.WriteActionStart("Reading header...");

long mMajVer = input.ReadInt64();
long mMinVer = input.ReadInt64();

TraceLogger.WriteActionEnd();

if(mMajVer != VERSION || mMinVer != VERSION)
TraceLogger.WriteWarn($"Unknown version: v{mMajVer}.{mMinVer} - Expected: v{VERSION}.{VERSION}");

TraceLogger.WriteActionStart("Decoding data...");
var widget = FontWidget.ReadBin(input);

TraceLogger.WriteActionEnd();

return widget;
}

/** <summary> Decodes a CFW2 File as a FontWidget Instance. </summary>

<param name = "inputPath"> The Path to the CFW2 File. </param>
<param name = "outputPath"> The Location where the Decoded File will be Saved. </param> */

public static void DecodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("Cfw2 Decoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".json");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

using FileStream inFile = FileManager.OpenRead(inputPath);
FontWidget widget = DecodeStream(inFile);

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(widget, outFile, FontWidget.Context);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("Cfw2 Decoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}    

}

}