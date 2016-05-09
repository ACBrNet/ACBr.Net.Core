// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-24-2016
//
// Last Modified By : RFTD
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="GenericEditable.cs" company="ACBr.Net">
//     Copyright © ACBr.Net 2014 - 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace ACBr.Net.Core.Generics
{
	/// <summary>
	/// Classe generica que implementa a interface IEditableObject
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericEditable<T> : IEditableObject where T : class
	{
		#region Fields

		private Hashtable props;

		#endregion Fields

		#region Methods

		/// <summary>
		/// Begins an edit on an object.
		/// </summary>
		public void BeginEdit()
		{
			//exit if in Edit mode
			//uncomment if  CancelEdit discards changes since the 
			//LAST BeginEdit call is desired action
			//otherwise CancelEdit discards changes since the 
			//FIRST BeginEdit call is desired action
			if (null != props)
				return;

			//enumerate properties            
			var properties = GetType().GetProperties
				(BindingFlags.Public | BindingFlags.Instance);

			props = new Hashtable(properties.Length - 1);

			foreach (var prop in properties)
			{
				//check if there is set accessor
				if (null == prop.GetSetMethod())
					continue;

				var value = prop.GetValue(this, null);

				// Begin child edit
				(value as IEditableObject)?.BeginEdit();

				props.Add(prop.Name, value);
			}
		}

		/// <summary>
		/// Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.
		/// </summary>
		public void CancelEdit()
		{
			//check for inappropriate call sequence
			if (null == props)
				return;

			//restore old values
			var properties = GetType().GetProperties
				(BindingFlags.Public | BindingFlags.Instance);

			foreach (var t in properties)
			{
				//check if there is set accessor
				if (null == t.GetSetMethod())
					continue;

				var value = props[t.Name];

				// Cancel child edit
				(value as IEditableObject)?.CancelEdit();

				t.SetValue(this, value, null);
			}

			//delete current values
			props = null;
		}

		/// <summary>
		/// Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.
		/// </summary>
		public void EndEdit()
		{
			//delete current values
			props = null;
		}

		#endregion Methods
	}
}