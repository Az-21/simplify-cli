namespace Test;

[TestClass]
public sealed class UnitTestRemoveBrackets
{
  [TestMethod]
  public void Test01()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "  (Text 0) [Text 0]  ()[] Text 0";

    // Test curly brackets removal
    Simplify.Function.RemoveCurlyBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test02()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";

    // Test curly brackets removal in inactive state
    Simplify.Function.RemoveCurlyBrackets(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test03()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0}   [Text 0] {} [] Text 0";

    // Test curved brackets removal
    Simplify.Function.RemoveCurvedBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test04()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";

    // Test curved brackets removal in inactive state
    Simplify.Function.RemoveCurvedBrackets(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test05()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0)   {}()  Text 0";

    // Test square brackets removal
    Simplify.Function.RemoveSquareBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test06()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";

    // Test square brackets removal in inactive state
    Simplify.Function.RemoveSquareBrackets(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test07()
  {
    // Expected I/O
    string filename = "{ Level 3 { Level 2 { Level 1 } } } (abc) [XYZ]";
    const string expected = "  (abc) [XYZ]";

    // Test nested curly brackets removal
    Simplify.Function.RemoveCurlyBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test08()
  {
    // Expected I/O
    string filename = "( Level 3 ( Level 2 ( Level 1 ) ) ) {abc} [xyz]";
    const string expected = "  {abc} [xyz]";

    // Test nested curved brackets removal
    Simplify.Function.RemoveCurvedBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test09()
  {
    // Expected I/O
    string filename = "[ Level 3 [ Level 2 [ Level 1 ] ] ] {abc} (xyz)";
    const string expected = "  {abc} (xyz)";

    // Test nested square brackets removal
    Simplify.Function.RemoveSquareBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test10()
  {
    // Expected I/O
    string filename = "{ { Level 3 { Level 2 { Level 1 } } } (abc) [XYZ]";
    const string expected = "{ { Level 3 { Level 2 { Level 1 } } } (abc) [XYZ]";

    // Test nested curly brackets removal where brackets are unbalanced => Skip and make no changes
    Simplify.Function.RemoveCurlyBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test11()
  {
    // Expected I/O
    string filename = "( ( Level 3 ( Level 2 ( Level 1 ) ) ) {abc} [xyz]";
    const string expected = "( ( Level 3 ( Level 2 ( Level 1 ) ) ) {abc} [xyz]";

    // Test nested curved brackets removal where brackets are unbalanced => Skip and make no changes
    Simplify.Function.RemoveCurvedBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test12()
  {
    // Expected I/O
    string filename = "[ Level 3 [ Level 2 [ Level 1 ] ] ] ] {abc} (xyz)";
    const string expected = "[ Level 3 [ Level 2 [ Level 1 ] ] ] ] {abc} (xyz)";

    // Test nested square brackets removal where brackets are unbalanced => Skip and make no changes
    Simplify.Function.RemoveSquareBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }
}
