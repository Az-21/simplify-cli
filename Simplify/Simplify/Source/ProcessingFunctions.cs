namespace Simplify;

public static class Process
{
  // Convert comma separated extensions to array of extension with `.` (dot) prefix
  public static string[] ConvertToExtensionList()
  {
    return Global.ImmutableConfig.Extensions
        .Split(',')
        .Select(x => $"*.{x.Trim()}")
        .ToArray();
  }
}
