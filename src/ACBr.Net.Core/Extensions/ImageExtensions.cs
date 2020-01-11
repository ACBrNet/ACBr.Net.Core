// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="ImageExtensions.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
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
            if (imageIn == null) return null;

            if (format == null)
            {
                format = imageIn.RawFormat;
            }

            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, format);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// To the base64.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64(this Image imageIn, ImageFormat format = null)
        {
            if (imageIn == null) return string.Empty;
            var imgBytes = imageIn.ToByteArray(format);
            return imgBytes.ToBase64();
        }

        /// <summary>
        /// To the stream.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <param name="format">The format.</param>
        /// <returns>MemoryStream.</returns>
        public static MemoryStream ToStream(this Image imageIn, ImageFormat format = null)
        {
            if (imageIn == null) return null;

            if (format == null)
            {
                format = imageIn.RawFormat;
            }

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
            if (imageIn == null) return null;

            if (format == null)
            {
                format = imageIn.RawFormat;
            }

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
            if (image.IsNull() || toAdd.IsNull()) return;

            //get the codec for tiff files
            var info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(ice => ice.MimeType == "image/tiff");
            if (info == null) return;

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