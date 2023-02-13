using System.Text.RegularExpressions;

namespace Simplify;

// Remove parentheses and its contents
public static partial class Function
{
  // Match innermost () https://stackoverflow.com/a/49533163/17753137
  [GeneratedRegex(@"\((?:[^()])*\)")]
  private static partial Regex CurvedBracketContainerRegex();

  public static void RemoveCurvedBrackets(ref string input, in bool removeCurvedBrackets)
  {
    if (!removeCurvedBrackets) { return; }
    input = CurvedBracketContainerRegex().Replace(input, Space);

    // Rematch recursively to go from innermost () to outermost (...()...)
    if (CurvedBracketContainerRegex().Match(input).Success) { RemoveCurvedBrackets(ref input, removeCurvedBrackets); }
  }
}

// Remove square brackets and its contents
public static partial class Function
{
  // Match innermost [] https://stackoverflow.com/a/49533163/17753137
  [GeneratedRegex(@"\[(?:[^\[\]])*\]")]
  private static partial Regex SquareBracketContainerRegex();

  public static void RemoveSquareBrackets(ref string input, in bool removeSquareBrackets)
  {
    if (!removeSquareBrackets) { return; }
    input = SquareBracketContainerRegex().Replace(input, Space);

    // Rematch recursively to go from innermost [] to outermost [...[]...]
    if (SquareBracketContainerRegex().Match(input).Success) { RemoveSquareBrackets(ref input, removeSquareBrackets); }
  }
}

// Remove curved brackets and its contents
public static partial class Function
{
  // Match innermost {} https://stackoverflow.com/a/49533163/17753137
  [GeneratedRegex(@"\{(?:[^{}])*\}")]
  private static partial Regex CurlyBracketContainerRegex();

  public static void RemoveCurlyBrackets(ref string input, in bool removeCurlyBrackets)
  {
    if (!removeCurlyBrackets) { return; }
    input = CurlyBracketContainerRegex().Replace(input, Space);

    // Rematch recursively to go from innermost {} to outermost {...{}...}
    if (CurlyBracketContainerRegex().Match(input).Success) { RemoveCurlyBrackets(ref input, removeCurlyBrackets); }
  }
}
