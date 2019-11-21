// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 20-12-2018
//
// Last Modified By : RFTD
// Last Modified On : 20-12-2018
// ***********************************************************************
// <copyright file="ACBrSerialDevice.cs" company="ACBr.Net">
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
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace ACBr.Net.Core.Device
{
    internal sealed class ACBrSerialDevice : ACBrDevice
    {
        #region Fields

        private readonly SerialPort serialPort;

        #endregion Fields

        #region Constructor

        public ACBrSerialDevice(ACBrDeviceConfig config) : base(config)
        {
            serialPort = new SerialPort();
        }

        #endregion Constructor

        #region Methods

        public override bool Ativar()
        {
            if (serialPort.IsOpen) return false;

            ConfigSerial();
            serialPort.Open();

            return serialPort.IsOpen;
        }

        public override bool Desativar()
        {
            if (!serialPort.IsOpen) return false;

            serialPort.Close();

            return !serialPort.IsOpen;
        }

        public override void SendCommand(byte[] cmd)
        {
            if (cmd.Length < 1) return;

            var writeBytes = WriteConvert(cmd);
            serialPort.Write(writeBytes, 0, writeBytes.Length);
        }

        public override byte[] GetResposta()
        {
            var ret = new List<byte>();
            while (serialPort.BytesToRead > 0)
            {
                var inbyte = new byte[1];
                serialPort.Read(inbyte, 0, 1);
                if (inbyte.Length < 1) continue;

                var value = (byte)inbyte.GetValue(0);
                ret.Add(value);
            }

            var readBytes = ReadConvert(ret.ToArray());
            return readBytes;
        }

        private void ConfigSerial()
        {
            serialPort.PortName = Config.Porta;
            serialPort.BaudRate = Config.Baud;
            serialPort.DataBits = Config.DataBits;
            serialPort.Parity = Config.Parity;
            serialPort.StopBits = Config.StopBits;
            serialPort.Handshake = Config.Handshake;
            serialPort.ReadTimeout = Config.TimeOut;
            serialPort.WriteTimeout = Config.TimeOut;
            serialPort.ReadBufferSize = Config.ReadBufferSize;
            serialPort.WriteBufferSize = Config.WriteBufferSize;
            serialPort.Encoding = Config.Encoding;
        }

        #endregion Methods

        #region Dispose Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing) GC.SuppressFinalize(this);

            serialPort?.Dispose();
        }

        #endregion Dispose Methods
    }
}