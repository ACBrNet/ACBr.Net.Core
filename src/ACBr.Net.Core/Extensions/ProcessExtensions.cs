// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="ImageExtensions.cs" company="ACBr.Net">
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
using System.Diagnostics;
using System.Linq;

#if NETFULL

using System.Management;

#endif

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