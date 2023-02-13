namespace Test;

[TestClass]
public sealed class UnitTestRemoveBrackets
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "  (Text 0) [Text 0]  ()[] Text 0";

    // Test curly brackets removal
    Simplify.Function.RemoveCurlyBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";

    // Test curly brackets removal in inactive state
    Simplify.Function.RemoveCurlyBrackets(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test3()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0}   [Text 0] {} [] Text 0";

    // Test curved brackets removal
    Simplify.Function.RemoveCurvedBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test4()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";

    // Test curved brackets removal in inactive state
    Simplify.Function.RemoveCurvedBrackets(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test5()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0)   {}()  Text 0";

    // Test square brackets removal
    Simplify.Function.RemoveSquareBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test6()
  {
    // Expected I/O
    string filename = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";
    const string expected = "{Text 0} (Text 0) [Text 0] {}()[] Text 0";

    // Test square brackets removal in inactive state
    Simplify.Function.RemoveSquareBrackets(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test7()
  {
    // Expected I/O
    string filename = "{ Level 3 { Level 2 { Level 1 } } } (abc) [XYZ]";
    const string expected = "  (abc) [XYZ]";

    // Test nested curly brackets removal
    Simplify.Function.RemoveCurlyBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test8()
  {
    // Expected I/O
    string filename = "( Level 3 ( Level 2 ( Level 1 ) ) ) {abc} [xyz]";
    const string expected = "  {abc} [xyz]";

    // Test nested curved brackets removal
    Simplify.Function.RemoveCurvedBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test9()
  {
    // Expected I/O
    string filename = "[ Level 3 [ Level 2 [ Level 1 ] ] ] {abc} (xyz)";
    const string expected = "  {abc} (xyz)";

    // Test nested square brackets removal
    Simplify.Function.RemoveSquareBrackets(ref filename, true);
    Assert.AreEqual(expected, filename);
  }
}
