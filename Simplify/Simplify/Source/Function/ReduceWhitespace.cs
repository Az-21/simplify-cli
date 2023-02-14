using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  // Replace all whitespace with a single space AND then trim
  [GeneratedRegex(@"\s+")]
  private static partial Regex TwoOrMoreWhitespaceRegex();

  public static void ReduceWhitespace(ref string input)
  {
    input = TwoOrMoreWhitespaceRegex()
      .Replace(input, SpaceString)
      .Trim(' ');
  }
}
