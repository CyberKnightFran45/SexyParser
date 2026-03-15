using System;
using System.IO;
using PopCapResManager;

namespace SexyParsers.Newton
{
/// <summary> Initializes Parsing Tasks for NEWTON Files </summary>

public static class NewtonParser
{
// SubGroup serializer

private static readonly ResGroupSerializer resSerializer = new();

/** <summary> Converts a ResGroup to NEWTON. </summary>

<param name = "inputPath"> The Path where the ResGroup to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded NEWTON File will be Saved. </param> */

public static void Encode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("NewRes Encoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".newton");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");	

TraceLogger.WriteActionStart("Loading ResGroup...");

using var inFile = FileManager.OpenRead(inputPath);
var resGroup = JsonSerializer.DeserializeObject<MResGroup>(inFile, MResGroup.Context);

TraceLogger.WriteActionEnd();

using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionStart("Encoding data...");
resSerializer.WriteBin(outFile, resGroup);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("Newton Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

/** <summary> Converts a NEWTON Stream to a Json ResGroup </summary>

<param name = "inputPath"> The Path where the NEWTON File to Encode is Located. </param>
<param name = "outputPath"> The Location where the ResGroup File will be Saved. </param> */

public static void Decode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("NewRes Decoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".json");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

using FileStream inFile = FileManager.OpenRead(inputPath);

TraceLogger.WriteActionStart("Decoding data...");
var resGroup = resSerializer.ReadBin(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving json...");

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(resGroup, outFile, MResGroup.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("Newton Decoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}