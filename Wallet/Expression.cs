﻿namespace Wallet;

public interface Expression {
  Money Reduce(Bank bank, string to);
}
