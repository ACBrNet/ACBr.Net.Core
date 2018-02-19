// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 05-31-2017
//
// Last Modified By : RFTD
// Last Modified On : 05-31-2017
// ***********************************************************************
// <copyright file="ACBrTxtWriter.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2014 - 2017 Grupo ACBr.Net
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Core
{
    /// <summary>
    /// Classe para auxiliar na geração de arquivos Txt.
    /// </summary>
    public sealed class ACBrTxtWriter : IDisposable
    {
        #region Fields

        private readonly StreamWriter internalWriter;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instancia da classe <see cref="ACBrTxtWriter" />.
        /// </summary>
        /// <param name="file">O arquivo que sera salvo/editado</param>
        public ACBrTxtWriter(string file) : this(file, false, Encoding.UTF8, 1024)
        {
        }

        /// <summary>
        /// Inicializa uma nova instancia da classe <see cref="ACBrTxtWriter" />.
        /// </summary>
        /// <param name="file">O arquivo que sera salvo/editado</param>
        /// <param name="append">Adiciona texto no final</param>
        public ACBrTxtWriter(string file, bool append) : this(file, append, Encoding.UTF8, 1024)
        {
        }

        /// <summary>
        /// Inicializa uma nova instancia da classe <see cref="ACBrTxtWriter" />.
        /// </summary>
        /// <param name="file">O arquivo que sera salvo/editado</param>
        /// <param name="encoding">Encoding</param>
        public ACBrTxtWriter(string file, Encoding encoding) : this(file, false, encoding, 1024)
        {
        }

        /// <summary>
        /// Inicializa uma nova instancia da classe <see cref="ACBrTxtWriter" />.
        /// </summary>
        /// <param name="file">O arquivo que sera salvo/editado</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="bufferSize">Tamanho do buffer</param>
        public ACBrTxtWriter(string file, Encoding encoding, int bufferSize) : this(file, false, encoding, bufferSize)
        {
        }

        /// <summary>
        /// Inicializa uma nova instancia da classe <see cref="ACBrTxtWriter" />.
        /// </summary>
        /// <param name="file">O arquivo que sera salvo/editado</param>
        /// <param name="append">Adiciona texto no final</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="bufferSize">Tamanho do buffer</param>
        public ACBrTxtWriter(string file, bool append, Encoding encoding, int bufferSize)
        {
            Guard.Against<ArgumentNullException>(file.IsEmpty(), nameof(file));
            Guard.Against<ArgumentNullException>(encoding == null, nameof(encoding));

            internalWriter = new StreamWriter(file, append, encoding, bufferSize);
            LineCount = 0;
        }

        /// <inheritdoc />
        ~ACBrTxtWriter()
        {
            Dispose(false);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Retorna a quantidade de linhas adicionadas no arquivo.
        /// </summary>
        public int LineCount { get; private set; }

        /// <summary>
        /// Retorna o o enconde usado no arquivo.
        /// </summary>
        public Encoding Encoding => internalWriter.Encoding;

        /// <summary>
        /// Retorna/define se o flush vai ser realizado de forma automática.
        /// Não utiliza o buffer.
        /// </summary>
        public bool AutoFLush
        {
            get => internalWriter.AutoFlush;
            set => internalWriter.AutoFlush = value;
        }

        #endregion Properties

        #region Methods

        /// <summary>Writes a string followed by a line terminator to the text string or stream.</summary>
        /// <param name="value">The string to write. If <paramref name="value" /> is null, only the line terminator is written. </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public void WriteLine(string value)
        {
            var values = value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            LineCount += values.Length;

            foreach (var line in values)
            {
                internalWriter.WriteLine(line);
            }
        }

        /// <summary>Writes a string followed by a line terminator to the text string or stream.</summary>
        /// <param name="values">The string to write. If <paramref name="values" /> is null, only the line terminator is written. </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public void WriteLine(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                var lines = value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                LineCount += lines.Length;
                foreach (var line in lines)
                {
                    internalWriter.WriteLine(line);
                }
            }
        }

        /// <summary>Closes the current StreamWriter object and the underlying stream.</summary>
        /// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
        public void Close()
        {
            Dispose(true);
        }

        /// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.</summary>
        /// <exception cref="T:System.ObjectDisposedException">The current writer is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair. </exception>
        public void Flush()
        {
            internalWriter.Flush();
        }

        #endregion Methods

        #region Interface Methods

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing"></param>
        /// <filterpriority>2</filterpriority>
        private void Dispose(bool disposing)
        {
            if (disposing) GC.SuppressFinalize(this);

            internalWriter.Dispose();
        }

        #endregion Interface Methods
    }
}