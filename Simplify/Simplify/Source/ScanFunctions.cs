namespace Simplify;
public static class Scan
{
  // Invalid path check
  public static void CheckIfDirectoryExists(string path)
  {
    if (!Directory.Exists(path))
    {
      Print.ErrorBlock();
      Console.WriteLine($"Directory {Print.ErrorText(path)} does not exist.\nExiting...");
      Environment.Exit(1);
    }
  }

  // Crawl the directory to find files with required extension
  public static IReadOnlyList<string> Files(string path)
  {
    // Invalid path check
    CheckIfDirectoryExists(path);

    // Load files in the directory
    string[] extensionList = Process.ConvertToExtensionList();

    SearchOption searchOption = Global.ImmutableConfig.GetAllDirectories ?
        SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

    IReadOnlyList<string> files =
        extensionList.SelectMany(f => Directory.GetFiles(path, f, searchOption)).ToList();

    // No files found check | Equivalent to `files.isEmpty()`
    if (!files.Any())
    {
      Print.InfoBlock();
      Console.WriteLine($"No file found with extension [{Print.InfoText(Global.ImmutableConfig.Extensions)}] in {Print.InfoText(path)}");
    }

    return files;
  }

  // Crawl the directory to find folders and subfolders
  public static IReadOnlyList<string> Folders(string path)
  {
    SearchOption searchOption = Global.ImmutableConfig.GetAllDirectories ?
          SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

    IReadOnlyList<string> folders = Directory.GetDirectories(path, "*", searchOption);

    // No folders found check | Equivalent to `files.isEmpty()`
    if (!folders.Any())
    {
      Print.InfoBlock();
      Console.WriteLine($"No folder found in {Print.InfoText(path)}");
    }

    return folders;
  }
}
