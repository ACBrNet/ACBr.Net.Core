// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-05-2017
//
// Last Modified By : RFTD
// Last Modified On : 04-05-2017
// ***********************************************************************
// <copyright file="ACBrIniFile.cs" company="ACBr.Net">
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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Core
{
    public sealed class ACBrIniFile : IEnumerable<ACBrIniSection>
    {
        #region Fields

        private List<ACBrIniSection> sections;

        #endregion Fields

        #region Constructors

        public ACBrIniFile() : this(Assembly.GetExecutingAssembly().GetPath(), "ACBr.ini", ACBrEncoding.ISO88591, 1024)
        {
        }

        public ACBrIniFile(string iniFilePath, string iniFileName) : this(iniFilePath, iniFileName, ACBrEncoding.ISO88591, 1024)
        {
        }

        public ACBrIniFile(string iniFilePath, string iniFileName, Encoding encoding, int bufferSize)
        {
            sections = new List<ACBrIniSection>();
            Encoding = encoding;
            BufferSize = bufferSize;
            IniFilePath = iniFilePath;
            IniFileName = iniFileName;
        }

        #endregion Constructors

        #region Properties

        public string IniFileName { get; set; }

        public string IniFilePath { get; set; }

        public Encoding Encoding { get; set; }

        public int BufferSize { get; set; }

        public int SectionCount => sections.Count;

        public ACBrIniSection this[string section]
        {
            get
            {
                var ret = sections.SingleOrDefault(x => x.Name == section);
                if (ret != null) return ret;

                ret = new ACBrIniSection(this, section);
                sections.Add(ret);

                return ret;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retorna true se a seção existir no ini.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool Contains(string section)
        {
            return sections.Any(x => x.Name == section);
        }

        public bool Contains(ACBrIniSection section)
        {
            return sections.Contains(section);
        }

        public ACBrIniSection AddNew(string section)
        {
            var ret = new ACBrIniSection(this, section);
            sections.Add(ret);
            return ret;
        }

        public void Add(ACBrIniSection section)
        {
            sections.Add(section);
        }

        public void Remove(string section)
        {
            var ret = sections.Single(x => x.Name == section);
            sections.Remove(ret);
        }

        public void Remove(ACBrIniSection section)
        {
            sections.Remove(section);
        }

        public TType Read<TType>(string section, string propertie, TType defaultValue = default(TType), IFormatProvider format = null)
        {
            if (propertie.IsEmpty()) return defaultValue;
            if (section.IsEmpty()) return defaultValue;

            var iniSection = this[section];
            return iniSection.GetValue(propertie, defaultValue, format);
        }

        public void Write(string section, string propertie, object value)
        {
            if (propertie.IsEmpty()) return;
            if (section.IsEmpty()) return;

            var iniSection = this[section];
            iniSection.Add(propertie, string.Format(CultureInfo.InvariantCulture, "{0}", value));
        }

        public void Save()
        {
            var file = Path.Combine(IniFilePath, IniFileName);

            using (var writer = new StreamWriter(file, false, Encoding, 1024))
            {
                foreach (var section in sections)
                {
                    writer.WriteLine($"[{section.Name}]");

                    foreach (var iniData in section)
                    {
                        writer.WriteLine($"{iniData.Key}={iniData.Value}");
                    }

                    writer.WriteLine("");
                }

                writer.Flush();
            }
        }

        public static ACBrIniFile Load(string file, Encoding encoding = null)
        {
            Guard.Against<FileNotFoundException>(!File.Exists(file), file);

            var path = Path.GetDirectoryName(file);
            var iniFileName = Path.GetFileName(file);

            ACBrIniFile ret;
            using (var fileStream = File.OpenRead(file))
                ret = Load(fileStream, encoding);

            ret.IniFileName = iniFileName;
            ret.IniFilePath = path;
            return ret;
        }

        public static ACBrIniFile Load(Stream stream, Encoding encoding = null)
        {
            Guard.Against<ArgumentNullException>(stream == null, nameof(stream));

            encoding = encoding ?? ACBrEncoding.ISO88591;
            var iniFile = new ACBrIniFile { Encoding = encoding };

            using (var reader = new StreamReader(stream, iniFile.Encoding))
            {
                string line;
                var section = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.IsEmpty()) continue;
                    if (line.StartsWith(";")) continue;

                    if (line.StartsWith("["))
                    {
                        section = line.Substring(1, line.Length - 2);
                        iniFile.sections.Add(new ACBrIniSection(iniFile, section));
                    }
                    else
                    {
                        if (section.IsEmpty()) continue;

                        var iniSection = iniFile[section];
                        var properties = line.Split('=');
                        iniSection.Add(properties[0], properties[1]);
                    }
                }
            }

            return iniFile;
        }

        #endregion Methods

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>

        public IEnumerator<ACBrIniSection> GetEnumerator()
        {
            return sections.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable
    }
}