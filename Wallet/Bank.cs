﻿using System.Collections.Generic;
using System.Collections.Immutable;

namespace Frognar.Wallet;

public class Bank {
  ImmutableDictionary<Pair, decimal> Rates { get; }

  Bank(ImmutableDictionary<Pair, decimal> rates) {
    Rates = rates;
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
