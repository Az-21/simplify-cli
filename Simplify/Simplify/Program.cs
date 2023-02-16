namespace Simplify;

public static class Global
{
  // Load preferences from config file as an immutable object
  public static readonly JsonConfig ImmutableConfig = Preferences.LoadConfig();

  // Initialize counters for unchanged, conflict, renamed files (0, 0, 0)
  public static readonly Counter MutableCounter = new();
}

public static class MainProgram
{
  public static void Main(string[] args)
  {
    // Parse library path from CLI. Defaults to path in Config.json
    string LibraryPath = Source.Parse.CommandLineArguments.ParseWorkingDirectory(args);

    // Parse runtime config flags from CLI
    RuntimeConfig RuntimeFlag = Source.Parse.CommandLineArguments.ParseFlags(args);

    // Rename folders
    if (RuntimeFlag.RenameFolders)
    {
      IReadOnlyList<string> folders = Scan.Folders(LibraryPath);
      if (folders.Any() && Print.FolderConfirmation(folders, RuntimeFlag.MakeChangesPermanent))
      {
        // WARNING: Reversed loop order because innermost folder must be renamed first
        // Simplifying the outermost folder first will break address of inner folders
        for (int i = folders.Count - 1; i >= 0; i--)
        {
          string fullPath = folders[i];
          Rename.SimplifyFolder(in RuntimeFlag, fullPath);
        }
      }
      Console.WriteLine("\n\n");
    }

    // Rename files
    IReadOnlyList<string> files = Scan.Files(LibraryPath);
    if (files.Any() && Print.FilesConfirmation(files, RuntimeFlag.MakeChangesPermanent))
    {
      foreach (string fullPath in files)
      {
        Rename.SimplifyFile(in RuntimeFlag, fullPath);
      }
    }

    // Print results
    Print.Results();
  }
}
