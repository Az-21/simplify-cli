using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  // Remove All Numbers
  [GeneratedRegex(@"\d")]
  private static partial Regex AnyNumberRegex();

  public static void RemoveNumbers(ref string input, in bool removeNumbers)
  {
    if (!removeNumbers) { return; }
    input = AnyNumberRegex().Replace(input, SpaceString);
  }
}
