namespace Wallet;

public class Pair {
  readonly string from;
  readonly string to;

  public Pair(string from, string to) {
    this.from = from;
    this.to = to;
  }

  public override bool Equals(object obj) {
    if (obj is Pair pair) {
      return from.Equals(pair.from) && to.Equals(pair.to);
    }
    return false;
  }

  public override int GetHashCode() {
    return 0;
  }
}