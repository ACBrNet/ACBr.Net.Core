using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class FillRightTest
	{
		[Fact]
		public void CompletarString()
		{
			Assert.Equal("ZZZACBrCompletaString", "ACBrCompletaString".FillRight(21, 'Z'));
			Assert.Equal("   ACBrCompletaString", "ACBrCompletaString".FillRight(21));
		}

		[Fact]
		public void ManterString()
		{
			Assert.Equal("ACBrMantemString", "ACBrMantemString".FillRight(16, 'Z'));
		}

		[Fact]
		public void TruncarString()
		{
			Assert.Equal("ACBrTrunca", "ACBrTruncaString".FillRight(10, 'Z'));
		}
	}
}