/*
 * Test for Utl
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System.Collections.Generic;
using MinimalTools.Utilities;
using Xunit;

namespace MinimalTools.Test.Utilities
{
    /// <summary>
    /// Test for Utl.sEnumerable&lt;T&gt;.
    /// </summary>
    public class Utl_AsEnumerable
    {
        [Fact(DisplayName = "If no argument is specified, it should return an empty sequence.")]
        [Trait(nameof(Utl), nameof(Utl.AsEnumerable))]
        public void NoArgument()
            => Utl.AsEnumerable<string>().Is(new string[] { } as IEnumerable<string>);


        [Fact(DisplayName = "If any argument is specified, it should return a sequence that containing elements.")]
        [Trait(nameof(Utl), nameof(Utl.AsEnumerable))]
        public void AnyArguments()
            => Utl.AsEnumerable(1, 2, 3, 99).Is(new int[]{1, 2, 3, 99} as IEnumerable<int>);
    }

    /// <summary>
    /// Test for Utl.ToEnumerable&lt;T&gt;.
    /// </summary>
    public class Utl_ToEnumerable
    {
        enum TestEnum
        {
            Zero = 0,
            First,

            Second,

            Third,
        }

        [Fact(DisplayName="Enum should be converted into a sequence.")]
        [Trait(nameof(Utl), nameof(Utl.ToEnumerable))]
        public void TypeIsEnum()
            => Utl.ToEnumerable<TestEnum>().Is(TestEnum.Zero, TestEnum.First, TestEnum.Second, TestEnum.Third);


        [Fact(DisplayName="Non-enum shuoud be converted into empty-sequence.")]
        [Trait(nameof(Utl), nameof(Utl.ToEnumerable))]
        public void TypeIsNotEnum()
            => Utl.ToEnumerable<int>().Is(new int[]{});
    }
}