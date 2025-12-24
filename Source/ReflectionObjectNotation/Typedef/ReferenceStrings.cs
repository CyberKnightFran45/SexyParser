using System.Collections.Generic;

namespace SexyParsers.ReflectionObjectNotation
{
/// <summary> Serves as a Reference Control for Cached Strings. </summary>

public static class ReferenceStrings
{
/// <summary> Contains the Native Strings found in a File. </summary>

private static readonly List<string> NativeStrings = new();

/// <summary> Contains the Unicode Strings found in a File. </summary>

private static readonly List<string> UnicodeStrings = new();

/** <summary> Adds a ReferenceString to the List. </summary> 

<param name = "str"> The String to be Added. </param> */

public static void Add(string str, bool isUnicode)
{

if(isUnicode)
UnicodeStrings.Add(str);

else
NativeStrings.Add(str);

}

/// <summary> Removes all Strings from List. </summary>

public static void Clear()
{
NativeStrings.Clear();

UnicodeStrings.Clear();
}

/** <summary> Gets the Index of a ReferenceString in the List. </summary>

<param name = "str"> The String to Locate in the List. </param>

<returns> The Index of the String. </returns> */

public static int IndexOf(string str, bool isUnicode)
{
return isUnicode ? UnicodeStrings.IndexOf(str) : NativeStrings.IndexOf(str);
}

/** <summary> Gets a String from the List by its Index. </summary>

<param name = "index"> The String index. </param>

<returns> The String Obtained. </returns> */

public static string Get(int index, bool isUnicode) 
{
return isUnicode ? UnicodeStrings[index] : NativeStrings[index];
}

/** <summary> Checks if a String is Contained in the List. </summary>

<param name = "str"> The String to Check. </param>

<returns> <b>true</b> if the String exists in the List; otherwise, <b>false</b> </returns> */

public static bool Contain(string str, bool isUnicode)
{
return isUnicode ? UnicodeStrings.Contains(str) : NativeStrings.Contains(str);
}

}

}