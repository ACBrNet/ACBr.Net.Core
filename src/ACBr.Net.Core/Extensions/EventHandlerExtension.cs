// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EventHandlerExtension.cs" company="ACBr.Net">
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
        public static void Raise(this EventHandler eventHandler,
            object sender, EventArgs e)
        {
            if (eventHandler == null) 
                return;

            var synchronizeInvoke = eventHandler.Target as ISynchronizeInvoke;
            if (synchronizeInvoke == null)
                eventHandler.DynamicInvoke(sender, e);
            else
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
        }

		/// <summary>
		/// Raises the specified e.
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		public static void Raise(this EventHandler eventHandler, EventArgs e)
		{
			if (eventHandler == null)
				return;

			var synchronizeInvoke = eventHandler.Target as ISynchronizeInvoke;
			if (synchronizeInvoke == null)
				eventHandler.DynamicInvoke(e);
			else
				synchronizeInvoke.Invoke(eventHandler, new object[] { e });
		}

        /// <summary>
        /// Chama o evento.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void Raise<T>(this EventHandler<T> eventHandler,
            object sender, T e) where T : EventArgs
        {
            if (eventHandler == null) 
                return;

            var synchronizeInvoke = eventHandler.Target as ISynchronizeInvoke;
            if (synchronizeInvoke == null)
                eventHandler.DynamicInvoke(sender, e);
            else
                synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
        }

		/// <summary>
		/// Chama o evento.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="e">The e.</param>
		public static void Raise<T>(this EventHandler<T> eventHandler, T e) where T : EventArgs
		{
			if (eventHandler == null)
				return;

			var synchronizeInvoke = eventHandler.Target as ISynchronizeInvoke;
			if (synchronizeInvoke == null)
				eventHandler.DynamicInvoke(e);
			else
				synchronizeInvoke.Invoke(eventHandler, new object[] { e });
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
			var synchronizeInvoke = eventHandler.Target as ISynchronizeInvoke;
			if (synchronizeInvoke == null)
				eventHandler.DynamicInvoke(sender, e);
			else
				synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
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
			var synchronizeInvoke = eventHandler.Target as ISynchronizeInvoke;
			if (synchronizeInvoke == null)
				eventHandler.DynamicInvoke(sender, e);
			else
				synchronizeInvoke.Invoke(eventHandler, new[] { sender, e });
		}
	}
}