namespace Simplify;

// NOTE: all of the following functions are 'call by reference' to increase performance
// Order sensitive functions (first)
public static partial class Simplify
{

}

// Order insensitive operations
public static partial class Simplify
{

}

// Order sensitive functions (last)
static partial class Simplify
{
  // CLI friendly conversion: `abc def` -> `abc-def`
  public static void ConvertToCliFriendly(ref string filename)
  {
    if (!Global.ImmutableConfig.IsCliFriendly) { return; }

    filename = filename.Replace(" ", Global.ImmutableConfig.CliSeparator);
    filename = filename.Replace("---", "-"); // Special case for smart episode dash creating 3 dashes
  }
}
