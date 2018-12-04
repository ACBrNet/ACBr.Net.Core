using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ACBr.Net.Core.Exceptions;

namespace ACBr.Net.Core.Extensions
{
    public static partial class X509Certificate2Extensions
    {
        #region Fields

        [DllImport("Advapi32.dll", EntryPoint = "CryptReleaseContext", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

        #endregion Fields

        #region Methods

        /// <summary>
        /// Retorna true se o certificado for do tipo A3.
        /// </summary>
        /// <param name="certificado">Certificado que deverá ser validado se é A3 ou não.</param>
        /// <returns></returns>
        public static bool IsA3(this X509Certificate2 certificado)
        {
            Guard.Against<ArgumentNullException>(certificado == null, nameof(certificado));

            var result = false;

            try
            {
                if (certificado.PrivateKey is RSACryptoServiceProvider service)
                {
                    result = service.CspKeyContainerInfo.Removable && service.CspKeyContainerInfo.HardwareDevice;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Seta o pin do certificado se o mesmo for do tipo A3.
        /// </summary>
        /// <param name="certificado">O certificado</param>
        /// <param name="pin">O pin</param>
        public static void SetPin(this X509Certificate2 certificado, string pin)
        {
            Guard.Against<ArgumentNullException>(pin.IsEmpty(), nameof(pin));
            if (!certificado.IsA3()) return;

            // prepare password
            var pass = new SecureString();
            foreach (var t in pin)
            {
                pass.AppendChar(t);
            }

            // take private key
            var privateKey = certificado.PrivateKey as RSACryptoServiceProvider;

            // make new CSP parameters based on parameters from current private key but throw in password
            var cspParameters = new CspParameters(privateKey.CspKeyContainerInfo.ProviderType,
                privateKey.CspKeyContainerInfo.ProviderName,
                privateKey.CspKeyContainerInfo.KeyContainerName,
                new System.Security.AccessControl.CryptoKeySecurity(),
                pass);

            // make RSA crypto provider based on given CSP parameters
            var rsaCsp = new RSACryptoServiceProvider(cspParameters);

            // set modified RSA crypto provider back
            certificado.PrivateKey = rsaCsp;
        }

        /// <summary>
        /// Força a liberação de certificado A3.
        /// </summary>
        /// <param name="certificado"></param>
        public static void ForceUnload(this X509Certificate2 certificado)
        {
            if (certificado == null) return;
            if (!certificado.IsA3()) return;

            CryptReleaseContext(certificado.Handle, 0);
        }

        #endregion Methods
    }
}