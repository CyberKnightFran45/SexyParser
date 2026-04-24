using System.Collections.Generic;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Serves as a Reference Control for Cached Strings </summary>

internal static class ReferenceStrings
{
// Native strings

private static readonly Dictionary<string, int> NativeLookup;

private static readonly List<string> NativeStrings;

// Unicode strings

private static readonly Dictionary<string, int> UnicodeLookup;

private static readonly List<string> UnicodeStrings;

// Init

static ReferenceStrings()
{
NativeLookup = new();
NativeStrings = new();

UnicodeLookup = new();
UnicodeStrings = new();
}

/** <summary> Adds a ReferenceString to the List. </summary> 

<param name = "str"> The String to be Added. </param> */

internal static void Add(string str, bool isUnicode)
{

if(isUnicode)
{
UnicodeLookup.TryAdd(str, UnicodeStrings.Count);
UnicodeStrings.Add(str);
}

else
{
NativeLookup.TryAdd(str, NativeStrings.Count);
NativeStrings.Add(str);
}

}

/// <summary> Removes all Strings from List. </summary>

internal static void Clear()
{
NativeLookup.Clear();
NativeStrings.Clear();
	
UnicodeLookup.Clear();
UnicodeStrings.Clear();
}

/** <summary> Gets the Index of a ReferenceString in the List. </summary>

<param name = "str"> The String to Locate </param>

<returns> The Index of the String. </returns> */

internal static int IndexOf(string str, bool isUnicode)
{
var dict = isUnicode ? UnicodeLookup : NativeLookup;
 
return dict.TryGetValue(str, out int index) ? index : -1;
}

/** <summary> Gets string from List by Index. </summary>

<param name = "index"> The String index. </param>

<returns> The String Obtained. </returns> */

internal static string Get(int index, bool isUnicode) 
{
return isUnicode ? UnicodeStrings[index] : NativeStrings[index];
}

/** <summary> Checks if a String is Contained in the List. </summary>

<param name = "str"> The String to Check. </param>

<returns> <b>true</b> if the String exists; otherwise, <b>false</b> </returns> */

internal static bool Contain(string str, bool isUnicode)
{
return isUnicode ? UnicodeLookup.ContainsKey(str) : NativeLookup.ContainsKey(str);
}

}

}