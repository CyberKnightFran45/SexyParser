namespace SexyParsers.PvZSave
{
/// <summary> Stores Progress for Puzzle Mode </summary>

public class PuzzleRecord
{
/// <summary> <c>Puzzle1</c> Completion State </summary>

public bool Puzzle1{ get; set; }

/// <summary> <c>Puzzle2</c> Completion State </summary>

public bool Puzzle2{ get; set; }

/// <summary> <c>Puzzle3</c> Completion State </summary>

public bool Puzzle3{ get; set; }

/// <summary> <c>Puzzle4</c> Completion State </summary>

public bool Puzzle4{ get; set; }

/// <summary> <c>Puzzle5</c> Completion State </summary>

public bool Puzzle5{ get; set; }

/// <summary> <c>Puzzle6</c> Completion State </summary>

public bool Puzzle6{ get; set; }

/// <summary> <c>Puzzle7</c> Completion State </summary>

public bool Puzzle7{ get; set; }

/// <summary> <c>Puzzle8</c> Completion State </summary>

public bool Puzzle8{ get; set; }

/// <summary> <c>Puzzle9</c> Completion State </summary>

public bool Puzzle9{ get; set; }

/// <summary> Streak for Endless Mode </summary>

public uint EndlessStreak{ get; set; }

/// <summary> Creates a new instance of <c>PuzzleRecord</c> </summary>

public PuzzleRecord()
{
}

}

}