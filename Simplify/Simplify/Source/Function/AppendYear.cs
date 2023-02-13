using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  // Regex to match 19## or 20##
  // NOTE: This pattern will also match ##..##(19|20)##...## which is not ideal
  [GeneratedRegex(@"(19|20)\d{2}", RegexOptions.RightToLeft)]
  private static partial Regex FourDigitNumberStarting19or20Regex();

  //Preserving release year for movie/series before BracketRemover functions
  public static void AppendYearPre(ref string input, in bool appendYear)
  {
    if (!appendYear) { return; }

    Match releaseYear = FourDigitNumberStarting19or20Regex().Match(input);
    if (releaseYear.Success)
    {
      // Special case of year = 1920 where it is most probably resolution and not year
      if (releaseYear.Value == "1920") { return; }

      input = input.Remove(releaseYear.Index, 4);
      input += $"{Space}PLACEHOLDERLEFT{releaseYear.Value}PLACEHOLDERRIGHT";
    }
  }

  //Restoring release year for movie/series and appending it at the last of input
  public static void AppendYearPost(ref string input, in bool appendYear)
  {
    if (!appendYear) { return; }

    input = input.Replace("PLACEHOLDERLEFT", "(");
    input = input.Replace("PLACEHOLDERRIGHT", ")");
  }
}
