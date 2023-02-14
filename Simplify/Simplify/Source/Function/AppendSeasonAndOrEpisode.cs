using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  [GeneratedRegex(@"(S\d+E\d+)|([ES]\d+)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft)]
  private static partial Regex SeasonAndOrEpisodeRegex(); // S##E## || S## || E##

  const string SeasonPlaceholderLeft = "#SEPLeft#";
  const string SeasonPlaceholderRight = "#SEPRight#";

  public static void AppendSeasonAndOrEpisodePre(ref string input, in bool smartEpisodeDash)
  {
    if (!smartEpisodeDash) { return; }

    Match match = SeasonAndOrEpisodeRegex().Match(input);
    if (match.Success)
    {
      // Delete season/episode info
      input = input.Remove(match.Index, match.Length);

      // Append uppercase version of season/episode info with a space
      input += SpaceString + SeasonPlaceholderLeft + match.Value.ToUpperInvariant() + SeasonPlaceholderRight;
    }
  }

  public static void AppendSeasonAndOrEpisodePost(ref string input, in bool smartEpisodeDash, in string prefix)
  {
    if (!smartEpisodeDash) { return; }

    input = input.Replace(SeasonPlaceholderLeft, $" {prefix} ");
    input = input.Replace(SeasonPlaceholderRight, SpaceString);
  }
}
