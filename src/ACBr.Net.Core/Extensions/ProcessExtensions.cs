using System;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class ProcessExtensions.
	/// </summary>
	public static class ProcessExtensions
	{
		/// <summary>
		/// Gets the owner.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <returns>System.String.</returns>
		public static string GetOwner(this Process process)
		{
			var query = "Select * From Win32_Process Where ProcessID = " + process.Id;
			var searcher = new ManagementObjectSearcher(query);
			var processList = searcher.Get();

			foreach (var obj in processList.Cast<ManagementObject>())
			{
				object[] argList = { string.Empty, string.Empty };
				var returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
				if (returnVal == 0) return argList[0].ToString();
			}

			return string.Empty;
		}
	}
}