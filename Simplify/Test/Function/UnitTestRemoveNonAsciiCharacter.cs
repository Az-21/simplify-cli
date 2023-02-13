namespace Test;

[TestClass]
public sealed class UnitTestRemoveNonAsciiCharacter
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "0123456789![' -- '](' ++ ')ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    const string expected = "0123456789![' -- '](' ++ ')ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    // Test with standard ASCII characters
    Simplify.Function.RemoveNonAsciiCharacters(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "Generic Isekai ジェネリック異世界";
    const string expected = "Generic Isekai  ";

    // Test with non-ASCII Japanese characters
    Simplify.Function.RemoveNonAsciiCharacters(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test3()
  {
    // Expected I/O
    string filename = "Generic Isekai ジェネリック異世界";
    const string expected = "Generic Isekai ジェネリック異世界";

    // Test for inactive state
    Simplify.Function.RemoveNonAsciiCharacters(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test4()
  {
    // Expected I/O
    string filename = "sûr økende";
    const string expected = "s r  kende";

    // Test with non-ASCII accented characters
    Simplify.Function.RemoveNonAsciiCharacters(ref filename, true);
    Assert.AreEqual(expected, filename);
  }
}
