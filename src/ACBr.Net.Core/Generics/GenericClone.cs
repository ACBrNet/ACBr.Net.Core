// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-06-2015
//
// Last Modified By : RFTD
// Last Modified On : 24-03-2016
// ***********************************************************************
// <copyright file="GenericClone.cs" company="ACBr.Net">
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
using System.Linq;
using System.Reflection;

namespace ACBr.Net.Core.Generics
{
    /// <summary>
    /// Classe GenericClone implementação generica da interface ICloneable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericClone<T> : ICloneable where T : class
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>T.</returns>
        public T Clone()
        {
            //First we create an instance of this specific type.
            var newObject = (T)Activator.CreateInstance(GetType());

            //We get the array of fields for the new type instance.
            var fields = newObject.GetType().GetFields();

            var i = 0;

            foreach (var fi in GetType().GetFields())
            {
                //We query if the fiels support the ICloneable interface.
                var ICloneType = fi.FieldType.GetInterface("ICloneable", true);

                if (ICloneType != null)
                {
                    //Getting the ICloneable interface from the object.
                    var IClone = (ICloneable)fi.GetValue(this);

                    //We use the clone method to set the new value to the field.
                    fields[i].SetValue(newObject, IClone.Clone());
                }
                else
                {
                    // If the field doesn't support the ICloneable
                    // interface then just set it.
                    fields[i].SetValue(newObject, fi.GetValue(this));
                }

                //Now we check if the object support the
                //IEnumerable interface, so if it does
                //we need to enumerate all its items and check if
                //they support the ICloneable interface.
                var IEnumerableType = fi.FieldType.GetInterface("IEnumerable", true);
                if (IEnumerableType != null)
                {
                    //Get the IEnumerable interface from the field.
                    var IEnum = (IEnumerable)fi.GetValue(this);

                    //This version support the IList and the
                    //IDictionary interfaces to iterate on collections.
                    var IListType = fields[i].FieldType.GetInterface("IList", true);
                    var IDicType = fields[i].FieldType.GetInterface("IDictionary", true);

                    var j = 0;
                    if (IListType != null)
                    {
                        //Getting the IList interface.
                        var list = (IList)fields[i].GetValue(newObject);

                        foreach (var obj in IEnum)
                        {
                            //Checking to see if the current item
                            //support the ICloneable interface.
                            ICloneType = obj.GetType().GetInterface("ICloneable", true);

                            if (ICloneType != null)
                            {
                                //If it does support the ICloneable interface,
                                //we use it to set the clone of
                                //the object in the list.
                                var clone = (ICloneable)obj;

                                list[j] = clone.Clone();
                            }

                            //NOTE: If the item in the list is not
                            //support the ICloneable interface then in the
                            //cloned list this item will be the same
                            //item as in the original list
                            //(as long as this type is a reference type).

                            j++;
                        }
                    }
                    else if (IDicType != null)
                    {
                        //Getting the dictionary interface.
                        var dic = (IDictionary)fields[i].GetValue(newObject);
                        j = 0;

                        foreach (DictionaryEntry de in IEnum)
                        {
                            //Checking to see if the item
                            //support the ICloneable interface.
                            ICloneType = de.Value.GetType().GetInterface("ICloneable", true);

                            if (ICloneType != null)
                            {
                                var clone = (ICloneable)de.Value;

                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }

        /// <inheritdoc />
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}