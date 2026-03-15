using System;
using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Converts PvZ Savefiles from Binary to JSON and viceversa </summary>

public static class SaveParser
{
// Save serializer

private static readonly PvZSaveSerializer saveSerializer = new();

// Encode json to stream

public static void EncodeStream(PvZUserdata save, Stream writer)
{
TraceLogger.WriteActionStart("Encoding data...");
saveSerializer.WriteBin(writer, save);

TraceLogger.WriteActionEnd();
}

/** <summary> Encodes a Json Save as a Binary. </summary>

<param name = "inputPath"> Save Path (must be a JSON File). </param>
<param name = "outputPath"> Location to Encoded File. </param> */

public static void EncodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("PvZUserdata Encoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".dat");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Loading Userdata...");

using var inFile = FileManager.OpenRead(inputPath);
var userdata = JsonSerializer.DeserializeObject<PvZUserdata>(inFile, PvZUserdata.Context);

TraceLogger.WriteActionEnd();

using FileStream outFile = FileManager.OpenWrite(outputPath);
EncodeStream(userdata, outFile);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("PvZUserdata Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}
	
// Get Save from BinaryStream

public static PvZUserdata DecodeStream(Stream input) 
{
TraceLogger.WriteActionStart("Decoding data...");
var userdata = saveSerializer.ReadBin(input);

TraceLogger.WriteActionEnd();

return userdata;
}

/** <summary> Decodes a Raw save as a PvZUserdata Instance. </summary>

<param name = "inputPath"> Path to Raw Save. </param>
<param name = "outputPath"> Location to Decoded Save </param> */

public static void DecodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("PvZUserdata Decoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".json");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

using FileStream inFile = FileManager.OpenRead(inputPath);
PvZUserdata userdata = DecodeStream(inFile);

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(userdata, outFile, PvZUserdata.Context);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("PvZUserdata Decoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}    

}

}