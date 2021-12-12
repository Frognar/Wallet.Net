namespace Wallet;

public abstract class Money
{
  protected int amount;

  public override bool Equals(object obj)
  {
    Money money = (Money)obj;
    return amount == money.amount
      && GetType().Equals(money.GetType());
  }

  public static Money Dollar(int amount)
  {
    return new Dollar(amount);
  }

  public static Money Franc(int amount)
  {
    return new Franc(amount);
  }

  public abstract Money Times(int amount);
  public abstract string Currency();
}

public class Dollar : Money
{
  public Dollar(int amount)
  {
    this.amount = amount;
  }

  public override string Currency()
  {
    return "USD";
  }

  public override Money Times(int multiplier)
  {
    return new Dollar(amount * multiplier);
  }
}

public class Franc : Money
{
  public Franc(int amount)
  {
    this.amount = amount;
  }

  public override string Currency()
  {
    return "CHF";
  }

  public override Money Times(int multiplier)
  {
    return new Franc(amount * multiplier);
  }
}
