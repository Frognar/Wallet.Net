namespace Frognar.Wallet;

public interface Expression {
  Money Reduce(Bank bank, string to);
  Expression Plus(Expression addend);
  Expression Times(decimal multiplier);
}
