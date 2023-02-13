namespace Test;

[TestClass]
public sealed class UnitTestRemoveAllNumbers
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "(123) [456] {789} 0";
    const string expected = "(   ) [   ] {   }  ";

    // Test with removeNumbers set to true
    Simplify.Function.RemoveNumbers(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "(123) [456] {789} 0";
    const string expected = "(123) [456] {789} 0";

    // Test with removeNumbers set to false
    Simplify.Function.RemoveNumbers(ref filename, false);
    Assert.AreEqual(expected, filename);
  }
}
