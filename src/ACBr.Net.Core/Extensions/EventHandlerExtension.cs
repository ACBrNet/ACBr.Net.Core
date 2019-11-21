// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EventHandlerExtension.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Classe EventHandlerExtension.
    /// </summary>
    public static class EventHandlerExtension
    {
        /// <summary>
        /// Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
            }
            else
            {
                eventHandler.DynamicInvoke(sender, e);
            }
        }

        /// <summary>
        /// Chama o evento com os argumentos passado.
        /// Passando null para o sender.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Raise(this EventHandler eventHandler, EventArgs e)
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { null, e });
            }
            else
            {
                eventHandler.DynamicInvoke(null, e);
            }
        }

        /// <summary>
        /// Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void Raise(this PropertyChangedEventHandler eventHandler, object sender, PropertyChangedEventArgs e)
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
            }
            else
            {
                eventHandler.DynamicInvoke(sender, e);
            }
        }

        /// <summary>
        /// Chama o evento.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
            }
            else
            {
                eventHandler.DynamicInvoke(sender, e);
            }
        }

        /// <summary>
        /// Chama o evento com os argumentos passado.
        /// Passando null para o sender.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">Argumentos do evento.</param>
        public static void Raise<T>(this EventHandler<T> eventHandler, T e) where T : EventArgs
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { null, e });
            }
            else
            {
                eventHandler.DynamicInvoke(null, e);
            }
        }

        /// <summary>
        /// Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        public static void Raise(this EventHandler<EventArgs> eventHandler, object sender)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
            }
            else
            {
                eventHandler.DynamicInvoke(sender, e);
            }
        }

        /// <summary>
        /// Chama o evento.
        /// Passando null para o sender e EventArgs.Empty
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        public static void Raise(this EventHandler<EventArgs> eventHandler)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { null, e });
            }
            else
            {
                eventHandler.DynamicInvoke(null, e);
            }
        }

        /// <summary>
        /// Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        public static void Raise(this EventHandler eventHandler, object sender)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
            }
            else
            {
                eventHandler.DynamicInvoke(sender, e);
            }
        }

        /// <summary>
        /// Chama o evento.
        /// Passando null para o sender e EventArgs.Empty
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        public static void Raise(this EventHandler eventHandler)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
            {
                synchronizeInvoke.Invoke(eventHandler, new[] { null, e });
            }
            else
            {
                eventHandler.DynamicInvoke(null, e);
            }
        }
    }
}