namespace Test;

[TestClass]
public sealed class UnitTestConvertCase
{
  [TestMethod]
  public void Test1()
  {
    // Expected I/O
    string filename = "[reZero] (USA) de7ta 火";
    const string expected = "[rezero] (usa) de7ta 火";

    // Test
    Simplify.Function.ConvertToLowercase(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test2()
  {
    // Expected I/O
    string filename = "[reZero] (USA) de7ta 火";
    const string expected = "[reZero] (USA) de7ta 火";

    // Test inactive state
    Simplify.Function.ConvertToLowercase(ref filename, false);
    Assert.AreEqual(expected, filename);
  }
}
