namespace Wallet;

public class Sum : Expression {
  public readonly Expression augend;
  public readonly Expression addend;

  public Sum(Expression augend, Expression addend) {
    this.augend = augend;
    this.addend = addend;
  }

  public Expression Plus(Expression addend) {
    return null;
  }

  public Money Reduce(Bank bank, string to) {
    int amount = augend.Reduce(bank, to).amount
               + addend.Reduce(bank, to).amount;
    return new Money(amount, to);
  }
}
