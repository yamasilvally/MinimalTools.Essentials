/*
 * DelegateEqualityComparer
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
    /// A class that made IEqualityComparer&lt;T&gt; implementable with delegate.
    /// </summary>
    /// <typeparam name="T">A type of object for determining equality.</typeparam>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{T}" />
    public class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {
        #region [ constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateEqualityComparer{T}"/> class.
        /// </summary>
        public DelegateEqualityComparer() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="delegateOfEquals">A delegate that performed when invokes Equals() method.</param>
        /// <param name="delegateOfGetHashCode">A delegate that performed when invokes GetHashCode() method.</param>
        public DelegateEqualityComparer(Func<T, T, bool> delegateOfEquals, Func<T, int> delegateOfGetHashCode)
        {
            this.DelegateOfEquals = delegateOfEquals;
            this.DelegateOfGetHashCode = delegateOfGetHashCode;
        }


        #endregion

        #region [ properties ]


        /// <summary>
        /// A delegate that performed when invokes Equals() method.
        /// </summary>
        public Func<T, T, bool> DelegateOfEquals { get; set; }


        /// <summary>
        /// A delegate that performed when invokes GetHashCode() method.
        /// </summary>
        public Func<T, int> DelegateOfGetHashCode { get; set; }


        #endregion

        #region [ methods ]


        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y) => this.DelegateOfEquals?.Invoke(x, y) ?? false;


        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(T obj) => this.DelegateOfGetHashCode?.Invoke(obj) ?? 0;


        #endregion
    }
}