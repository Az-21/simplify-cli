using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  // Convert to Lower Case
  public static void ConvertToLowercase(ref string input, in bool convertToLowercase)
  {
    if (!convertToLowercase) { return; }
    input = input.ToLowerInvariant();
  }
}

public static partial class Function
{
  // Replace all whitespace with a single space AND then trim
  [GeneratedRegex(@"[A-Z]")]
  private static partial Regex AnyUppercaseLetterRegex();

  public static void ConvertToSentenceCase(ref string input, in bool sentenceCase, in bool smartCapitalization)
  {
    if (!sentenceCase || !smartCapitalization) { return; }

    string[] splitInput = input.Split(Space);
    for (int i = 0; i < splitInput.Length; i++)
    {
      // Check to preserve words like 'USA', 'reZero' if the SmartCapitalization is turned on
      if (smartCapitalization && AnyUppercaseLetterRegex().IsMatch(splitInput[i])) { continue; }

      // Otherwise, **destructively** modify the word to convert first character to uppercase and rest to lowercase
      CapitalizeWord(ref splitInput[i]);
    }

    input = string.Join(Space, splitInput);
  }

  private static void CapitalizeWord(ref string input)
  {
    if (string.IsNullOrEmpty(input)) { return; }
    input = char.ToUpperInvariant(input[0]) + input[1..].ToLowerInvariant();
  }
}
