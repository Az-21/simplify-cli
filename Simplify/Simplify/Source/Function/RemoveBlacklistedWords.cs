using System.Text.RegularExpressions;
namespace Simplify;

public static partial class Function
{
  public static void RemoveBlacklistedWords(ref string input)
  {
    string[] blacklist = Global.ImmutableConfig.Blacklist
      .Split(',')
      .Select(x => x.Trim())
      .ToArray();

    for (int i = 0; i < blacklist.Length; i++)
    {
      input = input.Replace(blacklist[i], Space, (StringComparison)RegexOptions.IgnoreCase);
    }
  }
}
