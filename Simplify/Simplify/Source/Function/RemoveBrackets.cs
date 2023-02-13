using System.Text.RegularExpressions;

namespace Simplify;

// Check for balanced pairs of brackets
public static partial class Function
{
  const char OpeningCurly = '{';
  const char ClosingCurly = '}';

  const char OpeningCurved = '(';
  const char ClosingCurved = ')';

  const char OpeningSquare = '[';
  const char ClosingSquare = ']';

  private static bool IsBracketBalanced(ref string input, char opening, char closing)
  {
    int countOpening = input.Count(x => x == opening);
    int countClosing = input.Count(x => x == closing);

    if (countOpening - countClosing == 0) { return true; }

    // Reaching here implies brackets are imbalanced
    Print.WarningBlock("INFO");
    string pair = $"{opening} {closing}";
    Console.WriteLine(Print.WarningText($"{pair} do not exist in pairs."));
    Console.WriteLine(Print.WarningText($"Skipping bracket removal of {pair} in {input}.\n"));
    return false;
  }
}

// Remove curly brackets and its contents
public static partial class Function
{
  // Match innermost {} https://stackoverflow.com/a/49533163/17753137
  [GeneratedRegex(@"\{(?:[^{}])*\}")]
  private static partial Regex CurlyBracketContainerRegex();

  public static void RemoveCurlyBrackets(ref string input, in bool removeCurlyBrackets)
  {
    if (!removeCurlyBrackets) { return; }

    // Check for unbalanced curly brackets | #opening != #closing
    if (!IsBracketBalanced(ref input, OpeningCurly, ClosingCurly)) { return; }

    // If brackets are balanced, remove the innermost [] bracket to outermost [..[]..] bracket
    RecursivelyRemoveSquareBrackets(ref input);
  }

  private static void RecursivelyRemoveSquareBrackets(ref string input)
  {
    input = CurlyBracketContainerRegex().Replace(input, Space);
    // Rematch recursively to go from innermost {} to outermost {...{}...}
    if (CurlyBracketContainerRegex().Match(input).Success) { RecursivelyRemoveSquareBrackets(ref input); }
  }
}

// Remove curved brackets and its contents
public static partial class Function
{
  // Match innermost () https://stackoverflow.com/a/49533163/17753137
  [GeneratedRegex(@"\((?:[^()])*\)")]
  private static partial Regex CurvedBracketContainerRegex();

  public static void RemoveCurvedBrackets(ref string input, in bool removeCurvedBrackets)
  {
    if (!removeCurvedBrackets) { return; }

    // Check for unbalanced curved brackets | #opening != #closing
    if (!IsBracketBalanced(ref input, OpeningCurved, ClosingCurved)) { return; }

    // If brackets are balanced, remove the innermost () bracket to outermost (..()..) bracket
    RecursivelyRemoveCurvedBrackets(ref input);
  }

  private static void RecursivelyRemoveCurvedBrackets(ref string input)
  {
    input = CurvedBracketContainerRegex().Replace(input, Space);
    // Rematch recursively to go from innermost () to outermost (...()...)
    if (CurvedBracketContainerRegex().Match(input).Success) { RecursivelyRemoveCurvedBrackets(ref input); }
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

    // Check for unbalanced square brackets | #opening != #closing
    if (!IsBracketBalanced(ref input, OpeningSquare, ClosingSquare)) { return; }

    // If brackets are balanced, remove the innermost [] bracket to outermost [..[]..] bracket
    RecursivelyRemoveCurlyBrackets(ref input);
  }

  private static void RecursivelyRemoveCurlyBrackets(ref string input)
  {
    input = SquareBracketContainerRegex().Replace(input, Space);
    // Rematch recursively to go from innermost [] to outermost [...[]...]
    if (SquareBracketContainerRegex().Match(input).Success) { RecursivelyRemoveCurlyBrackets(ref input); }
  }
}
