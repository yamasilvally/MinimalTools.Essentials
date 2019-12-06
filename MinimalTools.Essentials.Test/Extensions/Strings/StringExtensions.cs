/*
 * Test for StringExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using MinimalTools.Extensions.Strings;
using Xunit;

namespace MinimalTools.Test.Extensions.Strings
{
    /// <summary>
    /// Test for StringExtensions.Repeat.
    /// </summary>
    public class StringExtensions_Repeat
    {
        class TestData : DataGenerator
        {
            public TestData() : base()
            {
                var s = "this is test";
                this.Add(s, -1, s);
                this.Add(s, 0, s);
                this.Add(s, 1, s);
                this.Add(s, 2, s + s);
                this.Add(s, 3, s + s + s);
            }
        }


        [Theory(DisplayName = "Repeat method should return string that repeated specified times, but if number of repeat times less than one, return string that same as source string.")]
        [ClassData(typeof(TestData))]
        [Trait(nameof(StringExtensions), nameof(StringExtensions.Repeat))]
        public void Normal(string src, int repeat, string expect) => src.Repeat(repeat).Is(expect);


        [Theory(DisplayName = "Repeat method should return string.Empty if source string is null.")]
        [InlineData(-1)][InlineData(0)][InlineData(1)][InlineData(2)][InlineData(3)]
        [Trait(nameof(StringExtensions), nameof(StringExtensions.Repeat))]
        public void NullString(int repeat)
            => (null as string).Repeat(repeat).Is(string.Empty);
    }


    /// <summary>
    /// Test for StringExtensions.IsNullOrEmpty.
    /// </summary>
    public class StringExtensions_IsNullOrEmpty
    {
        [Fact(DisplayName = "Non null string should return false.")]
        [Trait(nameof(StringExtensions), nameof(StringExtensions.IsNullOrEmpty))]
        public void TruePattern() => "a".IsNullOrEmpty().IsFalse();


        [Theory(DisplayName = "Null or empty string should return true.")]
        [InlineData(null)][InlineData("")]
        [Trait(nameof(StringExtensions), nameof(StringExtensions.IsNullOrEmpty))]
        public void FalsePattern(string s) => s.IsNullOrEmpty().IsTrue();
    }
}