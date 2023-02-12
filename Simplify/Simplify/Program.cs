namespace Simplify;
static class MainProgram
{
  // Load preferences from config file as an immutable object
  static readonly JsonConfig Config = Preferences.LoadConfig();

  public static void Main(string[] args)
  {
    // Process input arguments
    bool includeFolders = false;
    bool makeChangesPermanent = false;

    string libraryPath = Config.LibraryPath;
    if (args.Any())
    {
      if (args[0].Replace('\\', '/').Contains('/'))
      {
        libraryPath = args[0]; // if path is provided by argument, overrule config.json path
      }
      Console.WriteLine($"\nLibrary path: {Print.InfoText(libraryPath)}");

      // Flags
      string cliFlags = string.Join(" ", args).ToLowerInvariant();

      if (cliFlags.Contains("--rename")) { makeChangesPermanent = true; }
      if (cliFlags.Contains("--includefolders")) { includeFolders = true; }
    }

    // Update runtime preferences
    RuntimePreferences runtimePreferences = new(makeChangesPermanent, includeFolders);

    // Init counters (unchanged, conflict, renamed)
    Counter counter = new();

    // Rename folders
    if (runtimePreferences.RenameFolders)
    {
      string[] folders = Scan.Folders(libraryPath, Config);
      if (folders.Any() && Print.FolderConfirmation(folders, makeChangesPermanent))
      {
        for (int i = folders.Length - 1; i >= 0; i--)
        {
          // WARNING: Reversed loop order because innermost folder must be renamed first
          // Simplifying the outermost folder first will break address of inner folders
          string fullPath = folders[i];
          Rename.SimplifyFolder(in Config, in runtimePreferences, fullPath, ref counter);
        }
      }
      Console.WriteLine("\n\n");
    }

    // Rename files
    var files = Scan.Files(libraryPath, in Config);
    if (files.Any() && Print.FilesConfirmation(files, makeChangesPermanent))
    {
      foreach (string fullPath in files) { Rename.SimplifyFile(in Config, in runtimePreferences, fullPath, ref counter); }
    }

    // Print results
    Print.Results(counter.Renamed, counter.Conflict, counter.Unchanged);
  }
}
