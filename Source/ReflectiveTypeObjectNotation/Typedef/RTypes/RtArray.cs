using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents an Array in the RtSystem. </summary>

internal static class RtArray
{
/** <summary> Reads a RTON Array and writes it to its JSON equivalent </summary>

<param name = "buffer"> The RTON Reader </param>
<param name = "writer"> The JSON Writer. </param> */

internal static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer)
{
byte mArrayStart = buffer.GetUInt8(pos);
pos++;

if(mArrayStart != RTypeId.ARRAY_START)
{
TraceLogger.WriteError($"Unknown Array id: {mArrayStart:X2} @ {pos} (Expected: {RTypeId.ARRAY_START:X2})");
return;
}

writer.WriteStartArray();

int elementsCount = buffer.GetVarInt(pos, out int varLen);
pos += (ulong)varLen;

for(int i = 0; i < elementsCount; i++)
{
byte elementType = buffer.GetUInt8(pos);
pos++;

RtObject.DecodeRToken(buffer, ref pos, writer, elementType);
}

byte mArrayEnd = buffer.GetUInt8(pos);
pos++;

if(mArrayEnd != RTypeId.ARRAY_END)
{
TraceLogger.WriteError($"Invalid Array end: {mArrayEnd:X2} @ {pos} (Expected: {RTypeId.ARRAY_END:X2})");
return;
}

writer.WriteEndArray();
}

// Encode json array

private static int EncodeJArray(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
int count = 0;

while(reader.ReadToken() )
{

switch(reader.CurrentTokenType)
{
case JsonTokenType.EndArray:
return count;

default:
RtObject.EncodeJToken(reader, buffer, ref pos);

count++;
break;
}

}

TraceLogger.WriteError("Unexpected end of JSON before EndArray.");

return count;
}

/** <summary> Reads a JSON Array and Writes its equivalent as RTON </summary>

<param name = "reader"> The JSON reader. </param>
<param name = "writer"> The RTON writer. </param> */

internal static void Write(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.ARRAY);
pos++;

buffer.SetUInt8(pos, RTypeId.ARRAY_START);
pos++;

ulong arrayPos = 0;
using NativeBuffer rawArray = new(SizeT.ONE_MEGABYTE * 16);

int elementsCount = EncodeJArray(reader, rawArray, ref arrayPos);
pos += (ulong)buffer.SetVarInt(pos, elementsCount);

buffer.CopyFrom(rawArray, 0, pos, arrayPos);
pos += arrayPos;

buffer.SetUInt8(pos, RTypeId.ARRAY_END);
pos++;
}

}

}
