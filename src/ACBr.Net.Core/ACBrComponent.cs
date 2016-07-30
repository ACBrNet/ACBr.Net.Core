// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-31-2016
//
// Last Modified By : RFTD
// Last Modified On : 04-20-2014
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

#if COM_INTEROP

using System.Runtime.InteropServices;

#endif

namespace ACBr.Net.Core
{
#if COM_INTEROP

	[ComVisible(true)]
	[Guid("9FA0F390-307D-44AE-972F-6E63BB77509F")]
	[ClassInterface(ClassInterfaceType.AutoDual)]
#endif

	[DesignerCategory("ACBr.Net")]
	[DesignTimeVisible(true)]
	[TypeConverter(typeof(ACBrExpandableObjectConverter))]
	public abstract class ACBrComponent : IComponent
	{
		#region Fields

		private ISite site;
		private EventHandler disposed;

		#endregion Fields

		#region Events

		public event EventHandler Disposed
		{
#if COM_INTEROP
			[ComVisible(false)]
#endif
			add
			{
				disposed += value;
			}
#if COM_INTEROP
			[ComVisible(false)]
#endif
			remove
			{
				disposed -= value;
			}
		}

		#endregion Events

		#region Constructor

		protected ACBrComponent()
		{
			OnInitialize();
		}

		~ACBrComponent()
		{
			Dispose(false);
		}

		#endregion Constructor

		#region IComponent

		ISite IComponent.Site
		{
			get
			{
				return site;
			}
			set
			{
				site = value;
			}
		}

		[Browsable(false)]
		protected virtual bool DesignMode
		{
			get
			{
				bool isDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

				if (!isDesignMode)
				{
					isDesignMode = site != null && site.DesignMode;
				}

				return isDesignMode;
			}
		}

		#endregion IComponent

		#region Abstract Methods

		protected abstract void OnInitialize();

		protected abstract void OnDisposing();

		#endregion Abstract Methods

		#region Dispose Methods

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}

			OnDisposing();

			disposed?.Invoke(this, EventArgs.Empty);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		#endregion Dispose Methods
	}
}