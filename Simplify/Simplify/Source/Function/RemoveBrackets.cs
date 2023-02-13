﻿using System.Text.RegularExpressions;

namespace Simplify;

public static partial class Function
{
  // Remove parentheses and its contents
  [GeneratedRegex(@" ?\(.*?\)")]
  private static partial Regex CurvedBracketContainerRegex();

  public static void RemoveCurvedBrackets(ref string input, in bool removeCurvedBrackets)
  {
    if (!removeCurvedBrackets) { return; }
    input = CurvedBracketContainerRegex().Replace(input, " ");
  }

  // Remove square brackets and its contents
  [GeneratedRegex(@" ?\[.*?\]")]
  private static partial Regex SquareBracketContainerRegex();

  public static void RemoveSquareBrackets(ref string input, in bool removeSquareBrackets)
  {
    if (!removeSquareBrackets) { return; }
    input = SquareBracketContainerRegex().Replace(input, " ");
  }
}