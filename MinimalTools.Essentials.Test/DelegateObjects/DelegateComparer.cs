/*
 * Test for DelegateComparer
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System.Collections.Generic;
using MinimalTools.DelegateObjects;
using Xunit;

namespace MinimalTools.Test.DelegateObjects
{
    /// <summary>
    /// Test for DelegateComparer.Compare 
    /// </summary>
    public class DelegateComparer_Compare
    {
        [Theory(DisplayName = "Zero should be returned when delegate is not specified in arguments.")]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 0)]
        [Trait(nameof(DelegateComparer<int>), nameof(DelegateComparer<int>.Compare))]
        public void NonSetting(int arg1, int arg2, int expected)
            => new DelegateComparer<int>().Compare(arg1, arg2).Is(expected);


        [Theory(DisplayName = "Comparer that created with delegate in arguments of constructor should work.")]
        [InlineData(5, 5, 0)]
        [InlineData(5, 1, 1)]
        [InlineData(1, 5, -1)]
        [Trait(nameof(DelegateComparer<int>), nameof(DelegateComparer<int>.Compare))]
        public void SetDelegateOnConstructor(int arg1, int arg2, int expected)
            => new DelegateComparer<int>((x, y) => x.CompareTo(y)).Compare(arg1, arg2).Is(expected);


        [Fact(DisplayName = "The decending sort should work.")]
        [Trait(nameof(DelegateComparer<int>), nameof(DelegateComparer<int>.Compare))]
        public void ReverseSort()
        {
            var list = new List<int> { 6, 3, 4, 9, 10, 23, 66, 0, 1, 2, 45, };
            var expect = new int[] { 66, 45, 23, 10, 9, 6, 4, 3, 2, 1, 0, };
            var comparer = new DelegateComparer<int>
            {
                DelegateOfCompare = (x, y) => x == y ? 0 : x < y ? 1 : -1 // decending sort
            };
            list.Sort(comparer);

            list.ToArray().Is(expect);
        }


        [Fact(DisplayName = "The ascending sort should work.")]
        [Trait(nameof(DelegateComparer<string>), nameof(DelegateComparer<string>.Compare))]
        public void LengthSort()
        {
            var list = new List<string> { "amenbo", "akaina", "aeiou", "fuji", "a", "sanroku", "oomu", "naku", };
            var expect = new string[] { "a", "fuji", "oomu", "naku", "aeiou", "amenbo", "akaina", "sanroku", };
            var comparer = new DelegateComparer<string>
            {
                DelegateOfCompare = (x, y) => x.Length.CompareTo(y.Length)
            };
            list.Sort(comparer);

            list.ToArray().Is(expect);
        }
    }
}