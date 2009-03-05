using NUnit.Framework;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Test {

	/// <summary>
	/// Summary description for MoneyTest.
	/// </summary>
	[TestFixture]
	public class MoneyTest : TestBase {

		public MoneyTest() {
		}

		[Test]
		public void TestRoundUp() {
			Money money = new Money();

			money.RoundingMode = RoundingMode.AlwaysRoundUp;
			money.Amount = 1.111M;
			Assert.IsTrue(money.Amount == 1.12M, "Round Up failed (" + money.ToString() + " != 1.12");

			money.Amount = 2400;
			money.Multiply(.175M);
			Assert.IsTrue(money.Amount == 420M, "Round Up failed (" + money.ToString() + " != 420.00");
		}

		[Test]
		public void TestRoundDown() {
			Money money = new Money();

			money.RoundingMode = RoundingMode.AlwaysRoundDown;
			money.Amount = 1.119M;
			Assert.IsTrue(money.Amount == 1.11M, "Round Up failed (" + money.ToString() + " != 1.11");
		}
		
		[Test]
		public void TestRoundPoint5() {
			Money money = new Money();

			money.RoundingMode = RoundingMode.RoundPoint5AndHigher;
			money.Amount = 1.115M;
			Assert.IsTrue(money.Amount == 1.12M, "Round To Point 5 failed (" + money.ToString() + " != 1.12");
			money.Amount = 1.125M;
			Assert.IsTrue(money.Amount == 1.13M, "Round To Point 5 failed (" + money.ToString() + " != 1.13");
			money.Amount = 1.13495M;
			Assert.IsTrue(money.Amount == 1.14M, "Round To Point 5 failed (" + money.ToString() + " != 1.14");

			money.Amount = 453;
			money.Add(new Money(36));
			money.Add(new Money(10));
			money.Multiply(0.175M);
		
			Assert.IsTrue(money.Amount == 87.33M, "Round To Point 5 failed (" + money.ToString() + " != 87.33");

		}
		
		[Test]
		public void TestRoundIEEE() {
			Money money = new Money();

			money.RoundingMode = RoundingMode.IEEE754Section4;
			money.Amount = 1.115M;
			Assert.IsTrue(money.Amount == 1.12M, "Round Up failed (" + money.ToString() + " != 1.12");
			money.Amount = 1.125M;
			Assert.IsTrue(money.Amount == 1.12M, "Round Up failed (" + money.ToString() + " != 1.12");
			money.Amount = 1.135M;
			Assert.IsTrue(money.Amount == 1.14M, "Round Up failed (" + money.ToString() + " != 1.14");
		}
		
		[Test]
		public void TestAdd() {

			Money money1 = new Money(1.23M);
			Money money2 = new Money(1.11M);

			money1.Add(money2);
			Assert.IsTrue(money1.Amount == 2.34M, "Addition failed (" + money2.ToString() + " != 2.34");
		}

		[Test]
		public void TestSubtract() {

			Money money1 = new Money(2.23M);
			Money money2 = new Money(1.11M);

			money1.Subtract(money2);
			Assert.IsTrue(money1.Amount == 1.12M, "Subtraction failed (" + money2.ToString() + " != 1.12");
		}

		[Test]
		public void TestMultiply() {

			Money money1 = new Money(1.23M);
			money1.Multiply(1.5M);

			Assert.IsTrue(money1.Amount == 1.84M, "Multiplication failed (" + money1.ToString() + " != 1.84");
		}

		[Test]
		public void TestDivide() {

			Money money1 = new Money(1.23M);
			money1.Divide(1.5M);

			Assert.IsTrue(money1.Amount == 0.82M, "Addition failed (" + money1.ToString() + " != 0.82");
		}

        [Test]
        public void TestFormat() {
            Money m = new Money();
            m.Amount = 0.2750M;

            string c1 = m.ToString();
            string c2 = m.ToString(false);
    
            //m.Currency.CurrencyCode;
        }
	}
}