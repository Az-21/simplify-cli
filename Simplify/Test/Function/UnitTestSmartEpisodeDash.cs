namespace Test;

[TestClass]
public sealed class UnitTestSmartEpisodeDash
{
  [TestMethod]
  public void Test01()
  {
    // Expected I/O
    string filename = "something";
    const string expected = "[rezero] (usa) de7ta 火";

    // Test active state of destructive lowercase converter
    Simplify.Function.ConvertToLowercase(ref filename, true);
    Assert.AreEqual(expected, filename);
  }
}
