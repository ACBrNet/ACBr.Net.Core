using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class ValidadarCNPJTest
	{
		private const string ErrorMessage = "CNPJ Invalido";

		[Fact]
		public void Valido()
		{
			Assert.True("12345678000195".IsCNPJ(), ErrorMessage);
			Assert.True("191".IsCNPJ(true), ErrorMessage);
		}

		[Fact]
		public void ValidoComSeparadores()
		{
			Assert.True("12.345.678/0001-95".IsCNPJ(), ErrorMessage);
		}

		[Fact]
		public void Invalido()
		{
			Assert.False("12345678901234".IsCNPJ(), ErrorMessage);
		}

		[Fact]
		public void NumeroComZeros()
		{
			var cnpj = new string('0', 14);
			Assert.False(cnpj.IsCNPJ(), ErrorMessage);
		}

		[Fact]
		public void MenorQuatorzeDigitos()
		{
			Assert.False("1234567890".IsCNPJ(), ErrorMessage);
		}

		[Fact]
		public void MaiorQuatorzeDigitos()
		{
			Assert.False("123456789012345".IsCNPJ(), ErrorMessage);
		}

		[Fact]
		public void ComLetras()
		{
			Assert.False("1234567890ABCD".IsCNPJ(), ErrorMessage);
		}

		[Fact]
		public void Formatar()
		{
			Assert.Equal("00.000.000/0001-91", "191".FormataCNPJ());
			Assert.Equal("12.345.678/0001-95", "12345678000195".FormataCNPJ());
		}
	}
}