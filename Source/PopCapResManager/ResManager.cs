using System;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a Resource Manager for PopCap games. </summary>

public static class ResManager
{
// Convert OldRes (ResInfo) to New (ResGroup)

public static void ConvertToGroup(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("ResInfo Conversion Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Loading ResInfo...");

using var inFile = FileManager.OpenRead(inputPath);
var resInfo = JsonSerializer.DeserializeObject<ResInfo>(inFile, ResInfo.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Converting ResInfo to ResGroup...");
var resGroup = MResGroup.FromResInfo(resInfo);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving ResGroup...");

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(resGroup, outFile, MResGroup.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Convert ResInfo");
}

TraceLogger.WriteLine("ResInfo Conversion Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Convert NewRes (ResGroup) to Old (ResInfo)

public static void ConvertToInfo(string inputPath, string outputPath, PathType pStyle)
{
TraceLogger.Init();
TraceLogger.WriteLine("ResGroup Conversion Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Loading ResGroup...");

using var inFile = FileManager.OpenRead(inputPath);
var resGroup = JsonSerializer.DeserializeObject<ResGroup>(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Converting ResGroup to ResInfo...");
var resInfo = ResInfo.FromResGroup(resGroup, pStyle);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving ResInfo...");

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(resInfo, outFile, ResInfo.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("ResGroup Conversion Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Split Res into smaller files

public static void Split(string inputPath, bool useNewRes)
{
TraceLogger.Init();
TraceLogger.WriteLine("Res Split Started");

try
{
string outputDir = inputPath;
PathHelper.AddExtension(ref outputDir, ".split");

TraceLogger.WriteDebug($"{inputPath} --> {outputDir}");

TraceLogger.WriteActionStart("Opening files...");
using var inFile = FileManager.OpenRead(inputPath);

TraceLogger.WriteActionEnd();

if(useNewRes)
{
TraceLogger.WriteActionStart("Loading ResGroup...");
var newRes = JsonSerializer.DeserializeObject<MResGroup>(inFile, MResGroup.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Spliting info...");
newRes.Split(outputDir);
}

else
{
TraceLogger.WriteActionStart("Loading ResInfo...");
var oldRes = JsonSerializer.DeserializeObject<ResInfo>(inFile, ResInfo.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Spliting info...");
oldRes.Split(outputDir);
}

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Split file");
}

TraceLogger.WriteLine("Res Split Finished");
}

// Merge res

public static void Merge(string inputDir, bool useNewRes)
{
TraceLogger.Init();
TraceLogger.WriteLine("Res Merge Started");

try
{
string outputPath = inputDir;
PathHelper.ChangeExtension(ref outputPath, ".json");

TraceLogger.WriteDebug($"{inputDir} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

if(useNewRes)
{
TraceLogger.WriteActionStart("Merging info...");

var newRes = MResGroup.Merge(inputDir);
TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving json...");
JsonSerializer.SerializeObject(newRes, outFile, MResGroup.Context);
}

else
{
TraceLogger.WriteActionStart("Merging info...");

var oldRes = ResInfo.Merge(inputDir);
TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving json...");
JsonSerializer.SerializeObject(oldRes, outFile, ResInfo.Context);
}

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Merge files");
}

TraceLogger.WriteLine("Res Merge Finished");
}

}

}