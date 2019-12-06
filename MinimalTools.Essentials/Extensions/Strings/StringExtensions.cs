/*
 * StringExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System.Linq;
using System.Text;

namespace MinimalTools.Extensions.Strings
{
    /// <summary>
    /// Extension methods of string. 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Concatenates by repeating the original string specified number of times.
        /// </summary>
        /// <param name="arg">A original string.</param>
        /// <param name="count">Repeat count.</param>
        /// <returns>A string concatenated by repeating the original string specified number of times.</returns>
        /// <remarks>If original string is null, returns string.Empty.</remarks>
        public static string Repeat(this string arg, int count)
        {
            var sb = new StringBuilder();
            foreach (var i in Enumerable.Range(0, count > 0 ? count : 1)) sb.Append($"{arg}");
            return sb.ToString();
        }
        

        /// <summary>The shortcut of IsNullOrEmpty</summary>
        /// <param name="arg">A string</param>
        /// <returns>If the string is null or empty, returns true, otherwise returns false.</returns>
        public static bool IsNullOrEmpty(this string arg) => string.IsNullOrEmpty(arg);
    }
}