/*
 * Test for NullHandlingExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;
using MinimalTools.Extensions.NullHandling;
using Xunit;

namespace MinimalTools.Test.Extensions.NullHandling
{
    #region [ Exec ]

    /// <summary>
    /// Test for NullHandlingExtensions.Exec.
    /// </summary>
    public class NullExtensions_Exec
    {
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                //this.Add("abc" as string, 1); // test runner not work.
                this.Add(null as string, 0);
                this.Add(2, 1);
                this.Add(1 as int?, 1);
            }
        }


        [Theory(DisplayName = "Regardless of whether it is a class or struct, if the value is not null, delegate specified by the argument is executed, otherwise delegate should not be executed.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void NotInvokeIfNull<T>(T source, int expectCount)
        {
            var list = new List<T>();
            source?.Exec(list.Add);
            list.Count.Is(expectCount);
            list.FirstOrDefault().Is(source);
        }


        [Fact(DisplayName = "Regardless of whether it is a class or struct, if the value is not null, delegate specified by the argument is executed, otherwise delegate should not be executed.")]
        public void InvokeWhenNotNullString()
        {
            var list = new List<string>();
            var s = "abc";
            s?.Exec(list.Add);
            list.Count.Is(1);
            list.FirstOrDefault().Is(s);
        }


        [Fact(DisplayName = "if the value of nullable struct type is not null, delegate specified by the argument is executed, otherwise delegate should not be executed.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void NotInvokeIfNullableIntIsNull()
        {
            // The argument of Exec() interpreted as Action<int> instead of Action<int?>, 
            // because '?.' operator returns actual value when source has value.
            var list = new List<int?>();
            int? source = null;
            source?.Exec(i => list.Add(i));
            list.Count.Is(0);

            source = 1;
            source?.Exec(i => list.Add(i));
            list.Count.Is(1);
            list.FirstOrDefault().Is(source);
        }

        class MonoValue<T>
        {
            public T Val { get; set; }
        }


        [Fact(DisplayName = "Even if delegate given as an argument is to capture a local variable, it should work.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void UsingClosureShouldWork()
        {
            var source = new MonoValue<string>();
            var arg = "this is test.";

            source?.Exec(s => s.Val = arg);
            source.Val.Is(arg);

            source = null;
            source?.Exec(s => s.Val = "it is expected no exception throwing.");
        }


        [Theory(DisplayName = "Overload of Exec() using a delegate taking two arguments should work as well.")]
        [InlineData(1)]
//        [InlineData("this is test")] test runner abort
        [InlineData(13.50d)]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void ExecWithTwoArgument<T>(T arg)
        {
            var source = new MonoValue<T>();
            source?.Exec((s, v) => s.Val = v, arg);

            source.Val.Is(arg);

            source = null;
            source?.Exec((s, v) => s.Val = v, arg);  // it is expected no exception throwing.
        }

        [Fact(DisplayName = "Overload of Exec() using a delegate taking two arguments should work as well.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void ExecWithTwoArgumentString()
        {
            var arg = "this is test";
            var source = new MonoValue<string>();
            source?.Exec((s, v) => s.Val = v, arg);

            source.Val.Is(arg);

            source = null;
            source?.Exec((s, v) => s.Val = v, arg);  // it is expected no exception throwing.
        }

        [Fact(DisplayName = "ArgumentNullException should be thrown if action is null.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void NullAction()
            => Assert.Throws<ArgumentNullException>(
                    () => ""?.Exec(null as Action<string>))
                .ParamName.Is("action");


        [Fact(DisplayName = "ArgumentNullException should be thrown if action is null.(overload of Exec() using two arguments)")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.Exec))]
        public void NullActionTwoParameters()
            => Assert.Throws<ArgumentNullException>(
                    () => ""?.Exec(null as Action<string, string>, "aaa"))
                .ParamName.Is("action");
    }

    #endregion

    #region [ OrElse ]

    /// <summary>
    /// Test for NullExtensions.OrElse .
    /// </summary>
    public class NullExtensions_OrElse
    {
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
//                this.Add("abc", "cde", "abc"); // test runner abort.
//                this.Add(null, "cde", "cde"); // test runner abort.

                this.Add(1, 2, 1);

                this.Add(6 as int?, 5 as int?, 6 as int?);
                this.Add(null as int?, 3 as int?, 3 as int?);
            }
        }


        [Theory(DisplayName = "Regardless of type, its value should be returned unless the value is null. If the value is null, an alternative value specified by the argument should be returned.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrElse))]
        public void OrElseShouldWork<T>(T src, T alt, T expect) => src.OrElse(alt).Is(expect);

        [Fact(DisplayName = "Regardless of type, its value should be returned unless the value is null. If the value is null, an alternative value specified by the argument should be returned.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrElse))]
        public void OrElseShouldWorkForString()
        {
            string x = "abc";
            "abc".OrElse("efg").Is("abc");

            x = null;
            x?.OrElse("def").Is("def");
        }

        // class TestData002 : DataGenerator
        // {
        //     public TestData002() : base()
        //     {
        //         Func<string> f = () => "cde";
        //         this.Add("abc", f, "abc");
        //         this.Add(null as string, f, "cde");

        //         //Func<int> fi = () => 2;
        //         //this.Add(1, fi, 1);
        //     }
        // }


        [Fact(DisplayName = "Overload of OrElse() which generates alternate values by delegate should work as well.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrElse))]
        public void OrElseUsingDelegateToGenerateAlternativeValueShouldWork()
        {
            var src = 1;
            Func<int> supplier = () => 2;
            src.OrElse(supplier).Is(1);

            int? srcNullable = null;
            Func<int?> supplier2 = () => 3;
            srcNullable.OrElse(supplier2).Is(3);
        }

        [Fact(DisplayName = "Overload of OrElse() which generates alternate values by delegate should work as well.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrElse))]
        public void OrElseUsingDelegateToGenerateAlternativeValueShouldWorkForString()
        {
            var src = "abc";
            Func<string> supplier = () => "def";
            src.OrElse(supplier).Is("abc");

            src = null;
            src.OrElse(supplier).Is("def");
        }

        [Theory(DisplayName = "Overload of OrElse() which generates alternate values by delegate and apply nullable int value should work as well.")]
        [InlineData(null, 10)]
        [InlineData(2, 2)]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrElse))]
        public void OrElseForNullable(int? src, int? expect) => src.OrElse(() => 10).Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if delegate is null.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrElse))]
        public void NullSupplier()
            => Assert.Throws<ArgumentNullException>(
                    () => ((int?)1).OrElse<int?>(null as Func<int?>))
                .ParamName.Is("supplier");
    }

    #endregion

    #region [ OrThrow ]

    /// <summary>
    /// Test for NullExtensions.OrThrow.
    /// </summary>
    public class NullExtensions_OrThrow
    {
        const string message = "this is exceptin";

        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                // this.Add("this is test"); test runner abort
                this.Add(1);
                this.Add((int?)6);
            }
        }

        [Theory(DisplayName = "Actual value should be returned if an argument is not null.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void ReturnValues<T>(T arg) => arg.OrThrow(new Exception(message)).Is(arg);


        [Fact(DisplayName = "Actual value should be returned if an argument is not null.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void ReturnValuesForString() => "arg".OrThrow(new Exception(message)).Is("arg");


        [Theory(DisplayName = "Actual value should be returned if an argument is not null. The exception supplier is not used.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void ReturnValuesNotUsingDelegate<T>(T arg) => arg.OrThrow(() => new Exception(message)).Is(arg);


        [Fact(DisplayName = "Actual value should be returned if an argument is not null. The exception supplier is not used.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void ReturnValuesNotUsingDelegateForString() => "arg".OrThrow(() => new Exception(message)).Is("arg");


        [Fact(DisplayName = "Exception should be thrown if an argument is null.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void Thrown()
            => Assert.Throws<Exception>(
                    () => (null as string).OrThrow(new Exception(message)))
                .Message.Is(message);


        [Fact(DisplayName = "Exception that generate by specified delegate should be thrown if an argument is null.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void ThrowUsingDelegate()
            => Assert.Throws<Exception>(
                    () => (null as string).OrThrow(() => new Exception(message)))
                .Message.Is(message);


        [Fact(DisplayName = "ArgumentNullException should be thrown if delegate is null.")]
        [Trait(nameof(NullHandlingExtensions), nameof(NullHandlingExtensions.OrThrow))]
        public void NullSupplier()
            => Assert.Throws<ArgumentNullException>(
                    () => ((int?)1).OrThrow(null as Func<Exception>))
                .ParamName.Is("exceptionSupplier");
    }

    #endregion
}