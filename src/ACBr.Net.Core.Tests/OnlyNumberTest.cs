using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class OnlyNumberTest
	{
		[Fact]
		public void Texto()
		{
			Assert.Equal("", "TesteACBr".OnlyNumbers());
		}

		[Fact]
		public void Numeros()
		{
			Assert.Equal("12345", "12345".OnlyNumbers());
		}

		[Fact]
		public void TextoComNumeros()
		{
			Assert.Equal("12345", "TesteACBr12345".OnlyNumbers());
		}

		[Fact]
		public void TextoComSeparadores()
		{
			Assert.Equal("1234500", "1.2345,00".OnlyNumbers());
		}

		[Fact]
		public void TextoComCaractersEspeciais()
		{
			Assert.Equal("12345", "!1@2#34$5%".OnlyNumbers());
		}
	}
}