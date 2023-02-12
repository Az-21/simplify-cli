﻿namespace Simplify;

public static class Process
{
  // Convert comma separated extensions to array of extension with `.` (dot) prefix
  public static string[] ConvertToExtensionList(in JsonConfig prefs)
  {
    return prefs.Extensions
        .Split(',')
        .Select(x => $"*.{x.Trim()}")
        .ToArray();
  }

  public static string FirstCharToUppercase(string word)
  {
    if (string.IsNullOrEmpty(word)) { return string.Empty; }
    return char.ToUpperInvariant(word[0]) + word[1..];
  }
}
