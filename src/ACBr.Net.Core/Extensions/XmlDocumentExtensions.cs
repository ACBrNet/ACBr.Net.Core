// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="XmlDocumentExtensions.cs" company="ACBr.Net">
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
		/// Adiciona varias tag ao documento ignorando os elementos nulos.
		/// </summary>
		/// <param name="xmlDoc">The XML document.</param>
		/// <param name="tags">The tags.</param>
		public static void AddTag(this XmlNode xmlDoc, params XmlNode[] tags)
		{
			if (tags.Length < 1) return;

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
			if (element == null) return default(T);

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