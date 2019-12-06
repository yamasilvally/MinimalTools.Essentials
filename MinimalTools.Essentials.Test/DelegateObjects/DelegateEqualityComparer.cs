/*
 * Test for DelegateEqualityComparer
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System.Collections.Generic;
using System.Linq;
using MinimalTools.DelegateObjects;
using MinimalTools.Extensions.Linq;
using Xunit;

namespace MinimalTools.Test.DelegateObjects
{
    #region [ Equals ]

    /// <summary>
    /// Test for DelegateEqualityComparer.Equals
    /// </summary>
    public class DelegateEqualityComparer_Equals
    {
        [Theory(DisplayName = "Always 'Equals' should return false when delegate is not set.")]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(1, 1)]
        [Trait(nameof(DelegateEqualityComparer<int>), nameof(DelegateEqualityComparer<int>.Equals))]
        public void NonSettings(int arg1, int arg2)
            => new DelegateEqualityComparer<int>().Equals(arg1, arg2).IsFalse();


        [Theory(DisplayName = "'Equals' should work when an instance of DelegateEqualityComparer created with delegate in arguments.")]
        [InlineData(1, 2, false)]
        [InlineData(2, 1, false)]
        [InlineData(1, 1, true)]
        [Trait(nameof(DelegateEqualityComparer<int>), nameof(DelegateEqualityComparer<int>.Equals))]
        public void SetDelegateOnConstructor(int arg1, int arg2, bool expected)
            => new DelegateEqualityComparer<int>((x, y) => x == y, x => x.GetHashCode()).Equals(arg1, arg2).Is(expected);


        [Fact(DisplayName = "'Distinct' that extension method of LINQ for integer elements should work.")]
        [Trait(nameof(DelegateEqualityComparer<int>), nameof(DelegateEqualityComparer<int>.Equals))]
        public void DistinctInt()
        {
            var comparer = new DelegateEqualityComparer<int>((x, y) => x == y, x => x.GetHashCode());
            int[] source = new[] { 1, 2, 32, 3, 2, 1, 4, 4, 5, 6, 7, 8, 9, 9, };
            int[] expect = new[] { 1, 2, 32, 3, 4, 5, 6, 7, 8, 9, };

            source.Distinct(comparer).ToArray().Is(expect);
        }


        [Fact(DisplayName = "'Distinct' for string should work.")]
        [Trait(nameof(DelegateEqualityComparer<string>), nameof(DelegateEqualityComparer<string>.Equals))]
        public void DistinctString()
        {
            var comparer = new DelegateEqualityComparer<string>((x, y) => x == y, x => x.GetHashCode());
            string[] source = new[] { "1", "2", "32", "3", "2", "1", "4", "4", "5", "6", "7", "8", "9", "9", };
            string[] expect = new[] { "1", "2", "32", "3", "4", "5", "6", "7", "8", "9", };

            source.Distinct(comparer).ToArray().Is(expect);
        }
    }

    #endregion

    #region [ GetHashCode ]

    /// <summary>
    /// Test for DelegateEqualityComparer.GetHashCode.
    /// </summary>
    public class DelegateEqualityComparer_GetHashCode
    {
        [Theory(DisplayName = "Always 'GetHashCode' should return Zero when delegate is not set.")]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [Trait(nameof(DelegateEqualityComparer<int>), nameof(DelegateEqualityComparer<int>.GetHashCode))]
        public void NonSettings(int arg1, int expected)
            => new DelegateEqualityComparer<int>().GetHashCode(arg1).Is(expected);


        [Theory(DisplayName = "'GetHashCode' should work when an instance of DelegateEqualityComparer created with delegate in argument.")]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [Trait(nameof(DelegateEqualityComparer<int>), nameof(DelegateEqualityComparer<int>.GetHashCode))]
        public void SetDelegateOnConstructor(int arg1, int expected)
            => new DelegateEqualityComparer<int>((x, y) => x == y, x => x.GetHashCode()).GetHashCode(arg1).Is(expected);


        [Fact(DisplayName = "Creating HashSet using comparer should work.")]
        [Trait(nameof(DelegateEqualityComparer<int>), nameof(DelegateEqualityComparer<int>.GetHashCode))]
        public void ToSet()
        {
            var comparer = new DelegateEqualityComparer<(int x, string y)>((a, b) => a.y == b.y, x => x.y.GetHashCode());

            var source = new (int x, string y)[] { (1, "fizz"), (1, "buzz"), (2, "buzz"), (2, "fizzbuzz"), (3, "buzz"), (4, "bus"), (4, "gus"), (5, "bus"), };
            var expect = new (int x, string y)[] { (1, "fizz"), (1, "buzz"), (2, "fizzbuzz"), (4, "bus"), (4, "gus"), };

            var hs = new HashSet<(int x, string y)>(source, comparer);
            hs.OrderBy(a => a.x).ToArray().Is(expect);
        }
    }

    #endregion
}