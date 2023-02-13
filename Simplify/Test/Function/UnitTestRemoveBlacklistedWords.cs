namespace Test;

[TestClass]
public class UnitTestRemoveBlacklistedWords
{
  const string blacklist = "., -, _, webrip, x256, HEVC, camrip, nogrp, ddp5, x264";

  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "(lambda) (hevc) (.) (-)";
    const string expected = "(lambda) ( ) ( ) ( )";

    // Test
    Simplify.Function.RemoveBlacklistedWords(ref filename, blacklist);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "xhevcHEVChEVcx"; // HEVC three times without spaces
    const string expected = "x   x";

    // Test
    Simplify.Function.RemoveBlacklistedWords(ref filename, blacklist);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test3()
  {
    // Expected I/O
    string filename = "HEVxC"; // Partial match must not be removed
    const string expected = "HEVxC";

    // Test
    Simplify.Function.RemoveBlacklistedWords(ref filename, blacklist);
    Assert.AreEqual(expected, filename);
  }
}
