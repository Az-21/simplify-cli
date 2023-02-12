namespace Simplify;

public static class Rename
{
  private static void ApplySimplificationFunctions(ref string rename)
  {
    // Order sensitive operations (first)
    Simplify.RemoveNumbers(ref rename);
    Simplify.AppendYearPre(ref rename);

    // Order insensitive operations
    Simplify.RemoveCurvedBracket(ref rename);
    Simplify.RemoveSquareBracket(ref rename);
    Function.RemoveBlacklistedWords(ref rename);
    Simplify.RemoveNonASCII(ref rename);

    // Order sensitive operations (last)
    Simplify.AppendYearPost(ref rename);
    Simplify.SmartEpisodeDash(ref rename);
    Simplify.ReduceWhitespace(ref rename);
    Simplify.ConvertToSentenceCase(ref rename);
    Simplify.OptimizeArticles(ref rename);
    Simplify.ConvertToCliFriendly(ref rename);
    Simplify.ConvertToLowercase(ref rename);
  }

  public static void SimplifyFile(in RuntimeConfig runtimePreferences, string fullPath)
  {
    // Create file metadata object [creates an immutable object (record)]
    var file = new FileMetadata(fullPath.Replace('\\', '/'));
    string rename = file.Name;
    ApplySimplificationFunctions(ref rename);

    // Full address of processed filename
    string simplifiedFileAddress = $"{file.Directory}/{rename}{file.Extension}";

    // Already simplified form
    if (file.Name == rename)
    {
      Print.NoFileChangeRequired(file);
      Global.MutableCounter.Unchanged++;
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
        Global.MutableCounter.Renamed++;
      }

      // Actual conflict
      else
      {
        Print.FileRenameConflict(file, rename);
        Global.MutableCounter.Conflict++;
      }
    }

    // Can be renamed without any conflict
    else
    {
      Print.FileSuccess(file, rename);
      if (runtimePreferences.MakeChangesPermanent) { File.Move(file.FullPath, simplifiedFileAddress); }
      Global.MutableCounter.Renamed++;
    }
  }

  public static void SimplifyFolder(in RuntimeConfig runtimePreferences, string fullPath)
  {
    // Create folder metadata object [creates an immutable object (record)]
    var folder = new FolderMetadata(fullPath.Replace('\\', '/'));
    string rename = folder.Name;
    ApplySimplificationFunctions(ref rename);

    // Full address of processed filename
    string simplifiedFolderAddress = $"{folder.ParentDirectory}/{rename}";

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
