/*
 * ObsavableExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using MinimalTools.DelegateObjects;

namespace MinimalTools.Extensions.Reactive
{
    /// <summary>
    /// Extension methods for IObservable&lt;T&gt;.
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        /// Converts to IObservable&lt;T&gt; that can be subscribed with weak reference.
        /// </summary>
        /// <typeparam name="T">A type of element issued from observation target.</typeparam>
        /// <param name="source">The original observation target.</param>
        /// <returns>A instance of implementation of IObserbable&lt;T&gt;.</returns>
        public static IObservable<T> AsWeakObservable<T>(this IObservable<T> source)
            => source != null ? new DelegateObservable<T>(source.WeakSubscribe) : throw new ArgumentNullException(nameof(source));


        /// <summary>
        /// Converts to IObservable&lt;T&gt; that can be subscribed with "very" weak reference.
        /// </summary>
        /// <typeparam name="T">A type of element issued from observation target.</typeparam>
        /// <param name="source">The original observation target.</param>
        /// <returns>A instance of implementation of IObserbable&lt;T&gt;.</returns>
        public static IObservable<T> AsVeryWeakObservable<T>(this IObservable<T> source)
            => source != null
                ? new DelegateObservable<T>(source.VeryWeakSubscribe)
                : throw new ArgumentNullException(nameof(source));


        /// <summary>
        /// Subscribe using weak reference.
        /// </summary>
        /// <typeparam name="T">A type of element issued from observation target.</typeparam>
        /// <param name="source">The observation target.</param>
        /// <param name="observer">The subscriber.</param>
        /// <returns>An instance of IDIsposable to unsubscribe.</returns>
        /// <remarks>
        /// When the reference of the IDisposable is lost without invoking Dispose(),
        /// subscription is unsubscribed automatically.
        /// </remarks>
        public static IDisposable WeakSubscribe<T>(this IObservable<T> source, IObserver<T> observer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            var outer = new DelegateDisposable();
            var wref = new WeakReference<IDisposable>(outer);
            IDisposable inner = null;

            inner = source.Subscribe(new DelegateObserver<T>(
                onNext: v =>
                {
                    if (wref.TryGetTarget(out IDisposable d))
                    {
                        observer.OnNext(v);
                    }
                    else
                    {
                        inner.Dispose();
                    }
                },
                onError: e =>
                {
                    if (wref.TryGetTarget(out IDisposable d))
                    {
                        observer.OnError(e);
                    }
                    else
                    {
                        inner.Dispose();
                    }
                },
                onCompleted: () =>
                {
                    if (wref.TryGetTarget(out IDisposable d))
                    {
                        observer.OnCompleted();
                    }
                    else
                    {
                        inner.Dispose();
                    }
                }));

            outer.DisposingAction = inner.Dispose;
            return outer;
        }


        /// <summary>
        /// Subscribe using "very" weak reference.
        /// </summary>
        /// <typeparam name="T">A type of element issued from observation target.</typeparam>
        /// <param name="source">The observation target.</param>
        /// <param name="observer">The subscriber.</param>
        /// <returns>An instance of IDIsposable to unsubscribe.</returns>
        /// <remarks>
        /// When the reference of the IDisposable or observer's own reference is lost without invoking Dispose(),
        /// subscription is unsubscribed automatically.
        /// </remarks>
        public static IDisposable VeryWeakSubscribe<T>(this IObservable<T> source, IObserver<T> observer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            var outer = new DelegateDisposable();
            var wref = new WeakReference<IDisposable>(outer);
            var wo = new WeakReference<IObserver<T>>(observer);
            IDisposable inner = null;

            inner = source.Subscribe(new DelegateObserver<T>(
                onNext: v =>
                {
                    if (wref.TryGetTarget(out IDisposable d) && wo.TryGetTarget(out IObserver<T> o))
                    {
                        o.OnNext(v);
                    }
                    else
                    {
                        inner.Dispose();
                    }
                },
                onError: e =>
                {
                    if (wref.TryGetTarget(out IDisposable d) && wo.TryGetTarget(out IObserver<T> o))
                    {
                        o.OnError(e);
                    }
                    else
                    {
                        inner.Dispose();
                    }
                },
                onCompleted: () =>
                {
                    if (wref.TryGetTarget(out IDisposable d) && wo.TryGetTarget(out IObserver<T> o))
                    {
                        o.OnCompleted();
                    }
                    else
                    {
                        inner.Dispose();
                    }
                }));

            outer.DisposingAction = inner.Dispose;
            return outer;
        }
    }
}