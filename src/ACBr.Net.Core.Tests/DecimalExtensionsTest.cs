using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class DecimalExtensionsTest
	{
		[Fact]
		public void ToCurrencyTest()
		{
			decimal value = 15.001M;
			Assert.Equal("R$ 15,00", value.ToCurrency());
			Assert.Equal("R$ 15,001", value.ToCurrency(3));

			Assert.Equal("$15.00", value.ToCurrency(2, "$", '.', ','));
			Assert.Equal("$15.001", value.ToCurrency(3, "$", '.', ','));


			value = 15000.001M;
			Assert.Equal("R$ 15.000,00", value.ToCurrency());
			Assert.Equal("R$ 15.000,001", value.ToCurrency(3));

			Assert.Equal("$15,000.00", value.ToCurrency(2, "$", '.', ','));
			Assert.Equal("$15,000.001", value.ToCurrency(3, "$", '.', ','));
		}
	}
}