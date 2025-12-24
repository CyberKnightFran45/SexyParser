using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Initializes Parsing Tasks for RTON Files. </summary>

public static class RtonParser
{
/// <summary> The Header of a RTON File. </summary>

private const string HEADER = "RTON";

/// <summary> The Expected Version Number </summary>

private const uint VERSION = 1;

/// <summary> The Footer of a RTON File. </summary>

private const string FOOTER = "DONE";

// Write RTON

public static void EncodeStream(Stream input, Stream output)
{
TraceLogger.WriteActionStart("Writting header...");

output.WriteString(HEADER);
output.WriteUInt32(VERSION);

TraceLogger.WriteActionEnd();

using NativeJsonReader jsonReader = new(input);

long inputLen = input.Length;
string fileSize = SizeT.FormatSize(inputLen);

TraceLogger.WriteActionStart($"Encoding data... ({fileSize})");
RtObject.Write(jsonReader, output);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writting footer...");
output.WriteString(FOOTER);

TraceLogger.WriteActionEnd();

ReferenceStrings.Clear();
}

/** <summary> Converts a JSON File to RTON. </summary>

<param name = "inputPath"> The Path where the JSON File to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded RTON File will be Saved. </param> */

public static void EncodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("RTON Encoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".rton");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

EncodeStream(inFile, outFile);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("RTON Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Write JSON

public static void DecodeStream(Stream input, Stream output)
{
TraceLogger.WriteActionStart("Reading header...");

using var hOwner = input.ReadString(HEADER.Length);
var inputHeader = hOwner.AsSpan();

if(!inputHeader.SequenceEqual(HEADER) )
{
TraceLogger.WriteError($"Invalid header: \"{inputHeader}\", expected: \"{HEADER}\"");
return;
}

uint inputVer = input.ReadUInt32();

if(inputVer != VERSION)
TraceLogger.WriteWarn($"Unknown RTON version v{inputVer}, Expected: v{VERSION}");

TraceLogger.WriteActionEnd();

JsonWriterOptions writerCfg = new()
{
Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
Indented = true,
SkipValidation = false
};

long inputLen = input.Length - 12; // Exclude header and footer
string fileSize = SizeT.FormatSize(inputLen);

TraceLogger.WriteActionStart($"Decoding data... ({fileSize})");

using Utf8JsonWriter jsonWriter = new(output, writerCfg);
RtObject.Read(input, jsonWriter);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Reading footer...");

using var fOwner = input.ReadString(FOOTER.Length);
var inFooter = fOwner.AsSpan();

if(!inFooter.SequenceEqual(FOOTER) )
TraceLogger.WriteWarn($"Invalid footer: \"{inFooter}\", expected: \"{FOOTER}\"");

TraceLogger.WriteActionEnd();

ReferenceStrings.Clear();
}

/** <summary> Converts a RTON File to JSON. </summary>

<param name = "inputPath"> The Path where the RTON File to be Decoded is Located. </param>
<param name = "outputPath"> The Location where the Decoded JSON File will be Saved. </param> */

public static void DecodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("RTON Decoding Started");

try
{
PathHelper.ChangeExtension(ref outputPath, ".json");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

DecodeStream(inFile, outFile);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("RTON Decoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}