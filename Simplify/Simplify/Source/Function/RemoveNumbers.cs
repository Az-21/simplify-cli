﻿using System.Text.RegularExpressions;

namespace Simplify;

public static partial class Function
{
  // Remove All Numbers
  [GeneratedRegex("\\d")]
  private static partial Regex AnyNumberRegex();

  public static void RemoveNumbers(ref string filename)
  {
    if (!Global.ImmutableConfig.RemoveNumbers) { return; }
    filename = AnyNumberRegex().Replace(filename, string.Empty);
  }
}
