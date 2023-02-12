using System.Text.RegularExpressions;

namespace Simplify;

public static partial class Function
{
  private const string space = " ";
  public static void RemoveBlacklistedWords(ref string filename)
  {
    string[] blacklist = Global.ImmutableConfig.Blacklist
      .Split(',')
      .Select(x => x.Trim())
      .ToArray();

    for (int i = 0; i < blacklist.Length; i++)
    {
      filename = filename.Replace(
        blacklist[i],
        space,
        (StringComparison)RegexOptions.IgnoreCase
      );
    }
  }
}
