// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 02-28-2015
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EventHandlerExtension.cs" company="ACBr.Net">
// Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la
// sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio)
// qualquer vers�o posterior.
//
// Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM
// NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU
// ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor
// do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)
//
// Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto
// com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,
// no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Voc� tamb�m pode obter uma copia da licen�a em:
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