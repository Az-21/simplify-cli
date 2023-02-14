namespace Test;

[TestClass]
public sealed class UnitTestAppendYear
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "1999 Movie";
    const string expected = "  Movie  (1999)"; // AppendYearPre pads the output both sides -> AppendYearPost pads right side

    // Simple test where there is only one four digit number with 19##
    Simplify.Function.AppendYearPre(ref filename, true);
    Simplify.Function.AppendYearPost(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "2014 Movie";
    const string expected = "  Movie  (2014)";

    // Simple test where there is only one four digit number with 20##
    Simplify.Function.AppendYearPre(ref filename, true);
    Simplify.Function.AppendYearPost(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test3()
  {
    // Expected I/O
    string filename = "2100 Movie";
    const string expected = " 2100 Movie "; // Side-effect is expected

    // Simple test where there is no four digit number with 19## or 20##
    Simplify.Function.AppendYearPre(ref filename, true);
    Simplify.Function.AppendYearPost(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test4()
  {
    // Expected I/O
    string filename = "1999 Movie";
    const string expected = "1999 Movie"; // AppendYearPre will not pad both sides as it is returned early

    // Simple test where the config is inactive and should not affect 19## and 20##
    Simplify.Function.AppendYearPre(ref filename, false);
    Simplify.Function.AppendYearPost(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test5()
  {
    // Expected I/O
    string filename = "2001: A Space Adventure [1968] HEVC";
    const string expected = " 2001: A Space Adventure [] HEVC  (1968)";

    // Test to verify scan is done right to left
    Simplify.Function.AppendYearPre(ref filename, true);
    Simplify.Function.AppendYearPost(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test6()
  {
    // Expected I/O
    string filename = "2001: A Space Adventure [1920x1080]";
    const string expected = " 2001: A Space Adventure [1920x1080] "; // Side-effect is expected

    // Test to verify the function omits "1920" specifically (as it is most likely resolution)
    Simplify.Function.AppendYearPre(ref filename, true);
    Simplify.Function.AppendYearPost(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test7()
  {
    // Expected I/O
    string filename = "20000000 000020000 1997 198100 001981 19000 000190000";
    const string expected = " 20000000 000020000  198100 001981 19000 000190000  (1997)";

    // Verify the function strictly matches four digit numbers only. Ignores if it's a subset of a larger number.
    Simplify.Function.AppendYearPre(ref filename, true);
    Simplify.Function.AppendYearPost(ref filename, true);
    Assert.AreEqual(expected, filename);
  }
}
