using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class ValidadarIETest
	{
		[Fact]
		public void ValidoAC()
		{
			Assert.True("01.004.823/001-12".IsIE("AC"));
			Assert.True("013456784".IsIE("AC"));
		}

		[Fact]
		public void FormatarAC()
		{
			Assert.Equal("01.004.823/001-12", "0100482300112".FormatarIE("AC"));
		}

		[Fact]
		public void InvalidoAC()
		{
			Assert.False("".IsIE("AC"));
			Assert.False("99999".IsIE("AC"));
			Assert.False("01.004.823/001-99".IsIE("AC"));
		}

		[Fact]
		public void ValidoAL()
		{
			Assert.True("240123450".IsIE("AL"));
		}

		[Fact]
		public void InvalidoAL()
		{
			Assert.False("".IsIE("AL"));
			Assert.False("240123456".IsIE("AL"));
		}

		[Fact]
		public void FormatarAL()
		{
			Assert.Equal("240123450", "240123450".FormatarIE("AL"));
		}

		[Fact]
		public void ValidoAP()
		{
			Assert.True("030123459".IsIE("AP"));
			Assert.True("030170011".IsIE("AP"));
			Assert.True("030190225".IsIE("AP"));
			Assert.True("030190231".IsIE("AP"));
		}

		[Fact]
		public void InvalidoAP()
		{
			Assert.False("".IsIE("AP"));
			Assert.False("123456789".IsIE("AP"));
		}

		[Fact]
		public void FormatarAP()
		{
			Assert.Equal("030123459", "030123459".FormatarIE("AP"));
		}

		[Fact]
		public void ValidoAM()
		{
			Assert.True("123123127".IsIE("AM"));
		}

		[Fact]
		public void InvalidoAM()
		{
			Assert.False("".IsIE("AM"));
			Assert.False("123123123".IsIE("AM"));
		}

		[Fact]
		public void FormatarAM()
		{
			Assert.Equal("12.312.312-7", "123123127".FormatarIE("AM"));
		}

		[Fact]
		public void ValidoBA()
		{
			Assert.True("123456-63".IsIE("BA"));
			Assert.True("173456-13".IsIE("BA"));
		}

		[Fact]
		public void InvalidoBA()
		{
			Assert.False("".IsIE("BA"));
			Assert.False("123456-78".IsIE("BA"));
		}

		[Fact]
		public void FormatarBA()
		{
			Assert.Equal("0123456-63", "12345663".FormatarIE("BA"));
		}

		[Fact]
		public void ValidoCE()
		{
			Assert.True("023456787".IsIE("CE"));
		}

		[Fact]
		public void InvalidoCE()
		{
			Assert.False("".IsIE("CE"));
			Assert.False("123456789".IsIE("CE"));
		}

		[Fact]
		public void FormatarCE()
		{
			Assert.Equal("02345678-7", "023456787".FormatarIE("CE"));
		}

		[Fact]
		public void ValidoDF()
		{
			Assert.True("0734567890103".IsIE("DF"));
		}

		[Fact]
		public void InvalidoDF()
		{
			Assert.False("".IsIE("DF"));
			Assert.False("12345678901".IsIE("DF"));
			Assert.False("1234567890123".IsIE("DF"));
		}

		[Fact]
		public void FormatarDF()
		{
			Assert.Equal("07345678901-03", "0734567890103".FormatarIE("DF"));
		}

		[Fact]
		public void ValidoES()
		{
			Assert.True("123123127".IsIE("ES"));
		}

		[Fact]
		public void InvalidoES()
		{
			Assert.False("".IsIE("ES"));
			Assert.False("123123123".IsIE("ES"));
		}

		[Fact]
		public void FormatarES()
		{
			Assert.Equal("123123127", "123123127".FormatarIE("ES"));
		}

		[Fact]
		public void ValidoGO()
		{
			Assert.True("10.987.654-7".IsIE("GO"));
		}

		[Fact]
		public void InvalidoGO()
		{
			Assert.False("".IsIE("GO"));
			Assert.False("12.312.312-3".IsIE("GO"));
		}

		[Fact]
		public void FormatarGO()
		{
			Assert.Equal("10.987.654-7", "109876547".FormatarIE("GO"));
		}

		[Fact]
		public void ValidoMA()
		{
			Assert.True("120000385".IsIE("MA"));
		}

		[Fact]
		public void InvalidoMA()
		{
			Assert.False("".IsIE("MA"));
			Assert.False("123123123".IsIE("MA"));
		}

		[Fact]
		public void FormatarMA()
		{
			Assert.Equal("120000385", "120000385".FormatarIE("MA"));
		}

		[Fact]
		public void ValidoMT()
		{
			Assert.True("0013000001-9".IsIE("MT"));
		}

		[Fact]
		public void InvalidoMT()
		{
			Assert.False("".IsIE("MT"));
			Assert.False("1234567890-1".IsIE("MT"));
		}

		[Fact]
		public void FormatarMT()
		{
			Assert.Equal("0013000001-9", "130000019".FormatarIE("MT"));
		}

		[Fact]
		public void ValidoMS()
		{
			Assert.True("28.312.312-5".IsIE("MS"));
		}

		[Fact]
		public void InvalidoMS()
		{
			Assert.False("".IsIE("MS"));
			Assert.False("123123123".IsIE("MS"));
		}

		[Fact]
		public void FormatarMS()
		{
			Assert.Equal("28.312.312-5", "283123125".FormatarIE("MS"));
		}

		[Fact]
		public void ValidoMG()
		{
			Assert.True("062.307.904/0081".IsIE("MG"));
		}

		[Fact]
		public void InvalidoMG()
		{
			Assert.False("".IsIE("MG"));
			Assert.False("123.123.123/9999".IsIE("MG"));
		}

		[Fact]
		public void FormatarMG()
		{
			Assert.Equal("062.307.904/0081", "0623079040081".FormatarIE("MG"));
		}

		[Fact]
		public void ValidoPA()
		{
			Assert.True("15999999-5".IsIE("PA"));
		}

		[Fact]
		public void InvalidoPA()
		{
			Assert.False("".IsIE("PA"));
			Assert.False("15999999-9".IsIE("PA"));
		}

		[Fact]
		public void FormatarPA()
		{
			Assert.Equal("15-999999-5", "159999995".FormatarIE("PA"));
		}

		[Fact]
		public void ValidoPB()
		{
			Assert.True("16000001-7".IsIE("PB"));
		}

		[Fact]
		public void InvalidoPB()
		{
			Assert.False("".IsIE("PB"));
			Assert.False("06000001-9".IsIE("PB"));
		}

		[Fact]
		public void FormatarPB()
		{
			Assert.Equal("16000001-7", "160000017".FormatarIE("PB"));
		}

		[Fact]
		public void ValidoPR()
		{
			Assert.True("123.45678-50".IsIE("PR"));
		}

		[Fact]
		public void InvalidoPR()
		{
			Assert.False("".IsIE("PR"));
			Assert.False("123.45678-99".IsIE("PR"));
		}

		[Fact]
		public void FormatarPR()
		{
			Assert.Equal("123.45678-50", "1234567850".FormatarIE("PR"));
		}

		[Fact]
		public void ValidoPE()
		{
			Assert.True("0321418-40".IsIE("PE"));
			Assert.True("18.1.001.0000004-9".IsIE("PE"));
		}

		[Fact]
		public void InvalidoPE()
		{
			Assert.False("".IsIE("PE"));
			Assert.False("0321418-99".IsIE("PE"));
			Assert.False("18.1.001.0000004-0".IsIE("PE"));
		}

		[Fact]
		public void FormatarPE()
		{
			Assert.Equal("0321418-99", "032141899".FormatarIE("PE"));
			Assert.Equal("18.1.001.0000004-0", "18100100000040".FormatarIE("PE"));
		}

		[Fact]
		public void ValidoPI()
		{
			Assert.True("192345672".IsIE("PI"));
		}

		[Fact]
		public void InvalidoPI()
		{
			Assert.False("".IsIE("PI"));
			Assert.False("012345678".IsIE("PI"));
		}

		[Fact]
		public void FormatarPI()
		{
			Assert.Equal("192345672", "192345672".FormatarIE("PI"));
		}

		[Fact]
		public void ValidoRJ()
		{
			Assert.True("12.123.12-4".IsIE("RJ"));
		}

		[Fact]
		public void InvalidoRJ()
		{
			Assert.False("".IsIE("RJ"));
			Assert.False("12.123.12-9".IsIE("RJ"));
		}

		[Fact]
		public void FormatarRJ()
		{
			Assert.Equal("12.123.12-4", "12123124".FormatarIE("RJ"));
		}

		[Fact]
		public void ValidoRN()
		{
			Assert.True("20.040.040-1".IsIE("RN"));
			Assert.True("20.0.040.040-0".IsIE("RN"));
		}

		[Fact]
		public void InvalidoRN()
		{
			Assert.False("".IsIE("RN"));
			Assert.False("20.040.040-9".IsIE("RN"));
			Assert.False("20.0.040.040-9".IsIE("RN"));
		}

		[Fact]
		public void FormatarRN()
		{
			Assert.Equal("20.040.040-1", "200400401".FormatarIE("RN"));
			Assert.Equal("20.0.040.040-0", "2000400400".FormatarIE("RN"));
		}

		[Fact]
		public void ValidoRS()
		{
			Assert.True("224/3658792".IsIE("RS"));
		}

		[Fact]
		public void InvalidoRS()
		{
			Assert.False("".IsIE("RS"));
			Assert.False("224/1234567".IsIE("RS"));
		}

		[Fact]
		public void FormatarRS()
		{
			Assert.Equal("224/3658792", "2243658792".FormatarIE("RS"));
		}

		[Fact]
		public void ValidoRO()
		{
			Assert.True("101.62521-3".IsIE("RO"));
			Assert.True("0000000062521-3".IsIE("RO"));
		}

		[Fact]
		public void InvalidoRO()
		{
			Assert.False("".IsIE("RO"));
			Assert.False("101.12345-6".IsIE("RO"));
			Assert.False("1234567890521-3".IsIE("RO"));
		}

		[Fact]
		public void FormatarRO()
		{
			Assert.Equal("101.62521-3", "101625213".FormatarIE("RO"));
			Assert.Equal("0000000062521-3", "00000000625213".FormatarIE("RO"));
		}

		[Fact]
		public void ValidoRR()
		{
			Assert.True("24008266-8".IsIE("RR"));
		}

		[Fact]
		public void InvalidoRR()
		{
			Assert.False("".IsIE("RR"));
			Assert.False("12345678-8".IsIE("RR"));
		}

		[Fact]
		public void FormatarRR()
		{
			Assert.Equal("24008266-8", "240082668".FormatarIE("RR"));
		}

		[Fact]
		public void ValidoSC()
		{
			Assert.True("251.040.852".IsIE("SC"));
		}

		[Fact]
		public void InvalidoSC()
		{
			Assert.False("".IsIE("SC"));
			Assert.False("123.123.123".IsIE("SC"));
		}

		[Fact]
		public void FormatarSC()
		{
			Assert.Equal("251.040.852", "251040852".FormatarIE("SC"));
		}

		[Fact]
		public void ValidoSP()
		{
			Assert.True("110.042.490.114".IsIE("SP"));
			Assert.True("P-01100424.3/002".IsIE("SP"));
		}

		[Fact]
		public void InvalidoSP()
		{
			Assert.False("".IsIE("SP"));
			Assert.False("123.123.123.123".IsIE("SP"));
			Assert.False("P-12345678.9/002".IsIE("SP"));
		}

		[Fact]
		public void FormatarSP()
		{
			Assert.Equal("110.042.490.114", "110042490114".FormatarIE("SP"));
			Assert.Equal("P-01100424.3/123", "P011004243123".FormatarIE("SP"));
		}

		[Fact]
		public void ValidoSE()
		{
			Assert.True("27123456-3".IsIE("SE"));
		}

		[Fact]
		public void InvalidoSE()
		{
			Assert.False("".IsIE("SE"));
			Assert.False("12312312-3".IsIE("SE"));
		}

		[Fact]
		public void FormatarSE()
		{
			Assert.Equal("27.123.456-3", "271234563".FormatarIE("SE"));
		}

		[Fact]
		public void ValidoTO()
		{
			Assert.True("01.022.783-0".IsIE("TO"));
			Assert.True("29.01.022783-6".IsIE("TO"));
		}

		[Fact]
		public void InvalidoTO()
		{
			Assert.False("".IsIE("TO"));
			Assert.False("12.123.123-9".IsIE("TO"));
			Assert.False("12.34.567890-6".IsIE("TO"));
		}

		[Fact]
		public void FormatarTO()
		{
			Assert.Equal("01.022.783-0", "010227830".FormatarIE("TO"));
			Assert.Equal("29.01.022783-6", "29010227836".FormatarIE("TO"));
		}
	}
}