namespace Simplify;
public sealed record class Counter
{
  public int Unchanged { get; set; } = 0;
  public int Conflict { get; set; } = 0;
  public int Renamed { get; set; } = 0;
}
