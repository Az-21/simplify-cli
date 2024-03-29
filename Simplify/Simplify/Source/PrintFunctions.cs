﻿using Pastel;

namespace Simplify;

public static class Print
{
  // Colors
  const string red = "#F7525B";
  const string green = "#87E987";
  const string blue = "#7FACF7";
  const string yellow = "FFB900";
  const string gray = "68768A";
  const string white = "FFFFFF";
  const string black = "000000";

  // Reusable block identifiers
  public static void ErrorBlock(string message = "ERROR")
  {
    Console.Write($" {message} ".Pastel(black).PastelBg(red));
    Console.Write(" ");
  }
  public static void WarningBlock(string message = "WARNING")
  {
    Console.Write($" {message} ".Pastel(black).PastelBg(yellow));
    Console.Write(" ");
  }
  public static void SuccessBlock(string message = "SUCCESS")
  {
    Console.Write($" {message} ".Pastel(black).PastelBg(green));
    Console.Write(" ");
  }
  public static void InfoBlock(string message = "INFO")
  {
    Console.Write($" {message} ".Pastel(black).PastelBg(blue));
    Console.Write(" ");
  }

  // Colorize strings
  public static string InfoText(string message) => $"{message}".Pastel(blue);
  public static string ErrorText(string message) => $"{message}".Pastel(red);
  public static string GrayedText(string message) => $"{message}".Pastel(gray);
  public static string SuccessText(string message) => $"{message}".Pastel(green);
  public static string WarningText(string message) => $"{message}".Pastel(yellow);

  // Print selected files and get confirmation from user
  public static bool FilesConfirmation(IEnumerable<string> files, bool makeChangesPermanent)
  {
    InfoBlock();
    Console.WriteLine("Following files will be affected");

    foreach (string fileAddress in files)
    {
      string path = Path.GetFullPath(fileAddress);
      Console.WriteLine(InfoText(path));
    }

    return CommonConfirmation(makeChangesPermanent);
  }

  public static bool FolderConfirmation(IReadOnlyList<string> folders, bool makeChangesPermanent)
  {
    InfoBlock();
    Console.WriteLine("Following items will be affected");

    for (int i = 0; i < folders.Count; i++) { Console.WriteLine(InfoText(folders[i])); }
    return CommonConfirmation(makeChangesPermanent);
  }

  // Common confirmation user input shared between both files and folders
  private static bool CommonConfirmation(bool makeChangesPermanent)
  {
    Console.WriteLine();
    if (!makeChangesPermanent)
    {
      InfoBlock();
      Console.WriteLine($"You are currently in preview mode. Pass {"--rename".Pastel(blue)} to make changes permanent.");
    }
    else
    {
      WarningBlock();
      Console.WriteLine("\nYou are currently in rename mode.");
      Console.WriteLine(WarningText("Changes will be permanent. Make sure you have performed a dry run in preview mode."));
    }

    Console.WriteLine($"\nRename selected items? ({"y".Pastel(green)}/{"N".Pastel(red)})");
    if (Console.ReadLine() != "y") { return false; }
    Console.WriteLine();
    return true;
  }

  // Print 'no change required' message
  public static void NoFileChangeRequired(FileMetadata file)
  {
    InfoBlock();
    Console.WriteLine($"[{GrayedText(file.Directory)}]");
    Console.WriteLine($"{InfoText(file.NameWithExtension)} is already in simplified form\n");
  }
  public static void NoFolderChangeRequired(FolderMetadata folder)
  {
    InfoBlock("INFO");
    Console.WriteLine($"[{GrayedText(folder.ParentDirectory)}]");
    Console.WriteLine($"{InfoText(folder.Name)} is already in simplified form\n");
  }

  // Print 'rename conflict' message
  public static void FileRenameConflict(FileMetadata file, string rename)
  {
    WarningBlock("FILE RENAME CONFLICT");
    Console.WriteLine($"[{GrayedText(file.Directory)}]");
    Console.WriteLine(GrayedText(file.NameWithExtension));
    Console.WriteLine(WarningText($"{rename}{file.Extension}"));
    Console.WriteLine("File with same name already exists. Fix manually.\n");
  }

  // Print 'fix manually' message in case of conflict
  public static void FolderRenameConflict(FolderMetadata folder, string rename)
  {
    WarningBlock("FOLDER RENAME CONFLICT");
    Console.WriteLine($"[{GrayedText(folder.ParentDirectory)}]");
    Console.WriteLine(GrayedText(folder.Name));
    Console.WriteLine(WarningText($"{rename}"));
    Console.WriteLine("Folder with same name already exists. Fix manually.\n");
  }

  // Print 'success' message
  public static void FileSuccess(FileMetadata file, string rename)
  {
    SuccessBlock("FILE RENAMED");
    Console.WriteLine($"[{GrayedText(file.Directory)}]");
    Console.WriteLine(GrayedText(file.NameWithExtension));
    Console.WriteLine(SuccessText($"{rename}{file.Extension}\n"));
  }

  // Print before and after versions of folder
  public static void FolderSuccess(FolderMetadata folder, string rename)
  {
    SuccessBlock("FOLDER RENAMED");
    Console.WriteLine($"[{GrayedText(folder.ParentDirectory)}]");
    Console.WriteLine(GrayedText(folder.Name));
    Console.WriteLine(SuccessText($"{rename}\n"));
  }

  // Print results and stats
  public static void Results()
  {
    string renamed = Global.MutableCounter.Renamed.ToString();
    string unchanged = Global.MutableCounter.Unchanged.ToString();
    string conflict = Global.MutableCounter.Conflict.ToString();

    Console.WriteLine("\n SUMMARY ".Pastel(white).PastelBg(gray));
    Console.WriteLine($"Renamed files: {SuccessText(renamed)}");
    Console.WriteLine($"Already simplified : {InfoText(unchanged)}");
    Console.WriteLine($"Rename conflicts: {WarningText(conflict)}");
  }
}
