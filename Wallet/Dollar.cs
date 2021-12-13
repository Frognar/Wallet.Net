namespace Wallet;

public class Money {
  readonly int amount;
  readonly string currency;

  public Money(int amount, string currency) {
    this.amount = amount;
    this.currency = currency;
  }

  public static Money Dollar(int amount) {
    return new Money(amount, "USD");
  }

  public static Money Franc(int amount) {
    return new Money(amount, "CHF");
  }

  public Money Times(int multiplier) {
    return new Money(amount * multiplier, currency);
  }

  public string Currency() {
    return currency;
  }

  public override bool Equals(object obj) {
    Money money = (Money)obj;
    return amount == money.amount
      && currency.Equals(money.currency);
  }

  public override int GetHashCode() {
    throw new System.NotImplementedException();
  }
}
