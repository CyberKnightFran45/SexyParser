using System.IO;
using System.Text.Json;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Represents an Array in the RtSystem. </summary>

public static class RtArray
{
/** <summary> Reads a RTON Array and writes it to JSON. </summary>

<param name = "reader"> The RTON Reader </param>
<param name = "writer"> The JSON Writer. </param> */

public static void Read(Stream reader, Utf8JsonWriter writer)
{
const string ERROR_MSG = "Unknown Array id at Pos {0}: {1:X2} (Expected: {2:X2})";

byte mArrayStart = reader.ReadUInt8();

if(mArrayStart != RTypeId.ARRAY_START)
{
var invalidArrStart = string.Format(ERROR_MSG, reader.Position, mArrayStart, RTypeId.ARRAY_START);

TraceLogger.WriteError(invalidArrStart);
return;
}

writer.WriteStartArray();

int elementsCount = reader.ReadVarInt();

for(int i = 0; i < elementsCount; i++)
{
byte elementType = reader.ReadUInt8();

RtObject.Decode(reader, writer, elementType);
}

byte mArrayEnd = reader.ReadUInt8();

if(mArrayEnd != RTypeId.ARRAY_END)
{
var invalidArrEnd = string.Format(ERROR_MSG, reader.Position, mArrayEnd, RTypeId.ARRAY_END);

TraceLogger.WriteError(invalidArrEnd);
return;
}

writer.WriteEndArray();
}

/** <summary> Reads a JSON Array and Writes it to RTON. </summary>

<param name = "reader"> The JSON reader. </param>
<param name = "writer"> The RTON writer. </param> */

public static void Write(NativeJsonReader reader, Stream writer)
{
writer.WriteByte(RTypeId.ARRAY);
writer.WriteByte(RTypeId.ARRAY_START);

int elementsCount = reader.CountArrayElements();
writer.WriteVarInt(elementsCount);

for(int i = 0; i < elementsCount; i++)
{
reader.ReadToken();

RtObject.EncodeValue(reader, writer);
}

while(reader.CurrentTokenType != JsonTokenType.EndArray)
{

if(!reader.ReadToken() )
{
TraceLogger.WriteError("Unexpected end of JSON before EndArray.");
return;
}

}

writer.WriteByte(RTypeId.ARRAY_END);
}

}

}