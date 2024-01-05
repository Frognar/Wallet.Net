namespace Wallet;

public record Money : Expression {
  readonly decimal amount;
  readonly string currency;
  
  public decimal Amount => amount;
  public string Currency => currency;

  public Money(decimal amount, string currency) {
    this.amount = amount;
    this.currency = currency;
  }

  public static Money Dollar(decimal amount) {
    return new Money(amount, "USD");
  }

  public static Money Franc(decimal amount) {
    return new Money(amount, "CHF");
  }

  public Expression Times(decimal multiplier) {
    return new Money(Amount * multiplier, Currency);
  }

  public Expression Plus(Expression addend) {
    return new Sum(this, addend);
  }

  public Money Reduce(Bank bank, string to) {
    int rate = bank.Rate(Currency, to);
    return new Money(Amount / rate, to);
  }
}
