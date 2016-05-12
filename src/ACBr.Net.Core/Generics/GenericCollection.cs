// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-31-2015
// ***********************************************************************
// <copyright file="GenericCollection.cs" company="ACBr.Net">
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
using System.Runtime.CompilerServices;
using ACBr.Net.Core.Exceptions;

namespace ACBr.Net.Core.Generics
{
    /// <summary>
    /// Classe GenericCollection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public abstract class GenericCollection<T> : IEnumerable<T> where T : class
	{
		#region Fields

        /// <summary>
        /// The list
        /// </summary>
		protected List<T> List;

		#endregion Fields

		#region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCollection{T}" /> class.
        /// </summary>
		protected internal GenericCollection()
		{
			List = new List<T>();
		}

		#endregion Constructor

		#region Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
		public int Count => List.Count;

	    /// <summary>
        /// Gets or sets the <see cref="T" /> with the specified index.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>T.</returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// </exception>
        [IndexerName("GetItem")]
		public T this[int idx]
		{
			get
			{
				Guard.Against<IndexOutOfRangeException>(idx >= Count || idx < 0);
				return List[idx];
			}
			set
			{
				Guard.Against<IndexOutOfRangeException>(idx >= Count || idx < 0);
				List[idx] = value;
			}
		}

		#endregion Properties

		#region Methods

        /// <summary>
        /// Clears this instance.
        /// </summary>
		public virtual void Clear()
		{
			List.Clear();
		}

		#endregion Methods

		#region IEnumerable<T>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return List.GetEnumerator();
		}

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion IEnumerable<T>
	}
}
