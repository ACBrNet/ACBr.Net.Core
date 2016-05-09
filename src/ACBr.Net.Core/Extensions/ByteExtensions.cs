// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-19-2014
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="AssemblyExtenssions.cs" company="ACBr.Net">
// Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la
// sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério)
// qualquer versão posterior.
//
// Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM
// NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU
// ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor
// do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)
//
// Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto
// com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,
// no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Você também pode obter uma copia da licença em:
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