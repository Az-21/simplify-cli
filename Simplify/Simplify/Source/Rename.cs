namespace Simplify;
public static class Rename
{
  private static void ApplySimplificationFunctions(ref string filename)
  {
    // Order sensitive operations (first)
    Function.RemoveNumbers(ref filename, Global.ImmutableConfig.RemoveNumbers);
    Function.AppendYearPre(ref filename, Global.ImmutableConfig.AppendYear);
    Function.AppendSeasonAndOrEpisodePre(ref filename, Global.ImmutableConfig.AppendSeasonAndOrEpisode);

    // Order insensitive operations
    Function.RemoveCurlyBrackets(ref filename, Global.ImmutableConfig.RemoveCurlyBrackets);
    Function.RemoveCurvedBrackets(ref filename, Global.ImmutableConfig.RemoveCurvedBrackets);
    Function.RemoveSquareBrackets(ref filename, Global.ImmutableConfig.RemoveSquareBrackets);
    Function.RemoveBlacklistedWords(ref filename, Global.ImmutableConfig.Blacklist);
    Function.RemoveNonAsciiCharacters(ref filename, Global.ImmutableConfig.RemoveNonAsciiCharacters);

    // Order sensitive operations (last)
    Function.AppendYearPost(ref filename, Global.ImmutableConfig.AppendYear);
    Function.AppendSeasonAndOrEpisodePost(ref filename, Global.ImmutableConfig.AppendSeasonAndOrEpisode, Global.ImmutableConfig.SeasonAndOrEpisodePrefix);
    Function.ReduceWhitespace(ref filename); // Upcoming functions will be faster without extra whitespaces
    Function.ConvertToSentenceCase(ref filename, Global.ImmutableConfig.SentenceCase, Global.ImmutableConfig.SmartCapitalization);
    Function.OptimizeArticles(ref filename, Global.ImmutableConfig.OptimizeArticles);
    Function.ConvertToLowercase(ref filename, Global.ImmutableConfig.ConvertToLowercase);
  }

  public static void SimplifyFile(in RuntimeConfig runtimePreferences, in string fullPath)
  {
    // Create file metadata object [creates an immutable object (record)]
    FileMetadata file = new(fullPath);
    string filename = file.Name;
    ApplySimplificationFunctions(ref filename);

    // Full address of processed filename
    string simplifiedFileAddress = Path.Combine(file.Directory, $"{filename}{file.Extension}");

    // Already simplified form
    if (file.Name == filename)
    {
      Print.NoFileChangeRequired(file);
      Global.MutableCounter.Unchanged++;
    }

    // Rename conflict
    else if (File.Exists(simplifiedFileAddress))
    {
      // Check for Windows specific case-insensitive directory
      if (string.Equals(file.Name, filename, StringComparison.OrdinalIgnoreCase))
      {
        if (runtimePreferences.MakeChangesPermanent)
        {
          // A -> B -> C is done to prevent case-insensitive rename
          // Direct A -> C may result in `file.ext` -> `File.ext` which will throw an exception
          File.Move(file.FullPath, $"{file.Directory}/TEMP_SIMPLIFY_RENAME");
          File.Move($"{file.Directory}/TEMP_SIMPLIFY_RENAME", simplifiedFileAddress);
        }
        Print.FileSuccess(file, filename);
        Global.MutableCounter.Renamed++;
      }

      // Actual conflict
      else
      {
        Print.FileRenameConflict(file, filename);
        Global.MutableCounter.Conflict++;
      }
    }

    // Can be renamed without any conflict
    else
    {
      Print.FileSuccess(file, filename);
      if (runtimePreferences.MakeChangesPermanent) { File.Move(file.FullPath, simplifiedFileAddress); }
      Global.MutableCounter.Renamed++;
    }
  }

  public static void SimplifyFolder(in RuntimeConfig runtimePreferences, in string fullPath)
  {
    // Create folder metadata object [creates an immutable object (record)]
    FolderMetadata folder = new(fullPath);
    string rename = folder.Name;
    ApplySimplificationFunctions(ref rename);

    // Full address of processed folder
    string simplifiedFolderAddress = Path.Combine(folder.ParentDirectory, rename);

    // Already simplified form
    if (folder.Name == rename)
    {
      Print.NoFolderChangeRequired(folder);
      Global.MutableCounter.Unchanged++;
    }

    // Rename conflict
    else if (File.Exists(simplifiedFolderAddress))
    {
      // Check for Windows specific case-insensitive directory
      if (string.Equals(folder.Name, rename, StringComparison.OrdinalIgnoreCase))
      {
        if (runtimePreferences.MakeChangesPermanent)
        {
          // A -> B -> C is done to prevent case-insensitive rename
          // Direct A -> C may result in `FOLDER` -> `Folder` which will throw an exception
          Directory.Move(folder.FullPath, $"{folder.FullPath}_TEMP_SIMPLIFY_RENAME");
          Directory.Move($"{folder.FullPath}_TEMP_SIMPLIFY_RENAME", simplifiedFolderAddress);
        }
        Print.FolderSuccess(folder, rename);
        Global.MutableCounter.Renamed++;
      }

      // Actual conflict
      else
      {
        Print.FolderRenameConflict(folder, rename);
        Global.MutableCounter.Conflict++;
      }
    }

    // Can be renamed without any conflict
    else
    {
      Print.FolderSuccess(folder, rename);
      if (runtimePreferences.MakeChangesPermanent) { Directory.Move(folder.FullPath, simplifiedFolderAddress); }
      Global.MutableCounter.Renamed++;
    }
  }
}
