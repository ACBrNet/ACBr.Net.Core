using System;
using System.Collections.Generic;
using System.Linq;

namespace ACBr.Net.Core.Device
{
    public static class ACBrDeviceManager
    {
        #region Fields

        private static Dictionary<string, Type> communications;

        #endregion Fields

        #region Constructors

        static ACBrDeviceManager()
        {
            communications = new Dictionary<string, Type>
            {
                {"LPT", typeof(ACBrSerialDevice)},
                {"COM", typeof(ACBrSerialDevice)},
                {"TCP", typeof(ACBrTcpDevice)},
                {"RAW", typeof(ACBrRawDevice)}
            };
        }

        #endregion Constructors

        /// <summary>
        /// Registrar uma nova classe de comunicação
        /// </summary>
        /// <param name="tag"></param>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>(string tag) where T : ACBrDevice
        {
            communications.Add(tag, typeof(T));
        }

        /// <summary>
        /// Função para checar se a porta é valida
        /// </summary>
        /// <param name="porta"></param>
        /// <returns></returns>
        public static bool IsValidPort(string porta)
        {
            return (from c in communications
                    where porta.ToUpper().StartsWith(c.Key)
                    select c.Value).Any();
        }

        /// <summary>
        /// Retorna a classe para comunicação
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ACBrDevice GetCommunication(ACBrDeviceConfig config)
        {
            var communication = (from c in communications
                                 where config.Porta.ToUpper().StartsWith(c.Key)
                                 select c.Value).FirstOrDefault();

            Guard.Against<ACBrException>(communication == null, "Classe de comunicação não localizada.");
            return (ACBrDevice)Activator.CreateInstance(communication, config);
        }
    }
}