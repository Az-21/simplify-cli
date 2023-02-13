using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  // Regex to match 19## or 20##
  [GeneratedRegex("(19|20)\\d{2}", RegexOptions.RightToLeft)]
  private static partial Regex FourDigitNumberStarting19or20Regex();

  //Preserving release year for movie/series before BracketRemover functions
  public static void AppendYearPre(ref string filename, bool appendYear)
  {
    if (!appendYear) { return; }

    Match releaseYear = FourDigitNumberStarting19or20Regex().Match(filename);
    if (releaseYear.Success)
    {
      // Special case of year = 1920 where it is most probably resolution and not year
      if (releaseYear.Value == "1920") { return; }

      filename = filename.Remove(releaseYear.Index, 4);
      filename += $" PLACEHOLDERLEFT{releaseYear.Value}PLACEHOLDERRIGHT";
    }
  }
}
