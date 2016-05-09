// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="LoggerProvider.cs" company="ACBr.Net">
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
using System.Configuration;
using System.IO;
using System.Linq;

namespace ACBr.Net.Core.Logging
{
    /// <summary>
    /// Classe LoggerProvider.
    /// </summary>
	public class LoggerProvider
    {
        #region Fields

        /// <summary>
        /// The logger conf key
        /// </summary>
        private const string LoggerConfKey = "acbr-logger";
        /// <summary>
        /// The logger factory
        /// </summary>
		private readonly ILoggerFactory LoggerFactory;
        /// <summary>
        /// The _instance
        /// </summary>
		private static LoggerProvider _instance;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes static members of the <see cref="LoggerProvider"/> class.
        /// </summary>
        static LoggerProvider()
		{
			var loggerClass = GetLoggerClass();
			var loggerFactory = string.IsNullOrEmpty(loggerClass) ? new NoLoggingLoggerFactory() : GetLoggerFactory(loggerClass);
			SetLoggersFactory(loggerFactory);
		}

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the logger factory.
        /// </summary>
        /// <param name="loggerClass">The logger class.</param>
        /// <returns>ILoggerFactory.</returns>
        /// <exception cref="System.ApplicationException">
        /// Public constructor was not found for  + loggerFactoryType
        /// or
        /// or
        /// Unable to instantiate:  + loggerFactoryType
        /// </exception>
        private static ILoggerFactory GetLoggerFactory(string loggerClass)
		{
			ILoggerFactory loggerFactory;
			var loggerFactoryType = Type.GetType(loggerClass);
			try
			{
			    loggerFactory = (ILoggerFactory)Activator.CreateInstance(loggerFactoryType);
			}
			catch (MissingMethodException ex)
			{
				throw new ApplicationException("Public constructor was not found for " + loggerFactoryType, ex);
			}
			catch (InvalidCastException ex)
			{
				throw new ApplicationException(String.Format("{0}Type does not implement {1}", loggerFactoryType, typeof(ILoggerFactory)), ex);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Unable to instantiate: " + loggerFactoryType, ex);
			}
			return loggerFactory;
		}

        /// <summary>
        /// Gets the logger class.
        /// </summary>
        /// <returns>System.String.</returns>
		private static string GetLoggerClass()
		{
			var logger = ConfigurationManager.AppSettings.Keys.Cast<string>().FirstOrDefault(k => LoggerConfKey.Equals(k.ToLowerInvariant()));
			string loggerClass = null;
			if (string.IsNullOrEmpty(logger))
			{
				var baseDir = AppDomain.CurrentDomain.BaseDirectory;
				var relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
				var binPath = relativeSearchPath == null ? baseDir : Path.Combine(baseDir, relativeSearchPath);
				var nLogDllPath = Path.Combine(binPath, "NLog.dll");
                var log4NetDllPath = Path.Combine(binPath, "log4net.dll");

				if (File.Exists(nLogDllPath))
				{
					loggerClass = typeof(NLogLoggerFactory).AssemblyQualifiedName;
				}
                else if (File.Exists(log4NetDllPath))
                {
                    loggerClass = typeof(Log4NetLoggerFactory).AssemblyQualifiedName;
                }
			}
			else
			{
				loggerClass = ConfigurationManager.AppSettings[logger];
			}
			return loggerClass;
		}

        /// <summary>
        /// Sets the loggers factory.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
		public static void SetLoggersFactory(ILoggerFactory loggerFactory)
		{
			_instance = new LoggerProvider(loggerFactory);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerProvider"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
		private LoggerProvider(ILoggerFactory loggerFactory)
		{
			LoggerFactory = loggerFactory;
		}

        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>IInternalLogger.</returns>
		public static IInternalLogger LoggerFor(string keyName)
		{
			return _instance.LoggerFactory.LoggerFor(keyName);
		}

        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IInternalLogger.</returns>
		public static IInternalLogger LoggerFor(Type type)
		{
			return _instance.LoggerFactory.LoggerFor(type);
        }

        #endregion Methods
    }
}