// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EventHandlerExtension.cs" company="ACBr.Net">
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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class ImageExtensions.
	/// </summary>
	public static class ImageExtensions
	{
		/// <summary>
		/// To the byte array.
		/// </summary>
		/// <param name="imageIn">The image in.</param>
		/// <param name="format">The format.</param>
		/// <returns>System.Byte[].</returns>
		public static byte[] ToByteArray(this Image imageIn, ImageFormat format = null)
		{
			if (imageIn == null)
				return null;

			if (format == null)
				format = imageIn.RawFormat;

			using (var ms = new MemoryStream())
			{
				imageIn.Save(ms, format);
				return ms.ToArray();
			}
		}

		/// <summary>
		/// To the stream.
		/// </summary>
		/// <param name="imageIn">The image in.</param>
		/// <param name="format">The format.</param>
		/// <returns>MemoryStream.</returns>
		public static MemoryStream ToStream(this Image imageIn, ImageFormat format = null)
		{
			if (imageIn == null)
				return null;

			if (format == null)
				format = imageIn.RawFormat;

			var ms = new MemoryStream();
			imageIn.Save(ms, format);
			ms.Seek(0, SeekOrigin.Begin);
			return ms;
		}

		/// <summary>
		/// To the file stream.
		/// </summary>
		/// <param name="imageIn">The image in.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="format">The format.</param>
		/// <returns>FileStream.</returns>
		public static FileStream ToFileStream(this Image imageIn, string fileName, ImageFormat format = null)
		{
			if (imageIn == null)
				return null;

			if (format == null)
				format = imageIn.RawFormat;

			var ms = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite);
			imageIn.Save(ms, format);
			ms.Seek(0, SeekOrigin.Begin);
			return ms;
		}

		/// <summary>
		/// Adds the page.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="toAdd">To add.</param>
		public static void AddPage(this Image image, Image toAdd)
		{
			if (image.IsNull() || toAdd.IsNull())
				return;

			//get the codec for tiff files
			var info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(ice => ice.MimeType == "image/tiff");
			if (info == null)
				return;

			//use the save encoder
			var enc = Encoder.SaveFlag;
			var ep = new EncoderParameters(1);

			var frame = image.GetFrameCount(FrameDimension.Page);
			if (frame == 0)
			{
				//save the first frame
				ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);
				image.Save(toAdd.ToStream(), info, ep);
			}
			else
			{
				//save the intermediate frames
				ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);
				image.SaveAdd(toAdd, ep);
			}

			ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
			image.SaveAdd(ep);
		}
	}
}