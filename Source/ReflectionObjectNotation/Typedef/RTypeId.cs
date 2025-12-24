namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Defines the expected Type of a Value in the RtSystem. </summary>

public static class RTypeId
{
/// <summary> Type is a Boolean that holds <b>false</b> as a Value. </summary>
public const byte BOOLEAN_FALSE = 0x00;

/// <summary> Type is a Boolean that holds <b>true</b> as a Value. </summary>
public const byte BOOLEAN_TRUE = 0x01;

/// <summary> Type is a 8-bits Integer that holds a Value different from Zero. </summary>
public const byte UINT8 = 0x08;

/// <summary> Type is a 8-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte UINT8_ZERO = 0x09;

/// <summary> Type is a 8-bits signed Integer that holds a Value different from Zero. </summary>
public const byte INT8 = 0x0A;

/// <summary> Type is a 8-bits signed Integer that holds <b>Zero</b> as a Value. </summary>
public const byte INT8_ZERO = 0x0B;

/// <summary> Type is a 16-bits Integer that holds a Value different from Zero. </summary>
public const byte INT16 = 0x10;

/// <summary> Type is a 16-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte INT16_ZERO = 0x11;

/// <summary> Type is an unsigned 16-bits Integer that holds a Value different from Zero. </summary>
public const byte UINT16 = 0x12;

/// <summary> Type is an unsigned 16-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte UINT16_ZERO = 0x13;

/// <summary> Type is a 32-bits Integer that holds a Value different from Zero. </summary>
public const byte INT32 = 0x20;

/// <summary> Type is a 32-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte INT32_ZERO = 0x21;

/// <summary> Type is a 32-bits Float-point that holds a Value different from Zero. </summary>
public const byte FLOAT32 = 0x22;

/// <summary> Type is a 32-bits Float-point that holds <b>Zero</b> as a Value. </summary>
public const byte FLOAT32_ZERO = 0x23;

/// <summary> Type is a 32-bits variant Integer. </summary>
public const byte VAR_INT32 = 0x24;

/// <summary> Type is a 32-bits variant Integer that was Encoded with ZigZag. </summary>
public const byte ZIGZAG32 = 0x25;

/// <summary> Type is an unsigned 32-bits Integer that holds a Value different from Zero. </summary>
public const byte UINT32 = 0x26;

/// <summary> Type is an unsigned 32-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte UINT32_ZERO = 0x27;

/// <summary> Type is an unsigned 32-bits variant Integer. </summary>
public const byte VAR_UINT32 = 0x28;

/// <summary> Type is a 64-bits Integer that holds a Value different from Zero. </summary>
public const byte INT64 = 0x40;

/// <summary> Type is a 64-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte INT64_ZERO = 0x41;

/// <summary> Type is a 64-bits Float-point that holds a Value different from Zero. </summary>
public const byte FLOAT64 = 0x42;

/// <summary> Type is a 64-bits Float-point that holds <b>Zero</b> as a Value. </summary>
public const byte FLOAT64_ZERO = 0x43;

/// <summary> Type is a 64-bits variant Integer. </summary>
public const byte VAR_INT64 = 0x44;

/// <summary> Type is a 64-bits variant Integer that was Encoded with ZigZag. </summary>
public const byte ZIGZAG64 = 0x45;

/// <summary> Type is an unsigned 64-bits Integer that holds a Value different from Zero. </summary>
public const byte UINT64 = 0x46;

/// <summary> Type is an unsigned 64-bits Integer that holds <b>Zero</b> as a Value. </summary>
public const byte UINT64_ZERO = 0x47;

/// <summary> Type is an unsigned 64-bits variant Integer. </summary>
public const byte VAR_UINT64 = 0x48;

/// <summary> Type is a Native String. </summary>
public const byte NATIVE_STRING = 0x81;

/// <summary> Type is a Unicode String. </summary>
public const byte UNICODE_STRING = 0x82;

/// <summary> Type is a ID string that Serves as a Reference to an Object. </summary>
public const byte ID_STRING = 0x83;

/// <summary> Type is a RtID that holds a <b>null</b> Reference. </summary>
public const byte ID_STRING_NULL = 0x84;

/// <summary> Represents the Beginning of an Object. </summary>
public const byte OBJECT_START = 0x85;

/// <summary> Type is an Array. </summary>
public const byte ARRAY = 0x86;

/// <summary> Represents the Value of a Cached NativeString. </summary>
public const byte NATIVE_STRING_VALUE = 0x90;

/// <summary> Represents the Index of a Cached NativeString. </summary>
public const byte NATIVE_STRING_INDEX = 0x91;

/// <summary> Represents the Value of a Cached UnicodeString. </summary>
public const byte UNICODE_STRING_VALUE = 0x92;

/// <summary> Represents the Index of a Cached UnicodeString. </summary>
public const byte UNICODE_STRING_INDEX = 0x93;

/// <summary> Represents the Beginning of an Array. </summary>
public const byte ARRAY_START = 0xFD;

/// <summary> Represents the End of an Array. </summary>
public const byte ARRAY_END = 0xFE;

/// <summary> Represents the End of an Object. </summary>
public const byte OBJECT_END = 0xFF;
}

}