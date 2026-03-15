using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using SexyCryptor;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Supports parsing RTON files from PvZ 2 and other games </summary>

public static class RtonParser
{
/// <summary> Rton header </summary>

private const uint HEADER = 0x52544F4E;

/// <summary> Expected version </summary>

private const uint VERSION = 1;

/// <summary> Rton footer </summary>

private const uint FOOTER = 0x444F4E45;

// Encode core

private static NativeBuffer EncodeCore(NativeJsonReader reader)
{
NativeBuffer buffer = new(SizeT.ONE_MEGABYTE * 64);
ulong pos = 0;

TraceLogger.WriteActionStart("Writting header...");

buffer.SetUInt32(pos, HEADER, Endianness.BigEndian);
pos += 4;

buffer.SetUInt32(pos, VERSION);
pos += 4;

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encoding data...");
RtObject.Write(reader, buffer, ref pos);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writting footer...");

buffer.SetUInt32(pos, FOOTER, Endianness.BigEndian);
pos += 4;

TraceLogger.WriteActionEnd();

buffer.Realloc(pos);
ReferenceStrings.Clear();

return buffer;
}

// Encode stream

public static void EncodeStream(Stream input, Stream output, bool useEncryption = false)
{
using NativeJsonReader jsonReader = new(input);
using var rtonBuffer = EncodeCore(jsonReader);

if(useEncryption)
{
TraceLogger.WriteLine("============ RTON Encryption Started ============");

using ChunkedMemoryStream temp = new();

temp.Write(rtonBuffer.AsSpan() );
temp.Seek(0, SeekOrigin.Begin);

CRton.EncryptStream(temp, output);

TraceLogger.WriteLine("============ RTON Encryption Finished ============");
}

else
output.Write(rtonBuffer.AsSpan() );

}

/** <summary> Converts a JSON File to RTON. </summary>

<param name = "inputPath"> The Path where the JSON File to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded RTON File will be Saved. </param> */

public static void EncodeFile(string inputPath, string outputPath, bool useEncryption)
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

EncodeStream(inFile, outFile, useEncryption);
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("RTON Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Decode core

private static void DecodeCore(NativeBuffer buffer, Utf8JsonWriter writer)
{
TraceLogger.WriteActionStart("Reading header...");

ulong pos = 0;

uint inputMagic = buffer.GetUInt32(pos, Endianness.BigEndian);
pos += 4;

if(inputMagic != HEADER)
{
TraceLogger.WriteError($"Invalid Rton Identifier: {inputMagic:X8}, expected: {HEADER:X8}");
return;
}

uint inputVer = buffer.GetUInt32(pos);
pos += 4;

if(inputVer != VERSION)
TraceLogger.WriteWarn($"Unknown RTON version: v{inputVer}, expected: v{VERSION}");

TraceLogger.WriteActionEnd();

var inputLen = (long)buffer.Size - 12; // Exclude header and footer
string fileSize = SizeT.FormatSize(inputLen);

TraceLogger.WriteActionStart($"Decoding data... ({fileSize})");
RtObject.Read(buffer, ref pos, writer);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Reading footer...");

uint inFooter = buffer.GetUInt32(pos, Endianness.BigEndian);
pos += 4;

if(inFooter != FOOTER)
TraceLogger.WriteWarn($"Invalid footer: {inFooter:X8}, expected: {FOOTER:X8}");

TraceLogger.WriteActionEnd();

ReferenceStrings.Clear();
}

// Write JSON

public static void DecodeStream(Stream input, Stream output)
{
ushort encryptionFlags = input.ReadUInt16();
input.Seek(0, SeekOrigin.Begin); // Peek

NativeBuffer rawBuffer;

if(encryptionFlags == 0x10)
{
TraceLogger.WriteActionEnd();

TraceLogger.WriteLine("============ RTON Decryption Started ============");

using ChunkedMemoryStream temp = new();
CRton.DecryptStream(input, temp);

temp.Seek(0, SeekOrigin.Begin);

rawBuffer = temp.ReadPtr();

TraceLogger.WriteLine("============ RTON Decryption Finished ============");
}

else
rawBuffer = input.ReadPtr();

JsonWriterOptions writerCfg = new()
{
Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
Indented = true,
SkipValidation = false
};

using Utf8JsonWriter jsonWriter = new(output, writerCfg);
DecodeCore(rawBuffer, jsonWriter);

rawBuffer.Dispose();
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