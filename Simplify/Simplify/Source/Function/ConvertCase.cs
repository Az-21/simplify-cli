namespace Simplify;

public static partial class Function
{
  // Convert to Lower Case
  public static void ConvertToLowercase(ref string input, in bool convertToLowercase)
  {
    if (!convertToLowercase) { return; }
    input = input.ToLowerInvariant();
  }
}
