/*
 * Test for CollectionsExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;
using MinimalTools.Extensions.Collections;
using Xunit;

namespace MinimalTools.Test.Extensions.Collections
{
    #region [ IDictionary GetOrDefault ]

    /// <summary>
    /// Test for CollectionsExtensions.GetOrDefault (extension for IDictionary&gt;TKey, TValue&lt;).
    /// </summary>
    public class IDictionary_GetOrDefault
    {
        const string s1 = "testCode1";
        const string s2 = "testCode2";
        IDictionary<int, string> dict = new Dictionary<int, string> { { 1, s1 }, { 2, s2 } };

        [Theory(DisplayName = "The value should be returned if dictionary contains key.")]
        [InlineData(1, s1)]
        [InlineData(2, s2)]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrDefault))]
        public void ExistKey(int key, string expect)
            => dict.GetOrDefault(key).Is(expect);


        [Fact(DisplayName = "Null as default of type of string should be returned if dictionary dose not contains key.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrDefault))]
        public void NotExistKey_Reference()
            => dict.GetOrDefault(3).IsNull();


        [Fact(DisplayName = "Zero as default of type of int should be returned if dictionary dose not contains key.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrDefault))]
        public void NotExistKey_Value()
            => (new Dictionary<int, int> { { 1, 1 }, { 2, 2 } } as IDictionary<int, int>)
                .GetOrDefault(3).Is(default(int));


        [Fact(DisplayName = "Null as default of type of int? should be returned if dictionary dose not contains key.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrDefault))]
        public void NotExistKey_NullableValue()
            => (new Dictionary<string, int?> { { 1.ToString(), null }, { 2.ToString(), 0 } } as IDictionary<string, int?>)
                .GetOrDefault(3.ToString()).IsNull();


        [Fact(DisplayName = "Default value should be returned when dictionary is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrDefault))]
        public void NullDictionary()
            => (null as IDictionary<int, int?>).GetOrDefault(3).IsNull();


        [Fact(DisplayName = "Default value should be returned when key is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrDefault))]
        public void NullKey()
            => (new Dictionary<string, int?> { { "1", null }, { "2", 2 } } as IDictionary<string, int?>)
                .GetOrDefault(null as string).IsNull();
    }

    #endregion

    #region [ IDictionary GetOrElse ]

    /// <summary>
    /// Test for CollectionsExtensions.GetOrElse (extension for IDictionary&gt;TKey, TValue&lt;).
    /// </summary>
    public class IDictionary_GetOrElse
    {
        const string s1 = "testCode1";
        const string s2 = "testCode2";
        const string defStr = "default";
        IDictionary<int, string> dict = new Dictionary<int, string> { { 1, s1 }, { 2, s2 } };

        [Theory(DisplayName = "The value that contains dictionary should be returned.")]
        [InlineData(1, s1)]
        [InlineData(2, s2)]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrElse))]
        public void ExistKey(int key, string expect)
            => dict.GetOrElse(key, defStr).Is(expect);


        [Fact(DisplayName = "The alternative value that specified in arguments should be returned.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrElse))]
        public void NotExistKey_Reference()
            => dict.GetOrElse(3, defStr).Is(defStr);


        [Fact(DisplayName = "An alternative value should be returned if dictionary is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrElse))]
        public void NullDictionary()
            => (null as IDictionary<int, int>).GetOrElse(3, int.MinValue).Is(int.MinValue);


        [Fact(DisplayName = "An alternative value should be returned if key is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrElse))]
        public void NullKey()
            => (new Dictionary<string, int?> { { "1", null }, { "2", 2 } } as IDictionary<string, int?>)
                .GetOrElse(null as string, int.MinValue).Is(int.MinValue);
    }

    #endregion

    #region [ IDictionary GetOrAdd ]

    /// <summary>
    /// Test for CollectionsExtensions.GetOrAdd (extension for IDictionary&gt;TKey, TValue&lt;).
    /// </summary>
    public class IDictionary_GetOrAdd
    {
        const string val1 = "Value1";
        const string val2 = "Value1";
        const string defStr = "defalut";

        [Theory(DisplayName = "The Key and value should be added to dictionary if dictionary dose not contain the key.")]
        [InlineData(1, val1, 2, false)]
        [InlineData(2, val2, 2, false)]
        [InlineData(3, defStr, 3, true)]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrAdd))]
        public void ExistKey_Referece(int key, string expect, int count, bool contain)
        {
            var dict = new Dictionary<int, string> { { 1, val1 }, { 2, val2 } };
            var result = dict.GetOrAdd(key, defStr);
            result.Is(expect);
            dict.Count.Is(count);

            dict.Values.Contains(defStr).Is(contain);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if dictionary is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrAdd))]
        public void NullDictionary()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IDictionary<string, string>).GetOrAdd("x", "y"))
                .ParamName.Is("dict");


        [Fact(DisplayName = "ArgumentNullException should be thrown if key is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.GetOrAdd))]
        public void NullKey()
            => Assert.Throws<ArgumentNullException>(
                    () => (new Dictionary<string, string> { { "a", "b" } } as IDictionary<string, string>).GetOrAdd(null, "y"))
                .ParamName.Is("key");
    }

    #endregion

    #region [ IDictionary AddOrUpdate ]

    /// <summary>
    /// Test for CollectionsExtensions.GetOrElse (extension for IDictionary&gt;TKey, TValue&lt;).
    /// </summary>
    public class IDictionary_AddOrUpdate
    {
        IDictionary<string, string> TestDIct { get; } = new Dictionary<string, string>();

        [Fact(DisplayName = "The key and value should be added to dictionary if dictionary dose not contain the key.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AddOrUpdate))]
        public void NotExistKey()
        {
            var key = "k";
            var val = "this is test";
            this.TestDIct.AddOrUpdate(key, val);

            this.TestDIct.ContainsKey(key).IsTrue();
            this.TestDIct[key].Is(val);
        }


        [Fact(DisplayName = "The value should be updated if dictionary already contains the key.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AddOrUpdate))]
        public void ExistKey()
        {
            var key = "k";
            var val = "this is test";
            this.TestDIct.Add(key, val);

            var newVal = "this is test new value";
            this.TestDIct.AddOrUpdate(key, newVal);

            this.TestDIct[key].Is(newVal);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if dictionary is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AddOrUpdate))]
        public void NullDictionary()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IDictionary<string, string>).AddOrUpdate("x", "y"))
                .ParamName.Is("dict");


        [Fact(DisplayName = "ArgumentNullException should be thrown if the key is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AddOrUpdate))]
        public void NullKey()
            => Assert.Throws<ArgumentNullException>(
                    () => (new Dictionary<string, string>() as IDictionary<string, string>).AddOrUpdate(null, "y"))
                .ParamName.Is("key");
    }

    #endregion

    #region [ IList RemoveAll ]
    /// <summary>
    /// Test for CollectionsExtensions.RemoveAll (extension form IList&gt;T&lt;).
    /// </summary>
    public class IList_RemoveAll
    {
        class TestData001 : DataGenerator
        {
            public TestData001()
            {
                // remove all even.
                IEnumerable<int> data1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                Func<int, bool> predicate = x => x % 2 == 0;
                IEnumerable<int> expect = new int[] { 1, 3, 5, 7, 9, };
                this.Add(new object[] { data1, predicate, 5, expect });

                // remove all element.
                data1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                predicate = x => x < 10;
                expect = new int[] { };
                this.Add(new object[] { data1, predicate, 10, expect });

                // remove no element.
                data1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                predicate = x => x >= 10;
                expect = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                this.Add(new object[] { data1, predicate, 0, expect });
            }
        }


        [Theory(DisplayName = "Any elements that matches the condition should be removed from list.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.RemoveAll))]
        public void Remove(IEnumerable<int> source, Func<int, bool> predicate, int countOfRemoved, IEnumerable<int> expect)
        {
            IList<int> list = new List<int>(source);
            var x = list.RemoveAll(predicate);
            x.Is(countOfRemoved);
            expect.Is(list);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if list is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.RemoveAll))]
        public void NullList()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IList<string>).RemoveAll(x => true))
                .ParamName.Is("list");


        [Fact(DisplayName = "ArgumentNullException should be thrown if predicate is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.RemoveAll))]
        public void NullPredicate()
            => Assert.Throws<ArgumentNullException>(
                    () => (new List<string>() as IList<string>).RemoveAll(null as Func<string, bool>))
                .ParamName.Is("predicate");
    }

    #endregion

    #region [ IList AppendLast ]

    /// <summary>
    /// Test for CollectionsExtensions.AppendLast (extension for IList&gt;T&lt;).</summary>
    public class IList_AppendLast
    {
        class TestData001 : DataGenerator
        {
            public TestData001()
            {
                // case-1: Add another list to the empty list.
                IEnumerable<int> data1 = new int[] { };
                IEnumerable<int> data2 = new int[] { 0, 1, 2, 3, 4, 5 };
                IEnumerable<int> expect = new int[] { 0, 1, 2, 3, 4, 5 };
                this.Add(new object[] { data1, data2, expect, expect.Count() });

                // case-2: Add another list to the list with element.
                data1 = new int[] { 1, 2, 3 };
                data2 = new int[] { 0, 1, 2, 3, 4, 5 };
                expect = new int[] { 1, 2, 3, 0, 1, 2, 3, 4, 5 };
                this.Add(new object[] { data1, data2, expect, expect.Count() });

                // case-3: Add empty list to the empty list.
                data1 = new int[] { };
                data2 = new int[] { };
                expect = new int[] { };
                this.Add(new object[] { data1, data2, expect, expect.Count() });

                // case-4: Add empty list to the list with element.
                data1 = new int[] { 1, 2, 3 };
                data2 = new int[] { };
                expect = new int[] { 1, 2, 3, };
                this.Add(new object[] { data1, data2, expect, expect.Count() });
            }
        }
        

        [Theory(DisplayName = "The list should be able to append other list.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AppendLast))]
        public void AppendLast(IEnumerable<int> source, IEnumerable<int> appends, IEnumerable<int> expect, int expectCount)
        {
            IList<int> list = new List<int>(source);
            list.AppendLast(appends);
            list.Count.Is(expectCount);
            list.Is(expect);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if list is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AppendLast))]
        public void NullList()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IList<int>).AppendLast(new[] { 1, 2, 3, 4 }))
                .ParamName.Is("list");


        [Fact(DisplayName = "ArgumentNullException should be thrown if secondary collection is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.AppendLast))]
        public void NullCollection()
            => Assert.Throws<ArgumentNullException>(
                    () => (new List<string>() as IList<string>).AppendLast(null))
                .ParamName.Is("collection");
    }

    #endregion

    #region [ IList InsertForward ]

    /// <summary>
    /// Test for CollectionsExtensions.InsertForward (extension for IList&lt;T&gt;).
    /// </summary>
    public class IList_InsertForward
    {
        class TestData001 : DataGenerator
        {
            public TestData001()
            {
                // case-1: Add another list to the empty list.
                IEnumerable<int> data1 = new int[] { };
                IEnumerable<int> data2 = new int[] { 0, 1, 2, 3, 4, 5 };
                IEnumerable<int> expect = new int[] { 0, 1, 2, 3, 4, 5 };
                this.Add(new object[] { data1, data2, expect, expect.Count() });

                // case-2: Add another list to the list with element.
                data1 = new int[] { 1, 2, 3 };
                data2 = new int[] { 0, 1, 2, 3, 4, 5 };
                expect = new int[] { 0, 1, 2, 3, 4, 5, 1, 2, 3, };
                this.Add(new object[] { data1, data2, expect, expect.Count() });

                // case-3: Add empty list to the empty list.
                data1 = new int[] { };
                data2 = new int[] { };
                expect = new int[] { };
                this.Add(new object[] { data1, data2, expect, expect.Count() });

                // case-4: Add empty list to the list with element.
                data1 = new int[] { 1, 2, 3 };
                data2 = new int[] { };
                expect = new int[] { 1, 2, 3, };
                this.Add(new object[] { data1, data2, expect, expect.Count() });
            }
        }


        [Theory(DisplayName = "The list should be able to insert other list.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.InsertForward))]
        public void InsertForward(IEnumerable<int> source, IEnumerable<int> appends, IEnumerable<int> expect, int count)
        {
            IList<int> list = new List<int>(source);
            list.InsertForward(appends);
            list.Count.Is(count);
            list.Is(expect);
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if list is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.InsertForward))]
        public void NullList()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IList<int>).InsertForward(new[] { 1, 2, 3, 4 }))
                .ParamName.Is("list");


        [Fact(DisplayName = "ArgumentNullException should be thrown if secondary collection is null.")]
        [Trait(nameof(CollectionsExtensions), nameof(CollectionsExtensions.InsertForward))]
        public void NullCollection()
            => Assert.Throws<ArgumentNullException>(
                    () => (new List<string>() as IList<string>).InsertForward(null))
                .ParamName.Is("collection");
    }

    #endregion
}