using System.Text.Json;

namespace Simplify;

// Class to hold runtime config passed via CLI (not present in Config.json)
public sealed record class RuntimeConfig(bool MakeChangesPermanent, bool RenameFolders);

// Class to hold deserialized JSON preferences in Config.json
public static class Preferences
{
  // Load preferences from 'config.json'
  public static JsonConfig LoadConfig()
  {
    // Get location of 'Config.json'
    string configPath = GetJsonConfigLocation();

    // Not found check
    if (!File.Exists(configPath))
    {
      Print.ErrorBlock();
      Console.WriteLine("Config file not found, please re-install app.");
      Environment.Exit(1);
    }

    // Deserialize
    return JsonSerializer.Deserialize<JsonConfig>(File.ReadAllText(configPath))!;
  }

  private static string GetJsonConfigLocation()
  {
    const string jsonPath = "Config/Config.json";
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    return Path.Combine(baseDirectory, jsonPath);
  }
}

// Immutable class to help deserialize JSON
public sealed record class JsonConfig(
  bool GetAllDirectories,
  string Extensions,
  string Blacklist,
  bool RemoveCurvedBracket,
  bool RemoveSquareBracket,
  bool IsCliFriendly,
  string CliSeparator,
  bool SentenceCase,
  bool SmartCapitalization,
  bool OptimizeArticles,
  bool RemoveNonAscii,
  bool ConvertToLowercase,
  bool SmartEpisodeDash,
  bool RemoveNumbers,
  bool AppendYear
);
