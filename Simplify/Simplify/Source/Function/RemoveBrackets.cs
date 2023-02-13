using System.Text.RegularExpressions;

namespace Simplify;

// Check for balanced pairs of brackets
public static partial class Function
{
  const char LCurly = '{';
  const char RCurly = '}';

  const char LCurved = '(';
  const char RCurved = ')';

  const char LSquare = '[';
  const char RSquare = ']';

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

public static partial class Function
{
  public static void RemoveBrackets(ref string input, in bool removeBracket, char opening, char closing, Regex regex)
  {
    if (!removeBracket) { return; }

    // Check for unbalanced brackets => #opening != #closing
    if (!IsBracketBalanced(ref input, opening, closing)) { return; }

    // If brackets are balanced, recursively remove pairs from innermost to outermost
    RecursivelyRemoveBrackets(ref input, regex);
  }

  private static void RecursivelyRemoveBrackets(ref string input, in Regex regex)
  {
    input = regex.Replace(input, Space);
    // Rematch recursively to go from innermost bracket pair to outermost bracket pair
    if (regex.Match(input).Success) { RecursivelyRemoveBrackets(ref input, regex); }
  }
}

/* ℹ️
 * Regex logic to find the innermost bracket pair
 * https://stackoverflow.com/a/49533163/17753137
 */

// Remove curly brackets {} and its contents
public static partial class Function
{
  [GeneratedRegex(@"\{(?:[^{}])*\}")] // ℹ️
  private static partial Regex CurlyBracketRegex();

  public static void RemoveCurlyBrackets(ref string input, in bool removeCurlyBrackets) =>
    RemoveBrackets(ref input, removeCurlyBrackets, LCurly, RCurly, CurlyBracketRegex());
}

// Remove curved brackets () and its contents
public static partial class Function
{
  [GeneratedRegex(@"\((?:[^()])*\)")] // ℹ️
  private static partial Regex CurvedBracketRegex();

  public static void RemoveCurvedBrackets(ref string input, in bool removeCurvedBrackets) =>
    RemoveBrackets(ref input, removeCurvedBrackets, LCurved, RCurved, CurvedBracketRegex());
}

// Remove square brackets [] and its contents
public static partial class Function
{
  [GeneratedRegex(@"\[(?:[^\[\]])*\]")] // ℹ️
  private static partial Regex SquareBracketRegex();

  public static void RemoveSquareBrackets(ref string input, in bool removeSquareBrackets) =>
    RemoveBrackets(ref input, removeSquareBrackets, LSquare, RSquare, SquareBracketRegex());
}
