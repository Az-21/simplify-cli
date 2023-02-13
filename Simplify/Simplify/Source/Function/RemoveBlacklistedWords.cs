using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  public static void RemoveBlacklistedWords(ref string input, in string blacklist)
  {
    string[] rmList = blacklist
      .Split(',')
      .Select(x => x.Trim())
      .ToArray();

    for (int i = 0; i < rmList.Length; i++)
    {
      input = input.Replace(rmList[i], Space, (StringComparison)RegexOptions.IgnoreCase);
    }
  }
}
