using System.Collections;

namespace Wallet;

public class Bank {
  readonly Hashtable rates = new();

  public Money Reduce(Expression source, string to) {
    return source.Reduce(this, to);
  }

  public int Rate(string from, string to) {
    if (from.Equals(to)) {
      return 1;
    }
    int rate = (int)rates[new Pair(from, to)];
    return rate;
  }

  public void AddRate(string from, string to, int rate) {
    rates.Add(new Pair(from, to), rate);
  }
}
