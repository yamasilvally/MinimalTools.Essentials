/*
 * CollectionsExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimalTools.Extensions.Collections
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Returns the value corresponding to the key.
        /// If it is not included in the dictionary, it returns the default value for each type.
        /// </summary>
        /// <typeparam name="K">A type of the key.</typeparam>
        /// <typeparam name="V">A type of the value.</typeparam>
        /// <param name="dict">An instance of implementation of IDictionary&lt;K, V&gt;.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The value corresponding to the key if dictionary contains key, otherwise default value for each types.
        /// </returns>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> dict, K key) => dict.GetOrElse(key, default(V));


        /// <summary>
        /// Returns the value corresponding to the key.
        /// If it is not included in the dictionary, it returns the alternative value that specified in parameter.
        /// </summary>
        /// <typeparam name="K">A type of the key.</typeparam>
        /// <typeparam name="V">A type of the value.</typeparam>
        /// <param name="dict">An instance of implementation of IDictionary&lt;K, V&gt;.</param>
        /// <param name="key">The key.</param>
        /// <param name="alternativeValue">The alternative value.</param>
        /// <returns>
        /// The value corresponding to the key if dict contains key, otherwise the alternative value.
        /// </returns>
        public static V GetOrElse<K, V>(this IDictionary<K, V> dict, K key, V alternativeValue = default(V))
            => dict != null && key != null && dict.TryGetValue(key, out V val) ? val : alternativeValue;


        /// <summary>
        /// Returns the value corresponding to the key.
        /// If it is not included in the dictionary, add the key and the value to the dictionary.
        /// </summary>
        /// <typeparam name="K">A type of the key.</typeparam>
        /// <typeparam name="V">A type of the value.</typeparam>
        /// <param name="dict">An instance of implementation of IDictionary&lt;K, V&gt;.</param>
        /// <param name="key">The key.</param>
        /// <param name="newValue">The new value added when the dictionary dose not contains the key.</param>
        /// <returns>The value corresponding to the key.</returns>
        /// <exception cref="ArgumentNullException">
        /// dict
        /// or
        /// key
        /// </exception>
        public static V GetOrAdd<K, V>(this IDictionary<K, V> dict, K key, V newValue)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (!dict.ContainsKey(key))
            {
                dict.Add(key, newValue);
            }

            return dict[key];
        }


        /// <summary>
        /// Add the key and value to the dictionary. If it already exist, update the value.
        /// </summary>
        /// <typeparam name="K">A type of the key.</typeparam>
        /// <typeparam name="V">A type of the value.</typeparam>
        /// <param name="dict">An instance of implementation of IDictionary&lt;K, V&gt;.</param>
        /// <param name="key">The key.</param>
        /// <param name="newValue">
        /// The value to be added corresponding to the key when dictionary dose not
        /// contains the key, Otherwise to be updated.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// dict
        /// or
        /// key
        /// </exception>
        public static void AddOrUpdate<K, V>(this IDictionary<K, V> dict, K key, V newValue)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dict.ContainsKey(key))
            {
                dict[key] = newValue;
            }
            else
            {
                dict.Add(key, newValue);
            }
        }


        /// <summary>
        /// Remove all elements that satisfy the condition from the list.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="list">An instance of implementation of IList&lt;T&gt;.</param>
        /// <param name="predicate">A delegate for judging whether to remove.</param>
        /// <returns>Count of removed elements.</returns>
        /// <exception cref="ArgumentNullException">list or prediccate</exception>
        public static int RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int prev = list.Count;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                }
            }

            return prev - list.Count;
        }


        /// <summary>
        /// Add the collection to the end of the list.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="list">An instance of implementation of IList&lt;T&gt;.</param>
        /// <param name="collection">The collection to be added.</param>
        /// <exception cref="ArgumentNullException">list or collection</exception>
        public static void AppendLast<T>(this IList<T> list, IEnumerable<T> collection)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var arg in collection) list.Add(arg);
        }


        /// <summary>
        /// Insert collection into the top of the list.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="list">An instance of implementation of IList&lt;T&gt;.</param>
        /// <param name="collection">The collection to be inserted.</param>
        /// <exception cref="ArgumentNullException">list or collection</exception>
        public static void InsertForward<T>(this IList<T> list, IEnumerable<T> collection)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var arg in collection.Select((c, i) => new { c, i })) list.Insert(arg.i, arg.c);
        }
    }
}