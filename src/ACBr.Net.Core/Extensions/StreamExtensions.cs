// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 05-13-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="StreamExtensions.cs" company="ACBr.Net">
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

using System;
using System.IO;

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Class StreamExtensions.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        public static void CopyTo(this Stream input, Stream destination, int bufferSize = 1048576)
        {
            var buffer = new byte[bufferSize];
            int read = 0;
            do
            {
                read = input.Read(buffer, 0, bufferSize);
                destination.Write(buffer, 0, read);
            } while (read > 0);
        }

        /// <summary>
        /// Ares the equal.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if stream are equals, <c>false</c> otherwise.</returns>
        public static bool AreEqual(this Stream input, Stream other)
        {
            int buffer = sizeof(Int64);

            if (input.Length != other.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)input.Length / buffer);

            byte[] one = new byte[buffer];
            byte[] two = new byte[buffer];

            input.Position = 0;
            other.Position = 0;

            for (int i = 0; i < iterations; i++)
            {
                input.Read(one, 0, buffer);
                other.Read(two, 0, buffer);

                if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                {
                    input.Position = 0;
                    other.Position = 0;
                    return false;
                }
            }

            input.Position = 0;
            other.Position = 0;
            return true;
        }
    }
}
