namespace Wallet;

public readonly record struct Money(decimal Amount, string Currency) : Expression {
  public static Money Dollar(decimal amount) => new(amount, "USD");
  public static Money Franc(decimal amount) => new(amount, "CHF");

  public Expression Times(decimal multiplier) {
    return this with { Amount = Amount * multiplier };
  }

  public Expression Plus(Expression addend) {
    return new Sum(this, addend);
  }

  public Money Reduce(Bank bank, string to) {
    return new Money(Amount / bank.Rate(Currency, to), to);
  }
}