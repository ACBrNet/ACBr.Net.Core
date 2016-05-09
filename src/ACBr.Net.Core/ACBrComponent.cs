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

            if (disposed != null) disposed(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion Dispose Methods
	}
}