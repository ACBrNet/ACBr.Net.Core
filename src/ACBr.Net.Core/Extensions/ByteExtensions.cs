// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-19-2014
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="AssemblyExtenssions.cs" company="ACBr.Net">
// Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la
// sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio)
// qualquer vers�o posterior.
//
// Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM
// NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU
// ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor
// do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)
//
// Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto
// com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,
// no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Voc� tamb�m pode obter uma copia da licen�a em:
// http://www.opensource.org/licenses/lgpl-license.php
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Drawing;
using System.IO;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class ByteExtensions.
	/// </summary>
	public static class ByteExtensions
	{
		/// <summary>
		/// To the image.
		/// </summary>
		/// <param name="byteArrayIn">The byte array in.</param>
		/// <returns>Image.</returns>
		public static Image ToImage(this byte[] byteArrayIn)
		{
			if (byteArrayIn == null)
				return null;

			using (var ms = new MemoryStream(byteArrayIn))
			{
				var returnImage = Image.FromStream(ms);
				return returnImage;
			}
		}
	}
}