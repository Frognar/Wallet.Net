namespace Wallet;

public class Sum : Expression {
  readonly Expression augend;
  Expression Augend {
    get => augend;
    init => augend = value;
  }

  readonly Expression addend;
  Expression Addend {
    get => addend;
    init => addend = value;
  }

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
