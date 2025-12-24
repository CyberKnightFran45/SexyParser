using System.Text.Json.Serialization;

namespace SexyParsers.PopCapResManager
{
/// <summary> Represents a SubGroup Wrapper. </summary>

public class SubGroupWrapper
{
/** <summary> Gets or Sets the SubGroup ID. </summary>
<returns> The SubGroup ID. </returns> */

[JsonPropertyName("id") ]

public string ID{ get; set; } = string.Empty;

/** <summary> Gets or Sets the SubGroup Res. </summary>
<returns> The SubGroup Res. </returns> */

[JsonPropertyName("res") ]

public string Res{ get; set; } = string.Empty;

// ctor

public SubGroupWrapper()
{
}

// ctor 2

public SubGroupWrapper(string id, string res)
{
ID = id;
Res = res;
}

public static readonly JsonSerializerContext Context = new SubWrapperContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(SubGroupWrapper) ) ]

public partial class SubWrapperContext : JsonSerializerContext
{
}

}