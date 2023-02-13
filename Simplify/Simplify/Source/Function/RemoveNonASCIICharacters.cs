using System.Text.RegularExpressions;

namespace Simplify;
public static partial class Function
{
  // Remove non-ASCII characters
  [GeneratedRegex(@"[^\u0000-\u007F]+")]
  private static partial Regex NonAsciiCharacterRegex();

  public static void RemoveNonAsciiCharacters(ref string input, in bool removeNonAsciiCharacters)
  {
    if (!removeNonAsciiCharacters) { return; }
    input = NonAsciiCharacterRegex().Replace(input, Space);
  }
}
