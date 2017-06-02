using System;
using System.IO;
using Xunit;

namespace ACBr.Net.Core.Tests
{
	public class ACBrTxtWriterTest
	{
		[Fact]
		public void LineCountTest()
		{
			using (var writer = new ACBrTxtWriter("Teste.txt"))
			{
				writer.WriteLine("Teste 1");
				writer.WriteLine("Teste 2\nTeste 3");
				writer.WriteLine("Teste 4 \r\nTeste 5");
				writer.WriteLine($"Teste 6{Environment.NewLine}Teste 7\n");
				writer.WriteLine("");

				Assert.True(writer.LineCount == 9);

				var lines = new[]
				{
					"Teste1",
					"Teste2\nTeste3",
					"Teste4\r\nTeste5",
					$"Teste6{Environment.NewLine}Teste7"
				};

				writer.WriteLine(lines);
				Assert.True(writer.LineCount == 16);

				writer.Flush();

				Assert.True(File.Exists("Teste.txt"));
			}

			Assert.True(File.ReadAllLines("Teste.txt").Length == 16);
		}
	}
}