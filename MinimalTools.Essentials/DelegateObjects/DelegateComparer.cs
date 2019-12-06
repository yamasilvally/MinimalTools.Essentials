/*
 * DelegateComparer
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;

namespace MinimalTools.DelegateObjects
{
    /// <summary>
    /// A class that made IComparer&lt;T&gt; implementable with delegate.
    /// </summary>
    /// <typeparam name="T">A type of the element to compare.</typeparam>
    /// <seealso cref="System.Collections.Generic.IComparer{T}" />
    public class DelegateComparer<T> : IComparer<T>
    {
        #region [ constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateComparer{T}"/> class.
        /// </summary>
        public DelegateComparer() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateComparer{T}"/> class.
        /// </summary>
        /// <param name="delegateOfCompare">A delegate that performed when invokes Compare() method.</param>
        public DelegateComparer(Func<T, T, int> delegateOfCompare)
            => this.DelegateOfCompare = delegateOfCompare;


        #endregion

        #region [ properties ]


        /// <summary>
        /// A delegate that performed when invokes Compare() method.
        /// </summary>
        public Func<T, T, int> DelegateOfCompare { get; set; }


        #endregion

        #region [ methods ]


        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// If x is less than y, then return minus value,
        /// else if x equals y, then return zero,
        /// else if x greater than y, then return plus value except zero.
        /// </returns>
        public int Compare(T x, T y) => this.DelegateOfCompare?.Invoke(x, y) ?? 0;


        #endregion
    }
}