using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  [GeneratedRegex(@"(S\d+E\d+)|([ES]\d+)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft)]
  private static partial Regex SmartEpisodeDashRegex(); // S##E## || S## || E##

  public static void SmartEpisodeDashPre(ref string input, in bool smartEpisodeDash)
  {
    if (!smartEpisodeDash) { return; }

    Match match = SmartEpisodeDashRegex().Match(input);
    if (match.Success)
    {
      // Delete season/episode info
      input = input.Remove(match.Index, match.Length);

      // Append uppercase version of season/episode info
      input += $" #EPL#{match.Value.ToUpperInvariant()}#EPR#"; // EpisodePlaceholder Left/Right
    }
  }

  public static void SmartEpisodeDashPost(ref string input, in bool smartEpisodeDash)
  {
    if (!smartEpisodeDash) { return; }

    input = input.Replace("#EPL", "- ");
    input = input.Replace("#EPR", SpaceString);
  }
}
