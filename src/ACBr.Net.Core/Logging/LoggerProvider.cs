// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="LoggerProvider.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Extensions;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ACBr.Net.Core.Logging
{
	/// <summary>
	/// Classe LoggerProvider.
	/// </summary>
	public static class LoggerProvider
	{
		#region Fields

		/// <summary>
		/// The logger conf key
		/// </summary>
		private const string LoggerConfKey = "acbr-logger";

		/// <summary>
		/// The logger factory
		/// </summary>
		private static ILoggerFactory loggerFactory;

		#endregion Fields

		#region Constructor

		/// <summary>
		/// Initializes static members of the <see cref="LoggerProvider"/> class.
		/// </summary>
		static LoggerProvider()
		{
			var loggerClass = GetLoggerClass();
			var factory = loggerClass.IsEmpty() ? new NoLoggingLoggerFactory() : GetLoggerFactory(loggerClass);
			SetLoggerFactory(factory);
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
			ILoggerFactory factory;
			var loggerFactoryType = Type.GetType(loggerClass);
			try
			{
				factory = (ILoggerFactory)Activator.CreateInstance(loggerFactoryType);
			}
			catch (MissingMethodException ex)
			{
				throw new ApplicationException("Public constructor was not found for " + loggerFactoryType, ex);
			}
			catch (InvalidCastException ex)
			{
				throw new ApplicationException($"{loggerFactoryType}Type does not implement {typeof(ILoggerFactory)}", ex);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Unable to instantiate: " + loggerFactoryType, ex);
			}

			return factory;
		}

		/// <summary>
		/// Gets the logger class.
		/// </summary>
		/// <returns>System.String.</returns>
		private static string GetLoggerClass()
		{
			var logger = ConfigurationManager.AppSettings.Keys.Cast<string>().FirstOrDefault(k => LoggerConfKey.Equals(k.ToLowerInvariant()));
			string loggerClass = null;
			if (logger.IsEmpty())
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
		/// Sets the logger factory.
		/// </summary>
		/// <param name="factory">The factory.</param>
		public static void SetLoggerFactory(ILoggerFactory factory)
		{
			loggerFactory = factory;
		}

		/// <summary>
		/// Sets the logger factory.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void SetLoggerFactory<T>() where T : ILoggerFactory
		{
			loggerFactory = Activator.CreateInstance<T>();
		}

		/// <summary>
		/// Loggers for.
		/// </summary>
		/// <param name="keyName">Name of the key.</param>
		/// <returns>IACBrLogger.</returns>
		public static IACBrLogger LoggerFor(string keyName)
		{
			var ret = loggerFactory.LoggerFor(keyName) ?? new NoLoggingLogger();
			return ret;
		}

		/// <summary>
		/// Loggers for.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>IACBrLogger.</returns>
		public static IACBrLogger LoggerFor(Type type)
		{
			return loggerFactory.LoggerFor(type) ?? new NoLoggingLogger();
		}

		/// <summary>
		/// Loggers for.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>IACBrLogger.</returns>
		public static IACBrLogger LoggerFor<T>()
		{
			return loggerFactory.GetLogger<T>() ?? new NoLoggingLogger();
		}

		#endregion Methods
	}
}