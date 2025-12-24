using System.IO;
using System.Text.Json;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Represents an Object in the RtSystem. </summary>

public static class RtObject
{
/** <summary> Encodes a JSON object and writes its Tokens as RTON. </summary>

<param name = "reader"> The JSON Reader. </param>
<param name = "writer"> The RTON writer. </param> */

private static void Encode(NativeJsonReader reader, Stream writer)
{

while(reader.ReadToken() )
{

if(reader.CurrentTokenType == JsonTokenType.EndObject)
break;

if(reader.CurrentTokenType != JsonTokenType.PropertyName)
throw new JsonException("Expected property name");

RtNativeString.Write(writer, reader.CurrentPropertyName);

if(!reader.ReadToken() )
throw new JsonException("Unexpected end after property");

EncodeValue(reader, writer);
}
    
writer.WriteByte(RTypeId.OBJECT_END);
}

// Encode json token

public static void EncodeValue(NativeJsonReader reader, Stream writer)
{

switch(reader.CurrentTokenType)
{
case JsonTokenType.StartObject:
writer.WriteByte(RTypeId.OBJECT_START);

Encode(reader, writer);
break;

case JsonTokenType.StartArray:
RtArray.Write(reader, writer);
break;

case JsonTokenType.String:
RtString.Encode(reader, writer);
break;

case JsonTokenType.Number:
RtNumber.Write(reader, writer);
break;

case JsonTokenType.True:
writer.WriteByte(RTypeId.BOOLEAN_TRUE);
break;

case JsonTokenType.False:
writer.WriteByte(RTypeId.BOOLEAN_FALSE);
break;

default: // Ignore comments and null tokens
break;
}

}


/** <summary> Reads a root JSON and writes its contents to RTON. </summary>

<param name = "reader"> The JSON Reader. </param>
<param name = "writer"> The RTON writer. </param> */

public static void Write(NativeJsonReader reader, Stream writer)
{
reader.ReadToken(); // Root element (ignore)

if(reader.CurrentTokenType != JsonTokenType.StartObject)
{
TraceLogger.WriteError("JSON root must be an object");
return;
}

Encode(reader, writer);
}

/** <summary> Decodes an RTON Object and writes it to JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON Writer. </param>
<param name = "type"> The Value Type. </param> */

public static void Decode(Stream reader, Utf8JsonWriter writer, byte flags)
{
const string ERROR_MSG = "Unknown RtType id at Pos {0}: {1:X2}";

switch(flags)
{
case RTypeId.BOOLEAN_FALSE:
writer.WriteBooleanValue(false);
break;

case RTypeId.BOOLEAN_TRUE:
writer.WriteBooleanValue(true);
break;

case RTypeId.INT8:
writer.WriteNumberValue(reader.ReadInt8() );
break;

case RTypeId.UINT8:
writer.WriteNumberValue(reader.ReadByte() );
break;

case RTypeId.INT16:
writer.WriteNumberValue(reader.ReadInt16() );
break;

case RTypeId.UINT16:
writer.WriteNumberValue(reader.ReadUInt16() );
break;

case RTypeId.INT32:
writer.WriteNumberValue(reader.ReadInt32() );
break;

case RTypeId.UINT32:
writer.WriteNumberValue(reader.ReadUInt32() );
break;

case RTypeId.INT64:
writer.WriteNumberValue(reader.ReadInt64() );
break;

case RTypeId.UINT64:
writer.WriteNumberValue(reader.ReadUInt64() );
break;

case RTypeId.FLOAT32:
writer.WriteNumberValue(reader.ReadFloat() );
break;

case RTypeId.FLOAT64:
writer.WriteNumberValue(reader.ReadDouble() );
break;

case RTypeId.VAR_INT32:
writer.WriteNumberValue(reader.ReadVarInt() );
break;

case RTypeId.VAR_UINT32:
writer.WriteNumberValue(reader.ReadVarUInt() );
break;

case RTypeId.ZIGZAG32:
writer.WriteNumberValue(reader.ReadZigZag() );
break;

case RTypeId.VAR_INT64:
writer.WriteNumberValue(reader.ReadVarInt64() );
break;

case RTypeId.VAR_UINT64:
writer.WriteNumberValue(reader.ReadVarUInt64() );
break;

case RTypeId.ZIGZAG64:
writer.WriteNumberValue(reader.ReadZigZag64() );
break;

case RTypeId.NATIVE_STRING:
RtNativeString.Read(reader, writer, false);
break;

case RTypeId.UNICODE_STRING:
RtUnicodeString.Read(reader, writer, false);
break;

case RTypeId.ID_STRING:
RtidString.Read(reader, writer, false, false);
break;

case RTypeId.ID_STRING_NULL:
RtidString.Read(reader, writer, false, true);
break;

case RTypeId.OBJECT_START:
Read(reader, writer);
break;

case RTypeId.ARRAY:
RtArray.Read(reader, writer);
break;

case RTypeId.NATIVE_STRING_VALUE:
RtNativeString.ReadCached(reader, writer, false, false);
break;

case RTypeId.NATIVE_STRING_INDEX:
RtNativeString.ReadCached(reader, writer, true, false);
break;

case RTypeId.UNICODE_STRING_VALUE:
RtUnicodeString.ReadCached(reader, writer, false, false);
break;

case RTypeId.UNICODE_STRING_INDEX:
RtUnicodeString.ReadCached(reader, writer, true, false);
break;

case RTypeId.INT8_ZERO:
case RTypeId.UINT8_ZERO:
case RTypeId.INT16_ZERO:
case RTypeId.UINT16_ZERO:
case RTypeId.INT32_ZERO:
case RTypeId.UINT32_ZERO:
case RTypeId.INT64_ZERO:

case RTypeId.UINT64_ZERO:
writer.WriteNumberValue(0);
break;

case RTypeId.FLOAT32_ZERO:

case RTypeId.FLOAT64_ZERO:
writer.WriteNumberValue(0.0);
break;

default:
var unkRtype = string.Format(ERROR_MSG, reader.Position, flags);

TraceLogger.WriteError(unkRtype);
return;
}

}

/** <summary> Reads an RTON Object and Writes it as JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param> */

public static void Read(Stream reader, Utf8JsonWriter writer)
{
writer.WriteStartObject();

while(true)
{
byte keyFlags = reader.ReadUInt8();

if(keyFlags == RTypeId.OBJECT_END)
break;

switch(keyFlags)
{
case RTypeId.NATIVE_STRING:
RtNativeString.Read(reader, writer, true);
break;

case RTypeId.UNICODE_STRING:
RtUnicodeString.Read(reader, writer, true);
break;

case RTypeId.ID_STRING:
RtidString.Read(reader, writer, true, false);
break;

case RTypeId.ID_STRING_NULL:
RtidString.Read(reader, writer, true, true);
break;

case RTypeId.NATIVE_STRING_VALUE:
RtNativeString.ReadCached(reader, writer, false, true);
break;

case RTypeId.NATIVE_STRING_INDEX:
RtNativeString.ReadCached(reader, writer, true, true);
break;

case RTypeId.UNICODE_STRING_VALUE:
RtUnicodeString.ReadCached(reader, writer, false, true);
break;

case RTypeId.UNICODE_STRING_INDEX:
RtUnicodeString.ReadCached(reader, writer, true, true);
break;

default:
TraceLogger.WriteError($"Unknown key type in RtObject: {keyFlags:X2}, at Pos: {reader.Position}");
return;
}

byte valueFlags = reader.ReadUInt8();

Decode(reader, writer, valueFlags);
}

writer.WriteEndObject();
}

}

}