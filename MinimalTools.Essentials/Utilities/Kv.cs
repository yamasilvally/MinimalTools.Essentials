/*
 * Kv
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;

namespace MinimalTools.Utilities
{
    /// <summary>
    /// Class version of KeyValuePair.
    /// </summary>
    /// <typeparam name="TKey">A type of the key.</typeparam>
    /// <typeparam name="TValue">A type of the value.</typeparam>
    /// <remarks>
    /// This class can to be used instead of KeyValuePair when null has meaning in LINQ.
    /// This class can also be used to convert to KeyValuePair, ValueTuple,and Tuple.
    /// </remarks>
    public class Kv<TKey, TValue>
    {
        #region [ constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="Kv{TKey, TValue}"/> class.
        /// </summary>
        public Kv() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="Kv{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public Kv(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Kv{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">An instance of Kv to be copied.</param>
        public Kv(Kv<TKey, TValue> source)
        {
            if (source != null)
            {
                this.Key = source.Key;
                this.Value = source.Value;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Kv{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">An instance of KeyValuePair to be copied.</param>
        public Kv(KeyValuePair<TKey, TValue> source) : this(source.Key, source.Value) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="Kv{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">An instance of Tuple to be copied.</param>
        public Kv(Tuple<TKey, TValue> source)
        {
            if (source != null)
            {
                this.Key = source.Item1;
                this.Value = source.Item2;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Kv{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">An instance of ValueTuple to be copied.</param>
        public Kv((TKey, TValue) source) : this(source.Item1, source.Item2) { }


        #endregion

        #region [ properties ]


        /// <summary>The key.</summary>
        public TKey Key { get; set; }


        /// <summary>The value.</summary>
        public TValue Value { get; set; }


        #endregion

        #region [ methods ]


        /// <summary>Deconstruction.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Deconstruct(out TKey key, out TValue value)
        {
            key = this.Key;
            value = this.Value;
        }


        /// <summary>Converts to KeyValuePair.</summary>
        /// <returns>An instance of KeyValuePair.</returns>
        public KeyValuePair<TKey, TValue> ToKeyValuePair() => new KeyValuePair<TKey, TValue>(this.Key, this.Value);


        /// <summary>Converts to Tuple.</summary>
        /// <returns>An instance of Tuple</returns>
        public Tuple<TKey, TValue> ToTuple() => Tuple.Create(this.Key, this.Value);


        /// <summary>Converts to ValueTuple.</summary>
        /// <returns>An instance of ValueTuple.</returns>
        public (TKey, TValue) ToValueTuple() => (this.Key, this.Value);


        /// <summary>Clone itself.</summary>
        /// <returns>An instance of Kv.</returns>
        public Kv<TKey, TValue> Clone() => new Kv<TKey, TValue>(this);


        #endregion
    }


    /// <summary>Factory methods of Kv</summary>
    public static class Kv
    {
        /// <summary>Creates an instance of Kv.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
            => new Kv<TKey, TValue>(key, value);


        /// <summary>Creates an instance of Kv based on other instance.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of Kv to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> Create<TKey, TValue>(Kv<TKey, TValue> source)
            => new Kv<TKey, TValue>(source);


        /// <summary>Creates an instance of Kv based on KeyValuePair.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of KeyValuePair to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> Create<TKey, TValue>(KeyValuePair<TKey, TValue> source) => source.ToKv();


        /// <summary>Converts KeyValuePair to Kv.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of KeyValuePair to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> ToKv<TKey, TValue>(this KeyValuePair<TKey, TValue> source) => new Kv<TKey, TValue>(source);


        /// <summary>Creates an instance of Kv based on Tuple.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of Tuple to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> Create<TKey, TValue>(Tuple<TKey, TValue> source) => source.ToKv();


        /// <summary>Converts Tuple to Kv.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of Tuple to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> ToKv<TKey, TValue>(this Tuple<TKey, TValue> source) => new Kv<TKey, TValue>(source);


        /// <summary>Creates an instance of Kv based on ValueTuple.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of ValueTuple to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> Create<TKey, TValue>((TKey, TValue) source) => source.ToKv();


        /// <summary>Converts ValueTuple to Kv.</summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="source">An instance of ValueTuple to be copied.</param>
        /// <returns>An instance of Kv.</returns>
        public static Kv<TKey, TValue> ToKv<TKey, TValue>(this (TKey, TValue) source) => new Kv<TKey, TValue>(source);
    }
}