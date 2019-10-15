// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 20-12-2018
//
// Last Modified By : RFTD
// Last Modified On : 20-12-2018
// ***********************************************************************
// <copyright file="ACBrTcpDevice.cs" company="ACBr.Net">
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
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ACBr.Net.Core.Device
{
    internal sealed class ACBrTcpDevice : ACBrDevice
    {
        #region Fields

        private TcpClient client;
        private readonly IPEndPoint conEndPoint;

        #endregion Fields

        #region Constructor

        public ACBrTcpDevice(ACBrDeviceConfig config) : base(config)
        {
            var ports = Config.Porta.Split(':');
            if (ports.Length < 3) throw new ArgumentException("Endereço e porta não informados");

            conEndPoint = new IPEndPoint(IPAddress.Parse(ports[1]), int.Parse(ports[2]));
            client = new TcpClient();
        }

        #endregion Constructor

        #region Methods

        public override bool Ativar()
        {
            if (client.Connected) return false;

            client.Connect(conEndPoint);

            return client.Connected;
        }

        public override bool Desativar()
        {
            if (!client.Connected) return false;

            client.GetStream().Close();
            client.Close();

            client = null;
            client = new TcpClient();

            return !client.Connected;
        }

        public override void SendCommand(byte[] cmd)
        {
            if (cmd.Length < 1) return;

            var sendDados = WriteConvert(cmd);
            client.GetStream().Write(sendDados, 0, sendDados.Length);
        }

        public override byte[] GetResposta()
        {
            var ret = new List<byte>();
            var stream = client.GetStream();

            while (client.Available > 0)
            {
                var inbyte = new byte[1];
                stream.Read(inbyte, 0, 1);
                if (inbyte.Length < 1) continue;

                var value = (byte)inbyte.GetValue(0);
                ret.Add(value);
            }

            var retBytes = ReadConvert(ret.ToArray());
            return retBytes;
        }

        #endregion Methods

        #region Dispose Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing) GC.SuppressFinalize(this);
            ((IDisposable)client)?.Dispose();
        }

        #endregion Dispose Methods
    }
}