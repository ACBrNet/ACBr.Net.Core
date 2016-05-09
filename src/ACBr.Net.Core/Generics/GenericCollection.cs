// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-31-2015
// ***********************************************************************
// <copyright file="GenericCollection.cs" company="ACBr.Net">
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
