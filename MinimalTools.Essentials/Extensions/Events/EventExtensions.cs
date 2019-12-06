/*
 * CommonplaceExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using MinimalTools.DelegateObjects;

namespace MinimalTools.Extensions.Events
{
    /// <summary>
    /// Extension methods for event.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Register event handler using weak reference event pattern.
        /// </summary>
        /// <typeparam name="TEventSupplier">A type of the object that exposes an event.</typeparam>
        /// <typeparam name="TEventArgs">>A type of the parameter of event.</typeparam>
        /// <param name="supplier">The object that exposes an event.</param>
        /// <param name="addHandler">An Action to add event handler.</param>
        /// <param name="removeHandler">An Action to remove event handler.</param>
        /// <param name="handler">An event handler.</param>
        /// <returns>An instance of IDIsposable to unsubscribe.</returns>
        public static IDisposable WeakSubscribe<TEventSupplier>(
            this TEventSupplier supplier,
            Action<TEventSupplier, EventHandler> addHandler,
            Action<TEventSupplier, EventHandler> removeHandler,
            EventHandler handler)
        {
            if (supplier == null) throw new ArgumentNullException(nameof(supplier));
            if (addHandler == null) throw new ArgumentNullException(nameof(addHandler));
            if (removeHandler == null) throw new ArgumentNullException(nameof(removeHandler));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var inner = new DelegateDisposable();
            var outer = new DelegateDisposable();
            var wref = new WeakReference<IDisposable>(outer);

            void relayHandler(object s, EventArgs e)
            {
                if (wref.TryGetTarget(out IDisposable d))
                {
                    handler(s, e);
                }
                else
                {
                    inner.Dispose();
                }
            }

            addHandler(supplier, relayHandler);
            inner.DisposingAction = () => removeHandler(supplier, relayHandler);
            outer.DisposingAction = inner.Dispose;
            return outer;
        }


        /// <summary>
        /// Register event handler using weak reference event pattern.
        /// </summary>
        /// <typeparam name="TEventSupplier">A type of the object that exposes an event.</typeparam>
        /// <typeparam name="TEventArgs">A type of the parameter of event.</typeparam>
        /// <param name="supplier">The object that exposes an event.</param>
        /// <param name="addHandler">An Action to add event handler.</param>
        /// <param name="removeHandler">An Action to remove event handler.</param>
        /// <param name="handler">An event handler.</param>
        /// <returns>An instance of IDIsposable to unsubscribe.</returns>
        public static IDisposable WeakSubscribe<TEventSupplier, TEventArgs>(
            this TEventSupplier supplier,
            Action<TEventSupplier, EventHandler<TEventArgs>> addHandler,
            Action<TEventSupplier, EventHandler<TEventArgs>> removeHandler,
            EventHandler<TEventArgs> handler)
        {
            if (supplier == null) throw new ArgumentNullException(nameof(supplier));
            if (addHandler == null) throw new ArgumentNullException(nameof(addHandler));
            if (removeHandler == null) throw new ArgumentNullException(nameof(removeHandler));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var inner = new DelegateDisposable();
            var outer = new DelegateDisposable();
            var wref = new WeakReference<IDisposable>(outer);

            void relayHandler(object s, TEventArgs e)
            {
                if (wref.TryGetTarget(out IDisposable d))
                {
                    handler(s, e);
                }
                else
                {
                    inner.Dispose();
                }
            }

            addHandler(supplier, relayHandler);
            inner.DisposingAction = () => removeHandler(supplier, relayHandler);
            outer.DisposingAction = inner.Dispose;
            return outer;
        }


        /// <summary>
        /// Register event handler using weak reference event pattern.
        /// </summary>
        /// <typeparam name="TEventSupplier">A type of the object that exposes an event.</typeparam>
        /// <typeparam name="TDelegate">A type of the delegate that actual receive event.</typeparam>
        /// <param name="supplier">The object that exposes an event.</param>
        /// <param name="converter">A delegate that relays the event to an event handler that can not be registered directly.</param>
        /// <param name="addHandler">An Action to add event handler.</param>
        /// <param name="removeHandler">An Action to remove event handler.</param>
        /// <param name="handler">An event handler.</param>
        /// <returns>An instance of IDIsposable to unsubscribe.</returns>
        public static IDisposable WeakSubscribe<TEventSupplier, TDelegate>(
            this TEventSupplier supplier,
            Func<TDelegate, EventHandler> converter,
            Action<TEventSupplier, EventHandler> addHandler,
            Action<TEventSupplier, EventHandler> removeHandler,
            TDelegate handler)
        {
            if (supplier == null) throw new ArgumentNullException(nameof(supplier));
            if (converter == null) throw new ArgumentNullException(nameof(converter));
            if (addHandler == null) throw new ArgumentNullException(nameof(addHandler));
            if (removeHandler == null) throw new ArgumentNullException(nameof(removeHandler));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var inner = new DelegateDisposable();
            var outer = new DelegateDisposable();
            var wref = new WeakReference<IDisposable>(outer);

            void relayHandler(object s, EventArgs e)
            {
                if (wref.TryGetTarget(out IDisposable d))
                {
                    converter(handler)(s, e);
                }
                else
                {
                    inner.Dispose();
                }
            }

            addHandler(supplier, relayHandler);
            inner.DisposingAction = () => removeHandler(supplier, relayHandler);
            outer.DisposingAction = inner.Dispose;
            return outer;
        }


        /// <summary>
        /// Register event handler using weak reference event pattern.
        /// </summary>
        /// <typeparam name="TEventSupplier">A type of the object that exposes an event.</typeparam>
        /// <typeparam name="TDelegate">A type of the delegate that actual receive event.</typeparam>
        /// <param name="supplier">The object that exposes an event.</param>
        /// <param name="converter">A delegate that relays the event to an event handler that can not be registered directly.</param>
        /// <param name="addHandler">An Action to add event handler.</param>
        /// <param name="removeHandler">An Action to remove event handler.</param>
        /// <param name="handler">An event handler.</param>
        /// <returns>An instance of IDIsposable to unsubscribe.</returns>
        public static IDisposable WeakSubscribe<TEventSupplier, TEventArgs, TDelegate>(
            this TEventSupplier supplier,
            Func<TDelegate, EventHandler<TEventArgs>> converter,
            Action<TEventSupplier, EventHandler<TEventArgs>> addHandler,
            Action<TEventSupplier, EventHandler<TEventArgs>> removeHandler,
            TDelegate handler)
        {
            if (supplier == null) throw new ArgumentNullException(nameof(supplier));
            if (converter == null) throw new ArgumentNullException(nameof(converter));
            if (addHandler == null) throw new ArgumentNullException(nameof(addHandler));
            if (removeHandler == null) throw new ArgumentNullException(nameof(removeHandler));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var inner = new DelegateDisposable();
            var outer = new DelegateDisposable();
            var wref = new WeakReference<IDisposable>(outer);

            void relayHandler(object s, TEventArgs e)
            {
                if (wref.TryGetTarget(out IDisposable d))
                {
                    converter(handler)(s, e);
                }
                else
                {
                    inner.Dispose();
                }
            }

            addHandler(supplier, relayHandler);
            inner.DisposingAction = () => removeHandler(supplier, relayHandler);
            outer.DisposingAction = inner.Dispose;
            return outer;
        }
    }
}