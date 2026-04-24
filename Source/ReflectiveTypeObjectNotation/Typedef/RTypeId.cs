namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Identifier table for Reflective Types </summary>

internal static class RTypeId
{
/// <summary> Type is a Boolean that holds <b>false</b> as a Value. </summary>

internal const byte BOOLEAN_FALSE = 0x00;

/// <summary> Type is a Boolean that holds <b>true</b> as a Value. </summary>

internal const byte BOOLEAN_TRUE = 0x01;

/// <summary> Type is a <b>null</b> string represented as <c>*</c> </summary>

internal const byte NULL_STRING = 0x02;

/// <summary> Type is a 8-bits Integer that holds a Value different from Zero. </summary>

internal const byte UINT8 = 0x08;

/// <summary> Type is a 8-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte UINT8_ZERO = 0x09;

/// <summary> Type is a 8-bits signed Integer that holds a Value different from Zero. </summary>

internal const byte INT8 = 0x0A;

/// <summary> Type is a 8-bits signed Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte INT8_ZERO = 0x0B;

/// <summary> Type is a 16-bits Integer that holds a Value different from Zero. </summary>

internal const byte INT16 = 0x10;

/// <summary> Type is a 16-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte INT16_ZERO = 0x11;

/// <summary> Type is an unsigned 16-bits Integer that holds a Value different from Zero. </summary>

internal const byte UINT16 = 0x12;

/// <summary> Type is an unsigned 16-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte UINT16_ZERO = 0x13;

/// <summary> Type is a 32-bits Integer that holds a Value different from Zero. </summary>

internal const byte INT32 = 0x20;

/// <summary> Type is a 32-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte INT32_ZERO = 0x21;

/// <summary> Type is a 32-bits Float-point that holds a Value different from Zero. </summary>

internal const byte FLOAT32 = 0x22;

/// <summary> Type is a 32-bits Float-point that holds <b>Zero</b> as a Value. </summary>

internal const byte FLOAT32_ZERO = 0x23;

/// <summary> Type is a 32-bits variant Integer. </summary>

internal const byte VARINT = 0x24;

/// <summary> Type is a 32-bits variant Integer that was Encoded with ZigZag. </summary>

internal const byte ZIGZAG32 = 0x25;

/// <summary> Type is an unsigned 32-bits Integer that holds a Value different from Zero. </summary>

internal const byte UINT32 = 0x26;

/// <summary> Type is an unsigned 32-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte UINT32_ZERO = 0x27;

/// <summary> Type is an unsigned 32-bits variant Integer. </summary>

internal const byte UVARINT = 0x28;

/// <summary> Type is a 64-bits Integer that holds a Value different from Zero. </summary>

internal const byte INT64 = 0x40;

/// <summary> Type is a 64-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte INT64_ZERO = 0x41;

/// <summary> Type is a 64-bits Float-point that holds a Value different from Zero. </summary>

internal const byte FLOAT64 = 0x42;

/// <summary> Type is a 64-bits Float-point that holds <b>Zero</b> as a Value. </summary>

internal const byte FLOAT64_ZERO = 0x43;

/// <summary> Type is a 64-bits variant Integer. </summary>

internal const byte VARLONG = 0x44;

/// <summary> Type is a 64-bits variant Integer that was Encoded with ZigZag. </summary>

internal const byte ZIGZAG64 = 0x45;

/// <summary> Type is an unsigned 64-bits Integer that holds a Value different from Zero. </summary>

internal const byte UINT64 = 0x46;

/// <summary> Type is an unsigned 64-bits Integer that holds <b>Zero</b> as a Value. </summary>

internal const byte UINT64_ZERO = 0x47;

/// <summary> Type is an unsigned 64-bits variant Integer. </summary>

internal const byte UVARLONG = 0x48;

/// <summary> Type is a Native String. </summary>

internal const byte NATIVE_STRING = 0x81;

/// <summary> Type is a Unicode String. </summary>

internal const byte UNICODE_STRING = 0x82;

/// <summary> Type is a ID string that Serves as a Reference to an Object. </summary>

internal const byte ID_STRING = 0x83;

/// <summary> Type is a <b>null</b> Reference. </summary>

internal const byte ID_STRING_NULL = 0x84;

/// <summary> Represents the Beginning of an Object. </summary>

internal const byte OBJECT_START = 0x85;

/// <summary> Type is an Array. </summary>

internal const byte ARRAY = 0x86;

/// <summary> Type is Binary string. </summary>

internal const byte BINARY_STRING = 0x87;

/// <summary> Represents the Value of a Cached NativeString. </summary>

internal const byte NATIVE_STRING_CACHE = 0x90;

/// <summary> Represents the Index of a Cached NativeString. </summary>

internal const byte NATIVE_STRING_INDEX = 0x91;

/// <summary> Represents the Value of a Cached UnicodeString. </summary>

internal const byte UNICODE_STRING_CACHE = 0x92;

/// <summary> Represents the Index of a Cached UnicodeString. </summary>

internal const byte UNICODE_STRING_INDEX = 0x93;

/// <summary> Type is a Boolean with byte payload </summary>

internal const byte BOOLEAN = 0xBC;

/// <summary> Represents the Beginning of an Array. </summary>

internal const byte ARRAY_START = 0xFD;

/// <summary> Represents the End of an Array. </summary>

internal const byte ARRAY_END = 0xFE;

/// <summary> Represents the End of an Object. </summary>

internal const byte OBJECT_END = 0xFF;
}

}