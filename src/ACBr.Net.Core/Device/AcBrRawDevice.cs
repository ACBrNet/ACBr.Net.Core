// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 20-12-2018
//
// Last Modified By : RFTD
// Last Modified On : 20-12-2018
// ***********************************************************************
// <copyright file="ACBrRawDevice.cs" company="ACBr.Net">
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
using System.Runtime.InteropServices;

namespace ACBr.Net.Core.Device
{
    internal sealed class ACBrRawDevice : ACBrDevice
    {
        #region Fields

        private RawPrinter printer;

        #endregion Fields

        #region InnerTypes

        internal class RawPrinter
        {
            public sealed class Windows
            {
                #region InnerTypes

                // Structure and API declarions:
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public class DOCINFOA
                {
                    [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
                    [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
                    [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
                }

                #endregion InnerTypes

                #region Imports

                [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi,
                    ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter,
                    out IntPtr hPrinter,
                    IntPtr pd);

                [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true,
                    CallingConvention = CallingConvention.StdCall)]
                public static extern bool ClosePrinter(IntPtr hPrinter);

                [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi,
                    ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                public static extern bool StartDocPrinter(IntPtr hPrinter, int level,
                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                    DOCINFOA di);

                [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true,
                    CallingConvention = CallingConvention.StdCall)]
                public static extern bool EndDocPrinter(IntPtr hPrinter);

                [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true,
                    CallingConvention = CallingConvention.StdCall)]
                public static extern bool StartPagePrinter(IntPtr hPrinter);

                [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true,
                    CallingConvention = CallingConvention.StdCall)]
                public static extern bool EndPagePrinter(IntPtr hPrinter);

                [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true,
                    CallingConvention = CallingConvention.StdCall)]
                public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

                #endregion Imports

                #region Methods

                public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
                {
                    var di = new DOCINFOA();
                    var bSuccess = false; // Assume failure unless you specifically succeed.

                    di.pDocName = "RAW Document";
                    // Win7
                    di.pDataType = "RAW";

                    // Win8+
                    // di.pDataType = "XPS_PASS";

                    // Open the printer.
                    if (OpenPrinter(szPrinterName.Normalize(), out var hPrinter, IntPtr.Zero))
                    {
                        // Start a document.
                        if (StartDocPrinter(hPrinter, 1, di))
                        {
                            // Start a page.
                            if (StartPagePrinter(hPrinter))
                            {
                                // Write your bytes.
                                bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out _);
                                EndPagePrinter(hPrinter);
                            }

                            EndDocPrinter(hPrinter);
                        }

                        ClosePrinter(hPrinter);
                    }

                    // If you did not succeed, GetLastError may give more information
                    // about why not.
                    if (bSuccess == false)
                    {
                        Marshal.GetLastWin32Error();
                    }

                    return bSuccess;
                }

                #endregion Methods
            }

            public void SendCommand(string szPrinterName, byte[] dados)
            {
                var pUnmanagedBytes = Marshal.AllocCoTaskMem(dados.Length);
                Marshal.Copy(dados, 0, pUnmanagedBytes, dados.Length);
                Windows.SendBytesToPrinter(szPrinterName, pUnmanagedBytes, dados.Length);
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
            }
        }

        #endregion InnerTypes

        #region Constructor

        public ACBrRawDevice(ACBrDeviceConfig config) : base(config)
        {
            printer = new RawPrinter();
        }

        #endregion Constructor

        #region Methods

        public override bool Ativar()
        {
            return true;
        }

        public override bool Desativar()
        {
            return true;
        }

        public override void SendCommand(byte[] dados)
        {
            var sendDados = WriteConvert(dados);
            printer.SendCommand(Config.Porta.Replace("RAW:", string.Empty), sendDados);
        }

        public override byte[] GetResposta()
        {
            throw new NotImplementedException("RAW é apenas para envio de dados, " +
                                              "usado para enviar dados para impressora usando o spool do Windows/Linux.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) GC.SuppressFinalize(this);
            printer = null;
        }

        #endregion Methods
    }
}