using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  // Regex to match {Non-digit} {19 OR 20 followed by any two digits} {Non-digit}
  [GeneratedRegex(@"\D(19|20)\d{2}\D", RegexOptions.RightToLeft)]
  private static partial Regex FourDigitNumberStarting19or20Regex();

  const string YearPlaceholderLeft = "#YPLeft#";
  const string YearPlaceholderRight = "#YPRight";

  //Preserving release year for movie/series before BracketRemover functions
  public static void AppendYearPre(ref string input, in bool appendYear)
  {
    if (!appendYear) { return; }

    // Pad the input to ensure the match is always with length of 6
    // Also eliminates need for start and end characters in the match => (\D|^)(19|20)\d{2}(\D|$)
    input = Space + input + Space;

    Match releaseYear = FourDigitNumberStarting19or20Regex().Match(input);

    if (releaseYear.Success)
    {
      // Slice the match to remove left and right non-digit characters (\D)
      string year = releaseYear.Value[1..5]; // *####* => ####

      // Special case of year = 1920 where it is most probably resolution and not year
      if (year == "1920") { return; }

      input = input.Remove(releaseYear.Index + 1, 4); // +1 to start from the first digit
      input += SpaceString + YearPlaceholderLeft + year + YearPlaceholderRight;
    }
  }

  //Restoring release year for movie/series and appending it at the last of input
  public static void AppendYearPost(ref string input, in bool appendYear)
  {
    if (!appendYear) { return; }

    input = input.Replace(YearPlaceholderLeft, "(");
    input = input.Replace(YearPlaceholderRight, ")");
  }
}
