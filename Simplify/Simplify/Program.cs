namespace Simplify;
static class MainProgram
{
  // Load preferences from config file as an immutable object
  static readonly JsonConfig Config = Preferences.LoadConfig();

  public static void Main(string[] args)
  {
    // Parse library path from CLI. Defaults to path in Config.json
    string LibraryPath = Source.Parse.CommandLineArguments.ParseWorkingDirectory(args, Config);

    // Parse runtime config flags from CLI
    RuntimeConfig RuntimeFlag = Source.Parse.CommandLineArguments.ParseFlags(args);

    // Initialize counters for unchanged, conflict, renamed files (0, 0, 0)
    Counter AffectedCounter = new();

    // Rename folders
    if (RuntimeFlag.RenameFolders)
    {
      string[] folders = Scan.Folders(LibraryPath, Config);
      if (folders.Any() && Print.FolderConfirmation(folders, RuntimeFlag.MakeChangesPermanent))
      {
        for (int i = folders.Length - 1; i >= 0; i--)
        {
          // WARNING: Reversed loop order because innermost folder must be renamed first
          // Simplifying the outermost folder first will break address of inner folders
          string fullPath = folders[i];
          Rename.SimplifyFolder(in Config, in RuntimeFlag, fullPath, ref AffectedCounter);
        }
      }
      Console.WriteLine("\n\n");
    }

    // Rename files
    var files = Scan.Files(LibraryPath, in Config);
    if (files.Any() && Print.FilesConfirmation(files, RuntimeFlag.MakeChangesPermanent))
    {
      foreach (string fullPath in files)
      {
        Rename.SimplifyFile(in Config, in RuntimeFlag, fullPath, ref AffectedCounter);
      }
    }

    // Print results
    Print.Results(
      AffectedCounter.Renamed,
      AffectedCounter.Conflict,
      AffectedCounter.Unchanged
    );
  }
}
