using Simplify;

namespace Test;

[TestClass]
public class UnitTestRemoveBlacklistedWords
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "(lambda) (hevc) (.) (-)";
    const string expected = "(lambda) ( ) ( ) ( )";

    // Test
    Function.RemoveBlacklistedWords(ref filename);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "xhevcHEVChEVcx"; // HEVC three times without spaces
    const string expected = "x   x";

    // Test
    Function.RemoveBlacklistedWords(ref filename);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test3()
  {
    // Expected I/O
    string filename = "HEVxC"; // Partial match must not be removed
    const string expected = "HEVxC";

    // Test
    Function.RemoveBlacklistedWords(ref filename);
    Assert.AreEqual(expected, filename);
  }
}
