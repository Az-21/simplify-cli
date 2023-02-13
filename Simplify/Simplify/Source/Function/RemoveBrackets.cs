using System.Text.RegularExpressions;

namespace Simplify;

public static partial class Function
{
  // Remove parentheses and its contents
  [GeneratedRegex(@" ?\(.*?\)")]
  private static partial Regex CurvedBracketRegex();

  public static void RemoveCurvedBracket(ref string input, in bool removeCurvedBrackets)
  {
    if (!removeCurvedBrackets) { return; }
    input = CurvedBracketRegex().Replace(input, " ");
  }

  // Remove square brackets and its contents
  [GeneratedRegex(@" ?\[.*?\]")]
  private static partial Regex SquareBracketRegex();

  public static void RemoveSquareBracket(ref string input, in bool removeSquareBrackets)
  {
    if (!removeSquareBrackets) { return; }
    input = SquareBracketRegex().Replace(input, " ");
  }
}
