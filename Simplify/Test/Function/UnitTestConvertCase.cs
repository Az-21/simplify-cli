namespace Test;

[TestClass]
public sealed class UnitTestConvertCase
{
  [TestMethod]
  public void Test01()
  {
    // Expected I/O
    string filename = "[reZero] (USA) de7ta 火";
    const string expected = "[rezero] (usa) de7ta 火";

    // Test active state of destructive lowercase converter
    Simplify.Function.ConvertToLowercase(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test02()
  {
    // Expected I/O
    string filename = "[reZero] (USA) de7ta 火";
    const string expected = "[reZero] (USA) de7ta 火";

    // Test inactive state of destructive lowercase converter
    Simplify.Function.ConvertToLowercase(ref filename, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test03()
  {
    // Expected I/O
    string filename = "a reZero in the USA aNd uK";
    const string expected = "A Rezero In The Usa And Uk";

    // Test sentence case **without** smart capitalization
    Simplify.Function.ConvertToSentenceCase(ref filename, true, false);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test04()
  {
    // Expected I/O
    string filename = "a reZero in the USA aNd uK";
    const string expected = "A reZero In The USA aNd uK";

    // Test sentence case **with** smart capitalization
    Simplify.Function.ConvertToSentenceCase(ref filename, true, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test05()
  {
    // Expected I/O
    string filename = "a reZero in the USA aNd uK";
    const string expected = "a reZero in the USA aNd uK";

    // Test inactive state of sentence case | Activity of smartCapitalization should not affect output
    Simplify.Function.ConvertToSentenceCase(ref filename, false, true);
    Assert.AreEqual(expected, filename);
    Simplify.Function.ConvertToSentenceCase(ref filename, false, false);
    Assert.AreEqual(expected, filename);
  }
}
