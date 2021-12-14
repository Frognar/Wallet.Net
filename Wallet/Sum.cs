namespace Wallet {
  public class Sum : Expression {
    public readonly Money augend;
    public readonly Money addend;

    public Sum(Money augend, Money addend) {
      this.augend = augend;
      this.addend = addend;
    }

    public Money Reduce(string to) {
      int amount = augend.amount + addend.amount;
      return new Money(amount, to);
    }
  }
}
