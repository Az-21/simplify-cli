namespace Test;

[TestClass]
public sealed class UnitTestAppendSeasonAndOrEpisode
{
  [TestMethod]
  public void Test01()
  {
    // Expected I/O
    string filename = "S1 Series";
    const string expected = " Series  - S1 ";

    // Test season only with dash
    Simplify.Function.AppendSeasonAndOrEpisodePre(ref filename, true);
    Simplify.Function.AppendSeasonAndOrEpisodePost(ref filename, true, "-");
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test02()
  {
    // Expected I/O
    string filename = "S1 Series";
    const string expected = " Series    S1 ";

    // Test season only with space
    Simplify.Function.AppendSeasonAndOrEpisodePre(ref filename, true);
    Simplify.Function.AppendSeasonAndOrEpisodePost(ref filename, true, " ");
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test03()
  {
    // Expected I/O
    string filename = "S012345 Series";
    const string expected = " Series  - S012345 ";

    // Test complex season with dash separator
    Simplify.Function.AppendSeasonAndOrEpisodePre(ref filename, true);
    Simplify.Function.AppendSeasonAndOrEpisodePost(ref filename, true, "-");
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test04()
  {
    // Expected I/O
    string filename = "E012345 Series";
    const string expected = " Series  - E012345 ";

    // Test complex episode with dash separator
    Simplify.Function.AppendSeasonAndOrEpisodePre(ref filename, true);
    Simplify.Function.AppendSeasonAndOrEpisodePost(ref filename, true, "-");
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test05()
  {
    // Expected I/O
    string filename = "S012345E012345 Series";
    const string expected = " Series  - S012345E012345 ";

    // Test complex season+episode with dash separator
    Simplify.Function.AppendSeasonAndOrEpisodePre(ref filename, true);
    Simplify.Function.AppendSeasonAndOrEpisodePost(ref filename, true, "-");
    Assert.AreEqual(expected, filename);
  }

  [TestMethod]
  public void Test06()
  {
    // Expected I/O
    string filename = "[S012345E012345 meta data] Series";
    const string expected = "  Series  - S012345E012345 ";

    // Test extraction of season+episode info from a metadata container which will be destroyed
    Simplify.Function.AppendSeasonAndOrEpisodePre(ref filename, true);
    Simplify.Function.RemoveSquareBrackets(ref filename, true); // Replaces [...] with Space
    Simplify.Function.AppendSeasonAndOrEpisodePost(ref filename, true, "-");
    Assert.AreEqual(expected, filename);
  }
}
