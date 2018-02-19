// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-18-2018
//
// Last Modified By : RFTD
// Last Modified On : 02-18-2018
// ***********************************************************************
// <copyright file="ACBrContextHandle.cs" company="ACBr.Net">
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
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Logging;
using ExtraConstraints;

namespace ACBr.Net.Core.InteropServices
{
    public abstract class ACBrContextHandle : SafeHandle, IACBrLog
    {
        #region InnerTypes

        private class LibLoader
        {
            #region InnerTypes

            private class Windows
            {
                [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
                public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

                [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern IntPtr LoadLibraryW(string lpszLib);

                [DllImport("kernel32", SetLastError = true)]
                public static extern int FreeLibrary(IntPtr hModule);
            }

            private class Linux
            {
                [DllImport("libdl.so.2")]
                public static extern IntPtr dlopen(string path, int flags);

                [DllImport("libdl.so.2")]
                public static extern IntPtr dlsym(IntPtr handle, string symbol);

                [DllImport("libdl.so.2")]
                public static extern int dlclose(IntPtr handle);
            }

            private class OSX
            {
                [DllImport("/usr/lib/libSystem.dylib")]
                public static extern IntPtr dlopen(string path, int flags);

                [DllImport("/usr/lib/libSystem.dylib")]
                public static extern IntPtr dlsym(IntPtr handle, string symbol);

                [DllImport("/usr/lib/libSystem.dylib")]
                public static extern int dlclose(IntPtr handle);
            }

            #endregion InnerTypes

            #region Fields

            private static readonly IACBrLogger Logger;

            #endregion Fields

            #region Exports

            [DllImport("libc")]
            private static extern int uname(IntPtr buf);

            #endregion Exports

            #region Constructors

            static LibLoader()
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                        IsWindows = true;
                        break;

                    case PlatformID.Unix:
                        try
                        {
                            var num = Marshal.AllocHGlobal(8192);
                            if (uname(num) == 0 && Marshal.PtrToStringAnsi(num) == "Darwin")
                                IsOSX = true;

                            Marshal.FreeHGlobal(num);
                            break;
                        }
                        catch
                        {
                            break;
                        }

                    case PlatformID.MacOSX:
                        IsOSX = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Logger = LoggerProvider.LoggerFor(typeof(ACBrContextHandle));
            }

            #endregion Constructors

            #region Properties

            private static readonly bool IsWindows;

            private static readonly bool IsOSX;

            #endregion Properties

            #region Methods

            public static IntPtr LoadLibrary(string libname)
            {
                if (IsWindows) return Windows.LoadLibraryW(libname);
                return IsOSX ? OSX.dlopen(libname, 1) : Linux.dlopen(libname, 1);
            }

            public static int FreeLibrary(IntPtr library)
            {
                if (IsWindows) return Windows.FreeLibrary(library);
                return IsOSX ? OSX.dlclose(library) : Linux.dlclose(library);
            }

            public static IntPtr GetProcAddress(IntPtr library, string function)
            {
                var num = !IsWindows ? (!IsOSX ? Linux.dlsym(library, function) : OSX.dlsym(library, function)) : Windows.GetProcAddress(library, function);

                if (num == IntPtr.Zero) Logger.Warn("Função não encontrada: " + function);
                return num;
            }

            public static T LoadFunction<[DelegateConstraint]T>(IntPtr procaddress) where T : class
            {
                Guard.Against<ArgumentException>(!typeof(T).IsSubclassOf(typeof(Delegate)), $"{typeof(T).Name} is not a delegate type");
                if (procaddress == IntPtr.Zero) return default(T);

                var funcaoSat = Marshal.GetDelegateForFunctionPointer(procaddress, typeof(T));

                return funcaoSat as T;
            }

            #endregion Methods
        }

        #endregion InnerTypes

        #region Fields

        protected static object sessionLOCK = new object();
        protected readonly Dictionary<Type, string> methodList;
        protected readonly string className;

        public static readonly IntPtr MinusOne;

        #endregion Fields

        #region Constructors

        static ACBrContextHandle()
        {
            MinusOne = new IntPtr(-1);
        }

        /// <inheritdoc />
        protected ACBrContextHandle(string dllPath)
            : base(IntPtr.Zero, true)
        {
            lock (sessionLOCK)
            {
                methodList = new Dictionary<Type, string>();
                className = GetType().Name;

                var pNewSession = LibLoader.LoadLibrary(dllPath);
                Guard.Against<ACBrException>(pNewSession == IntPtr.Zero, "Não foi possivel carregar a biblioteca.");
                SetHandle(pNewSession);
            }
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc />
        public override bool IsInvalid
        {
            get
            {
                if (handle != IntPtr.Zero)
                {
                    return (handle == MinusOne);
                }

                return true;
            }
        }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            if (IsInvalid) return true;

            lock (sessionLOCK)
            {
                LibLoader.FreeLibrary(handle);
                SetHandleAsInvalid();
            }

            return true;
        }

        /// <summary>
        /// Adicionar um delegate a lista para a função informada.
        /// </summary>
        /// <param name="functionName">Nome da função para exportar</param>
        /// <typeparam name="T">Delegate da função</typeparam>
        protected virtual void AddMethod<[DelegateConstraint]T>(string functionName) where T : class
        {
            methodList.Add(typeof(T), functionName);
        }

        /// <summary>
        /// Retorna o delegate para uso.
        ///  </summary>
        /// <typeparam name="T">Delegate</typeparam>
        /// <returns></returns>
        /// <exception cref="ACBrException"></exception>
        protected virtual T GetMethod<[DelegateConstraint]T>() where T : class
        {
            if (!methodList.ContainsKey(typeof(T))) throw CreateException($"Função não adicionada para o [{nameof(T)}].");

            var method = methodList[typeof(T)];
            this.Log().Debug($"{className} : Acessando o método [{method}] da biblioteca.");

            var mHandler = LibLoader.GetProcAddress(handle, method);

            Guard.Against<ArgumentNullException>(mHandler == IntPtr.Zero || mHandler == MinusOne, "Função não encontrada: " + method);

            var methodHandler = LibLoader.LoadFunction<T>(mHandler);
            this.Log().Debug($"{className} : Método [{method}] carregado.");

            return methodHandler;
        }

        /// <summary>
        /// Executa a função e trata erros nativos.
        /// </summary>
        /// <param name="method"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ACBrException"></exception>
        [HandleProcessCorruptedStateExceptions]
        protected virtual T ExecuteMethod<T>(Func<T> method)
        {
            try
            {
                return method();
            }
            catch (Exception exception)
            {
                throw ProcessException(exception);
            }
        }

        protected virtual ACBrException CreateException(string errorMessage)
        {
            this.Log().Error($"{className} - Erro: {errorMessage}");
            throw new ACBrException(errorMessage);
        }

        protected virtual ACBrException ProcessException(Exception exception)
        {
            this.Log().Error($"{className} - Erro: {exception.Message}", exception);
            return new ACBrException(exception, exception.Message);
        }

        #endregion Methods
    }
}