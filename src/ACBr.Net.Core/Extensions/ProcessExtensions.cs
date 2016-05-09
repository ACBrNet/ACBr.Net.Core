// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EventHandlerExtension.cs" company="ACBr.Net">
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
				if (returnVal == 0)
					return argList[0].ToString();
			}

			return string.Empty;
		}
	}
}