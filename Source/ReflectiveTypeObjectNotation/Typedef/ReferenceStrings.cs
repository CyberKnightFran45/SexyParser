using System.Collections.Generic;

namespace SexyParsers.ReflectiveTypeObjectNotation
{
/// <summary> Serves as a Reference Control for Cached Strings </summary>

internal static class ReferenceStrings
{
/// <summary> Native strings </summary>

private static List<string> nativeStrs = new();

/// <summary> Unicode strings </summary>

private static List<string> unicodeStrs = new();

/** <summary> Adds a ReferenceString to the List. </summary> 

<param name = "str"> The String to be Added. </param> */

internal static void Add(string str, bool isUnicode)
{

if(isUnicode)
unicodeStrs.Add(str);

else
nativeStrs.Add(str);

}

/// <summary> Removes all Strings from List. </summary>

internal static void Clear()
{
nativeStrs.Clear();

unicodeStrs.Clear();
}

/** <summary> Gets the Index of a ReferenceString in the List. </summary>

<param name = "str"> The String to Locate </param>

<returns> The Index of the String. </returns> */

internal static int IndexOf(string str, bool isUnicode)
{
return isUnicode ? unicodeStrs.IndexOf(str) : nativeStrs.IndexOf(str);
}

/** <summary> Gets a String from the List by its Index. </summary>

<param name = "index"> The String index. </param>

<returns> The String Obtained. </returns> */

internal static string Get(int index, bool isUnicode) 
{
return isUnicode ? unicodeStrs[index] : nativeStrs[index];
}

/** <summary> Checks if a String is Contained in the List. </summary>

<param name = "str"> The String to Check. </param>

<returns> <b>true</b> if the String exists in the List; otherwise, <b>false</b> </returns> */

internal static bool Contain(string str, bool isUnicode)
{
return isUnicode ? unicodeStrs.Contains(str) : nativeStrs.Contains(str);
}

}

}