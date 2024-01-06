using Xunit;

namespace Wallet.Tests;

public class WalletTests {
  readonly Money fiveBucks = Money.Dollar(5);
  readonly Money tenFrancs = Money.Franc(10);
  
  [Fact]
  public void TestMultiplication() {
    Assert.Equal(Money.Dollar(10), fiveBucks.Times(2));
    Assert.Equal(Money.Dollar(12.5m), fiveBucks.Times(2.5m));
    Assert.Equal(Money.Dollar(15), fiveBucks.Times(3));
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
    Expression sum = fiveBucks.Plus(fiveBucks);
    Bank bank = new();
    Money reduced = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(10), reduced);
  }

  [Fact]
  public void TestReduceSum() {
    Sum sum = new(Money.Dollar(3), Money.Dollar(4));
    Bank bank = new();
    Money reduced = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(7), reduced);
  }

  [Fact]
  public void TestReduceMoney() {
    Bank bank = new();
    Money reduced = bank.Reduce(Money.Dollar(1), "USD");
    Assert.Equal(Money.Dollar(1), reduced);
  }

  [Fact]
  public void TestReduceMoneyDifferentCurrency() {
    Bank bank = new([("CHF", "USD", 2.5m)]);
    Money reduced = bank.Reduce(Money.Franc(2), "USD");
    Assert.Equal(Money.Dollar(.8m), reduced);
  }

  [Fact]
  public void TestIdentityRate() {
    Assert.Equal(1, new Bank().Rate("USD", "USD"));
  }

  [Fact]
  public void TestMixedAddition() {
    Bank bank = new([("CHF", "USD", 2.5m)]);
    Money reduced = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
    Assert.Equal(Money.Dollar(9), reduced);
  }

  [Fact]
  public void TestSumPlusMoney() {
    Bank bank = new([("CHF", "USD", 2.5m)]);
    Expression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
    Money reduced = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(14), reduced);
  }

  [Fact]
  public void TestSumTimes() {
    Bank bank = new([("CHF", "USD", 2.5m)]);
    Expression sum = new Sum(fiveBucks, tenFrancs).Times(2.5m);
    Money reduced = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(22.5m), reduced);
  }
}
