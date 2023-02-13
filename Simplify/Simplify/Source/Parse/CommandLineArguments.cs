namespace Simplify.Source.Parse;

public static class CommandLineArguments
{
  public static RuntimeConfig ParseFlags(in string[] args)
  {
    // Default flags
    bool includeFolders = false;
    bool makeChangesPermanent = false;

    // Return early with default flags if nothing is passed
    if (!args.Any()) { return new(makeChangesPermanent, includeFolders); }

    // Search for runtime config flags
    string cliFlags = string.Join(" ", args).ToLowerInvariant();
    if (cliFlags.Contains("--rename")) { makeChangesPermanent = true; }
    if (cliFlags.Contains("--includefolders")) { includeFolders = true; }

    // Return runtime config with appropriate flags
    return new(makeChangesPermanent, includeFolders);
  }

  public static string ParseWorkingDirectory(in string[] args)
  {
    string libraryPath;

    // Search for slash (/) character to determine if a working directory is given as an argument
    if (args.Any() && args[0].Replace('\\', '/').Contains('/')) { libraryPath = args[0]; }
    // Otherwise, default to the current location of the terminal
    else { libraryPath = System.Environment.CurrentDirectory; }

    // Print and return
    Console.WriteLine($"\nLibrary path: {Print.InfoText(libraryPath)}");
    return libraryPath;
  }
}
