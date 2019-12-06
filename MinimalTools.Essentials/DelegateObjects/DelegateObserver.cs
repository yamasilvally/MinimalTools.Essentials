/*
 * DelegateObserver
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
    /// A class that made IObserver&lt;T&gt; implementable with delegate.
    /// </summary>
    /// <typeparam name="T">A type of elements to be observed.</typeparam>
    /// <seealso cref="System.IObserver{T}" />
    public class DelegateObserver<T> : IObserver<T>
    {
        #region [ constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateObserver{T}"/> class.
        /// </summary>
        public DelegateObserver() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateObserver{T}"/> class.
        /// </summary>
        /// <param name="onNext">A delegate that performed when invokes OnNext() method.</param>
        /// <param name="onError">A delegate that performed when invokes OnError() method.</param>
        /// <param name="onCompleted">A delegate that performed when invokes OnCompleted() method.</param>
        public DelegateObserver(Action<T> onNext, Action<Exception> onError = null, Action onCompleted = null)
        {
            this.Next = onNext;
            this.Error = onError;
            this.Completed = onCompleted;
        }


        #endregion

        #region [ properties ]


        /// <summary>
        /// A delegate that performed when invokes OnNext() method.
        /// </summary>
        public Action<T> Next { get; set; }


        /// <summary>
        /// A delegate that performed when invokes OnError() method.
        /// </summary>
        public Action<Exception> Error { get; set; }


        /// <summary>
        /// A delegate that performed when invokes OnCompleted() method.
        /// </summary>
        public Action Completed { get; set; }


        #endregion


        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(T value) => this.Next?.Invoke(value);


        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error) => this.Error?.Invoke(error);


        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted() => this.Completed?.Invoke();
    }
}