namespace Wallet;

public class Sum : Expression {
  Expression Augend { get; }
  Expression Addend { get; }

  public Sum(Expression augend, Expression addend) {
    Augend = augend;
    Addend = addend;
  }

  public Expression Plus(Expression addend) {
    return new Sum(this, addend);
  }

  public Expression Times(decimal multiplier) {
    return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
  }

  public Money Reduce(Bank bank, string to) {
    return new Money(Augend.Reduce(bank, to).Amount + Addend.Reduce(bank, to).Amount, to);
  }
}
