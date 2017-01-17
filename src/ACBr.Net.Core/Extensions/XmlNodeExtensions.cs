using System;
using System.Globalization;
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
				xmlDoc.WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				return xmlString.ToString();
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
		/// <typeparam name="TType"></typeparam>
		/// <param name="element">The element.</param>
		/// <param name="format"></param>
		/// <returns>T.</returns>
		public static TType GetValue<TType>(this XmlNode element, IFormatProvider format = null)
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
	}
}