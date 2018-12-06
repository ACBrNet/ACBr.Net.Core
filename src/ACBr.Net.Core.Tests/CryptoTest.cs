using System.Security.Cryptography;
using ACBr.Net.Core.Extensions;
using Xunit;

namespace ACBr.Net.Core.Tests
{
    public class CryptoTest
    {
        private const string Key = @"a1!B78s!5(h23y1g12\t\98w";

        [Fact]
        public void TripeDesCrypt()
        {
            Assert.Equal("N9DN1Vek2Gw1nT3hv7JuBA==", "TesteACBr".Encrypt(Key));
            Assert.Equal("TesteACBr", "N9DN1Vek2Gw1nT3hv7JuBA==".Decrypt(Key));
            Assert.Throws<ACBrException>(() => "N9DN1Vek2Gw1nT3hv7JuBA===".Decrypt(Key));
        }

        [Fact]
        public void DesCrypt()
        {
            Assert.Equal("gp93NTcn4qg88xlB6QSF0A==", "TesteACBr".Encrypt<DESCryptoServiceProvider>(Key));
            Assert.Equal("TesteACBr", "gp93NTcn4qg88xlB6QSF0A==".Decrypt<DESCryptoServiceProvider>(Key));
            Assert.Throws<ACBrException>(() => "gp93NTcn4qg88xlB6QSF0A===".Decrypt<DESCryptoServiceProvider>(Key));
        }

        [Fact]
        public void AesCrypt()
        {
            Assert.Equal("WQ3DZ+pyDepYyUX7UuotMg==", "TesteACBr".Encrypt<AesCryptoServiceProvider>(Key));
            Assert.Equal("TesteACBr", "WQ3DZ+pyDepYyUX7UuotMg==".Decrypt<AesCryptoServiceProvider>(Key));
            Assert.Throws<ACBrException>(() => "WQ3DZ+pyDepYyUX7UuotMg===".Decrypt<AesCryptoServiceProvider>(Key));
        }

        [Fact]
        public void RijndaelCrypt()
        {
            Assert.Equal("WQ3DZ+pyDepYyUX7UuotMg==", "TesteACBr".Encrypt<RijndaelManaged>(Key));
            Assert.Equal("TesteACBr", "WQ3DZ+pyDepYyUX7UuotMg==".Decrypt<RijndaelManaged>(Key));
            Assert.Throws<ACBrException>(() => "WQ3DZ+pyDepYyUX7UuotMg===".Decrypt<RijndaelManaged>(Key));
        }

        [Fact]
        public void RC2Crypt()
        {
            Assert.Equal("Aif7jZ9sARRI4lG2p450hg==", "TesteACBr".Encrypt<RC2CryptoServiceProvider>(Key));
            Assert.Equal("TesteACBr", "Aif7jZ9sARRI4lG2p450hg==".Decrypt<RC2CryptoServiceProvider>(Key));
            Assert.Throws<ACBrException>(() => "Aif7jZ9sARRI4lG2p450hg===".Decrypt<RC2CryptoServiceProvider>(Key));
        }

        [Fact]
        public void AesManagedCrypt()
        {
            Assert.Equal("WQ3DZ+pyDepYyUX7UuotMg==", "TesteACBr".Encrypt<AesManaged>(Key));
            Assert.Equal("TesteACBr", "WQ3DZ+pyDepYyUX7UuotMg==".Decrypt<AesManaged>(Key));
            Assert.Throws<ACBrException>(() => "WQ3DZ+pyDepYyUX7UuotMg===".Decrypt<AesManaged>(Key));
        }
    }
}