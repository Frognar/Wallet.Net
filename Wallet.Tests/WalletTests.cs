using Xunit;

namespace Wallet.Tests;

public class WalletTests {
  [Fact]
  public void TestMultiplication() {
    Money five = Money.Dollar(5);
    Assert.Equal(Money.Dollar(10), five.Times(2));
    Assert.Equal(Money.Dollar(15), five.Times(3));
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
    Money five = Money.Dollar(5);
    Expression result = five.Plus(five);
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
    Bank bank = new();
    bank.AddRate("CHF", "USD", 2);
    Money result = bank.Reduce(Money.Franc(2), "USD");
    Assert.Equal(Money.Dollar(1), result);
  }

  [Fact]
  public void TestIdentityRate() {
    Assert.Equal(1, new Bank().Rate("USD", "USD"));
  }

  [Fact]
  public void TestMixedAddition() {
    Expression fiveBucks = Money.Dollar(5);
    Expression tenFrancs = Money.Franc(10);
    Bank bank = new();
    bank.AddRate("CHF", "USD", 2);
    Expression result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
    Assert.Equal(Money.Dollar(10), result);
  }

  [Fact]
  public void TestSumPlusMoney() {
    Expression fiveBucks = Money.Dollar(5);
    Expression tenFrancs = Money.Franc(10);
    Bank bank = new();
    bank.AddRate("CHF", "USD", 2);
    Expression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
    Money result = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(15), result);
  }

  [Fact]
  public void TestSumTimes() {
    Expression fiveBucks = Money.Dollar(5);
    Expression tenFrancs = Money.Franc(10);
    Bank bank = new();
    bank.AddRate("CHF", "USD", 2);
    Expression sum = new Sum(fiveBucks, tenFrancs).Times(2);
    Money result = bank.Reduce(sum, "USD");
    Assert.Equal(Money.Dollar(20), result);
  }

  [Fact]
  public void TestCreateMoneyWithDecimal() {
    Money money = new(1.23m, "PLN");
    Assert.Equal(1.23m, money.Amount);
  }
}
