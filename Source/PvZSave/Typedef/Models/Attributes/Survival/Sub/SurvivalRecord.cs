namespace SexyParsers.PvZSave
{
/// <summary> Stores Progress in Survival Mode </summary>

public class SurvivalRecord
{
/// <summary> Flags attained at Survival: Day </summary>

public uint Day{ get; set; }

/// <summary> Flags attained at Survival: Night </summary>

public uint Night{ get; set; }

/// <summary> Flags attained at Survival: Pool </summary>

public uint Pool{ get; set; }

/// <summary> Flags attained at Survival: Fog </summary>

public uint Fog{ get; set; }

/// <summary> Flags attained at Survival: Roof </summary>

public uint Roof{ get; set; }

/// <summary> Creates a new <c>SurvivalRecord</c> </summary>

public SurvivalRecord()
{
}

}

}