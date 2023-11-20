namespace Simplify;

// Article formatting (a, an, the, etc.)
public static partial class Function
{
  private static readonly HashSet<string> Articles = ["a", "an", "the", "of", "and", "in"];
  public static void OptimizeArticles(ref string input, in bool optimizeArticles)
  {
    if (!optimizeArticles) { return; }

    string[] splitFilename = input.Split(Space);
    for (int i = 1; i < splitFilename.Length; i++) // Start from [1] to ignore first word
    {
      foreach (string article in Articles)
      {
        if (splitFilename[i].Equals(article, StringComparison.InvariantCultureIgnoreCase)) {
          splitFilename[i] = article;
        }
      }
    }

    input = string.Join(Space, splitFilename);
  }
}
