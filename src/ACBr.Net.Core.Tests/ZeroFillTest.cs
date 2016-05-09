using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class ZeroFillTest
	{
		[Fact]
		public void ZeroFill()
		{
			Assert.Equal("000000000000000000000", "ACBrZeroFill".ZeroFill(21));
		}

		[Fact]
		public void TruncarString()
		{
			Assert.Equal("12", "12345".ZeroFill(2));
		}

		[Fact]
		public void CompletarString()
		{
			Assert.Equal("0012345", "12345".ZeroFill(7));
			Assert.Equal("0000012345", "12345".ZeroFill(10));
		}

		[Fact]
		public void CompletarStringComLetrasEVirgula()
		{
			Assert.Equal("0012345", "R$ 123,45".ZeroFill(7));
		}
	}
}