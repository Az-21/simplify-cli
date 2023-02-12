using System.Text.RegularExpressions;

namespace Simplify;

// NOTE: all of the following functions are 'call by reference' to increase performance
// Order sensitive functions (first)
public static partial class Simplify
{
  //Preserving release year for movie/series before BracketRemover function
  [GeneratedRegex("(19|20)\\d{2}", RegexOptions.RightToLeft)]
  private static partial Regex FourDigitNumberStarting19or20Regex();

  public static void AppendYearPre(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.AppendYear) { return; }

    Match releaseYear = FourDigitNumberStarting19or20Regex().Match(filename);
    if (releaseYear.Success)
    {
      filename = filename.Remove(releaseYear.Index, 4);
      filename = filename.Replace("()", string.Empty);
      filename = filename.Replace("[]", string.Empty);
      filename += $" PLACEHOLDERLEFT{releaseYear.Value}PLACEHOLDERRIGHT";
    }
  }

  // Remove All Numbers
  [GeneratedRegex("\\d")]
  private static partial Regex AnyNumberRegex();

  public static void RemoveNumbers(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.RemoveNumbers) { return; }
    filename = AnyNumberRegex().Replace(filename, string.Empty);
  }
}

// Order insensitive operations
public static partial class Simplify
{
  // Blacklist


  // Convert to Lower Case
  public static void ConvertToLowercase(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.ConvertToLowercase) { return; }
    filename = filename.ToLowerInvariant();
  }

  // Remove non-ASCII characters
  [GeneratedRegex("[^\\u0000-\\u007F]+")]
  private static partial Regex NonAsciiCharacterRegex();

  public static void RemoveNonASCII(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.RemoveNonAscii) { return; }
    filename = NonAsciiCharacterRegex().Replace(filename, string.Empty);
  }

  // Remove parentheses + text: `abc (def)` -> `abc  `
  [GeneratedRegex(" ?\\(.*?\\)")]
  private static partial Regex CurvedBracketRegex();

  public static void RemoveCurvedBracket(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.RemoveCurvedBracket) { return; }
    filename = CurvedBracketRegex().Replace(filename, " ");
  }

  // Remove square brackets + text: `abc [def]` -> `abc  `
  [GeneratedRegex(" ?\\[.*?\\]")]
  private static partial Regex SquareBracketRegex();

  public static void RemoveSquareBracket(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.RemoveSquareBracket) { return; }
    filename = SquareBracketRegex().Replace(filename, " ");
  }
}

// Order sensitive functions (last)
static partial class Simplify
{
  //Restoring release year for movie/series and appending it at the last of filename
  public static void AppendYearPost(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.AppendYear) { return; }

    filename = filename.Replace("PLACEHOLDERLEFT", "(");
    filename = filename.Replace("PLACEHOLDERRIGHT", ")");
  }

  // Remove 2+ and trailing whitespace: ` abc    def ` -> `abc def`
  [GeneratedRegex("\\s+")]
  private static partial Regex TwoOrMoreWhitespaceRegex();

  public static void ReduceWhitespace(ref string filename)
  {
    filename = TwoOrMoreWhitespaceRegex().Replace(filename, " ").Trim(' ');
  }

  // Smart Capitalization `abc aBc` -> `Abc  aBc` || Sentence Case `abc aBc` -> `Abc  ABc`
  [GeneratedRegex("[A-Z]")]
  private static partial Regex AnyUppercaseLetterRegex();

  public static void ConvertToSentenceCase(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.SmartCapitalization || !prefs.SentenceCase) { return; }

    string[] splitFilename = filename.Split(' ');
    for (int i = 0; i < splitFilename.Length; i++)
    {
      // Check to preserve words like 'USA', 'reZero'
      if (prefs.SmartCapitalization && AnyUppercaseLetterRegex().IsMatch(splitFilename[i])) { continue; }
      splitFilename[i] = Process.FirstCharToUppercase(splitFilename[i]);
    }

    filename = string.Join(' ', splitFilename);
  }

  // Article formatting (a, an, the, etc.)
  static readonly string[] articles = { "a", "an", "the", "of", "and", "in", "into", "onto", "from" };
  public static void OptimizeArticles(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.OptimizeArticles) { return; }

    string[] splitFilename = filename.Split(' ');
    for (int i = 1; i < splitFilename.Length; i++)
    {
      foreach (string article in articles)
      {
        if (splitFilename[i].ToLowerInvariant() == article) { splitFilename[i] = article; }
      }
    }

    filename = string.Join(' ', splitFilename);
  }

  // Smart Episode Dash Adder
  [GeneratedRegex("(S\\d+E\\d+)|([ES]\\d+)", RegexOptions.IgnoreCase, "en-US")]
  private static partial Regex SmartEpisodeDashRegex(); // S##E##

  public static void SmartEpisodeDash(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.SmartEpisodeDash) { return; }

    Match match = SmartEpisodeDashRegex().Match(filename);
    if (match.Success) { filename = filename.Insert(match.Index, " - "); }
  }

  // CLI friendly conversion: `abc def` -> `abc-def`
  public static void ConvertToCliFriendly(ref string filename, in JsonConfig prefs)
  {
    if (!prefs.IsCliFriendly) { return; }

    filename = filename.Replace(" ", prefs.CliSeparator);
    filename = filename.Replace("---", "-"); // Special case for smart episode dash creating 3 dashes
  }
}
