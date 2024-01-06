namespace Wallet;

public class Sum(Expression augend, Expression addend) : Expression {
  Expression Augend { get; } = augend;
  Expression Addend { get; } = addend;

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