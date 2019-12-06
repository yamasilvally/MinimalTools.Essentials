/*
 * DelegateObservable
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;

namespace MinimalTools.DelegateObjects
{
    /// <summary>
    /// A class that made IObservable&lt;T&gt; implementable with delegate.
    /// </summary>
    /// <typeparam name="T">A type of elements to be observed.</typeparam>
    /// <seealso cref="System.IObservable{T}" />
    public class DelegateObservable<T> : IObservable<T>
    {
        #region [ constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateObservable{T}"/> class.
        /// </summary>
        public DelegateObservable() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateObservable{T}"/> class.
        /// </summary>
        /// <param name="delegateOfSubscribe">A delegate that performed when invokes Subscribe() method.</param>
        public DelegateObservable(Func<IObserver<T>, IDisposable> delegateOfSubscribe)
        {
            this.DelegateOfSubscribe = delegateOfSubscribe;
        }


        #endregion

        #region [ properties ]


        /// <summary>
        /// A delegate that performed when invokes Subscribe() method.
        /// </summary>
        public Func<IObserver<T>, IDisposable> DelegateOfSubscribe { get; set; }


        #endregion


        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<T> observer)
            => this.DelegateOfSubscribe?.Invoke(observer) ?? new DelegateDisposable();
    }
}