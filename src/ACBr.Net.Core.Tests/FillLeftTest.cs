using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class FillLeftTest
	{
		[Fact]
		public void CompletarString()
		{
			Assert.Equal("ACBrCompletaStringZZZ", "ACBrCompletaString".FillLeft(21, 'Z'));
			Assert.Equal("ACBrCompletaString   ", "ACBrCompletaString".FillLeft(21));
		}

		[Fact]
		public void ManterString()
		{
			Assert.Equal("ACBrMantemString", "ACBrMantemString".FillLeft(16, 'Z'));
		}

		[Fact]
		public void TruncarString()
		{
			Assert.Equal("ACBrTrunca", "ACBrTruncaString".FillLeft(10, 'Z'));
		}
	}
}