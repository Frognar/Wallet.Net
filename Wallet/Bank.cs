using System.Collections.Generic;
using System.Collections.Immutable;

namespace Wallet;

public class Bank {
  readonly ImmutableDictionary<Pair, decimal> rates;
  ImmutableDictionary<Pair, decimal> Rates => rates;

  private Bank(ImmutableDictionary<Pair, decimal> rates) {
    this.rates = rates;
  }

  public Bank() : this(ImmutableDictionary<Pair, decimal>.Empty) {
  }

  public Bank(IEnumerable<(string from, string to, decimal rate)> rates)
  : this(rates.ToImmutableDictionary(
    x => new Pair(x.from, x.to),
    x => x.rate)) {
  }

  public Money Reduce(Expression source, string to) {
    return source.Reduce(this, to);
  }

  public decimal Rate(string from, string to) {
    return from.Equals(to) ? 1m : Rates[new Pair(from, to)];
  }
}
