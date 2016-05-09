using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
    public class RoundABNTTest
	{
		[Fact]
	    public void AsIntegerImpar()
		{
			var expected = 5M;
			Assert.Equal(expected, 5.1M.RoundABNT(0));
			Assert.Equal(expected, 5.2M.RoundABNT(0));
			Assert.Equal(expected, 5.3M.RoundABNT(0));
			Assert.Equal(expected, 5.4M.RoundABNT(0));

			expected = 6M;
			Assert.Equal(expected, 5.5M.RoundABNT(0));
			Assert.Equal(expected, 5.6M.RoundABNT(0));
			Assert.Equal(expected, 5.7M.RoundABNT(0));
			Assert.Equal(expected, 5.8M.RoundABNT(0));
			Assert.Equal(expected, 5.9M.RoundABNT(0));
		}

		[Fact]
		public void AsIntegerPar()
		{
			var expected = 4M;
			Assert.Equal(expected, 4.1M.RoundABNT(0));
			Assert.Equal(expected, 4.2M.RoundABNT(0));
			Assert.Equal(expected, 4.3M.RoundABNT(0));
			Assert.Equal(expected, 4.4M.RoundABNT(0));
			Assert.Equal(expected, 4.5M.RoundABNT(0));

			expected = 5M;
			Assert.Equal(expected, 4.501M.RoundABNT(0));
			Assert.Equal(expected, 4.6M.RoundABNT(0));
			Assert.Equal(expected, 4.7M.RoundABNT(0));
			Assert.Equal(expected, 4.8M.RoundABNT(0));
			Assert.Equal(expected, 4.9M.RoundABNT(0));
		}

		[Fact]
		public void TresParaDuasCasasDecimais()
		{
			var expected = 4.86M;
			Assert.Equal(expected, 4.855M.RoundABNT());

			expected = 4.56M;
			Assert.Equal(expected, 4.555M.RoundABNT());

			expected = 5.10M;
			Assert.Equal(expected, 5.101M.RoundABNT());
			Assert.Equal(expected, 5.102M.RoundABNT());
			Assert.Equal(expected, 5.103M.RoundABNT());
			Assert.Equal(expected, 5.104M.RoundABNT());
			Assert.Equal(expected, 5.105M.RoundABNT());

			expected = 5.11M;
			Assert.Equal(expected, 5.1050123M.RoundABNT());
			Assert.Equal(expected, 5.106M.RoundABNT());
			Assert.Equal(expected, 5.107M.RoundABNT());
			Assert.Equal(expected, 5.108M.RoundABNT());
			Assert.Equal(expected, 5.109M.RoundABNT());
		}

		[Fact]
		public void QuatroParaDuasCasasDecimais()
		{
			var expected = 5.10M;
			Assert.Equal(expected, 5.1010M.RoundABNT());
			Assert.Equal(expected, 5.1011M.RoundABNT());
			Assert.Equal(expected, 5.1012M.RoundABNT());
			Assert.Equal(expected, 5.1013M.RoundABNT());
			Assert.Equal(expected, 5.1014M.RoundABNT());
			Assert.Equal(expected, 5.1015M.RoundABNT());
			Assert.Equal(expected, 5.1016M.RoundABNT());
			Assert.Equal(expected, 5.1017M.RoundABNT());
			Assert.Equal(expected, 5.1018M.RoundABNT());
			Assert.Equal(expected, 5.1019M.RoundABNT());

			Assert.Equal(expected, 5.1020M.RoundABNT());
			Assert.Equal(expected, 5.1021M.RoundABNT());
			Assert.Equal(expected, 5.1022M.RoundABNT());
			Assert.Equal(expected, 5.1023M.RoundABNT());
			Assert.Equal(expected, 5.1024M.RoundABNT());
			Assert.Equal(expected, 5.1025M.RoundABNT());
			Assert.Equal(expected, 5.1026M.RoundABNT());
			Assert.Equal(expected, 5.1027M.RoundABNT());
			Assert.Equal(expected, 5.1028M.RoundABNT());
			Assert.Equal(expected, 5.1029M.RoundABNT());

			Assert.Equal(expected, 5.1030M.RoundABNT());
			Assert.Equal(expected, 5.1031M.RoundABNT());
			Assert.Equal(expected, 5.1032M.RoundABNT());
			Assert.Equal(expected, 5.1033M.RoundABNT());
			Assert.Equal(expected, 5.1034M.RoundABNT());
			Assert.Equal(expected, 5.1035M.RoundABNT());
			Assert.Equal(expected, 5.1036M.RoundABNT());
			Assert.Equal(expected, 5.1037M.RoundABNT());
			Assert.Equal(expected, 5.1038M.RoundABNT());
			Assert.Equal(expected, 5.1039M.RoundABNT());

			Assert.Equal(expected, 5.1040M.RoundABNT());
			Assert.Equal(expected, 5.1041M.RoundABNT());
			Assert.Equal(expected, 5.1042M.RoundABNT());
			Assert.Equal(expected, 5.1043M.RoundABNT());
			Assert.Equal(expected, 5.1044M.RoundABNT());
			Assert.Equal(expected, 5.1045M.RoundABNT());
			Assert.Equal(expected, 5.1046M.RoundABNT());
			Assert.Equal(expected, 5.1047M.RoundABNT());
			Assert.Equal(expected, 5.1048M.RoundABNT());
			Assert.Equal(expected, 5.1049M.RoundABNT());
			Assert.Equal(expected, 5.1050M.RoundABNT());

			expected = 5.11M;
			Assert.Equal(expected, 5.1051M.RoundABNT());
			Assert.Equal(expected, 5.1052M.RoundABNT());
			Assert.Equal(expected, 5.1053M.RoundABNT());
			Assert.Equal(expected, 5.1054M.RoundABNT());
			Assert.Equal(expected, 5.1055M.RoundABNT());
			Assert.Equal(expected, 5.1056M.RoundABNT());
			Assert.Equal(expected, 5.1057M.RoundABNT());
			Assert.Equal(expected, 5.1058M.RoundABNT());
			Assert.Equal(expected, 5.1059M.RoundABNT());

			Assert.Equal(expected, 5.1060M.RoundABNT());
			Assert.Equal(expected, 5.1061M.RoundABNT());
			Assert.Equal(expected, 5.1062M.RoundABNT());
			Assert.Equal(expected, 5.1063M.RoundABNT());
			Assert.Equal(expected, 5.1064M.RoundABNT());
			Assert.Equal(expected, 5.1065M.RoundABNT());
			Assert.Equal(expected, 5.1066M.RoundABNT());
			Assert.Equal(expected, 5.1067M.RoundABNT());
			Assert.Equal(expected, 5.1068M.RoundABNT());
			Assert.Equal(expected, 5.1069M.RoundABNT());

			Assert.Equal(expected, 5.1070M.RoundABNT());
			Assert.Equal(expected, 5.1071M.RoundABNT());
			Assert.Equal(expected, 5.1072M.RoundABNT());
			Assert.Equal(expected, 5.1073M.RoundABNT());
			Assert.Equal(expected, 5.1074M.RoundABNT());
			Assert.Equal(expected, 5.1075M.RoundABNT());
			Assert.Equal(expected, 5.1076M.RoundABNT());
			Assert.Equal(expected, 5.1077M.RoundABNT());
			Assert.Equal(expected, 5.1078M.RoundABNT());
			Assert.Equal(expected, 5.1079M.RoundABNT());

			Assert.Equal(expected, 5.1080M.RoundABNT());
			Assert.Equal(expected, 5.1081M.RoundABNT());
			Assert.Equal(expected, 5.1082M.RoundABNT());
			Assert.Equal(expected, 5.1083M.RoundABNT());
			Assert.Equal(expected, 5.1084M.RoundABNT());
			Assert.Equal(expected, 5.1085M.RoundABNT());
			Assert.Equal(expected, 5.1086M.RoundABNT());
			Assert.Equal(expected, 5.1087M.RoundABNT());
			Assert.Equal(expected, 5.1088M.RoundABNT());
			Assert.Equal(expected, 5.1089M.RoundABNT());

			Assert.Equal(expected, 5.1090M.RoundABNT());
			Assert.Equal(expected, 5.1091M.RoundABNT());
			Assert.Equal(expected, 5.1092M.RoundABNT());
			Assert.Equal(expected, 5.1093M.RoundABNT());
			Assert.Equal(expected, 5.1094M.RoundABNT());
			Assert.Equal(expected, 5.1095M.RoundABNT());
			Assert.Equal(expected, 5.1096M.RoundABNT());
			Assert.Equal(expected, 5.1097M.RoundABNT());
			Assert.Equal(expected, 5.1098M.RoundABNT());
			Assert.Equal(expected, 5.1099M.RoundABNT());
		}

		[Fact]
		public void ExpressaoDuasCasasDecimais()
		{
			const decimal dblValorUnit = 0.99M;
			const decimal dblQtde = 0.995M;
			const decimal dblTotal = dblValorUnit * dblQtde;

			var expected = 0.98M;
			Assert.Equal(expected, 0.9849M.RoundABNT());
			Assert.Equal(expected, 0.9850M.RoundABNT());

			expected = 0.99M;
			Assert.Equal(expected, 0.98505M.RoundABNT());
			Assert.Equal(expected, dblTotal.RoundABNT());
			Assert.Equal(expected, (dblValorUnit * dblQtde).RoundABNT());
		}

		[Fact]
	    public void TestesEstouro()
	    {
			var expected = 12334234.46M;
			Assert.Equal(expected, 12334234.4567567567567567567M.RoundABNT(-2));

			expected = 12334234.4568M;
			Assert.Equal(expected, 12334234.4567567567567567567M.RoundABNT(-4));

			expected = 5233.456757M;
			Assert.Equal(expected, 5233.4567567567567567567M.RoundABNT(-6));

			expected = 9999999999.46M;
			Assert.Equal(expected, 9999999999.4567567567567567567M.RoundABNT(-2));
		}

		[Fact]
	    public void ValoresNegativos()
		{
			var expected = -2M;
			Assert.Equal(expected, expected.RoundABNT(0));
			Assert.Equal(expected, expected.RoundABNT(-1));
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -0.94M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -0.95M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -0.96M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -0.97M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -0.98M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -0.99M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -1.94M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -1.95M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -1.96M;
			Assert.Equal(expected, expected.RoundABNT(-2));

			expected = -1.97M;
			Assert.Equal(expected, expected.RoundABNT(-2));
		}

		[Fact]
		public void DoctoABNTRegra2_1()
	    {
		    Assert.Equal(1.3M, 1.3333M.RoundABNT(1));
	    }

		[Fact]
		public void DoctoABNTRegra2_2()
		{
			Assert.Equal(1.7M, 1.6666M.RoundABNT(1));
			Assert.Equal(4.9M, 4.8505M.RoundABNT(1));
		}

		[Fact]
		public void DoctoABNTRegra2_3()
		{
			Assert.Equal(4.6M, 4.5500M.RoundABNT(1));
		}

		[Fact]
		public void DoctoABNTRegra2_4()
		{
			Assert.Equal(4.8M, 4.8500M.RoundABNT(1));
		}
	}
}
