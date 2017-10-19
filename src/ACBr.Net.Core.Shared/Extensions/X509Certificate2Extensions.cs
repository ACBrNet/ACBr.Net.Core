using ACBr.Net.Core.Exceptions;
using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Extensões para Certificados
	/// </summary>
	public static class X509Certificate2Extensions
	{
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
				var service = certificado.PrivateKey as RSACryptoServiceProvider;

				if (service != null)
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
		/// Retorna o CNPJ do certificado se o mesmo possuir.
		/// </summary>
		/// <param name="certificado">Certificado</param>
		/// <returns></returns>
		public static string GetCNPJ(this X509Certificate2 certificado)
		{
			Guard.Against<ArgumentNullException>(certificado == null, nameof(certificado));

			var cnpj = string.Empty;
			var extensions = from X509Extension extension in certificado.Extensions
							 select extension.Format(true) into s1
							 select s1.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach (var lines in extensions)
			{
				foreach (var t in lines)
				{
					if (!t.Trim().StartsWith("2.16.76.1.3.3")) continue;

					var value = t.Substring(t.IndexOf('=') + 1);
					var elements = value.Split(' ');
					var cnpjBytes = new byte[14];

					for (var j = 0; j < cnpjBytes.Length; j++)
					{
						cnpjBytes[j] = Convert.ToByte(elements[j + 2], 16);
					}

					cnpj = Encoding.UTF8.GetString(cnpjBytes);
					break;
				}

				if (!cnpj.IsEmpty()) break;
			}

			return cnpj;
		}

		/// <summary>
		/// Seta o pin do certificado se o mesmo for do tipo A3.
		/// </summary>
		/// <param name="certificado">O certificado</param>
		/// <param name="pin">O pin</param>
		public static void SetPin(this X509Certificate2 certificado, string pin)
		{
			Guard.Against<ArgumentNullException>(certificado == null, nameof(certificado));

			if (!certificado.IsA3()) return;

			// prepare password
			var pass = new SecureString();
			var passPhrase = pin.ToCharArray();

			foreach (var t in passPhrase)
			{
				pass.AppendChar(t);
			}

			// take private key
			var privateKey = certificado.PrivateKey as RSACryptoServiceProvider;

			// make new CSP parameters based on parameters from current private key but throw in password
			var cspParameters = new CspParameters(privateKey.CspKeyContainerInfo.ProviderType,
				privateKey.CspKeyContainerInfo.ProviderName,
				privateKey.CspKeyContainerInfo.KeyContainerName)
			{ KeyPassword = pass };

			// make RSA crypto provider based on given CSP parameters
			var rsaCsp = new RSACryptoServiceProvider(cspParameters);

			// set modified RSA crypto provider back
			certificado.PrivateKey = rsaCsp;
		}
	}
}