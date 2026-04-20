using System.Text.Json;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Represents an Object in the RtSystem. </summary>

internal static class RtObject
{
// Encode json token

internal static void EncodeJToken(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{

switch(reader.CurrentTokenType)
{
case JsonTokenType.StartObject:

buffer.SetUInt8(pos, RTypeId.OBJECT_START);
pos++;

EncodeJObject(reader, buffer, ref pos);
break;

case JsonTokenType.StartArray:
RtArray.Write(reader, buffer, ref pos);
break;

case JsonTokenType.String:
RtString.WriteRton(reader, buffer, ref pos);
break;

case JsonTokenType.Number:
RtNumber.Write(reader, buffer, ref pos);
break;

case JsonTokenType.True:
buffer.SetUInt8(pos, RTypeId.BOOLEAN_TRUE);

pos++;
break;

case JsonTokenType.False:
buffer.SetUInt8(pos, RTypeId.BOOLEAN_FALSE);

pos++;
break;

default: // Ignore comments and null tokens
break;
}

}

// Encode JSON object and writes its Tokens as RTON </summary>

private static void EncodeJObject(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{

while(reader.ReadToken() )
{

if(reader.CurrentTokenType == JsonTokenType.EndObject)
break;

if(reader.CurrentTokenType != JsonTokenType.PropertyName)
throw new JsonException("Expected property name");

RtNativeString.Write(reader.CurrentPropertyName, buffer, ref pos);

if(!reader.ReadToken() )
throw new JsonException("Unexpected end after property");

EncodeJToken(reader, buffer, ref pos);
}
    
buffer.SetUInt8(pos, RTypeId.OBJECT_END);
pos++;
}

/** <summary> Reads a root JSON and writes its contents to RTON. </summary>

<param name = "reader"> The JSON Reader. </param>
<param name = "buffer"> The RTON Buffer. </param> */

internal static void Write(NativeJsonReader reader, NativeBuffer buffer, ref ulong pos)
{
reader.ReadToken(); // First element (ignore json root, parse if Array)

switch(reader.CurrentTokenType)
{
case JsonTokenType.StartObject:
EncodeJObject(reader, buffer, ref pos);
break;

case JsonTokenType.StartArray:
RtArray.Write(reader, buffer, ref pos);
break;

default:
TraceLogger.WriteError("JSON root must be an object or array");
return;
}

}

// Decode Rton property name

private static void DecodeRProp(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, byte flags)
{

switch(flags)
{
case RTypeId.NATIVE_STRING:
RtNativeString.Read(buffer, ref pos, writer, true);
break;

case RTypeId.UNICODE_STRING:
RtUnicodeString.Read(buffer, ref pos, writer, true);
break;

case RTypeId.ID_STRING:
RtIdString.Read(buffer, ref pos, writer, true, false);
break;

case RTypeId.ID_STRING_NULL:
RtIdString.Read(buffer, ref pos, writer, true, true);
break;

case RTypeId.NATIVE_STRING_VALUE:
RtNativeString.ReadCached(buffer, ref pos, writer, false, true);
break;

case RTypeId.NATIVE_STRING_INDEX:
RtNativeString.ReadCached(buffer, ref pos, writer, true, true);
break;

case RTypeId.UNICODE_STRING_VALUE:
RtUnicodeString.ReadCached(buffer, ref pos, writer, false, true);
break;

case RTypeId.UNICODE_STRING_INDEX:
RtUnicodeString.ReadCached(buffer, ref pos, writer, true, true);
break;

default:
TraceLogger.WriteError($"Unknown RType identifier for property: {flags:X2} @ {pos}");
return;
}

}

/** <summary> Decodes RTON Token and write its JSON equivalent. </summary>

<param name = "buffer"> RTON buffer. </param>
<param name = "writer"> JSON Writer. </param>
<param name = "flags"> The Value Type. </param> */

internal static void DecodeRToken(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer, byte flags)
{

switch(flags)
{
case RTypeId.BOOLEAN_FALSE:
writer.WriteBooleanValue(false);
break;

case RTypeId.BOOLEAN_TRUE:
writer.WriteBooleanValue(true);
break;

case RTypeId.INT8:

sbyte i8 = buffer.GetInt8(pos);
pos++;

writer.WriteNumberValue(i8);
break;

case RTypeId.UINT8:

byte u8 = buffer.GetUInt8(pos);
pos++;

writer.WriteNumberValue(u8);
break;

case RTypeId.INT16:

short i16 = buffer.GetInt16(pos);
pos += 2;

writer.WriteNumberValue(i16);
break;

case RTypeId.UINT16:

ushort u16 = buffer.GetUInt16(pos);
pos += 2;

writer.WriteNumberValue(u16);
break;

case RTypeId.INT32:

int i32 = buffer.GetInt32(pos);
pos += 4;

writer.WriteNumberValue(i32);
break;

case RTypeId.UINT32:

uint u32 = buffer.GetUInt32(pos);
pos += 4;

writer.WriteNumberValue(u32);
break;

case RTypeId.INT64:

long i64 = buffer.GetInt64(pos);
pos += 8;

writer.WriteNumberValue(i64);
break;

case RTypeId.UINT64:

ulong u64 = buffer.GetUInt64(pos);
pos += 8;

writer.WriteNumberValue(u64);
break;

case RTypeId.FLOAT32:

float f32 = buffer.GetFloat(pos);
pos += 4;

writer.WriteNumberValue(f32);
break;

case RTypeId.FLOAT64:

double f64 = buffer.GetDouble(pos);
pos += 8;

writer.WriteNumberValue(f64);
break;

case RTypeId.VAR_INT32:

int vInt32 = buffer.GetVarInt(pos, out int varLen32);
pos += (ulong)varLen32;

writer.WriteNumberValue(vInt32);
break;

case RTypeId.VAR_UINT32:

var vUInt32 = (uint)buffer.GetVarInt(pos, out int uVarLen32);
pos += (ulong)uVarLen32;

writer.WriteNumberValue(vUInt32);
break;

case RTypeId.ZIGZAG32:

int zInt32 = buffer.GetZigZag(pos, out int zLen32);
pos += (ulong)zLen32;

writer.WriteNumberValue(zInt32);
break;

case RTypeId.VAR_INT64:

long vInt64 = buffer.GetVarInt(pos, out int varLen64);
pos += (ulong)varLen64;

writer.WriteNumberValue(vInt64);
break;

case RTypeId.VAR_UINT64:

var vUInt64 = (ulong)buffer.GetVarInt(pos, out int uVarLen64);
pos += (ulong)uVarLen64;

writer.WriteNumberValue(vUInt64);
break;

case RTypeId.ZIGZAG64:

long zInt64 = buffer.GetZigZag(pos, out int zLen64);
pos += (ulong)zLen64;

writer.WriteNumberValue(zInt64);
break;

case RTypeId.NATIVE_STRING:
RtNativeString.Read(buffer, ref pos, writer, false);
break;

case RTypeId.UNICODE_STRING:
RtUnicodeString.Read(buffer, ref pos, writer, false);
break;

case RTypeId.ID_STRING:
RtIdString.Read(buffer, ref pos, writer, false, false);
break;

case RTypeId.ID_STRING_NULL:
RtIdString.Read(buffer, ref pos, writer, false, true);
break;

case RTypeId.OBJECT_START:
Read(buffer, ref pos, writer);
break;

case RTypeId.ARRAY:
RtArray.Read(buffer, ref pos, writer);
break;

case RTypeId.NATIVE_STRING_VALUE:
RtNativeString.ReadCached(buffer, ref pos, writer, false, false);
break;

case RTypeId.NATIVE_STRING_INDEX:
RtNativeString.ReadCached(buffer, ref pos, writer, true, false);
break;

case RTypeId.UNICODE_STRING_VALUE:
RtUnicodeString.ReadCached(buffer, ref pos, writer, false, false);
break;

case RTypeId.UNICODE_STRING_INDEX:
RtUnicodeString.ReadCached(buffer, ref pos, writer, true, false);
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
TraceLogger.WriteError($"Unknown RtType id for value: {flags:X2} @ {pos}");
return;
}

}

/** <summary> Reads an RTON Object and Writes it as JSON. </summary>

<param name = "reader"> The RTON reader. </param>
<param name = "writer"> The JSON writer. </param> */

internal static void Read(NativeBuffer buffer, ref ulong pos, Utf8JsonWriter writer)
{
byte first = buffer.GetUInt8(pos);

if(first == RTypeId.ARRAY)
{
pos++;
RtArray.Read(buffer, ref pos, writer);

return;
}

writer.WriteStartObject();

while(true)
{
byte keyFlags = buffer.GetUInt8(pos);
pos++;

if(keyFlags == RTypeId.OBJECT_END)
break;

DecodeRProp(buffer, ref pos, writer, keyFlags);

byte valueFlags = buffer.GetUInt8(pos);
pos++;

DecodeRToken(buffer, ref pos, writer, valueFlags);
}

writer.WriteEndObject();
}

}

}
