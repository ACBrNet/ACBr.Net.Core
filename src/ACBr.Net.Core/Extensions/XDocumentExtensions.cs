// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 07-30-2016
//
// Last Modified By : RFTD
// Last Modified On : 07-30-2016
// ***********************************************************************
// <copyright file="XDocumentExtensions.cs" company="ACBr.Net">
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ACBr.Net.Core.Extensions
{
	public static class XDocumentExtensions
	{
		public static string AsString(this XContainer document, bool identado = false, bool showDeclaration = true)
		{
			return document.AsString(identado, showDeclaration, Encoding.UTF8);
		}

		public static string AsString(this XContainer document, bool identado, bool showDeclaration, Encoding encode)
		{
			var settings = new XmlWriterSettings
			{
				Indent = identado,
				Encoding = encode,
				OmitXmlDeclaration = !showDeclaration,
				NamespaceHandling = NamespaceHandling.OmitDuplicates
			};

			using (var xmlString = new ACBrStringWriter(encode))
			using (var xmlTextWriter = XmlWriter.Create(xmlString, settings))
			{
				document.WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				return xmlString.ToString();
			}
		}

		public static TType GetValue<TType>(this XElement element, IFormatProvider format = null)
		{
			if (element == null) return default(TType);

			TType ret;
			try
			{
				if (format == null) format = CultureInfo.InvariantCulture;

				ret = (TType)Convert.ChangeType(element.Value, typeof(TType), format);
			}
			catch (Exception)
			{
				ret = default(TType);
			}

			return ret;
		}

		public static void RemoveEmptyNs(this XContainer doc)
		{
			foreach (var node in doc.Descendants())
			{
				if (!node.Name.NamespaceName.IsEmpty()) continue;

				node.Attributes("xmlns").Remove();
				if (node.Parent != null)
				{
					node.Name = node.Parent.Name.Namespace + node.Name.LocalName;
				}
			}
		}

		public static void AddChild(this XContainer parent, params XElement[] childrens)
		{
			if (childrens == null || parent == null) return;
			if (childrens.Length < 1) return;

			parent.Add(childrens);
		}

		public static void AddAttribute(this XContainer parent, params XAttribute[] attributes)
		{
			if (attributes == null || parent == null) return;
			if (attributes.Length < 1) return;

			parent.Add(attributes);
		}

		public static XElement[] ElementsAnyNs(this XContainer source, string name)
		{
			return source.Elements().Where(e => e.Name.LocalName == name).ToArray();
		}

		public static XElement ElementAnyNs(this XContainer source, string name)
		{
			return source.Elements().SingleOrDefault(e => e.Name.LocalName == name);
		}

		public static XmlDocument ToXmlDocument(this XDocument document)
		{
			using (var reader = document.CreateReader())
			{
				var xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				return xmlDocument;
			}
		}
	}
}