namespace Wallet;

public class Sum : Expression {
  readonly Expression augend;
  readonly Expression addend;

  public Sum(Expression augend, Expression addend) {
    this.augend = augend;
    this.addend = addend;
  }

  public Expression Plus(Expression addend) {
    return new Sum(this, addend);
  }

  public Expression Times(int multiplier) {
    return new Sum(augend.Times(multiplier), addend.Times(multiplier));
  }

  public Expression Times(decimal multiplier) {
    return new Sum(augend.Times(multiplier), addend.Times(multiplier));
  }

  public Money Reduce(Bank bank, string to) {
    return new Money(augend.Reduce(bank, to).Amount + addend.Reduce(bank, to).Amount, to);
  }
}
