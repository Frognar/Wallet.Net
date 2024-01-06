using System.Collections;
using System.Collections.Generic;

namespace Wallet;

public class Bank {
  readonly Hashtable rates = new();

  public Bank() {
  }

  public Bank(IEnumerable<(string from, string to, decimal rate)> rates) {
    foreach ((string from, string to, decimal rate) in rates) {
      this.rates.Add(new Pair(from, to), rate);
    }
  }

  public Money Reduce(Expression source, string to) {
    return source.Reduce(this, to);
  }

  public decimal Rate(string from, string to) {
    if (from.Equals(to)) {
      return 1;
    }
    
    return (decimal)rates[new Pair(from, to)];
  }
}
