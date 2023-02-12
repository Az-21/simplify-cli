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
  public static IEnumerable<string> Files(string path)
  {
    // Invalid path check
    CheckIfDirectoryExists(path);

    // Load files in the directory
    string[] extensionList = Process.ConvertToExtensionList();
    IEnumerable<string> files = Global.ImmutableConfig.GetAllDirectories ?
        extensionList.SelectMany(f => Directory.GetFiles(path, f, SearchOption.AllDirectories)) :
        extensionList.SelectMany(f => Directory.GetFiles(path, f, SearchOption.TopDirectoryOnly));

    // No files found check | Equivalent to `files.isEmpty()`
    if (!files.Any())
    {
      Print.InfoBlock();
      Console.WriteLine($"No file found with extension [{Print.InfoText(Global.ImmutableConfig.Extensions)}] in {Print.InfoText(path)}");
    }

    return files;
  }

  // Crawl the directory to find folders and subfolders
  public static string[] Folders(string path)
  {
    string[] folders = Global.ImmutableConfig.GetAllDirectories ?
        Directory.GetDirectories(path, "*", SearchOption.AllDirectories) :
        Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

    for (int i = 0; i < folders.Length; i++)
    {
      folders[i] = folders[i].Replace("\\", "/");
    }

    // No folders found check
    if (!folders.Any())
    {      // equivalent to `files.isEmpty()`
      Print.InfoBlock();
      Console.WriteLine($"No folder found in {Print.InfoText(path)}");
    }

    return folders;
  }
}
