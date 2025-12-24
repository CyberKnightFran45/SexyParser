using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a SubGroup. </summary>

public class MSubGroupData
{
/** <summary> Gets or Sets the SubGroup Type. </summary>
<returns> The SubGroup Type. </returns> */

[JsonPropertyName("type") ]

public string Type{ get; set; }

/** <summary> Gets or Sets the SubGroup Packet. </summary>
<returns> The SubGroup Type. </returns> */

[JsonPropertyName("packet") ]

public object Packet{ get; set; }

// ctor

public MSubGroupData()
{
}

// ctor2

public MSubGroupData(object packet)
{
Packet = packet;
}

// ctor3

public MSubGroupData(string type, object packet)
{
Type = type;
Packet = packet;
}

public static readonly JsonSerializerContext Context = new MSubDataContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(MSubGroupData) ) ]

public partial class MSubDataContext : JsonSerializerContext
{
}

}