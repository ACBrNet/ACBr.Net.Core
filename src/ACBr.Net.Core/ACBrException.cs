using System;

namespace ACBr.Net.Core
{
	public class ACBrException : ApplicationException
	{
		#region Constructor

		public ACBrException(string message) : base(message)
		{
		}

		public ACBrException(string format, params object[] args) : base(string.Format(format, args))
		{
		}

		#endregion Constructor
	}
}