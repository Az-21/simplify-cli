namespace Test;

[TestClass]
public sealed class UnitTestOptimizeArticles
{
  [TestMethod]
  public void Test01()
  {
    // Expected I/O
    string filename = "a an the planet moon A An aN tHE thE The the IN";
    const string expected = "a an the planet moon a an an the the the the in";

    // Test active state of article optimizer
    Simplify.Function.OptimizeArticles(ref filename, true);
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test02()
  {
    // Expected I/O
    string filename = "a an the planet moon A An aN tHE thE The the IN";
    const string expected = "a an the planet moon A An aN tHE thE The the IN";

    // Test inactive state of article optimizer
    Simplify.Function.OptimizeArticles(ref filename, false);
    Assert.AreEqual(expected, filename);
  }
}
