using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
    public class ValidadarCPFTest
    {
        private const string ErrorMessage = "CPF Invalido";

        [Fact]
        public void Valido()
        {
            Assert.True("12345678909".IsCPF(), ErrorMessage);
            Assert.True("191".IsCPF(true), ErrorMessage);
        }

        [Fact]
        public void ValidoComSeparadores()
        {
            Assert.True("123.456.789-09".IsCPF(), ErrorMessage);
        }

        [Fact]
        public void Invalido()
        {
            Assert.False("12345678901".IsCPF(), ErrorMessage);
        }

        [Fact]
        public void NumerosSequenciais()
        {
            for (var i = 0; i < 10; i++)
            {
                var cpf = new string(i.ToString()[0], 11);
                Assert.False(cpf.IsCPF(), ErrorMessage);
            }
        }

        [Fact]
        public void MenorOnzeDigitos()
        {
            Assert.False("123456789".IsCPF(), ErrorMessage);
        }

        [Fact]
        public void MaiorOnzeDigitos()
        {
            Assert.False("1234567890123".IsCPF(), ErrorMessage);
        }

        [Fact]
        public void ComLetras()
        {
            Assert.False("123456789AB".IsCPF(), ErrorMessage);
        }

        [Fact]
        public void Formatar()
        {
            Assert.Equal("000.000.001-91", "191".FormataCPF());
            Assert.Equal("123.456.789-09", "12345678909".FormataCPF());
        }
    }
}