using System.Text.Json;
namespace Simplify;

// Class to hold deserialized JSON preferences in Config.json
public static class Preferences
{
  // Load preferences from 'config.json'
  public static JsonConfig LoadConfig()
  {
    // Generate location of 'Config.json'
    string configPath = GetJsonConfigLocation();

    // Check if the 'Config.json' exists
    if (!File.Exists(configPath)) { PrintConfigFileNotFoundMessage(); }

    // Deserialize
    return DeserializeJsonConfig(configPath);
  }

  private static string GetJsonConfigLocation()
  {
    const string jsonPath = "Config/Config.json";
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    return Path.Combine(baseDirectory, jsonPath);
  }

  private static void PrintConfigFileNotFoundMessage()
  {
    Print.ErrorBlock();
    Console.WriteLine("Config file not found, please re-install app.");
    Environment.Exit(1);
  }

  private static JsonConfig DeserializeJsonConfig(in string configPath)
  {
    JsonSerializerOptions options = new()
    {
      ReadCommentHandling = JsonCommentHandling.Skip,
      AllowTrailingCommas = true,
    };

    // Deserialize
    try
    {
      return JsonSerializer.Deserialize<JsonConfig>(File.ReadAllText(configPath), options)!;
    }
    catch (Exception)
    {
      Print.ErrorBlock();
      Console.WriteLine("Invalid JSON. Check for typos, or re-install app.");
      Environment.Exit(1);
      return null; // Just to satisfy compiler. This is never returned.
    }
  }
}

// Class to hold runtime config passed via CLI (not present in Config.json)
public sealed record class RuntimeConfig(bool MakeChangesPermanent, bool RenameFolders);

// Immutable class to help deserialize JSON
public sealed record class JsonConfig(
  bool GetAllDirectories,
  string Extensions,
  string Blacklist,
  bool RemoveCurlyBrackets,
  bool RemoveCurvedBrackets,
  bool RemoveSquareBrackets,
  bool SentenceCase,
  bool SmartCapitalization,
  bool OptimizeArticles,
  bool RemoveNonAsciiCharacters,
  bool AppendYear,
  bool AppendSeasonAndOrEpisode,
  string SeasonAndOrEpisodePrefix,
  bool RemoveNumbers,
  bool ConvertToLowercase
);
