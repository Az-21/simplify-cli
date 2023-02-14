using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  [GeneratedRegex(@"(S\d+E\d+)|([ES]\d+)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft)]
  private static partial Regex SmartEpisodeDashRegex(); // S##E## || S## || E##

  public static void SmartEpisodeDash(ref string input, in bool smartEpisodeDash)
  {
    if (!smartEpisodeDash) { return; }

    Match match = SmartEpisodeDashRegex().Match(input);
    if (match.Success)
    {
      // Convert episode number to uppercase
      input = input.Replace(match.Value, match.Value.ToUpperInvariant());

      // Prefix dash with spaces
      input = input.Insert(match.Index, " - ");
    }
  }
}
