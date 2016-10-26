using System.IO;
using System.Text;

namespace ACBr.Net.Core
{
	public sealed class ACBrStringWriter : StringWriter
	{
		#region Constructors

		public ACBrStringWriter(Encoding encoding)
		{
			Encoding = encoding;
		}

		#endregion Constructors

		#region Propriedades

		public override Encoding Encoding { get; }

		#endregion Propriedades
	}
}