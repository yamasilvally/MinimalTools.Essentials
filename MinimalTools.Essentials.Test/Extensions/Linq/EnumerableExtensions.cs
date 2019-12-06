/*
 * Test for EnumerableExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MinimalTools.DelegateObjects;
using MinimalTools.Extensions.Linq;
using MinimalTools.Utilities;
using Xunit;
using static MinimalTools.Test.Extensions.Linq.TestResource;

namespace MinimalTools.Test.Extensions.Linq
{
    class TestResource
    {
        public const string SEQUENCE = "sequence";
        public const string ACTION = "action";
        public const string PREDICATE = "predicate";
        public const string SELECTOR = "selector";
        public const string KEY_SELECTOR = "keySelector";
        public const string VALUE_SELECTOR = "valueSelector";
        public const string INNER = "inner";
        public const string OUTER = "outer";
        public const string RESULT_SELECTOR = "resultSelector";
        public const string INNER_KEY_SELECTOR = "innerKeySelector";
        public const string OUTER_KEY_SELECTOR = "outerKeySelector";
        public const string COMPARER = "comparer";
        public const string SECOND_SEQUENCE = "secondSequence";
    }

    #region [ Foreach ]

    /// <summary>
    /// Test for EnumerableExtensions.Foreach (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Foreach
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<string> source = new[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                Func<string, string> process = s => $"++{s}++";
                IEnumerable<string> expect = new[] { "++aaa++", "++bbbb++", "++c++", "++dd++", "++eeeee++" };
                this.Add(source, process, expect);

                source = new string[] { };
                expect = new string[] { };
                this.Add(source, process, expect);

                IEnumerable<int> intSource = new[] { 1, 2, 3, 4, 5 };
                Func<int, int> intProc = i => i * 2;
                IEnumerable<int> intExpect = new[] { 2, 4, 6, 8, 10 };
                this.Add(intSource, intProc, intExpect);

                // In xUnit, the following can not be analyzed well (Assert.Equal does not pass)
                //IEnumerable<int?> niSource = new int?[] { 1, null, 3, null, 5 };
                //Func<int?, int?> niProc = i => (i ?? 0) * 2;
                //IEnumerable<int?> niExpect = new int?[] { 2, 0, 6, 0, 10 };
                //this.TestData.Add(new object[] { niSource, niProc, niExpect });

                IEnumerable<(int, string)> vtSource = new (int, string)[] { (1, "one"), (2, null), (3, "three"), };
                Func<(int, string), (int, string)> vtProc = vt => (vt.Item1 * 2, string.IsNullOrEmpty(vt.Item2) ? string.Empty : $"multiple {vt.Item2}");
                IEnumerable<(int, string)> vtExpect = new (int, string)[] { (2, "multiple one"), (4, string.Empty), (6, "multiple three"), };
                this.Add(vtSource, vtProc, vtExpect);
            }
        }


        [Theory(DisplayName = "All elements should be proceed.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void Normal<T>(IEnumerable<T> source, Func<T, T> process, IEnumerable<T> expect)
        {
            var result = new List<T>();
            source.Foreach(s => result.Add(process(s)));

            result.Is(expect);
        }


        /// <summary>
        /// Test Data for overload of Foreach() taking condition as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<string> source = new[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                Func<string, string> process = s => $"++{s}++";
                Func<string, bool> predicate = s => s.Length > 3;
                IEnumerable<string> expect = new[] { "++bbbb++", "++eeeee++" };
                this.Add(source, predicate, process, expect);

                source = new[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                predicate = s => s.Length <= 3;
                expect = new[] { "++aaa++", "++c++", "++dd++", };
                this.Add(source, predicate, process, expect);

                source = new string[] { };
                expect = new string[] { };
                this.Add(source, predicate, process, expect);

                IEnumerable<int> intSource = new[] { 1, 2, 3, 4, 5 };
                Func<int, int> intProc = i => i * 2;
                Func<int, bool> intPredicate = i => i > 2;
                IEnumerable<int> intExpect = new[] { 6, 8, 10 };
                this.Add(intSource, intPredicate, intProc, intExpect);
            }
        }


        [Theory(DisplayName = "Only elements that matches the condition should be proceed.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NormalWithPredicate<T>(
                IEnumerable<T> source, Func<T, bool> predicate, Func<T, T> process, IEnumerable<T> expect)
        {
            var result = new List<T>();
            source.Foreach(predicate, s => result.Add(process(s)));

            result.Is(expect);
        }


        /// <summary>
        /// Test Data for overload of Foreach() taking index as an argument.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                IEnumerable<string> source = new[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                Func<string, int, string> process = (s, i) => $"{i}.{s}";
                IEnumerable<string> expect = new[] { "0.aaa", "1.bbbb", "2.c", "3.dd", "4.eeeee" };
                this.Add(source, process, expect);

                source = new string[] { };
                expect = new string[] { };
                this.Add(source, process, expect);

                IEnumerable<int> intSource = new[] { 1, 2, 3, 4, 5 };
                Func<int, int, int> intProc = (s, i) => i * s;
                IEnumerable<int> intExpect = new[] { 0, 2, 6, 12, 20 };
                this.Add(intSource, intProc, intExpect);

                IEnumerable<(int, string)> vtSource = new (int, string)[] { (1, "one"), (2, null), (3, "three"), };
                Func<(int, string), int, (int, string)> vtProc =
                    (vt, i) => (vt.Item1 * 2 + i, string.IsNullOrEmpty(vt.Item2) ? string.Empty : $"multiple {vt.Item2}");
                IEnumerable<(int, string)> vtExpect = new (int, string)[] { (2, "multiple one"), (5, string.Empty), (8, "multiple three"), };
                this.Add(vtSource, vtProc, vtExpect);
            }
        }


        [Theory(DisplayName = "All elements should be proceed with the index.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NormalWithIndex<T>(IEnumerable<T> source, Func<T, int, T> process, IEnumerable<T> expect)
        {
            var result = new List<T>();
            source.Foreach((s, i) => result.Add(process(s, i)));

            result.Is(expect);
        }


        /// <summary>
        /// Test Data for overload of Foreach() taking index and condition as arguments.
        /// </summary>
        class TestData004 : DataGenerator
        {
            public TestData004() : base()
            {
                IEnumerable<string> source = new[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                Func<string, int, string> process = (s, i) => $"++{i}.{s}++";
                Func<string, bool> predicate = s => s.Length > 3;
                IEnumerable<string> expect = new[] { "++0.bbbb++", "++1.eeeee++" };
                this.Add(source, predicate, process, expect);

                predicate = s => s.Length <= 3;
                expect = new[] { "++0.aaa++", "++1.c++", "++2.dd++", };
                this.Add(source, predicate, process, expect);

                source = new string[] { };
                expect = new string[] { };
                this.Add(source, predicate, process, expect);

                IEnumerable<int> intSource = new[] { 1, 2, 3, 4, 5 };
                Func<int, int, int> intProc = (s, i) => s * i;
                Func<int, bool> intPredicate = i => i > 2;
                IEnumerable<int> intExpect = new[] { 0, 4, 10 };
                this.Add(intSource, intPredicate, intProc, intExpect);
            }
        }


        [Theory(DisplayName = "Only elements that matches the condition should be proceed with index.")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NormalWithPredicateAndIndex<T>(
                IEnumerable<T> source, Func<T, bool> predicate, Func<T, int, T> process, IEnumerable<T> expect)
        {
            var result = new List<T>();
            source.Foreach(predicate, (s, i) => result.Add(process(s, i)));

            result.Is(expect);
        }


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData005 : DataGenerator
        {
            public TestData005() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3 };
                Action<int> action = i => { var x = i; };

                this.Add(null, action, SEQUENCE);
                this.Add(source, null, ACTION);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData005))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NullChecks(IEnumerable<int> source, Action<int> action, string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.Foreach(action))
                .ParamName.Is(expect);


        /// <summary>
        /// Test Data for null checking of overload of Foreach() that takes an index as an argument.
        /// </summary>
        class TestData006 : DataGenerator
        {
            public TestData006() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3 };
                Action<int, int> action = (s, i) => { var x = s * i; };

                this.Add(null, action, SEQUENCE);
                this.Add(source, null, ACTION);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData006))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NullChecksWithIndex(IEnumerable<int> source, Action<int, int> action, string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.Foreach(action))
                .ParamName.Is(expect);


        /// <summary>
        /// Test Data for null checking of overload of Foreach() that takes condition as an argument.
        /// </summary>
        class TestData007 : DataGenerator
        {
            public TestData007() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3 };
                Action<int> action = i => { var x = i * i; };
                Func<int, bool> predicate = i => i % 2 == 0;
                this.Add(null, predicate, action, SEQUENCE);
                this.Add(source, null, action, PREDICATE);
                this.Add(source, predicate, null, ACTION);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData007))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NullChecksWithPredicate(
                IEnumerable<int> source,
                Func<int, bool> predicate,
                Action<int> action,
                string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.Foreach(predicate, action))
                .ParamName.Is(expect);


        /// <summary>
        /// Test Data for null checking of overload of Foreach() that takes an index and condition as arguments.
        /// </summary>
        class TestData008 : DataGenerator
        {
            public TestData008() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3 };
                Action<int, int> action = (s, i) => { var x = i * s; };
                Func<int, bool> predicate = i => i % 2 == 0;
                this.Add(null, predicate, action, SEQUENCE);
                this.Add(source, null, action, PREDICATE);
                this.Add(source, predicate, null, ACTION);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData008))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Foreach))]
        public void NullChecksWithPredicateAndIndex(
                IEnumerable<int> source,
                Func<int, bool> predicate,
                Action<int, int> action,
                string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.Foreach(predicate, action))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ Do ]

    /// <summary>
    /// Test for EnumerableExtensions.Do (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Do
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<string> source = new string[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                Func<string, string> process = s => $"++{s}++";
                IEnumerable<string> expect1 = new string[] { "++aaa++", "++bbbb++", "++c++", "++dd++", "++eeeee++" };
                Func<string, string> selector = s => $"{s}{s}";
                IEnumerable<string> expect2 = new string[] { "aaaaaa", "bbbbbbbb", "cc", "dddd", "eeeeeeeeee" };
                this.Add(new object[] { source, process, selector, expect1, expect2 });

                source = new string[] { };
                expect1 = new string[] { };
                expect2 = new string[] { };
                this.Add(new object[] { source, process, selector, expect1, expect2 });

                IEnumerable<int> iSource = new int[] { 1, 2, 3 };
                Func<int, int> iProc = i => i * 2;
                IEnumerable<int> iExpect1 = new int[] { 2, 4, 6 };
                Func<int, int> iSelector = i => i * i;
                IEnumerable<int> iExpect2 = new int[] { 1, 4, 9 };
                this.Add(new object[] { iSource, iProc, iSelector, iExpect1, iExpect2 });
            }
        }


        [Theory(DisplayName = "All elements should be proceed.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Do))]
        public void All<T>(
                IEnumerable<T> source,
                Func<T, T> process,
                Func<T, T> selector,
                IEnumerable<T> except1,
                IEnumerable<T> except2)
        {
            var result1 = new List<T>();

            var q = source.Do(x => result1.Add(process(x))).Select(selector);

            // It is not executed yet at this timing.
            result1.Any().IsFalse();

            // Actually executed by calling ToArray().
            var result2 = q.ToArray();

            result1.Is(except1);
            result2.Is(except2);
        }


        /// <summary>
        /// Test Data for overload of Do() that takes condition as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<string> source = new string[] { "aaa", "bbbb", "c", "dd", "eeeee" };
                Func<string, bool> predicate = s => s.Length > 3;
                Func<string, string> process = s => $"++{s}++";
                IEnumerable<string> expect1 = new string[] { "++bbbb++", "++eeeee++" };
                Func<string, string> selector = s => $"{s}{s}";
                IEnumerable<string> expect2 = new string[] { "aaaaaa", "bbbbbbbb", "cc", "dddd", "eeeeeeeeee" };
                this.Add(new object[] { source, predicate, process, selector, expect1, expect2 });

                source = new string[] { };
                expect1 = new string[] { };
                expect2 = new string[] { };
                this.Add(new object[] { source, predicate, process, selector, expect1, expect2 });

                IEnumerable<int> iSource = new int[] { 1, 2, 3 };
                Func<int, bool> iPred = i => i % 2 == 0;
                Func<int, int> iProc = i => i * 2;
                IEnumerable<int> iExpect1 = new int[] { 4, };
                Func<int, int> iSelector = i => i * i;
                IEnumerable<int> iExpect2 = new int[] { 1, 4, 9 };
                this.Add(new object[] { iSource, iPred, iProc, iSelector, iExpect1, iExpect2 });
            }
        }


        [Theory(DisplayName = "Only elements that matches the condition should be proceed.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Do))]
        public void Predicate<T>(
                IEnumerable<T> source,
                Func<T, bool> predicate,
                Func<T, T> process,
                Func<T, T> selector,
                IEnumerable<T> except1,
                IEnumerable<T> except2)
        {
            var result1 = new List<T>();

            var q = source.Do(predicate, x => result1.Add(process(x))).Select(selector);

            // It is not executed yet at this timing.
            result1.Any().IsFalse();

            // Actually executed by calling ToArray().
            var result2 = q.ToArray();

            result1.Is(except1);
            result2.Is(except2);
        }


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                IEnumerable<int> sequence = new int[] { 1, 2, 3 };
                Action<int> action = i => { };

                this.Add(null, action, SEQUENCE);
                this.Add(sequence, null, ACTION);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Do))]
        public void NullCheck(IEnumerable<int> source, Action<int> action, string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.Do(action)).ParamName.Is(expect);


        /// <summary>
        /// Test data for null checking of overload of Do() that takes condition as an argument.
        /// </summary>
        class TestData004 : DataGenerator
        {
            public TestData004() : base()
            {
                IEnumerable<int> sequence = new int[] { 1, 2, 3 };
                Action<int> action = i => { };
                Func<int, bool> predicate = i => true;

                this.Add(null, predicate, action, SEQUENCE);
                this.Add(sequence, null, action, PREDICATE);
                this.Add(sequence, predicate, null, ACTION);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Do))]
        public void NullCheckWithPredicate(
                IEnumerable<int> source,
                Func<int, bool> predicate,
                Action<int> action,
                string expect)
            => Assert.Throws<ArgumentNullException>(
                () => source.Do(predicate, action)).ParamName.Is(expect);
    }

    #endregion

    #region [ ExceptNull ]

    /// <summary>
    /// Test for EnumerableExtensions.ExceptNull (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_ExceptNull
    {
        [Fact(DisplayName = "Null element should be removed.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ExceptNull))]
        public void Normal()
            => new[] { null, "1", null, "2", null, "3", null, }.ExceptNull().ToArray()
                .Is(new[] { "1", "2", "3", });


        [Fact(DisplayName = "Null element of int? should be removed.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ExceptNull))]
        public void NormalNullable()
            => new int?[] { null, 1, null, 2, null, 3, null, }.ExceptNull().ToArray()
                .Is(new int?[] { 1, 2, 3, });


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ExceptNull))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<string>).ExceptNull())
                .ParamName.Is(SEQUENCE);
    }

    #endregion

    #region [ ToDictionarySafely ]

    /// <summary>
    /// Test for EnumerableExtensions.ToDictionarySafely (extension for IEnumerable&lt;T&gt;)
    /// </summary>
    public class Enumerable_ToDictionarySafely
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            KeyValuePair<K, V> Kv<K, V>(K key, V val) => new KeyValuePair<K, V>(key, val);

            public TestData001() : base()
            {
                IEnumerable<(int, string)> source = null;
                IEnumerable<KeyValuePair<int, (int, string)>> expect = null;
                bool? mode = null;

                source = new (int, string)[]{ (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, (int, string)>[] { this.Kv(1, (1, "aaa")), this.Kv(2, (2, "bbbb")), this.Kv(3, (3, "c")), this.Kv(4, (4, "eeeee")), };
                mode = null;
                this.Add(source, expect, mode);

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, (int, string)>[] { this.Kv(1, (1, "aaa")), this.Kv(2, (2, "bbbb")), this.Kv(3, (3, "c")), this.Kv(4, (4, "eeeee")), };
                mode = false;
                this.Add(source, expect, mode);

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, (int, string)>[] { this.Kv(1, (1, "fff")), this.Kv(2, (2, "bbbb")), this.Kv(3, (3, "dd")), this.Kv(4, (4, "eeeee")), };
                mode = true;
                this.Add(source, expect, mode);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, (int, string)>[] { };
                mode = null;
                this.Add(source, expect, mode);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, (int, string)>[] { };
                mode = true;
                this.Add(source, expect, mode);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, (int, string)>[] { };
                mode = false;
                this.Add(source, expect, mode);
            }
        }


        [Theory(DisplayName = "Even if there are duplicate keys, those should be safely dictionarized without valueSelector.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafely))]
        public void Normal(
                IEnumerable<(int, string)> source,
                IEnumerable<KeyValuePair<int, (int, string)>> expect,
                bool? mode)
            => (mode.HasValue
                    ? source.ToDictionarySafely(vt => vt.Item1, mode.Value)
                    : source.ToDictionarySafely(vt => vt.Item1))
                .OrderBy(kv => kv.Key).ToArray()
                .Is(expect.ToArray());


        /// <summary>
        /// Test Data for overload of ToDictionarySafely() that takes the valueSelector as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            KeyValuePair<K, V> Kv<K, V>(K key, V val) => new KeyValuePair<K, V>(key, val);

            public TestData002() : base()
            {
                IEnumerable<(int, string)> source = null;
                IEnumerable<KeyValuePair<int, string>> expect = null;
                bool? mode = null;

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, string>[] { this.Kv(1, "aaa"), this.Kv(2, "bbbb"), this.Kv(3, "c"), this.Kv(4, "eeeee"), };
                mode = null;
                this.Add(source, expect, mode);

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, string>[] { this.Kv(1, "aaa"), this.Kv(2, "bbbb"), this.Kv(3, "c"), this.Kv(4, "eeeee"), };
                mode = false;
                this.Add(source, expect, mode);

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, string>[] { this.Kv(1, "fff"), this.Kv(2, "bbbb"), this.Kv(3, "dd"), this.Kv(4, "eeeee"), };
                mode = true;
                this.Add(source, expect, mode);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, string>[] { };
                mode = null;
                this.Add(source, expect, mode);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, string>[] { };
                mode = false;
                this.Add(source, expect, mode);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, string>[] { };
                mode = true;
                this.Add(source, expect, mode);
            }
        }


        [Theory(DisplayName = "Even if there are duplicate keys, those should be safely dictionarized, and value selector should work.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafely))]
        public void NormalWithValueGenerate(
                IEnumerable<(int, string)> source,
                IEnumerable<KeyValuePair<int, string>> expect,
                bool? mode)
            => (mode.HasValue
                    ? source.ToDictionarySafely(vt => vt.Item1, vt => vt.Item2, mode.Value)
                    : source.ToDictionarySafely(vt => vt.Item1, vt => vt.Item2))
                .OrderBy(kv => kv.Key).ToArray()
                .Is(expect.ToArray());


        /// <summary>
        /// Test data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                IEnumerable<(int x, string y)> src = new (int x, string y)[] { (1, "1"), (2, "2") };
                Func<(int x, string y), int> keySelector = xy => xy.x;

                this.Add(null, keySelector, null, SEQUENCE);
                this.Add(null, keySelector, false, SEQUENCE);
                this.Add(null, keySelector, true, SEQUENCE);

                this.Add(src, null, null, KEY_SELECTOR);
                this.Add(src, null, false, KEY_SELECTOR);
                this.Add(src, null, true, KEY_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafely))]
        public void NullChecks(
                IEnumerable<(int x, string y)> source,
                Func<(int x, string y), int> keySelector,
                bool? mode,
                string expect)
            => Assert.Throws<ArgumentNullException>(() =>
                    {
                        if (mode.HasValue)
                        {
                            source.ToDictionarySafely(keySelector, mode.Value);
                        }
                        else
                        {
                            source.ToDictionarySafely(keySelector);
                        }
                    })
                .ParamName.Is(expect);


        /// <summary>
        /// Test data for null checking of overload of ToDictionarySafely() that takes the valueSelector as an argument.
        /// </summary>
        class TestData004 : DataGenerator
        {
            public TestData004() : base()
            {
                IEnumerable<(int x, string y)> src = new (int x, string y)[] { (1, "1"), (2, "2") };
                Func<(int x, string y), int> keySelector = xy => xy.x;
                Func<(int x, string y), string> ValueSelector = xy => xy.y;
                bool? mode = null;

                this.Add(null, keySelector, ValueSelector, mode, SEQUENCE);
                this.Add(null, keySelector, ValueSelector, false, SEQUENCE);
                this.Add(null, keySelector, ValueSelector, true, SEQUENCE);

                this.Add(src, null, ValueSelector, mode, KEY_SELECTOR);
                this.Add(src, null, ValueSelector, false, KEY_SELECTOR);
                this.Add(src, null, ValueSelector, true, KEY_SELECTOR);

                this.Add(src, keySelector, null, mode, VALUE_SELECTOR);
                this.Add(src, keySelector, null, false, VALUE_SELECTOR);
                this.Add(src, keySelector, null, true, VALUE_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafely))]
        public void NullChecksWithValueSelector(
                IEnumerable<(int x, string y)> source,
                Func<(int x, string y), int> keySelector,
                Func<(int x, string y), string> valueSelector,
                bool? mode,
                string expect)
            => Assert.Throws<ArgumentNullException>(() =>
                    {
                        if (mode.HasValue)
                        {
                            source.ToDictionarySafely(keySelector, valueSelector, mode.Value);
                        }
                        else
                        {
                            source.ToDictionarySafely(keySelector, valueSelector);
                        }
                    })
                .ParamName.Is(expect);
    }

    #endregion

    #region [ ToDictionarySafelyUpdate ]

    /// <summary>
    /// Test for EnumerableExtensions.ToDictionarySafelyUpdate (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_ToDictionarySafelyUpdate
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            KeyValuePair<K, V> Kv<K, V>(K key, V val) => new KeyValuePair<K, V>(key, val);

            public TestData001() : base()
            {
                IEnumerable<(int, string)> source = null;
                IEnumerable<KeyValuePair<int, (int, string)>> expect = null;

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, (int, string)>[] { this.Kv(1, (1, "fff")), this.Kv(2, (2, "bbbb")), this.Kv(3, (3, "dd")), this.Kv(4, (4, "eeeee")), };
                this.Add(source, expect);

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, (int, string)>[] { };
                this.Add(source, expect);
            }
        }


        [Theory(DisplayName = "Even if there are duplicate keys, those should be safely dictionarized without valueSelector, and the value associated the duplicate key should be updated.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafelyUpdate))]
        public void Normal(
                IEnumerable<(int, string)> source, IEnumerable<KeyValuePair<int, (int, string)>> expect)
            => source.ToDictionarySafelyUpdate(vt => vt.Item1).OrderBy(kv => kv.Key).ToArray()
                .Is(expect.ToArray());


        /// <summary>
        /// Test Data for overload of ToDictionarySafelyUpdate() that takes the valueSelector as an argument.   
        /// </summary>
        class TestData002 : DataGenerator
        {
            KeyValuePair<K, V> Kv<K, V>(K key, V val) => new KeyValuePair<K, V>(key, val);

            public TestData002() : base()
            {
                IEnumerable<(int, string)> source = null;
                IEnumerable<KeyValuePair<int, string>> expect = null;

                source = new (int, string)[] { (1, "aaa"), (2, "bbbb"), (3, "c"), (3, "dd"), (4, "eeeee"), (1, "fff") };
                expect = new KeyValuePair<int, string>[] { this.Kv(1, "fff"), this.Kv(2, "bbbb"), this.Kv(3, "dd"), this.Kv(4, "eeeee"), };
                this.Add(new object[] { source, expect, });

                source = new (int, string)[] { };
                expect = new KeyValuePair<int, string>[] { };
                this.Add(source, expect);
            }
        }


        [Theory(DisplayName = "Even if there are duplicate keys, those should be safely dictionarized using valueSelector, and the value associated the duplicate key should be updated.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafelyUpdate))]
        public void NormalWithValueGenerate(
                IEnumerable<(int, string)> source, IEnumerable<KeyValuePair<int, string>> expect)
            => source.ToDictionarySafelyUpdate(vt => vt.Item1, vt => vt.Item2)
                .OrderBy(kv => kv.Key).ToArray()
                .Is(expect.ToArray());


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                IEnumerable<(int, string)> source = new (int, string)[] { };
                Func<(int, string), int> keySelector = kv => kv.Item1;

                this.Add(null, keySelector, SEQUENCE);
                this.Add(source, null, KEY_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if sequence or keySelector is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafelyUpdate))]
        public void NullChecks(
                IEnumerable<(int, string)> source, Func<(int, string), int> keySelector, string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.ToDictionarySafelyUpdate(keySelector))
                .ParamName.Is(expect);


        /// <summary>
        /// Test data for null checking of overload of ToDictionarySafelyUpdate() that takes the valueSelector as an argument.
        /// </summary>
        class TestData004 : DataGenerator
        {
            public TestData004() : base()
            {
                IEnumerable<(int, string)> source = new (int, string)[] { };
                Func<(int, string), int> keySelector = kv => kv.Item1;
                Func<(int, string), string> valueSelector = kv => kv.Item2;

                this.Add(null, keySelector, valueSelector, SEQUENCE);
                this.Add(source, null, valueSelector, KEY_SELECTOR);
                this.Add(source, keySelector, null, VALUE_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToDictionarySafelyUpdate))]
        public void NullChecksWithValueSelector(
                IEnumerable<(int, string)> source,
                Func<(int, string), int> keySelector,
                Func<(int, string), string> valueSelector,
                string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.ToDictionarySafelyUpdate(keySelector, valueSelector))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ Buffer ]

    /// <summary>
    /// Test for EnumerableExtensions.Buffer (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Buffer
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, }; ;
                IEnumerable<IEnumerable<int>> expect = null;
                int bufferCount = 0;

                expect = new int[][]
                {
                    new int[] { 1, },
                    new int[] { 2, },
                    new int[] { 3, },
                    new int[] { 4, },
                    new int[] { 5, },
                    new int[] { 6, },
                    new int[] { 7, },
                    new int[] { 8, },
                    new int[] { 9, },
                    new int[] { 10, },
                };
                bufferCount = -1;
                this.Add(source, bufferCount, expect);

                expect = new int[][]
                {
                    new int[] { 1, },
                    new int[] { 2, },
                    new int[] { 3, },
                    new int[] { 4, },
                    new int[] { 5, },
                    new int[] { 6, },
                    new int[] { 7, },
                    new int[] { 8, },
                    new int[] { 9, },
                    new int[] { 10, },
                };
                bufferCount = 0;
                this.Add(source, bufferCount, expect);

                expect = new int[][]
                {
                    new int[] { 1, },
                    new int[] { 2, },
                    new int[] { 3, },
                    new int[] { 4, },
                    new int[] { 5, },
                    new int[] { 6, },
                    new int[] { 7, },
                    new int[] { 8, },
                    new int[] { 9, },
                    new int[] { 10, },
                };
                bufferCount = 1;
                this.Add(source, bufferCount, expect);

                expect = new int[][]
                {
                    new int[] { 1, 2 },
                    new int[] { 3, 4 },
                    new int[] { 5, 6 },
                    new int[] { 7, 8 },
                    new int[] { 9, 10 },
                };
                bufferCount = 2;
                this.Add(source, bufferCount, expect);

                expect = new int[][]
                {
                    new int[] { 1, 2, 3, },
                    new int[] { 4 ,5, 6, },
                    new int[] { 7, 8, 9, },
                    new int[] { 10, },
                };
                bufferCount = 3;
                this.Add(source, bufferCount, expect);

                source = new int[] { };
                expect = new int[][] { };
                bufferCount = 3;
                this.Add(source, bufferCount, expect);
            }
        }


        [Theory(DisplayName = "Elements should be buffered each of times that specified in argument.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Buffer))]
        public void Normal(IEnumerable<int> source, int bufferCount, IEnumerable<IEnumerable<int>> expect)
            => source.Buffer(bufferCount).ToArray().Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Buffer))]
        public void NullSequence()
        => Assert.Throws<ArgumentNullException>(
                () => (null as IEnumerable<int>).Buffer(3))
            .ParamName.Is(SEQUENCE);
    }

    #endregion

    #region [ BufferWhere ]

    /// <summary>
    /// Test for EnumerableExtensions.BufferWhere (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_BufferWhere
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, }; ;
                IEnumerable<IEnumerable<int>> expect = null;
                Func<int, bool> predicate = null;

                expect = new int[][]
                {
                    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, },
                };
                predicate = i => true;
                this.Add(source, predicate, expect);

                expect = new int[][] { };
                predicate = i => false;
                this.Add(source, predicate, expect);

                expect = new int[][]
                {
                    new int[] { 2, },
                    new int[] { 4, },
                    new int[] { 6, },
                    new int[] { 8, },
                    new int[] { 10, },
                };
                predicate = i => i % 2 == 0;
                this.Add(source, predicate, expect);


                source = new int[] { 1, 2, 5, 8, 10, 2, 3, 4, 1, 5, 7, 4, 6, 4 };
                expect = new int[][]
                {
                    new int[] { 1, 2, },
                    new int[] { 2, 3, 4, 1, },
                    new int[] { 4, },
                    new int[] { 4, },
                };
                predicate = i => i < 5;
                this.Add(source, predicate, expect);


                source = new int[] { };
                expect = new int[][] { };
                this.Add(source, predicate, expect);
            }
        }


        [Theory(DisplayName = "Elements should be buffered for each matching the condition.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferWhere))]
        public void Normal(
                IEnumerable<int> source, Func<int, bool> predicate, IEnumerable<IEnumerable<int>> expect)
            => source.BufferWhere(predicate).ToArray().Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferWhere))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).BufferWhere(i => i % 2 == 0))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if predicate is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferWhere))]
        public void NullPredicate()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3, 4 }.BufferWhere(null as Func<int, bool>))
                .ParamName.Is(PREDICATE);
    }

    #endregion

    #region [ BufferWhile ]

    /// <summary>
    /// Test for EnumerableExtensions.BufferWhile (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_BufferWhile
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                Func<(int x, int y, int z), (int x, int y, int z), bool> predicate = null;
                IEnumerable<IEnumerable<(int x, int y, int z)>> expect = null;
                IEnumerable<(int x, int y, int z)> source =
                    new (int x, int y, int z)[]
                    {
                        (1,1,1),
                        (1,1,2),
                        (1,1,3),
                        (1,2,4),
                        (1,2,5),
                        (1,2,6),
                        (1,3,1),
                        (1,3,2),
                        (1,3,3),
                        (2,3,3),
                        (2,3,3),
                        (3,1,1),
                        (3,2,1),
                    };

                predicate = (p, c) => p.x == c.x && p.y == c.y && p.z + 1 == c.z;
                expect = new (int x, int y, int z)[][]
                {
                    new (int x, int y, int z)[]{ (1, 1, 1), (1, 1, 2), (1, 1, 3), },
                    new (int x, int y, int z)[]{ (1, 2, 4), (1, 2, 5), (1, 2, 6), },
                    new (int x, int y, int z)[]{ (1, 3, 1), (1, 3, 2), (1, 3, 3), },
                    new (int x, int y, int z)[]{ (2, 3, 3), },
                    new (int x, int y, int z)[]{ (2, 3, 3), },
                    new (int x, int y, int z)[]{ (3, 1, 1), },
                    new (int x, int y, int z)[]{ (3, 2, 1), },
                };
                this.Add(source, predicate, expect);

                predicate = (p, c) => true;
                expect = new (int x, int y, int z)[][]
                {
                    new(int x, int y, int z)[]
                    {
                        (1,1,1),
                        (1,1,2),
                        (1,1,3),
                        (1,2,4),
                        (1,2,5),
                        (1,2,6),
                        (1,3,1),
                        (1,3,2),
                        (1,3,3),
                        (2,3,3),
                        (2,3,3),
                        (3,1,1),
                        (3,2,1),
                    },
                };
                this.Add(source, predicate, expect);

                predicate = (p, c) => false;
                expect = new (int x, int y, int z)[][]
                {

                    new (int x, int y, int z)[]{ (1, 1, 1), },
                    new (int x, int y, int z)[]{ (1, 1, 2), },
                    new (int x, int y, int z)[]{ (1, 1, 3), },
                    new (int x, int y, int z)[]{ (1, 2, 4), },
                    new (int x, int y, int z)[]{ (1, 2, 5), },
                    new (int x, int y, int z)[]{ (1, 2, 6), },
                    new (int x, int y, int z)[]{ (1, 3, 1), },
                    new (int x, int y, int z)[]{ (1, 3, 2), },
                    new (int x, int y, int z)[]{ (1, 3, 3), },
                    new (int x, int y, int z)[]{ (2, 3, 3), },
                    new (int x, int y, int z)[]{ (2, 3, 3), },
                    new (int x, int y, int z)[]{ (3, 1, 1), },
                    new (int x, int y, int z)[]{ (3, 2, 1), },
                };
                this.Add(source, predicate, expect);

                source = new (int x, int y, int z)[] { };
                expect = new (int x, int y, int z)[][] { };
                this.Add(source, predicate, expect);
            }
        }


        [Theory(DisplayName = "Elements should be buffered while the conditions are satisfied by comparing the elements before and after.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferWhile))]
        public void Normal(
                IEnumerable<(int x, int y, int z)> source,
                Func<(int x, int y, int z), (int x, int y, int z), bool> predicate,
                IEnumerable<IEnumerable<(int x, int y, int z)>> expect)
            => source.BufferWhile(predicate).ToArray().Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferWhile))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).BufferWhile((p, c) => p == c))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if predicate is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferWhile))]
        public void NullPredicate()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3, 4 }.BufferWhile(null as Func<int, int, bool>))
                .ParamName.Is(PREDICATE);
    }

    #endregion

    #region [ BufferAlternative ]

    /// <summary>
    /// Test for EnumerableExtensions.BufferAlternative (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_BufferAlternative
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, }; ;
                IEnumerable<IEnumerable<int>> expect = null;
                Func<int, bool> predicate = null;

                expect = new int[][]
                {
                    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, },
                };
                predicate = i => true;
                this.Add(source, predicate, expect);

                predicate = i => false;
                this.Add(source, predicate, expect);

                expect = new int[][]
                {
                    new int[] { 1, },
                    new int[] { 2, },
                    new int[] { 3, },
                    new int[] { 4, },
                    new int[] { 5, },
                    new int[] { 6, },
                    new int[] { 7, },
                    new int[] { 8, },
                    new int[] { 9, },
                    new int[] { 10, },
                };
                predicate = i => i % 2 == 0;
                this.Add(source, predicate, expect);


                source = new int[] { 1, 2, 5, 8, 10, 2, 3, 4, 1, 5, 7, 4, 6, 4 };
                expect = new int[][]
                {
                    new int[] { 1, 2, },
                    new int[] { 5, 8, 10, },
                    new int[] { 2, 3, 4, 1, },
                    new int[] { 5, 7, },
                    new int[] { 4, },
                    new int[] { 6, },
                    new int[] { 4, },
                };
                predicate = i => i < 5;
                this.Add(source, predicate, expect);


                source = new int[] { };
                expect = new int[][] { };
                this.Add(source, predicate, expect);
            }
        }


        [Theory(DisplayName = "Elements should be buffered alternately between elements that satisfy the condition and those that do not.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferAlternative))]
        public void Normal(
                IEnumerable<int> source, Func<int, bool> predicate, IEnumerable<IEnumerable<int>> expect)
            => source.BufferAlternative(predicate).ToArray().Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferAlternative))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).BufferAlternative(i => i % 0 == 0))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if predicate is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BufferAlternative))]
        public void NullPredicate()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3, 4 }.BufferAlternative(null as Func<int, bool>))
                .ParamName.Is(PREDICATE);
    }

    #endregion

    #region [ Concat ]

    /// <summary>
    /// Test for EnumerableExtensions.Concat (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Concat
    {
        [Fact(DisplayName = "Elements given with variable length arguments should be joined behind the first sequence.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void Refs()
            => new[] { "1", "2", "3" }.Concat("A", "B", "C").ToArray()
                .Is(new[] { "1", "2", "3", "A", "B", "C" });


        [Fact(DisplayName = "Elements given by variable length arguments containing null element should be joined behind the first sequence as is.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void RefsWithNull()
            => new[] { "1", "2", "3" }.Concat(null, "B", "C").ToArray()
                .Is(new[] { "1", "2", "3", null, "B", "C" });


        [Fact(DisplayName = "If there is no argument, an empty sequence should be joined behind the first sequence.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void RefsAllNull()
            => new[] { "1", "2", "3" }.Concat().ToArray()
                .Is(new[] { "1", "2", "3" });


        [Fact(DisplayName = "Elements of struct type given with variable length arguments should be joined behind the first sequence.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void Vals()
            => new[] { 1, 2, 3, 4, 5 }.Concat(9, 8, 7).ToArray()
                .Is(new[] { 1, 2, 3, 4, 5, 9, 8, 7 });


        [Fact(DisplayName = "If there is no argument of struct type, an empty sequence should be joined behind the first sequence.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void ValsParamsNull()
            => new[] { 1, 2, 3, 4, 5 }.Concat().ToArray()
                .Is(new[] { 1, 2, 3, 4, 5 });


        [Fact(DisplayName = "Elements of nullable struct type given with variable length arguments should be joined behind the first sequence.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void NullableVals()
            => new int?[] { 1, null, 3, 4, 5 }.Concat(null, 8, 7).ToArray()
                .Is(new int?[] { 1, null, 3, 4, 5, null, 8, 7 });


        [Fact(DisplayName = "If there is no argument of nullable struct type, an empty sequence should be joined behind the first sequence.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void NullableParamsNull()
            => new int?[] { 1, null, 3, 4, 5 }.Concat().ToArray()
                .Is(new int?[] { 1, null, 3, 4, 5 });


        [Fact(DisplayName = "ArgumentNullException should be thrown if first sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Concat))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).Concat(1, 2, 3))
                .ParamName.Is(SEQUENCE);
    }

    #endregion

    #region [ Maxes ]

    /// <summary>
    /// Test for EnumerableExtensions.Maxes (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Maxes
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<int> source = null;
                IEnumerable<int> expect = null;

                source = new int[] { 1, 4, 5, 6, 3, 2, 9, 3, 1, 3, };
                expect = new int[] { 9, };
                this.Add(source, expect);

                source = new int[] { 1, 4, 5, 6, 3, 9, 2, 9, 3, 1, 3, 9 };
                expect = new int[] { 9, 9, 9, };
                this.Add(source, expect);

                source = new int[] { };
                expect = new int[] { };
                this.Add(source, expect);
            }
        }


        [Theory(DisplayName = "All of max values in sequence should be returned.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Maxes))]
        public void Normal(IEnumerable<int> source, IEnumerable<int> expect)
            => source.Maxes().Is(expect);


        /// <summary>
        /// Test Data for overload of Maxes() that takes the selector as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<(int x, string y)> source = null;
                IEnumerable<int> expect = null;

                source = new (int x, string y)[] { (99, "99"), (1, "1"), (2, "2"), (9, "nine"), (4, "yonn"), (7, "seven"), (1, "one"), (99, "nintynine"), (5, "5"), (99, "きゅーじゅーきゅー"), (8, "はち"), };
                expect = new int[] { 99, 99, 99, };
                this.Add(source, expect);

                source = new (int x, string y)[] { (1, "1"), (2, "2"), (9, "nine"), (4, "yonn"), (7, "seven"), (1, "one"), (5, "5"), (99, "きゅーじゅーきゅー"), (8, "はち"), };
                expect = new int[] { 99 };
                this.Add(source, expect);

                source = new (int x, string y)[] { };
                expect = new int[] { };
                this.Add(source, expect);
            }
        }


        [Theory(DisplayName = "All of max values that converted by selector in sequence should be returned.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Maxes))]
        public void NormalWithKeySelector(IEnumerable<(int x, string y)> source, IEnumerable<int> expect)
            => source.Maxes(x => x.x).Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Maxes))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).Maxes())
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if selector is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Maxes))]
        public void NullSelector()
             => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }.Maxes(null as Func<int, int>))
                .ParamName.Is(SELECTOR);
    }

    #endregion

    #region [ Mins ]

    /// <summary>
    /// Test for EnumerableExtensions.Mins (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Mins
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<int> source = null;
                IEnumerable<int> expect = null;

                source = new int[] { 1, 4, 5, 6, 3, 2, 9, 3, 3, };
                expect = new int[] { 1, };
                this.Add(source, expect);

                source = new int[] { 1, 4, 5, 6, 3, 9, 2, 9, 3, 1, 3, 9 };
                expect = new int[] { 1, 1, };
                this.Add(source, expect);

                source = new int[] { };
                expect = new int[] { };
                this.Add(source, expect);
            }
        }


        [Theory(DisplayName = "All of minimum values in sequence should be returned.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Mins))]
        public void Normal(IEnumerable<int> source, IEnumerable<int> expect)
            => source.Mins().Is(expect);


        /// <summary>
        /// Test Data for overload of Mins() that takes the selector as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<(int x, string y)> source = null;
                IEnumerable<int> expect = null;

                source = new (int x, string y)[] { (99, "99"), (1, "1"), (2, "2"), (9, "nine"), (4, "yonn"), (7, "seven"), (1, "one"), (99, "nintynine"), (5, "5"), (99, "きゅーじゅーきゅー"), (8, "はち"), };
                expect = new int[] { 1, 1 };
                this.Add(source, expect);

                source = new (int x, string y)[] { (2, "2"), (9, "nine"), (4, "yonn"), (7, "seven"), (1, "one"), (5, "5"), (99, "きゅーじゅーきゅー"), (8, "はち"), };
                expect = new int[] { 1 };
                this.Add(source, expect);

                source = new (int x, string y)[] { };
                expect = new int[] { };
                this.Add(source, expect);
            }
        }


        [Theory(DisplayName = "All of minimum values that converted by selector in sequence should be returned.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Mins))]
        public void NormalWithKeySelector(IEnumerable<(int x, string y)> source, IEnumerable<int> expect)
            => source.Mins(x => x.x).Is(expect);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Mins))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).Mins())
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if selector is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Mins))]
        public void NullSelector()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }.Mins(null as Func<int, int>))
                .ParamName.Is(SELECTOR);
    }

    #endregion

    #region [ FullZip ]

    /// <summary>
    /// Test for EnumerableExtensions.FullZip (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_FullZip
    {
        /// <summary>
        /// Test Data.     
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001()
            {
                IEnumerable<int> source = null;
                IEnumerable<string> second = null;
                IEnumerable<(int, string)> expect = null;

                source = new int[] { 1, 4, 5, 6, 3, 2, };
                second = new string[] { "one", "four", "five", };
                expect = new (int, string)[] { (1, "one"), (4, "four"), (5, "five"), (6, null), (3, null), (2, null) };
                this.Add(source, second, expect);

                source = new int[] { 1, 4, 5, };
                second = new string[] { "one", "four", "five", };
                expect = new (int, string)[] { (1, "one"), (4, "four"), (5, "five"), };
                this.Add(source, second, expect);

                source = new int[] { 1, 4, 5, };
                second = new string[] { "one", "four", "five", "six", "seven", "eight" };
                expect = new (int, string)[] { (1, "one"), (4, "four"), (5, "five"), (default(int), "six"), (default(int), "seven"), (default(int), "eight") };
                this.Add(source, second, expect);

                source = new int[] { 1, 4, 5, };
                second = new string[] { };
                expect = new (int, string)[] { (1, null), (4, null), (5, null), };
                this.Add(source, second, expect);

                source = new int[] { };
                second = new string[] { "one", "four", "five", };
                expect = new (int, string)[] { (default(int), "one"), (default(int), "four"), (default(int), "five"), };
                this.Add(source, second, expect);

                source = new int[] { };
                second = new string[] { };
                expect = new (int, string)[] { };
                this.Add(source, second, expect);
            }
        }


        [Theory(DisplayName = "Elements retrieved from two sequences are combined, and should be supplemented by element default values if either is short.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.FullZip))]
        public void Normal(IEnumerable<int> source, IEnumerable<string> second, IEnumerable<(int, string)> expect)
            => source.FullZip(second, (i, s) => (i, s)).ToArray().Is(expect);


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<int> source = new int[] { 1, 2, 3 };
                IEnumerable<int> second = new int[] { 1, 2, 3 };
                Func<int, int, int> resutlSelector = (x, y) => x + y;

                this.Add(null, second, resutlSelector, SEQUENCE);
                this.Add(source, null, resutlSelector, SECOND_SEQUENCE);
                this.Add(source, second, null, RESULT_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.FullZip))]
        public void NullChecks(IEnumerable<int> source, IEnumerable<int> second, Func<int, int, int> resutlSelector, string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => source.FullZip(second, resutlSelector))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ ConcatWith ]

    /// <summary>
    /// Test for EnumerableExtensions.ConcatWith (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_ConcatWith
    {
        [Fact(DisplayName = "Elements should be converted to a comma-separated string if it is not specified delimiter.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void Normal()
            => new int[] { 1, 4, 5, 6, 3, 2, 9, 3, 1, 3, }.ConcatWith().Is("1,4,5,6,3,2,9,3,1,3");


        /// <summary>
        /// Test Data for overload of ConcatWith() that takes delimiter as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<int> source = null;
                string expect = null;
                string separator = null;

                source = new int[] { 1, 4, 5, 6, 3, 2, 9, 3, 1, 3, };
                expect = "1456329313";
                separator = null;
                this.Add(source, separator, expect);

                separator = string.Empty;
                this.Add(source, separator, expect);

                separator = ",";
                expect = "1,4,5,6,3,2,9,3,1,3";
                this.Add(source, separator, expect);

                separator = "(^^)/";
                expect = "1(^^)/4(^^)/5(^^)/6(^^)/3(^^)/2(^^)/9(^^)/3(^^)/1(^^)/3";
                this.Add(source, separator, expect);
            }
        }


        [Theory(DisplayName = "Elements should be converted to a string that is delimited by string specified in arguments.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NormalSpecifiedSeparator<T>(IEnumerable<T> source, string separator, string expect)
            => source.ConcatWith(separator).Is(expect);


        [Fact(DisplayName = "Elements should be converted to a comma-separated string that is formatted by format-provider.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NormalWithFormat()
        {
            var source = new int[] { 1, 4, 5, 6, 3, 2, 9, 3, 1, 3, };
            var yen = 1.ToString("C", new CultureInfo("ja-JP")).Replace("1", string.Empty);
            var expect = $"{yen}1,{yen}4,{yen}5,{yen}6,{yen}3,{yen}2,{yen}9,{yen}3,{yen}1,{yen}3";

            source.ConcatWith("C0", provider: new CultureInfo("ja-JP")).Is(expect);
        }


        [Fact(DisplayName = "Elements should be converted to a string that is formatted by format-provider and delimited by string specified in argument.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NormalSpecifiedSeparatorWithFormat()
        {
            var source = new int[] { 1, 4, 5, 6, 3, 2, 9, 3, 1, 3, };
            var yen = 1.ToString("C", new CultureInfo("ja-JP")).Replace("1", string.Empty);
            var expect = $"{yen}1(^_^){yen}4(^_^){yen}5(^_^){yen}6(^_^){yen}3(^_^){yen}2(^_^){yen}9(^_^){yen}3(^_^){yen}1(^_^){yen}3";

            source.ConcatWith("C0", "(^_^)", new CultureInfo("ja-JP")).Is(expect);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ConcatWith())
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null. (with format-provider)")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NullSequenceWithFormat()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ConcatWith("C", provider: new CultureInfo("ja-JP")))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null. (with delimiter)")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NullSequenceSpecifiedSeparator()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ConcatWith("##"))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null. (with format-provider and delimiter)")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ConcatWith))]
        public void NullSequenceSpecifiedSeparatorWithFormat()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ConcatWith("C", "//", provider: new CultureInfo("ja-JP")))
                .ParamName.Is(SEQUENCE);
    }

    #endregion

    #region [ ToSet ]

    /// <summary>
    /// Test for EnumerableExtensions.ToSet (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_ToSet
    {
        /// <summary>
        /// Test Data.     
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<int> src = new int[] { 1, 3, 4, 5, 2, 3, 4, 1, 2, 3, 7, 8, 9, 3, 4 };
                ISet<int> exp = new HashSet<int> { 1, 3, 4, 5, 2, 3, 4, 1, 2, 3, 7, 8, 9, 3, 4 };
                this.Add(src, exp);

                IEnumerable<string> ssrc = new string[] { "aaa", "b", "ccc", "ccc", null, "def", "aaa", "chd", };
                ISet<string> sexp = new HashSet<string> { "aaa", "b", "ccc", "ccc", null, "def", "aaa", "chd", };
                this.Add(src, exp);
            }
        }


        [Theory(DisplayName = "Elements should be converted to Set.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToSet))]
        public void Normal<T>(IEnumerable<T> source, ISet<T> expect)
            => source.ToSet().OrderBy(i => i).Is(expect.OrderBy(i => i));


        [Fact(DisplayName = "Elements of nullable int type should be converted to Set.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToSet))]
        public void NormalNullable()
            => new int?[] { 1, 3, 4, 5, 2, null, 4, 1, null, 3, 7, 8, 9, 3, 4 }.ToSet().OrderBy(i => i)
                .Is(new HashSet<int?> { 1, 3, 4, 5, 2, null, 4, 1, null, 3, 7, 8, 9, 3, 4 }.OrderBy(i => i));


        /// <summary>
        /// Test Data for overload of ToSet() that takes condition as an argument.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                IEnumerable<int> src = new int[] { 1, 3, 4, 5, 2, 3, 4, 1, 2, 3, 7, 8, 9, 3, 4 };
                ISet<int> exp = new HashSet<int> { 1, 3, 4, 2, 3, 4, 1, 2, 3, 3, 4 };
                Func<int, bool> predicate = i => i < 5;
                this.Add(src, predicate, exp);

                IEnumerable<string> ssrc = new string[] { "aaa", "b", "ccc", "sccc", null, "def", "aa", "chd", };
                ISet<string> sexp = new HashSet<string> { "sccc", "aa", };
                Func<string, bool> spred = s => s?.Length % 2 == 0;
                this.Add(ssrc, spred, sexp);
            }
        }


        [Theory(DisplayName = "Elements that matches the condition should be converted to Set.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToSet))]
        public void NormalPredicate<T>(IEnumerable<T> source, Func<T, bool> predicate, ISet<T> expect)
            => source.ToSet(predicate).OrderBy(i => i).Is(expect.OrderBy(i => i));


        [Fact(DisplayName = "Elements of nullable int type that matches the condition should be converted to Set.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToSet))]
        public void NormalNullableWithPredicate()
            => new int?[] { 1, 3, 4, 5, 2, null, 4, 1, null, 3, 7, 8, 9, 3, 4 }
                .ToSet(i => i.HasValue && i.Value % 2 == 0).OrderBy(i => i)
                .Is(new HashSet<int?> { 4, 2, 4, 8, 4 }.OrderBy(i => i));


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ToSet())
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null. (with predicate)")]
        public void NullSequencePredicate()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ToSet(x => x % 2 == 0))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if predicate is null.")]
        public void NullPredicate()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }.ToSet(null as Func<int, bool>))
                .ParamName.Is(PREDICATE);


        /// <summary> 
        /// Test Data for overload of ToSet() that takes comparer as an argument.
        /// </summary>
        class TestData004 : DataGenerator
        {
            public TestData004() : base()
            {
                var source = new (int x, string y)[] { (1, "fizz"), (1, "buzz"), (2, "buzz"), (2, "fizzbuzz"), (3, "buzz"), (4, "bus"), (4, "gus"), (5, "bus"), };
                var comparer = new DelegateEqualityComparer<(int x, string y)>((a, b) => a.y == b.y, x => x.y.GetHashCode());
                var expect = new (int x, string y)[] { (1, "fizz"), (1, "buzz"), (2, "fizzbuzz"), (4, "bus"), (4, "gus"), };
                this.Add(source, comparer, null, new HashSet<(int x, string y)>(expect));

                Func<(int x, string y), bool> predicate = a => a.x % 2 == 0;
                expect = new (int x, string y)[] { (2, "buzz"), (2, "fizzbuzz"), (4, "bus"), (4, "gus"), };
                this.Add(source, comparer, predicate, new HashSet<(int x, string y)>(expect));

                comparer = new DelegateEqualityComparer<(int x, string y)>((a, b) => a.x == b.x, x => x.x.GetHashCode());
                expect = new (int x, string y)[] { (1, "fizz"), (2, "buzz"), (3, "buzz"), (4, "bus"), (5, "bus"), };
                this.Add(source, comparer, null, new HashSet<(int x, string y)>(expect));

                expect = new (int x, string y)[] { (2, "buzz"), (4, "bus"), };
                this.Add(source, comparer, predicate, new HashSet<(int x, string y)>(expect));
            }
        }


        [Theory(DisplayName = "008.正常系 [comparerあり]")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.ToSet))]
        public void WithComparer(
                IEnumerable<(int x, string y)> source,
                IEqualityComparer<(int x, string y)> comparer,
                Func<(int x, string y), bool> predicate,
                ISet<(int x, string y)> expect)
            => (predicate != null
                    ? source.ToSet(comparer, predicate)
                    : source.ToSet(comparer)).OrderBy(a => a.x).ToArray()
                .Is(expect.OrderBy(a => a.x).ToArray());


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null. (with comparer)")]
        public void NullSequenceWithComparer()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).ToSet(new DelegateEqualityComparer<int>()))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if comparer is null.")]
        public void NullComparer()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }.ToSet(null as IEqualityComparer<int>))
                .ParamName.Is(COMPARER);


        [Fact(DisplayName = "ArgumentNullException should be thrown if predicate is null. (with comparer)")]
        public void NullPredicateWithComparer()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }.ToSet(new DelegateEqualityComparer<int>(), null))
                .ParamName.Is(PREDICATE);
    }

    #endregion

    #region [ LeftJoin ]

    /// <summary>
    /// Test for EnumerableExtensions.LeftJoin (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_LeftJoin
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<Kv<int, string>> outer = new Kv<int, string>[]
                {
                    Kv.Create(0, "zero"),
                    Kv.Create(1,"one"),
                    Kv.Create(2,"two"),
                    Kv.Create(3,"three"),
                    Kv.Create(4,"four"),
                };

                IEnumerable<Kv<int, string>> inner = new Kv<int, string>[]
                {
                    Kv.Create(0, "ZERO0"),
                    Kv.Create(0, "ZERO1"),
                    Kv.Create(1, "ONE0"),
                    Kv.Create(2, "TWO0"),
                    Kv.Create(2, "TWO1"),
                    Kv.Create(2, "TWO2"),
                    Kv.Create(4, "FOUR"),
                    Kv.Create(5, "FIVE"),
                };

                IEnumerable<string> expect = new string[]
                {
                    "zero_ZERO0",
                    "zero_ZERO1",
                    "one_ONE0",
                    "two_TWO0",
                    "two_TWO1",
                    "two_TWO2",
                    "three_",
                    "four_FOUR",
                };

                this.Add(outer, inner, expect);

                outer = new Kv<int, string>[] { };
                inner = new Kv<int, string>[] { };
                expect = new string[] { };

                this.Add(outer, inner, expect);
            }
        }


        [Theory(DisplayName = "The two sequences should be outer joined relative to first sequence.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LeftJoin))]
        public void Normal(IEnumerable<Kv<int, string>> outer, IEnumerable<Kv<int, string>> inner, IEnumerable<string> expect)
            => outer.LeftJoin(inner, kv => kv.Key, kv => kv.Key, (o, i) => $"{o.Value}_{i?.Value}").ToArray()
                .Is(expect.ToArray());


        /// <summary> 
        /// Test Data for overload of LeftJoin() that takes the comparer as an argument.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                var comparer = new DelegateEqualityComparer<int>();
                comparer.DelegateOfEquals = (x, y) => x == 2 ? false : x == y;
                comparer.DelegateOfGetHashCode = x => x.GetHashCode();


                IEnumerable<Kv<int, string>> outer = new Kv<int, string>[]
                {
                    Kv.Create(0, "zero"),
                    Kv.Create(1,"one"),
                    Kv.Create(2,"two"),
                    Kv.Create(3,"three"),
                    Kv.Create(4,"four"),
                };

                IEnumerable<Kv<int, string>> inner = new Kv<int, string>[]
                {
                    Kv.Create(0, "ZERO0"),
                    Kv.Create(0, "ZERO1"),
                    Kv.Create(1, "ONE0"),
                    Kv.Create(2, "TWO0"),
                    Kv.Create(2, "TWO1"),
                    Kv.Create(2, "TWO2"),
                    Kv.Create(4, "FOUR"),
                    Kv.Create(5, "FIVE"),
                };

                IEnumerable<string> expect = new string[]
                {
                    "zero_ZERO0",
                    "zero_ZERO1",
                    "one_ONE0",
                    "two_",
                    "three_",
                    "four_FOUR",
                };

                this.Add(outer, inner, expect, comparer);

                outer = new Kv<int, string>[] { };
                inner = new Kv<int, string>[] { };
                expect = new string[] { };

                this.Add(outer, inner, expect, comparer);
            }
        }


        [Theory(DisplayName = "The two sequences should be outer joined relative to first sequence according to the comparer.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LeftJoin))]
        public void NormalComparer(
                IEnumerable<Kv<int, string>> outer,
                IEnumerable<Kv<int, string>> inner,
                IEnumerable<string> expect,
                IEqualityComparer<int> comparer)
            => outer.LeftJoin(inner, kv => kv.Key, kv => kv.Key, (o, i) => $"{o.Value}_{i?.Value}", comparer).ToArray()
                .Is(expect.ToArray());


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                IEnumerable<int> outer = new int[] { 1, 2, 3 };
                IEnumerable<int> inner = new int[] { 1, 2, 3 };
                Func<int, int> outerKeySelector = i => i;
                Func<int, int> innerKeySelector = i => i;
                Func<int, int, string> resultSelector = (o, i) => $"{o}{i}";
                DelegateEqualityComparer<int> comparer = new DelegateEqualityComparer<int>();
                comparer.DelegateOfEquals = (x, y) => x == 2 ? false : x == y;
                comparer.DelegateOfGetHashCode = x => x.GetHashCode();

                // pattern of null outer 
                this.Add(null, inner, outerKeySelector, innerKeySelector, resultSelector, null, OUTER);
                this.Add(null, inner, outerKeySelector, innerKeySelector, resultSelector, comparer, OUTER);

                // pattern of null inner
                this.Add(outer, null, outerKeySelector, innerKeySelector, resultSelector, null, INNER);
                this.Add(outer, null, outerKeySelector, innerKeySelector, resultSelector, comparer, INNER);

                // pattern of null outerKeySelector
                this.Add(outer, inner, null, innerKeySelector, resultSelector, null, OUTER_KEY_SELECTOR);
                this.Add(outer, inner, null, innerKeySelector, resultSelector, comparer, OUTER_KEY_SELECTOR);

                // pattern of null innerKeySelector
                this.Add(outer, inner, outerKeySelector, null, resultSelector, null, INNER_KEY_SELECTOR);
                this.Add(outer, inner, outerKeySelector, null, resultSelector, comparer, INNER_KEY_SELECTOR);

                // pattern of null resultSelector
                this.Add(outer, inner, outerKeySelector, innerKeySelector, null, null, RESULT_SELECTOR);
                this.Add(outer, inner, outerKeySelector, innerKeySelector, null, comparer, RESULT_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LeftJoin))]
        public void NullChecks(
                IEnumerable<int> outer,
                IEnumerable<int> inner,
                Func<int, int> outerKeySelector,
                Func<int, int> innerKeySelector,
                Func<int, int, string> resultSelector,
                IEqualityComparer<int> comparer,
                string except)
            => Assert.Throws<ArgumentNullException>(() =>
                {
                    if (comparer == null)
                    {
                        var r = outer.LeftJoin(inner, outerKeySelector, innerKeySelector, resultSelector);
                    }
                    else
                    {
                        var r = outer.LeftJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
                    }
                }).ParamName.Is(except);


        [Fact(DisplayName = "ArgumentNullException should be thrown if comparer is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LeftJoin))]
        public void NullComparer()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }
                            .LeftJoin(new int[] { 1, 2, 3 }, o => o, i => i, (o, i) => string.Empty, null))
                        .ParamName.Is(COMPARER);
    }

    #endregion

    #region [ LooseJoin ]

    /// <summary>
    /// Test for EnumerableExtensions.LooseJoin (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_LooseJoin
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<Kv<int, string>> outer = null;
                IEnumerable<int> inner = null;
                Func<Kv<int, string>, int, bool> predicate = null;
                Func<Kv<int, string>, int, string> resultSelector = null;
                IEnumerable<string> expect = null;

                outer = new Kv<int, string>[]
                {
                    Kv.Create(0,"zero"),
                    Kv.Create(1,"one"),
                    Kv.Create(2,"two"),
                    Kv.Create(3,"three"),
                    Kv.Create(4,"four"),
                };

                inner = new int[] { 2, 4, 5, };

                predicate = (kv, i) =>
                    kv.Key == 0
                        ? false : i % kv.Key == 0;

                resultSelector = (kv, i) => $"{i}_{kv.Value}";

                expect = new string[] {
                    "2_one", "2_two",
                    "4_one", "4_two","4_four",
                    "5_one",
                };

                this.Add(outer, inner, predicate, resultSelector, expect);

                var nullouter = new Kv<int, string>[] { };
                var nullExpect = new string[] { };

                this.Add(nullouter, inner, predicate, resultSelector, nullExpect);

                var nullinner = new int[] { };

                this.Add(outer, nullinner, predicate, resultSelector, nullExpect);

                this.Add(nullouter, nullinner, predicate, resultSelector, nullExpect);
            }
        }


        [Theory(DisplayName = "The two sequences should be joined according to the condition.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LooseJoin))]
        public void Normal(
                IEnumerable<Kv<int, string>> outer,
                IEnumerable<int> inner,
                Func<Kv<int, string>, int, bool> predicate,
                Func<Kv<int, string>, int, string> resultSelector,
                IEnumerable<string> expect)
            => outer.LooseJoin(inner, predicate, resultSelector).ToArray().OrderBy(s => s)
                .Is(expect.OrderBy(s => s));


        /// <summary>
        /// TestData for null checking.
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<int> outer = new int[] { 1, 2, 3 };
                IEnumerable<int> inner = new int[] { 1, 2, 3 };
                Func<int, int, bool> predicate = (x, y) => x == y;
                Func<int, int, int> resultSelector = (x, y) => x + y;

                this.Add(null, inner, predicate, resultSelector, OUTER);
                this.Add(outer, null, predicate, resultSelector, INNER);
                this.Add(outer, inner, null, resultSelector, PREDICATE);
                this.Add(outer, inner, predicate, null, RESULT_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LooseJoin))]
        public void NullCheckes(
                IEnumerable<int> outer,
                IEnumerable<int> inner,
                Func<int, int, bool> predicate,
                Func<int, int, int> resultSelector,
                string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => outer.LooseJoin(inner, predicate, resultSelector))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ LooseLeftJoin ]

    /// <summary>
    /// Test for EnumerableExtensions.LooseLeftJoin (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_LooseLeftJoin
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                IEnumerable<Kv<int, string>> outer = null;
                IEnumerable<int> inner = null;
                Func<Kv<int, string>, int, bool> predicate = null;
                Func<Kv<int, string>, int, string> resultSelector = null;
                IEnumerable<string> expect = null;

                outer = new Kv<int, string>[]
                {
                    Kv.Create(0,"zero"),
                    Kv.Create(1,"one"),
                    Kv.Create(2,"two"),
                    Kv.Create(3,"three"),
                    Kv.Create(4,"four"),
                };

                inner = new int[] { 2, 4, 5, };

                predicate = (kv, i) =>
                    kv.Key == 0
                        ? false : i % kv.Key == 0;

                resultSelector = (kv, i) => $"{i}_{kv.Value}";

                expect = new string[] {
                    "0_zero","0_three",
                    "2_one", "2_two",
                    "4_one", "4_two","4_four",
                    "5_one",
                };

                this.Add(outer, inner, predicate, resultSelector, expect);

                var nullouter = new Kv<int, string>[] { };
                var nullExpect = new string[] { };

                this.Add(nullouter, inner, predicate, resultSelector, nullExpect);

                var nullinner = new int[] { };
                var leftExcept = new string[] { "0_zero", "0_one", "0_two", "0_three", "0_four" };
                this.Add(outer, nullinner, predicate, resultSelector, leftExcept);

                this.Add(nullouter, nullinner, predicate, resultSelector, nullExpect);
            }
        }


        [Theory(DisplayName = "The two sequences should be outer joined according to the condition.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LooseLeftJoin))]
        public void Normal(
                IEnumerable<Kv<int, string>> outer,
                IEnumerable<int> inner,
                Func<Kv<int, string>, int, bool> predicate,
                Func<Kv<int, string>, int, string> resultSelector,
                IEnumerable<string> expect)
            => outer.LooseLeftJoin(inner, predicate, resultSelector).ToArray().OrderBy(s => s)
                .Is(expect.OrderBy(s => s));


        /// <summary>
        /// Test Data for null checking. 
        /// </summary>
        class TestData002 : DataGenerator
        {
            public TestData002() : base()
            {
                IEnumerable<int> outer = new int[] { 1, 2, 3 };
                IEnumerable<int> inner = new int[] { 1, 2, 3 };
                Func<int, int, bool> predicate = (x, y) => x == y;
                Func<int, int, int> resultSelector = (x, y) => x + y;

                this.Add(null, inner, predicate, resultSelector, OUTER);
                this.Add(outer, null, predicate, resultSelector, INNER);
                this.Add(outer, inner, null, resultSelector, PREDICATE);
                this.Add(outer, inner, predicate, null, RESULT_SELECTOR);
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameters is null.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.LooseLeftJoin))]
        public void NullCheckes(
                IEnumerable<int> outer,
                IEnumerable<int> inner,
                Func<int, int, bool> predicate,
                Func<int, int, int> resultSelector,
                string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => outer.LooseLeftJoin(inner, predicate, resultSelector))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ BestEffortCount ]

    /// <summary>
    /// Test for EnumerableExtensions.BestEffortCount (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_BestEffortCount
    {
        /// <summary>
        /// Enumerator that has no property of elements count.
        /// </summary>
        public class TestCollection : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                yield return 1;
                yield return 2;
                yield return 3;
                yield return 4;
                yield return 5;
                yield return 6;
                yield return 7;
                yield break;
            }
            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }

        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                // Array
                this.Add(new int[] { 1, 2, 3, 4 }, 4);

                // List
                this.Add(new List<double> { 1.0, 2.0 }, 2);

                // ReadOnlyCollection
                this.Add(new ReadOnlyCollection<string>(new List<string> { "a", "b", "c" }), 3);

                // Enumerator that has no property of element count.
                this.Add(new TestCollection(), 7);
            }
        }


        [Theory(DisplayName = "Elements count should be returned regardless of acutual implementation of IEnumerable<T>.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BestEffortCount))]
        public void Normal<T>(IEnumerable<T> source, int expect)
            => source.BestEffortCount().Is(expect);


        [Fact(DisplayName = "Elements count should be returned even if the implementation of IEnumerable <T> is a dictionary.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BestEffortCount))]
        public void DictionaryCount()
            => new Dictionary<int, string> { { 1, "1" }, { 2, "2" } }.BestEffortCount().Is(2);


        [Fact(DisplayName = "Elements count should be returned even if the implementation of IEnumerable <T> is a readonly-dictionary.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BestEffortCount))]
        public void ReadOnlyDictionaryCount()
            => new ReadOnlyDictionary<int, string>(
                    new Dictionary<int, string> { { 1, "1" }, { 2, "2" } })
                .BestEffortCount().Is(2);


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.BestEffortCount))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).BestEffortCount())
                .ParamName.Is(SEQUENCE);
    }

    #endregion

    #region [ Distinct ]

    /// <summary>
    /// Test for EnumerableExtensions.Distinct (extension for IEnumerable&lt;T&gt;).
    /// </summary>
    public class Enumerable_Distinct
    {
        /// <summary>
        /// Test Data.
        /// </summary>
        class TestData001 : DataGenerator
        {
            public TestData001() : base()
            {
                var intAry = new[] { 1, 2, 2, 3, 5, 7, 2, 1, 3, 8, 9, };
                Func<int, int> intSelector = i => i;
                var expect = new[] { 1, 2, 3, 5, 7, 8, 9, };
                this.Add(intAry, intSelector, expect);

                var strAry = new[] { "1", "2", "2", "3", "5", "7", "2", "1", "3", "8", "9", };
                Func<string, string> strSelector = i => i;
                var strExpect = new[] { "1", "2", "3", "5", "7", "8", "9", };
                this.Add(strAry, strSelector, strExpect);
            }
        }


        [Theory(DisplayName = "Duplicate elements should be omitted.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Distinct))]
        public void Normal<T>(IEnumerable<T> source, Func<T, T> selector, IEnumerable<T> expect)
            => source.Distinct(selector).ToArray().Is(expect);


        [Fact(DisplayName = "Duplicate elements that selected by selector should be omitted. (first property of valueTuple selected)")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Distinct))]
        public void SelectorItemInt()
        {
            IEnumerable<(int, string)> source = new (int, string)[]
            {
                (1,"a"),(2,"a"),(3,"a"),(2,"c"),(4,"a"),(2,"b"),(2,"c"),(1,"d")
            };

            IEnumerable<(int, string)> expect = new (int, string)[]
            {
                (1,"a"),(2,"a"),(3,"a"),(4, "a")
            };

            source.Distinct(v => v.Item1).ToArray().Is(expect);
        }


        [Fact(DisplayName = "Duplicate elements that selected by selector should be omitted. (second property of valueTuple selected)")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Distinct))]
        public void SelectorItemString()
        {
            IEnumerable<(int, string)> source = new (int, string)[]
            {
                (1,"a"),(2,"a"),(3,"a"),(2,"c"),(4,"a"),(2,"b"),(2,"c"),(1,"d")
            };

            IEnumerable<(int, string)> expect = new (int, string)[]
            {
                (1,"a"),(2,"c"),(2,"b"),(1,"d")
            };

            source.Distinct(v => v.Item2).ToArray().Is(expect);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if sequence is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Distinct))]
        public void NullSequence()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IEnumerable<int>).Distinct(i => i))
                .ParamName.Is(SEQUENCE);


        [Fact(DisplayName = "ArgumentNullException should be thrown if selector is null.")]
        [Trait(nameof(EnumerableExtensions), nameof(EnumerableExtensions.Distinct))]
        public void NullSelector()
            => Assert.Throws<ArgumentNullException>(
                    () => new int[] { 1, 2, 3 }.Distinct(null as Func<int, int>))
                .ParamName.Is(SELECTOR);
    }

    #endregion
}