/*
 * Utl
 *
 * Copyright (c) 2018 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimalTools.Utilities
{
    /// <summary>
    /// A measly utility
    /// </summary>
    public static class Utl
    {
        /// <summary>
        /// Converts variable length arguments to IEnumerable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="args">Variable length arguments.</param>
        /// <returns>An instance of IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> AsEnumerable<T>(params T[] args) => args ?? new T[] { };


        /// <summary>
        /// Converts member of enum to IEnumerable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">A type of enum.</typeparam>
        /// <returns>An instance of IEnumerable&lt;T&gt;.</returns>
        /// <remarks>if <typeparamref name="T"/> is not enum, returns empty sequence.</remarks>
        public static IEnumerable<T> ToEnumerable<T>() where T : struct
            => typeof(T).IsEnum
                ? Enum.GetNames(typeof(T)).Select(s => (T)Enum.Parse(typeof(T), s)).ToArray()
                : new T[] { };
    }
}