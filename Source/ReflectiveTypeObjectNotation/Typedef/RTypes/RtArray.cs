using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents an Array in the RtSystem. </summary>

internal static class RtArray
{
/** <summary> Reads a RTON Array and writes it to JSON. </summary>

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

/** <summary> Reads a JSON Array and Writes it to RTON. </summary>

<param name = "reader"> The JSON reader. </param>
<param name = "writer"> The RTON writer. </param> */

internal static void Write(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
buffer.SetUInt8(pos, RTypeId.ARRAY);
pos++;

buffer.SetUInt8(pos, RTypeId.ARRAY_START);
pos++;

int elementsCount = reader.CountArrayElements();
pos += (ulong)buffer.SetVarInt(pos, elementsCount);

for(int i = 0; i < elementsCount; i++)
{
reader.ReadToken();

RtObject.EncodeJToken(reader, buffer, ref pos);
}

while(reader.CurrentTokenType != JsonTokenType.EndArray)
{

if(!reader.ReadToken() )
{
TraceLogger.WriteError("Unexpected end of JSON before EndArray.");
return;
}

}

buffer.SetUInt8(pos, RTypeId.ARRAY_END);
pos++;
}

}

}
