// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-31-2016
//
// Last Modified By : RFTD
// Last Modified On : 05-18-2017
// ***********************************************************************
// <copyright file="ACBrComponent.cs" company="ACBr.Net">
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
using System.ComponentModel;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Core
{
	/// <summary>
	/// Classe base para os componentes ACBr .Net
	/// </summary>
	[DesignerCategory("ACBr.Net")]
	[DesignTimeVisible(true)]
	[TypeConverter(typeof(ACBrExpandableObjectConverter))]
	public abstract class ACBrComponent : IComponent
	{
		#region Fields

		private ISite site;

		#endregion Fields

		#region Events

		/// <summary>
		/// Represents the method that handles the <see cref="E:System.ComponentModel.IComponent.Disposed" /> event of a component.
		/// </summary>
		public event EventHandler Disposed;

		#endregion Events

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="ACBrComponent"/> class.
		/// </summary>
		protected ACBrComponent()
		{
			OnInitialize();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="ACBrComponent"/> class.
		/// </summary>
		~ACBrComponent()
		{
			Dispose(false);
		}

		#endregion Constructor

		#region IComponent

		ISite IComponent.Site
		{
			get => site;
			set => site = value;
		}

		/// <summary>
		///
		/// </summary>
		[Browsable(false)]
		protected virtual bool DesignMode
		{
			get
			{
				var isDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

				if (!isDesignMode)
				{
					isDesignMode = site != null && site.DesignMode;
				}

				return isDesignMode;
			}
		}

		#endregion IComponent

		#region Abstract Methods

		/// <summary>
		/// Função executada no inicio do componente.
		/// </summary>
		protected abstract void OnInitialize();

		/// <summary>
		/// Função executa no dispose do componente.
		/// </summary>
		protected abstract void OnDisposing();

		#endregion Abstract Methods

		#region Dispose Methods

		private void Dispose(bool disposing)
		{
			if (disposing) GC.SuppressFinalize(this);

			OnDisposing();
			Disposed.Raise(this, EventArgs.Empty);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		#endregion Dispose Methods
	}
}