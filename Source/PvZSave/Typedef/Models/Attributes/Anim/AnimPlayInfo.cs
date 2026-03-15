namespace SexyParsers.PvZSave
{
/// <summary> Stores Info about which Animations should be Played </summary>

public class AnimPlayInfo
{
/// <summary> Animate unlocking of last unlocked <c>Minigame</c> </summary>

public bool LastLevelUnlocked_Minigames{ get; set; }

/// <summary> Animate unlocking of last unlocked level: <c>Vasebreaker</c> </summary>

public bool LastLevelUnlocked_Vasebreaker{ get; set; }

/// <summary> Animate unlocking of last unlocked level: <c>I, Zombie</c> </summary>

public bool LastLevelUnlocked_IZombie{ get; set; }

/// <summary> Animate unlocking of last unlocked level: <c>Survival Mode</c> </summary>

public bool LastLevelUnlocked_Survival{ get; set; }

/// <summary> Animate unlocking of last level unlocked: <c>Limbo Page</c> (unused) </summary>

public bool LastLevelUnlocked_Limbo{ get; set; }

/// <summary> Show <c>Adventure Complete!</c> dialog on next visit to Main Menu </summary>

public bool AdventureComplete{ get; set; }

/// <summary> Creates a new instance of <c>AnimPlayInfo</c> </summary>

public AnimPlayInfo()
{
}

}

}