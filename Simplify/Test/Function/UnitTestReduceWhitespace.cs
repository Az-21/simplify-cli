namespace Test;

[TestClass]
public sealed class UnitTestReduceWhitespace
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "        |   |       | ";
    const string expected = "| | |";

    // Test
    Simplify.Function.ReduceWhitespace(ref filename);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "[\t]   (\t)  [H  E  V  C]";
    const string expected = "[ ] ( ) [H E V C]";

    // Test
    Simplify.Function.ReduceWhitespace(ref filename);
    Assert.AreEqual(expected, filename);
  }
}
