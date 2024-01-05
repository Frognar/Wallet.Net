﻿namespace Wallet;

public class Money : Expression {
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

  public Expression Times(int multiplier) {
    return new Money(amount * multiplier, currency);
  }

  public Expression Plus(Expression addend) {
    return new Sum(this, addend);
  }

  public Money Reduce(Bank bank, string to) {
    int rate = bank.Rate(currency, to);
    return new Money(amount / rate, to);
  }

  public override bool Equals(object obj) {
    Money money = (Money)obj;
    return amount == money.amount
        && currency.Equals(money.currency);
  }

  public override int GetHashCode() {
    return 0;
  }
}