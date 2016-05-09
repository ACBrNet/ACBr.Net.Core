// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="XmlDocumentExtensions.cs" company="ACBr.Net">
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

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Class XmlNodeExtensions.
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Retorna a XML como string
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <param name="identado">Se for <c>true</c> o XML sai [identado].</param>
        /// <param name="showDeclaration">if set to <c>true</c> [show declaration].</param>
        /// <returns>System.String.</returns>
        public static string AsString(this XmlNode xmlDoc, bool identado = false, bool showDeclaration = true)
        {
            return xmlDoc.AsString(identado, showDeclaration, Encoding.UTF8);
        }

        /// <summary>
        /// Retorna a XML como string
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <param name="identado">Se for <c>true</c> o XML sai [identado].</param>
        /// <param name="showDeclaration">if set to <c>true</c> [show declaration].</param>
        /// <param name="encode">O enconding do XML.</param>
        /// <returns>System.String.</returns>
        public static string AsString(this XmlNode xmlDoc, bool identado, bool showDeclaration, Encoding encode)
        {
            using (var stringWriter = new StringWriter())
            {
                var settings = new XmlWriterSettings
                {
                    Indent = identado,
                    Encoding = encode,
                    OmitXmlDeclaration = !showDeclaration
                };
                using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlDoc.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                }
            }
        }

        /// <summary>
        /// Adiciona uma tag ao documento ignorando os elementos nulos.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <param name="tag">The tag.</param>
        public static void AddTag(this XmlNode xmlDoc, XmlNode tag)
        {
            if (tag == null)
                return;

            xmlDoc.AppendChild(tag);
        }

        /// <summary>
        /// Adiciona varias tag ao documento ignorando os elementos nulos.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <param name="tags">The tags.</param>
        public static void AddTag(this XmlNode xmlDoc, XmlNode[] tags)
        {
            if (tags.Length < 1)
                return;

            foreach (var tag in tags.Where(tag => tag != null))
            {
                xmlDoc.AppendChild(tag);
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <returns>T.</returns>
        public static T GetValue<T>(this XmlNode element)
        {
            if (element == null)
                return default(T);

            T ret;
            try
            {
                ret = (T)Convert.ChangeType(element.InnerText, typeof(T));
            }
            catch (Exception)
            {
                ret = default(T);
            }

            return ret;
        }
    }
}
