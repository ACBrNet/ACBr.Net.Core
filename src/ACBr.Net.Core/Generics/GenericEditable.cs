// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-24-2016
//
// Last Modified By : RFTD
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="GenericEditable.cs" company="ACBr.Net">
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
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace ACBr.Net.Core.Generics
{
    public class GenericEditable<T> : IEditableObject where T : class
    {
        #region Fields

        private Hashtable props;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Begins an edit on an object.
        /// </summary>
        public virtual void BeginEdit()
        {
            //exit if in Edit mode
            //uncomment if  CancelEdit discards changes since the
            //LAST BeginEdit call is desired action
            //otherwise CancelEdit discards changes since the
            //FIRST BeginEdit call is desired action
            if (null != props) return;

            //enumerate properties
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            props = new Hashtable(properties.Length - 1);

            foreach (var prop in properties)
            {
                //check if there is set accessor
                if (null == prop.GetSetMethod()) continue;

                var value = prop.GetValue(this, null);

                // Begin child edit
                (value as IEditableObject)?.BeginEdit();
                props.Add(prop.Name, value);
            }
        }

        /// <summary>
        /// Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.
        /// </summary>
        public virtual void CancelEdit()
        {
            //check for inappropriate call sequence
            if (null == props) return;

            //restore old values
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var t in properties)
            {
                //check if there is set accessor
                if (null == t.GetSetMethod()) continue;

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
        public virtual void EndEdit()
        {
            foreach (var t in props.Values)
                (t as IEditableObject)?.EndEdit();

            //delete current values
            props = null;
        }

        #endregion Methods
    }
}