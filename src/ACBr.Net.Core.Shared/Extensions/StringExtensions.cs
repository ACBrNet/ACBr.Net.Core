// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-31-2016
//
// Last Modified By : RFTD
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="ACBr.Net">
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods

        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="password">The password.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Erro ao criptografar a string</exception>
        /// <exception cref="Exception">Erro ao criptografar a string</exception>
        public static string Encrypt(this string data, string password)
        {
            return Encrypt<TripleDESCryptoServiceProvider>(data, password);
        }

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="password">The password.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Erro ao descriptografar string</exception>
        /// <exception cref="Exception">Erro ao descriptografar string</exception>
        public static string Decrypt(this string data, string password)
        {
            return Decrypt<TripleDESCryptoServiceProvider>(data, password);
        }

        /// <summary>
        /// Encrypts the specified password.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ACBrException">Erro ao criptografar a string</exception>
        public static string Encrypt<T>(this string value, string password,
            string salt = "aselrias38490a32", string vector = "8947az34awl34kjq")
            where T : SymmetricAlgorithm, new()
        {
            try
            {
                var vectorBytes = Encoding.UTF8.GetBytes(vector);
                var saltBytes = Encoding.UTF8.GetBytes(salt);
                var valueBytes = Encoding.UTF8.GetBytes(value);

                var keysize = 256;
                byte[] encrypted;
                using (var cipher = new T())
                {
                    if (cipher is DESCryptoServiceProvider)
                    {
                        keysize = 64;
                    }

                    if (cipher is TripleDESCryptoServiceProvider || cipher is RC2CryptoServiceProvider)
                    {
                        keysize = 128;
                    }

                    var passwordBytes = new Rfc2898DeriveBytes(password, saltBytes, 2);
                    var keyBytes = passwordBytes.GetBytes(keysize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (var encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    using (var to = new MemoryStream())
                    using (var writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                    {
                        writer.Write(valueBytes, 0, valueBytes.Length);
                        writer.FlushFinalBlock();
                        encrypted = to.ToArray();
                    }

                    cipher.Clear();
                }

                return encrypted.ToBase64();
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao criptografar a string", ex);
            }
        }

        /// <summary>
        /// Decrypts the specified password.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ACBrException">Erro ao descriptografar string</exception>
        public static string Decrypt<T>(this string value, string password,
            string salt = "aselrias38490a32", string vector = "8947az34awl34kjq") where T : SymmetricAlgorithm, new()
        {
            try
            {
                var vectorBytes = Encoding.UTF8.GetBytes(vector);
                var saltBytes = Encoding.UTF8.GetBytes(salt);
                var valueBytes = Convert.FromBase64String(value);

                var keysize = 256;
                byte[] decrypted;
                int decryptedByteCount;

                using (var cipher = new T())
                {
                    if (cipher is DESCryptoServiceProvider)
                    {
                        keysize = 64;
                    }

                    if (cipher is TripleDESCryptoServiceProvider || cipher is RC2CryptoServiceProvider)
                    {
                        keysize = 128;
                    }

                    var passwordBytes = new Rfc2898DeriveBytes(password, saltBytes, 2);
                    var keyBytes = passwordBytes.GetBytes(keysize / 8);

                    cipher.Mode = CipherMode.CBC;
                    using (var decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    using (var from = new MemoryStream(valueBytes))
                    using (var reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                    {
                        decrypted = new byte[valueBytes.Length];
                        decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                    }

                    cipher.Clear();
                }

                return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao descriptografar string", ex);
            }
        }

        /// <summary>
        /// To the m d5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Erro ao gerar hash MD5</exception>
        /// <exception cref="Exception">Erro ao gerar hash MD5</exception>
        public static string ToMd5Hash(this string input)
        {
            try
            {
                // Primeiro passo, calcular o MD5 hash a partir da string
                using (var md5 = MD5.Create())
                {
                    var inputBytes = Encoding.UTF8.GetBytes(input);
                    var hash = md5.ComputeHash(inputBytes);

                    // Segundo passo, converter o array de bytes em uma string hexadecimal
                    var sb = new StringBuilder();
                    foreach (var t in hash)
                    {
                        sb.Append(t.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao gerar hash MD5", ex);
            }
        }

        /// <summary>
        /// To the sh a1 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Erro ao gerar SHA1 hash</exception>
        /// <exception cref="Exception">Erro ao gerar SHA1 hash</exception>
        public static string ToSha1Hash(this string input)
        {
            try
            {
                using (var sha = SHA1.Create())
                {
                    var data = Encoding.UTF8.GetBytes(input);
                    var hash = sha.ComputeHash(data);

                    var sb = new StringBuilder();
                    foreach (var t in hash)
                    {
                        sb.Append(t.ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao gerar SHA1 hash", ex);
            }
        }

        /// <summary>
        /// Strings the reverse.
        /// </summary>
        /// <param name="toReverse">To reverse.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Erro ao reverter string</exception>
        /// <exception cref="Exception">Erro ao reverter string</exception>
        public static string StringReverse(this string toReverse)
        {
            try
            {
                if (toReverse.IsEmpty() || toReverse.Length == 1) return toReverse;

                return new string(toReverse.ToCharArray().Reverse().ToArray());
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao reverter string", ex);
            }
        }

        /// <summary>
        /// Formatars the specified valor.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="mascara">The mascara.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Erro ao formatar string</exception>
        /// <exception cref="Exception">Erro ao formatar string</exception>
        public static string Formatar(this string input, string mascara)
        {
            try
            {
                if (input.IsEmpty())
                    return input;

                var output = string.Empty;
                var index = 0;
                foreach (var m in mascara)
                {
                    if (m == '#')
                    {
                        if (index >= input.Length)
                            continue;

                        output += input[index];
                        index++;
                    }
                    else
                        output += m;
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao formatar string", ex);
            }
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(this string dados, double def = -1)
        {
            return ToDouble(dados, def, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(this string dados, IFormatProvider format)
        {
            return ToDouble(dados, -1, format);
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        /// <exception cref="Exception">Erro ao converter string</exception>
        public static double ToDouble(this string dados, double def, IFormatProvider format)
        {
            try
            {
                if (!double.TryParse(dados, NumberStyles.Any, format, out var converted))
                    converted = def;

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <returns>System.Decimal.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        /// <exception cref="Exception">Erro ao converter string</exception>
        public static decimal ToDecimal(this string dados, decimal def = -1)
        {
            return ToDecimal(dados, def, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal ToDecimal(this string dados, IFormatProvider format)
        {
            return ToDecimal(dados, -1, format);
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Decimal.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        public static decimal ToDecimal(this string dados, decimal def, IFormatProvider format)
        {
            try
            {
                if (!decimal.TryParse(dados, NumberStyles.Any, format, out var converted))
                    converted = def;

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        /// <exception cref="Exception">Erro ao converter string</exception>
        public static int ToInt32(this string dados, int def = -1)
        {
            return ToInt32(dados, def, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt32(this string dados, IFormatProvider format)
        {
            return ToInt32(dados, -1, format);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        public static int ToInt32(this string dados, int def, IFormatProvider format)
        {
            try
            {
                if (!int.TryParse(dados, NumberStyles.Any, format, out var converted))
                    converted = def;

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <returns>System.Int64.</returns>
        public static long ToInt64(this string dados, long def = -1)
        {
            return ToInt64(dados, def, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Int64.</returns>
        public static long ToInt64(this string dados, IFormatProvider format)
        {
            return ToInt64(dados, -1, format);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <param name="format">The format.</param>
        /// <returns>Int64.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        /// <exception cref="Exception">Erro ao converter string</exception>
        public static long ToInt64(this string dados, long def, IFormatProvider format)
        {
            try
            {
                if (!long.TryParse(dados, NumberStyles.Any, format, out var converted))
                    converted = def;

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        /// To the data.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <returns>DateTime.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        /// <exception cref="Exception">Erro ao converter string</exception>
        public static DateTime ToData(this string dados)
        {
            try
            {
                if (!DateTime.TryParse(dados, out var converted))
                    converted = default(DateTime);

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        /// To the data.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <returns>DateTime.</returns>
        /// <exception cref="System.Exception">Erro ao converter string</exception>
        /// <exception cref="Exception">Erro ao converter string</exception>
        public static DateTimeOffset ToDataOffset(this string dados)
        {
            try
            {
                if (!DateTimeOffset.TryParse(dados, out var converted))
                    converted = default(DateTimeOffset);

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        /// Retorna apenas os numeros da string.
        /// </summary>
        /// <param name="toNormalize">String para processar.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ACBrException">Erro ao processar a string</exception>
        public static string OnlyNumbers(this string toNormalize)
        {
            try
            {
                if (toNormalize.IsEmpty()) return string.Empty;

                var toReturn = Regex.Replace(toNormalize, "[^0-9]", string.Empty);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao processar a string", ex);
            }
        }

        /// <summary>
        /// Remove pontuações, espaços e traços de uma string, deixando apenas Dígitos e Letras
        /// </summary>
        /// <param name="str">String para processar.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ACBrException">Erro ao processar a string</exception>
        public static string RemoveMask(this string str)
        {
            try
            {
                if (str.IsEmpty()) return str;

                var digitsOnlyRegex = new Regex(@"[^\w]");
                return digitsOnlyRegex.Replace(str, string.Empty);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao processar a string", ex);
            }
        }

        /// <summary>
        /// Determines whether the specified cep is cep.
        /// </summary>
        /// <param name="cep">The cep.</param>
        /// <returns><c>true</c> if the specified cep is cep; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao validar CEP</exception>
        /// <exception cref="Exception">Erro ao validar CEP</exception>
        public static bool IsCep(this string cep)
        {
            try
            {
                cep = cep.OnlyNumbers();

                if (cep.Length == 8)
                {
                    cep = $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}";
                }

                return Regex.IsMatch(cep, "[0-9]{5}-[0-9]{3}");
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar CEP", ex);
            }
        }

        /// <summary>
        /// Checar se a string é um [CPF ou CNPJ] válido.
        /// </summary>
        /// <param name="cpfcnpj">CPFCNPJ</param>
        /// <returns><c>true</c> se o [CPF ou CNPJ] é válido; senão, <c>false</c>.</returns>
        public static bool IsCPFOrCNPJ(this string cpfcnpj)
        {
            var value = cpfcnpj.OnlyNumbers();
            switch (value.Length)
            {
                case 11:
                    return value.IsCPF();

                case 14:
                    return value.IsCNPJ();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Checa se a string é um CPF válido.
        /// </summary>
        /// <param name="vrCPF">CPF</param>
        /// <param name="ajustarTamanho">if set to <c>true</c> [ajustar tamanho].</param>
        /// <returns><c>true</c> se o CPF for válido; senão, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao validar CPF</exception>
        /// <exception cref="Exception">Erro ao validar CPF</exception>
        public static bool IsCPF(this string vrCPF, bool ajustarTamanho = false)
        {
            try
            {
                var cpf = vrCPF.OnlyNumbers();
                if (ajustarTamanho)
                    cpf = cpf.ZeroFill(11);

                if (cpf.Length != 11)
                    return false;

                //if (new string(cpf[0], cpf.Length) == cpf ||
                //    cpf == "12345678909")
                //    return false;

                if (new string(cpf[0], cpf.Length) == cpf)
                    return false;

                var numeros = new int[11];
                for (var i = 0; i < 11; i++)
                    numeros[i] = int.Parse(cpf[i].ToString());

                var soma = 0;
                for (var i = 0; i < 9; i++)
                    soma += (10 - i) * numeros[i];

                var resultado = soma % 11;
                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[9] != 0)
                        return false;
                }
                else if (numeros[9] != 11 - resultado)
                    return false;

                soma = 0;
                for (var i = 0; i < 10; i++)
                    soma += (11 - i) * numeros[i];

                resultado = soma % 11;
                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[10] != 0)
                        return false;
                }
                else if (numeros[10] != 11 - resultado)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar CPF", ex);
            }
        }

        /// <summary>
        /// Checa se a string é um CNPJ válido.
        /// </summary>
        /// <param name="vrCNPJ">CNPJ.</param>
        /// <param name="ajustarTamanho">if set to <c>true</c> [ajustar tamanho].</param>
        /// <returns><c>true</c> se o CNPJ for válido; senão, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao validar CNPJ</exception>
        /// <exception cref="Exception">Erro ao validar CNPJ</exception>
        public static bool IsCNPJ(this string vrCNPJ, bool ajustarTamanho = false)
        {
            try
            {
                var cnpj = vrCNPJ.OnlyNumbers();
                if (ajustarTamanho)
                    cnpj = cnpj.ZeroFill(14);

                if (cnpj.Length != 14)
                    return false;

                if (new string(cnpj[0], cnpj.Length) == cnpj)
                    return false;

                var resultado = new int[2];
                int nrDig;
                const string ftmt = "6543298765432";
                var cnpjOk = new bool[2];
                var digitos = new int[14];
                var soma = new int[2];
                soma[0] = 0;
                soma[1] = 0;
                resultado[0] = 0;
                resultado[1] = 0;
                cnpjOk[0] = false;
                cnpjOk[1] = false;

                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1));
                    if (nrDig <= 12)
                        soma[1] += digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = soma[nrDig] % 11;
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        cnpjOk[nrDig] = digitos[12 + nrDig] == 0;
                    else
                        cnpjOk[nrDig] = digitos[12 + nrDig] == 11 - resultado[nrDig];
                }

                return cnpjOk[0] && cnpjOk[1];
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar CNPJ", ex);
            }
        }

        /// <summary>
        /// Determines whether the specified p inscr is ie.
        /// </summary>
        /// <param name="pInscr">The p inscr.</param>
        /// <param name="pUf">The p uf.</param>
        /// <returns><c>true</c> if the specified p inscr is ie; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao IE</exception>
        /// <exception cref="Exception">Erro ao IE</exception>
        public static bool IsIE(this string pInscr, string pUf)
        {
            try
            {
                if (pInscr.IsEmpty()) return false;
                if (pInscr.Trim().ToUpper() == "ISENTO") return true;
                if (!pUf.ValidarUF() || pUf.ToUpper() == "EX") return false;

                const string c09 = "0-9";
                int[,] cPesos =
                {
                    {0, 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6},
                    {0, 0, 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5},
                    {2, 0, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6},
                    {0, 2, 3, 4, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 8, 7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 0, 0},
                    {0, 2, 3, 4, 5, 6, 7, 0, 0, 8, 9, 0, 0, 0},
                    {0, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5},
                    {0, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 7, 8},
                    {0, 0, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 7},
                    {0, 0, 2, 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 0},
                    {0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 2, 3, 0},
                    {0, 0, 0, 0, 10, 8, 7, 6, 5, 4, 3, 1, 0, 0},
                    {0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 2, 3, 0, 0},
                    {0, 0, 2, 3, 4, 5, 6, 7, 8, 3, 4, 5, 6, 7}
                };

                string[] vDigitos = { "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                var fsDocto = "";
                char d;
                for (var i = 1; i <= pInscr.Trim().Length; i++)
                {
                    if ("0123456789P".IndexOf(pInscr.Substring(i - 1, 1), 0, StringComparison.OrdinalIgnoreCase) + 1 > 0)
                        fsDocto += pInscr.Substring(i - 1, 1);
                }

                var tamanho = 0;
                var xRot = "E";
                var xMd = 11;
                var xTp = 1;
                var yRot = "";
                var yMd = 0;
                var yTp = 0;
                var fatorF = 0;
                var fatorG = 0;

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (pUf.ToUpper())
                {
                    case "AC":
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (fsDocto.Length)
                        {
                            case 9:
                                tamanho = 9;
                                vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "1", "0", "", "", "", "", "" };
                                break;

                            case 13:
                                tamanho = 13;
                                xTp = 2;
                                yRot = "E";
                                yMd = 11;
                                yTp = 1;
                                vDigitos = new[] { "DVY", "DVX", c09, c09, c09, c09, c09, c09, c09, c09, c09, "1", "0", "" };
                                break;
                        }
                        break;

                    case "AL":
                        tamanho = 9;
                        xRot = "BD";
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "4", "2", "", "", "", "", "" };
                        break;

                    case "AP":
                        if (fsDocto.Length == 9)
                        {
                            tamanho = 9;
                            xRot = "CE";
                            vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "3", "0", "", "", "", "", "" };

                            if (fsDocto.ToInt64().IsBetween(30170010, 30190229))
                                fatorF = 1;
                            else if (fsDocto.ToInt64() >= 30190230)
                                xRot = "E";
                        }
                        break;

                    case "AM":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "", "", "", "", "" };
                        break;

                    case "BA":
                        if (fsDocto.Length < 9)
                            fsDocto = fsDocto.ZeroFill(9);

                        tamanho = 9;
                        xTp = 2;
                        yTp = 3;
                        yRot = "E";
                        vDigitos = new[] { "DVX", "DVY", c09, c09, c09, c09, c09, c09, c09, "", "", "", "", "" };
                        if (fsDocto[1].IsIn('0', '1', '2', '3', '4', '5', '8'))
                        {
                            xMd = 10;
                            yMd = 10;
                        }
                        else
                        {
                            xMd = 11;
                            yMd = 11;
                        }
                        break;

                    case "CE":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, "0", "", "", "", "", "" };
                        break;

                    case "DF":
                        tamanho = 13;
                        xTp = 2;
                        yRot = "E";
                        yMd = 11;
                        yTp = 1;
                        vDigitos = new[] { "DVY", "DVX", c09, c09, c09, c09, c09, c09, c09, c09, c09, "7", "0", "" };
                        break;

                    case "ES":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "", "", "", "", "" };
                        break;

                    case "GO":
                        if (fsDocto.Length == 9)
                        {
                            tamanho = 9;
                            vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "0,1,5", "1", "", "", "", "", "" };

                            if (fsDocto.ToInt64().IsBetween(101031050, 101199979))
                                fatorG = 1;
                        }
                        break;

                    case "MA":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "2", "1", "", "", "", "", "" };
                        break;

                    case "MT":
                        if (fsDocto.Length == 9)
                            fsDocto = fsDocto.ZeroFill(11);

                        tamanho = 11;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, "", "", "" };
                        break;

                    case "MS":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "8", "2", "", "", "", "", "" };
                        break;

                    case "MG":
                        tamanho = 13;
                        xRot = "AE";
                        xMd = 10;
                        xTp = 10;
                        yRot = "E";
                        yMd = 11;
                        yTp = 11;
                        vDigitos = new[] { "DVY", "DVX", c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, "" };
                        break;

                    case "PA":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "5", "1", "", "", "", "", "" };
                        break;

                    case "PB":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "6", "1", "", "", "", "", "" };
                        break;

                    case "PR":
                        tamanho = 10;
                        xTp = 9;
                        yRot = "E";
                        yMd = 11;
                        yTp = 8;
                        vDigitos = new[] { "DVY", "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "", "", "", "" };
                        break;

                    case "PE":
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (fsDocto.Length)
                        {
                            case 14:
                                tamanho = 14;
                                xTp = 7;
                                fatorF = 1;
                                vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, "1-9", "8", "1" };
                                break;

                            case 9:
                                tamanho = 9;
                                xTp = 14;
                                xMd = 11;
                                yRot = "E";
                                yMd = 11;
                                yTp = 7;
                                vDigitos = new[] { "DVY", "DVX", c09, c09, c09, c09, c09, c09, c09, "", "", "", "", "" };
                                break;
                        }
                        break;

                    case "PI":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "9", "1", "", "", "", "", "" };
                        break;

                    case "RJ":
                        tamanho = 8;
                        xTp = 8;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "1,7,8,9", "", "", "", "", "", "" };
                        break;

                    case "RN":
                        xRot = "BD";
                        switch (fsDocto.Length)
                        {
                            case 9:
                                tamanho = 9;
                                vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "0", "2", "", "", "", "", "" };
                                break;

                            case 10:
                                tamanho = 10;
                                xTp = 11;
                                vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, "0", "2", "", "", "", "" };
                                break;
                        }
                        break;

                    case "RS":
                        tamanho = 10;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "0-4", "", "", "", "" };
                        break;

                    case "RO":
                        fatorF = 1;
                        switch (fsDocto.Length)
                        {
                            case 9:
                                tamanho = 9;
                                xTp = 4;
                                vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, "1-9", "", "", "", "", "" };
                                break;

                            case 14:
                                tamanho = 14;
                                vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, c09, c09 };
                                break;
                        }
                        break;

                    case "RR":
                        tamanho = 9;
                        xRot = "D";
                        xMd = 9;
                        xTp = 5;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "4", "2", "", "", "", "", "" };
                        break;

                    case "SC":
                    case "SE":
                        tamanho = 9;
                        vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "", "", "", "", "" };
                        break;

                    case "SP":
                        xRot = "D";
                        xTp = 12;
                        if (fsDocto.ToUpper()[0] == 'P')
                        {
                            tamanho = 13;
                            vDigitos = new[] { c09, c09, c09, "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "P", "" };
                        }
                        else
                        {
                            tamanho = 12;
                            yRot = "D";
                            yMd = 11;
                            yTp = 13;
                            vDigitos = new[] { "DVY", c09, c09, "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "", "" };
                        }
                        break;

                    case "TO":
                        if (fsDocto.Length == 11)
                        {
                            tamanho = 11;
                            xTp = 6;
                            vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, "1,2,3,9", "0,9", "9", "2", "", "", "" };
                        }
                        else
                        {
                            tamanho = 9;
                            vDigitos = new[] { "DVX", c09, c09, c09, c09, c09, c09, c09, c09, "", "", "", "", "" };
                        }
                        break;
                }

                var ok = (tamanho > 0) && (fsDocto.Length == tamanho);
                if (!ok)
                    return false;

                fsDocto = fsDocto.FillRight(14);
                var dvx = 0;
                var dvy = 0;
                var I = 13;

                //Verificando os digitos nas posicoes são permitidos
                while (I >= 0)
                {
                    d = fsDocto[13 - I];

                    switch (vDigitos[I])
                    {
                        case "":
                            ok = d == ' ';
                            break;

                        case "DVX":
                        case "DVY":
                        case c09:
                            ok = char.IsNumber(d);
                            // ReSharper disable once SwitchStatementMissingSomeCases
                            switch (vDigitos[I])
                            {
                                case "DVX":
                                    dvx = d.ToInt32(0);
                                    break;

                                case "DVY":
                                    dvy = d.ToInt32(0);
                                    break;
                            }
                            break;

                        default:
                            if (vDigitos[I].Contains(','))
                            {
                                ok = vDigitos[I].Contains(d);
                            }
                            else if (vDigitos[I].Contains('-'))
                            {
                                ok = d.ToInt32().IsBetween(vDigitos[I].Substring(0, 1).ToInt32(), vDigitos[I].Substring(2, 1).ToInt32());
                            }
                            else
                            {
                                ok = d == vDigitos[I][0];
                            }
                            break;
                    }

                    if (!ok)
                        return false;

                    I--;
                }

                var passo = 'X';
                while (xTp > 0)
                {
                    var soma = 0;
                    var somAq = 0;
                    I = 14;

                    while (I > 0)
                    {
                        d = fsDocto[14 - I];
                        if (char.IsNumber(d))
                        {
                            var nD = d.ToInt32(0);
                            var m = nD * cPesos[xTp - 1, I - 1];
                            soma += m;

                            if (xRot.Contains('A'))
                                somAq = somAq + (int)Math.Truncate((decimal)m / 10);
                        }

                        I--;
                    }

                    if (xRot.Contains('A'))
                        soma += somAq;
                    else if (xRot.Contains('B'))
                        soma *= 10;
                    else if (xRot.Contains('C'))
                        soma += 5 + (4 * fatorF);

                    //Calculando digito verificador
                    var dv = (int)Math.Truncate((decimal)soma % xMd);
                    if (xRot.Contains('E'))
                        dv = (int)Math.Truncate((decimal)xMd - dv);

                    //Apenas GO modifica o FatorG para diferente de 0
                    switch (dv)
                    {
                        case 10:
                            dv = fatorG;
                            break;

                        case 11:
                            dv = fatorF;
                            break;
                    }

                    if (passo == 'X')
                        ok = dvx == dv;
                    else
                        ok = dvy == dv;

                    if (!ok)
                        return false;

                    if (passo == 'X')
                    {
                        passo = 'Y';
                        xRot = yRot;
                        xMd = yMd;
                        xTp = yTp;
                    }
                    else
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar IE", ex);
            }
        }

        /// <summary>
        /// Formatars the ie.
        /// </summary>
        /// <param name="pInscr">The p inscr.</param>
        /// <param name="pUf">The p uf.</param>
        /// <returns>System.String.</returns>
        public static string FormatarIE(this string pInscr, string pUf)
        {
            var mascara = new string('#', pInscr.Length);
            pUf = pUf.Trim().ToUpper();

            Guard.Against<ArgumentException>(!ValidarUF(pUf), "UF infomada invalida.");

            switch (pUf)
            {
                case "AC":
                    mascara = "##.###.###/###-##";
                    break;

                case "AL":
                    mascara = "#########";
                    break;

                case "AP":
                    mascara = "#########";
                    break;

                case "AM":
                    mascara = "##.###.###-#";
                    break;

                case "BA":
                    mascara = "#######-##";
                    break;

                case "CE":
                    mascara = "########-#";
                    break;

                case "DF":
                    mascara = "###########-##";
                    break;

                case "ES":
                    mascara = "#########";
                    break;

                case "GO":
                    mascara = "##.###.###-#";
                    break;

                case "MA":
                    mascara = "#########";
                    break;

                case "MT":
                    mascara = "##########-#";
                    break;

                case "MS":
                    mascara = "##.###.###-#";
                    break;

                case "MG":
                    mascara = "###.###.###/####";
                    break;

                case "PA":
                    mascara = "##-######-#";
                    break;

                case "PB":
                    mascara = "########-#";
                    break;

                case "PR":
                    mascara = "###.#####-##";
                    break;

                case "PE":
                    mascara = pInscr.Length > 9 ? "##.#.###.#######-#" : "#######-##";
                    break;

                case "PI":
                    mascara = "#########";
                    break;

                case "RJ":
                    mascara = "##.###.##-#";
                    break;

                case "RN":
                    mascara = pInscr.Length > 9 ? "##.#.###.###-#" : "##.###.###-#";
                    break;

                case "RS":
                    mascara = "###/#######";
                    break;

                case "RO":
                    mascara = pInscr.Length > 13 ? "#############-#" : "###.#####-#";
                    break;

                case "RR":
                    mascara = "########-#";
                    break;

                case "SC":
                    mascara = "###.###.###";
                    break;

                case "SP":
                    mascara = pInscr.Length > 1 && pInscr[0] == 'P' ? "#-########.#/###" : "###.###.###.###";
                    break;

                case "SE":
                    mascara = "##.###.###-#";
                    break;

                case "TO":
                    mascara = pInscr.Length == 11 ? "##.##.######-#" : "##.###.###-#";
                    break;
            }

            var fsDocto = "";
            for (var i = 1; i <= pInscr.Trim().Length; i++)
            {
                if ("0123456789P".IndexOf(pInscr.Substring(i - 1, 1), 0, StringComparison.OrdinalIgnoreCase) + 1 > 0)
                    fsDocto += pInscr.Substring(i - 1, 1);
            }

            return fsDocto.Length < mascara.Count(x => x == '#')
                ? fsDocto.ZeroFill(mascara.Count(x => x == '#')).Formatar(mascara)
                : fsDocto.Formatar(mascara);
        }

        /// <summary>
        /// Validars the uf.
        /// </summary>
        /// <param name="uf">The uf.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ValidarUF(this string uf)
        {
            if (uf.IsEmpty()) return false;

            string[] cUFsValidas =
            {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS",
                "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC",
                "SP", "SE", "TO", "EX"
            };

            return cUFsValidas.Contains(uf.Trim().ToUpper());
        }

        /// <summary>
        /// Determines whether the specified pis is pis.
        /// </summary>
        /// <param name="pis">The pis.</param>
        /// <returns><c>true</c> if the specified pis is pis; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao validar PIS</exception>
        /// <exception cref="Exception">Erro ao validar PIS</exception>
        public static bool IsPIS(this string pis)
        {
            try
            {
                var multiplicador = new[] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var soma = 0;

                if (pis.Trim().Length == 0)
                    return false;

                pis = pis.Trim();
                pis = pis.Replace("-", string.Empty).Replace(".", string.Empty).PadLeft(11, '0');
                for (var i = 0; i < 10; i++)
                    soma += int.Parse(pis[i].ToString()) * multiplicador[i];

                var resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                return pis.EndsWith(resto.ToString());
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar PIS", ex);
            }
        }

        /// <summary>
        /// Determines whether the specified email is email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns><c>true</c> if the specified email is email; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao validar Email</exception>
        /// <exception cref="Exception">Erro ao validar Email</exception>
        public static bool IsEmail(this string email)
        {
            try
            {
                var emailRegex = @"^(([^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+"
                                 + @"(\.[^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+)*)|(\"".+\""))@"
                                 + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|"
                                 + @"(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

                // Instância da classe Regex, passando como
                // argumento sua Expressão Regular
                var rx = new Regex(emailRegex);

                // Método IsMatch da classe Regex que retorna
                // verdadeiro caso o e-mail passado estiver
                // dentro das regras da sua regex.
                return rx.IsMatch(email);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar Email", ex);
            }
        }

        /// <summary>
        /// Determines whether the specified site is site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns><c>true</c> if the specified site is site; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.Exception">Erro ao validar endereço web</exception>
        /// <exception cref="Exception">Erro ao validar endereço web</exception>
        public static bool IsSite(this string site)
        {
            try
            {
                //string siteRegex = @"/^http:\/\/www\.[a-z]+\.(com)|(edu)|(net)$/";
                const string siteRegex = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

                var rx = new Regex(siteRegex);
                return rx.IsMatch(site);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar endereço web", ex);
            }
        }

        /// <summary>
        /// Verifica se a string é numerica.
        /// </summary>
        /// <param name="strNum">The string number.</param>
        /// <returns>Retorna true/false se a string é numerica</returns>
        /// <exception cref="System.Exception">Erro ao validar string</exception>
        /// <exception cref="Exception">Erro ao validar string</exception>
        public static bool IsNumeric(this string strNum)
        {
            try
            {
                var reNum = new Regex(@"^\d+$");
                return reNum.IsMatch(strNum);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao validar string", ex);
            }
        }

        /// <summary>
        /// Converte a string para UTF8.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <returns>Uma string com a codificação UTF8</returns>
        /// <exception cref="System.Exception">Erro ao codificar string</exception>
        /// <exception cref="Exception">Erro ao codificar string</exception>
        public static string ToUtf8(this string value)
        {
            if (value.IsEmpty()) return string.Empty;

            try
            {
                var bytes = Encoding.Default.GetBytes(value);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao codificar string", ex);
            }
        }

        /// <summary>
        /// Transforma um array de string em uma unica string.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>String com todos os dados do array de strings</returns>
        /// <exception cref="System.Exception">Erro ao converter array</exception>
        /// <exception cref="Exception">Erro ao converter array</exception>
        public static string AsString(this string[] array)
        {
            try
            {
                return string.Join(Environment.NewLine, array);
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter array", ex);
            }
        }

        /// <summary>
        /// Alinha a string a direita/esquerda e preenche com caractere informado ate ficar do tamanho especificado.
        /// </summary>
        /// <param name="text">O texto</param>
        /// <param name="length">Tamanho final desejado</param>
        /// <param name="with">Caractere para preencher</param>
        /// <param name="esquerda">Direção do preenchimento</param>
        /// <returns>String do tamanho especificado e se menor complementada com o caractere informado a direita/esquerda</returns>
        public static string StringFill(this string text, int length, char with = ' ', bool esquerda = true)
        {
            if (text.IsEmpty())
            {
                text = string.Empty;
            }

            if (text.Length > length)
            {
                text = text.Remove(length);
            }
            else
            {
                length -= text.Length;

                if (esquerda)
                    text = new string(with, length) + text;
                else
                    text += new string(with, length);
            }

            return text;
        }

        /// <summary>
        /// Alinha a string a direita e preenche a esquerda com o caracter informado até ficar do tamanho especificado.
        /// Se tamanho menor que a string atual retorna uma string do tamanho especificado.
        /// </summary>
        /// <param name="text">O texto.</param>
        /// <param name="length">Tamanho final desejado</param>
        /// <param name="with">Caractere para preencher</param>
        /// <returns>String do tamanho especificado e se menor complementada com o caractere informado a esquerda</returns>
        public static string FillRight(this string text, int length, char with = ' ')
        {
            return text.StringFill(length, with);
        }

        /// <summary>
        /// Alinha a string a esquerda e preenche a direita com o caracter informado até ficar do tamanho especificado.
        /// Se tamanho menor que a string atual retorna uma string do tamanho especificado.
        /// </summary>
        /// <param name="text">O texto.</param>
        /// <param name="length">Tamanho final desejado</param>
        /// <param name="with">Caractere para preencher</param>
        /// <returns>String do tamanho especificado e se menor complementada com o caractere informado a direita</returns>
        public static string FillLeft(this string text, int length, char with = ' ')
        {
            return text.StringFill(length, with, false);
        }

        /// <summary>
        /// Preenche uma string com zero a direita ate ficar do tamanho especificado.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">Tamanho final desejado</param>
        /// <returns>String do tamanho especificado e se menor complementada com zero a direita/esquerda</returns>
        public static string ZeroFill(this string text, int length)
        {
            return text.OnlyNumbers().StringFill(length, '0');
        }

        /// <summary>
        /// Substitui os caracteres acentuados de uma string.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <returns>String sem carateres especiais e normalizada</returns>
        public static string RemoveAccent(this string value)
        {
            if (value.IsEmpty()) return value;

            value = Regex.Replace(value, "[áàâãª]", "a");
            value = Regex.Replace(value, "[ÁÀÂÃÄ]", "A");
            value = Regex.Replace(value, "[éèêë]", "e");
            value = Regex.Replace(value, "[ÉÈÊË]", "E");
            value = Regex.Replace(value, "[íìîï]", "i");
            value = Regex.Replace(value, "[ÍÌÎÏ]", "I");
            value = Regex.Replace(value, "[óòôõöº]", "o");
            value = Regex.Replace(value, "[ÓÒÔÕÖ]", "O");
            value = Regex.Replace(value, "[úùûü]", "u");
            value = Regex.Replace(value, "[ÚÙÛÜ]", "U");
            value = Regex.Replace(value, "[Ç]", "C");
            value = Regex.Replace(value, "[ç]", "c");
            return value;
        }

        /// <summary>
        /// Limpa os caracteres especiais de uma string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>String sem carateres especiais</returns>
        public static string CleanCe(this string text)
        {
            try
            {
                if (text.IsEmpty())
                    return string.Empty;

                var retorno = text.RemoveAccent();
                var cEspeciais = new[] { "#39", "---", "--", "-", "'", "#", Environment.NewLine,
                                          "\n", "\r", ",", ".", "?", "&", ":", "/", "!", ";",
                                          "%", "‘", "’", "(", ")", "\\", "”", "“", "+", "ƒ", "‡" };

                retorno = retorno.ReplaceAny(cEspeciais, string.Empty);
                return retorno.Trim();
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao limpar string.", ex);
            }
        }

        /// <summary>
        /// Subistitui todos os caracteres passado no array pelo novo caracter e retorna a nova string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="oldChars">The old chars.</param>
        /// <param name="newChar">The new character.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceAny(this string text, IEnumerable<char> oldChars, char newChar)
        {
            var builder = new StringBuilder(text);

            foreach (var oldChar in oldChars)
                builder.Replace(oldChar, newChar);

            return builder.ToString();
        }

        /// <summary>
        /// Subistitui todos os caracteres passado no array pelo novo caracter e retorna a nova string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="oldChars">The old chars.</param>
        /// <param name="newChar">The new character.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceAny(this string text, IEnumerable<string> oldChars, string newChar)
        {
            var builder = new StringBuilder(text);

            foreach (var oldChar in oldChars)
                builder.Replace(oldChar, newChar);

            return builder.ToString();
        }

        /// <summary>
        /// Formata o CPF ou CNPJ no formato: 000.000.000-00, 00.000.000/0001-00 respectivamente.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <returns>CPF/CNPJ Formatado</returns>
        public static string FormataCPFCNPJ(this string value)
        {
            value = value.OnlyNumbers();
            switch (value.Trim().Length)
            {
                case 11:
                    return FormataCPF(value);

                case 14:
                    return FormataCNPJ(value);

                default:
                    return value;
            }
        }

        /// <summary>
        /// Formata o número do CPF 92074286520 para 920.742.865-20
        /// </summary>
        /// <param name="cpf">Sequencia numérica de 11 dígitos. Exemplo: 00000000000</param>
        /// <returns>CPF formatado</returns>
        public static string FormataCPF(this string cpf)
        {
            try
            {
                return cpf.ZeroFill(11).Formatar("###.###.###-##");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formata o CNPJ. Exemplo 00.316.449/0001-63
        /// </summary>
        /// <param name="cnpj">Sequencia numérica de 14 dígitos. Exemplo: 00000000000000</param>
        /// <returns>CNPJ formatado</returns>
        public static string FormataCNPJ(this string cnpj)
        {
            try
            {
                return cnpj.ZeroFill(14).Formatar("##.###.###/####-##");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formato o CEP em 00.000-000
        /// </summary>
        /// <param name="cep">Sequencia numérica de 8 dígitos. Exemplo: 00000000</param>
        /// <returns>CEP formatado</returns>
        public static string FormataCEP(this string cep)
        {
            try
            {
                return cep.OnlyNumbers().Formatar("##.###-###");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formata agência e conta
        /// </summary>
        /// <param name="agencia">Código da agência</param>
        /// <param name="digitoAgencia">Dígito verificador da agência. Pode ser vazio.</param>
        /// <param name="conta">Código da conta</param>
        /// <param name="digitoConta">dígito verificador da conta. Pode ser vazio.</param>
        /// <returns>Agência e conta formatadas</returns>
        public static string FormataAgenciaConta(this string agencia, string digitoAgencia, string conta, string digitoConta)
        {
            try
            {
                var agenciaConta = agencia;
                if (digitoAgencia != string.Empty)
                {
                    agenciaConta += "-" + digitoAgencia;
                }

                agenciaConta += "/" + conta;
                if (digitoConta != string.Empty)
                {
                    agenciaConta += "-" + digitoConta;
                }

                return agenciaConta;
            }
            catch (Exception exception)
            {
                throw new ACBrException("Erro ao formatar agencia conta", exception);
            }
        }

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string Right(this string value, int length)
        {
            if (length > value.Length)
            {
                length = value.Length;
            }

            return value.Substring(value.Length - length);
        }

        /// <summary>
        /// Froms the julian date.
        /// </summary>
        /// <param name="julianDate">The julian date.</param>
        /// <returns>DateTime.</returns>
        public static DateTime FromJulianDate(this string julianDate)
        {
            if (julianDate.Length < 1 || julianDate.Length > 5) return default(DateTime);

            var ano = 2000 + int.Parse(julianDate.Substring(0, 2));
            var dias = int.Parse(julianDate.Substring(2));
            return new DateTime(ano, 1, 1).AddDays(dias);
        }

        /// <summary>
        /// Safes the replace.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="wordToFind">The word to find.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="ignorecase">if set to <c>true</c> [ignorecase].</param>
        /// <returns>System.String.</returns>
        public static string SafeReplace(this string original, string wordToFind, string replacement, bool ignorecase = false)
        {
            var pattern = $@"\b{wordToFind}\b";
            var opt = ignorecase ? RegexOptions.IgnoreCase : RegexOptions.None;
            return Regex.Replace(original, pattern, replacement, opt);
        }

        /// <summary>
        /// To the title case.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public static string ToTitleCase(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        /// <summary>
        /// Determines whether the specified value is null or empty or whitespace.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Befores the specified end.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="end">The end.</param>
        /// <returns>System.String.</returns>
        public static string Before(this string value, int end)
        {
            return end < 1 ? string.Empty : value.GetStrBetween(0, end - 1);
        }

        /// <summary>
        /// Afters the specified start.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start.</param>
        /// <returns>System.String.</returns>
        public static string After(this string value, int start)
        {
            return value.GetStrBetween(start + 1, int.MaxValue);
        }

        /// <summary>
        /// Betweens the specified start.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>System.String.</returns>
        public static string GetStrBetween(this string value, int start, int end)
        {
            if (value.IsEmpty()) return string.Empty;

            var len = value.Length;

            if (start < 0)
            {
                start += len;
            }

            if (end < 0)
            {
                end += len;
            }

            if (len == 0 || start > len - 1 || end < start) return string.Empty;

            if (start < 0)
            {
                start = 0;
            }

            if (end >= len)
            {
                end = len - 1;
            }

            return value.Substring(start, end - start + 1);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="before">The string inicio.</param>
        public static string Before(this string value, string before)
        {
            var posA = value.IndexOf(before);
            return posA == -1 ? string.Empty : value.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="after">The string inicio.</param>
        public static string After(this string value, string after)
        {
            var posA = value.LastIndexOf(after);
            if (posA == -1) return string.Empty;

            var adjustedPosA = posA + after.Length;
            return adjustedPosA >= value.Length ? string.Empty : value.Substring(adjustedPosA);
        }

        /// <summary>
        /// Retorna a string que esta entre as duas string informadas.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="strInicio">The string inicio.</param>
        /// <param name="strFinal">The string final.</param>
        /// <returns>System.String.</returns>
        public static string GetStrBetween(this string value, string strInicio, string strFinal)
        {
            return Regex.Match(value, Regex.Replace(strInicio, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" +
                Regex.Replace(strInicio, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" + Regex.Replace(strFinal, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" +
                Regex.Replace(strFinal, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(strInicio, "").Replace(strFinal, "");
        }

        /// <summary>
        /// Substitutes the specified arguments.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>System.String.</returns>
        public static string Substitute(this string format, params object[] args)
        {
            if (format.IsEmpty()) return string.Empty;
            if (args.Length == 0) return format;

            try
            {
                return string.Format(format, args);
            }
            catch (FormatException)
            {
                return format;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.String.</returns>
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64s the decode.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns>System.String.</returns>
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Remove as tag HTML do texto.
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <returns>System.String.</returns>
        public static string StripHtml(this string htmlString)
        {
            const string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        /// <summary>
        /// Remove os espaços duplicados da string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string RemoveDoubleSpaces(this string value)
        {
            return Regex.Replace(value, "[ ]{2,}", " ", RegexOptions.None);
        }

        #endregion Methods
    }
}