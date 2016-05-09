// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EventHandlerExtension.cs" company="ACBr.Net">
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