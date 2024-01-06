using Xunit;

namespace Wallet.Tests;

public class WalletTests {
  readonly Money fiveBucks = Money.Dollar(5);
  readonly Money tenFrancs = Money.Franc(10);
  
  [Fact]
  public void TestMultiplication() {
    Assert.Equal(Money.Dollar(10), fiveBucks.Times(2));
    Assert.Equal(Money.Dollar(15), fiveBucks.Times(3));
  }
  
  [Fact]
  public void TestDecimalMultiplication() {
    Assert.Equal(Money.Dollar(10), fiveBucks.Times(2m));
    Assert.Equal(Money.Dollar(15), fiveBucks.Times(3m));
  }

  [Fact]
  public void TestEquality() {
    Assert.True(Money.Dollar(5).Equals(Money.Dollar(5m)));
    Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)));
    Assert.False(Money.Franc(5m).Equals(Money.Dollar(5)));
  }

  [Fact]
  public void TestCurrency() {
    Assert.Equal("USD", Money.Dollar(1).Currency);
    Assert.Equal("CHF", Money.Franc(1).Currency);
  }

  [Fact]
  public void TestSimpleAddition() {
    Expression result = fiveBucks.Plus(fiveBucks);
    Sum sum = (Sum)result;
    Bank bank = new();
    Money reduced = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(10), reduced);
  }

  [Fact]
  public void TestReduceSum() {
    Expression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
    Bank bank = new();
    Money result = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(7), result);
  }

  [Fact]
  public void TestReduceMoney() {
    Bank bank = new();
    Money result = bank.Reduce(Money.Dollar(1), "USD");
    Assert.Equal(Money.Dollar(1), result);
  }

  [Fact]
  public void TestReduceMoneyDifferentCurrency() {
    Bank bank = new([("CHF", "USD", 2m)]);
    Money result = bank.Reduce(Money.Franc(2), "USD");
    Assert.Equal(Money.Dollar(1), result);
  }

  [Fact]
  public void TestIdentityRate() {
    Assert.Equal(1, new Bank().Rate("USD", "USD"));
  }

  [Fact]
  public void TestMixedAddition() {
    Bank bank = new([("CHF", "USD", 2m)]);
    Expression result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
    Assert.Equal(Money.Dollar(10), result);
  }

  [Fact]
  public void TestSumPlusMoney() {
    Bank bank = new([("CHF", "USD", 2m)]);
    Expression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
    Money result = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(15), result);
  }

  [Fact]
  public void TestSumTimes() {
    Bank bank = new([("CHF", "USD", 2m)]);
    Expression sum = new Sum(fiveBucks, tenFrancs).Times(2.5m);
    Money result = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(25), result);
  }

  [Fact]
  public void TestReduceWithDecimalExchangeRate() {
    Bank bank = new([("CHF", "USD", 2.5m)]);
    Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
    Assert.Equal(Money.Dollar(9), result);
  }

  [Fact]
  public void TestCreateBankWithExchangeRates() {
    Bank bank = new Bank([("CHF", "USD", 2.5m)]);
  }
}
