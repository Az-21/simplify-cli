namespace Simplify;

public static class Rename
{
  private static void ApplySimplificationFunctions(ref string rename, in JsonConfig prefs)
  {
    // Order sensitive operations (first) [NOTE: all are call by reference] | prefs is const reference
    Simplify.RemoveNumbers(ref rename, in prefs);
    Simplify.AppendYearPre(ref rename, in prefs);

    // Order insensitive operations [NOTE: all are call by reference] | prefs is const reference
    Simplify.RemoveCurvedBracket(ref rename, in prefs);
    Simplify.RemoveSquareBracket(ref rename, in prefs);
    Function.RemoveBlacklistedWords(ref rename, in prefs);
    Simplify.RemoveNonASCII(ref rename, in prefs);

    // Order sensitive operations (last) [NOTE: all are call by reference] | prefs is const reference
    Simplify.AppendYearPost(ref rename, in prefs);
    Simplify.SmartEpisodeDash(ref rename, in prefs);
    Simplify.ReduceWhitespace(ref rename);
    Simplify.ConvertToSentenceCase(ref rename, in prefs);
    Simplify.OptimizeArticles(ref rename, in prefs);
    Simplify.ConvertToCliFriendly(ref rename, in prefs);
    Simplify.ConvertToLowercase(ref rename, in prefs);
  }

  public static void SimplifyFile(in JsonConfig prefs, in RuntimeConfig runtimePreferences, string fullPath, ref Counter counter)
  {
    // Create file metadata object [creates an immutable object (record)]
    var file = new FileMetadata(fullPath.Replace('\\', '/'));
    string rename = file.Name;
    ApplySimplificationFunctions(ref rename, prefs);

    // Full address of processed filename
    string simplifiedFileAddress = $"{file.Directory}/{rename}{file.Extension}";


    // Already simplified form
    if (file.Name == rename)
    {
      Print.NoFileChangeRequired(file);
      counter.Unchanged++;
    }

    // Rename conflict
    else if (File.Exists(simplifiedFileAddress))
    {
      // Check for Windows specific case-insensitive directory
      if (string.Equals(file.Name, rename, StringComparison.OrdinalIgnoreCase))
      {
        if (runtimePreferences.MakeChangesPermanent)
        {
          File.Move(file.FullPath, $"{file.Directory}/TEMP_SIMPLIFY_RENAME");
          File.Move($"{file.Directory}/TEMP_SIMPLIFY_RENAME", simplifiedFileAddress);
        }
        Print.FileSuccess(file, rename);
        counter.Renamed++;
      }

      // Actual conflict
      else
      {
        Print.FileRenameConflict(file, rename);
        counter.Conflict++;
      }
    }

    // Can be renamed without any conflict
    else
    {
      Print.FileSuccess(file, rename);
      if (runtimePreferences.MakeChangesPermanent) { File.Move(file.FullPath, simplifiedFileAddress); }
      counter.Renamed++;
    }
  }

  public static void SimplifyFolder(in JsonConfig prefs, in RuntimeConfig runtimePreferences, string fullPath, ref Counter counter)
  {
    // Create folder metadata object [creates an immutable object (record)]
    var folder = new FolderMetadata(fullPath.Replace('\\', '/'));
    string rename = folder.Name;
    ApplySimplificationFunctions(ref rename, in prefs);

    // Full address of processed filename
    string simplifiedFolderAddress = $"{folder.ParentDirectory}/{rename}";

    // Already simplified form
    if (folder.Name == rename)
    {
      Print.NoFolderChangeRequired(folder);
      counter.Unchanged++;
    }

    // Rename conflict
    else if (File.Exists(simplifiedFolderAddress))
    {
      // Check for Windows specific case-insensitive directory
      if (string.Equals(folder.Name, rename, StringComparison.OrdinalIgnoreCase))
      {
        if (runtimePreferences.MakeChangesPermanent)
        {
          Directory.Move(folder.FullPath, $"{folder.FullPath}_TEMP_SIMPLIFY_RENAME");
          Directory.Move($"{folder.FullPath}_TEMP_SIMPLIFY_RENAME", simplifiedFolderAddress);
        }
        Print.FolderSuccess(folder, rename);
        counter.Renamed++;
      }

      // Actual conflict
      else
      {
        Print.FolderRenameConflict(folder, rename);
        counter.Conflict++;
      }
    }

    // Can be renamed without any conflict
    else
    {
      Print.FolderSuccess(folder, rename);
      if (runtimePreferences.MakeChangesPermanent) { Directory.Move(folder.FullPath, simplifiedFolderAddress); }
      counter.Renamed++;
    }
  }
}
