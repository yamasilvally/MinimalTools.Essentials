/*
 * Test for Kv
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using MinimalTools.Utilities;
using Xunit;
using static MinimalTools.Test.Utilities.TestResource;

namespace MinimalTools.Test.Utilities
{
    /// <summary>
    /// Test resorces.
    /// </summary>
    public static class TestResource
    {
        public static Kv<string, int> KvSrc = Kv.Create("this is test", 100);
        public static KeyValuePair<double, decimal?> KvpSrc { get; } = new KeyValuePair<double, decimal?>(3.14, null);
        public static Tuple<int, string> TpSrc { get; } = Tuple.Create(1024, "2^10");
        public static (string, string) VtpSrc { get; } = ("this is ", "test resouces");
    }


    /// <summary>
    /// Test for Kv.Create.
    /// </summary>
    public class Kv_Factory
    {
        [Fact(DisplayName = "Parameters should set correctly to properties.")]
        [Trait(nameof(Kv), nameof(Kv.Create))]
        public void GetWithSpecifiedParameters()
        {
            var kv = Kv.Create("test", 66);
            kv.Key.Is("test");
            kv.Value.Is(66);
        }


        [Fact(DisplayName = "An instance of Kv should be created fron KeyValuePair.")]
        [Trait(nameof(Kv), nameof(Kv.Create))]
        public void GetWithKeyValuePair()
        {
            var kv = Kv.Create(KvpSrc);
            kv.Key.Is(KvpSrc.Key);
            kv.Value.Is(KvpSrc.Value);
        }


        [Fact(DisplayName = "An instance of Kv should be created from Tuple.")]
        [Trait(nameof(Kv), nameof(Kv.Create))]
        public void GetWithTuple()
        {
            var kv = Kv.Create(TpSrc);
            kv.Key.Is(TpSrc.Item1);
            kv.Value.Is(TpSrc.Item2);
        }


        [Fact(DisplayName = "An instance of Kv should be created from ValueTuple.")]
        [Trait(nameof(Kv), nameof(Kv.Create))]
        public void GetWithValueTuple()
        {
            var kv = Kv.Create(VtpSrc);

            kv.Key.Is(VtpSrc.Item1);
            kv.Value.Is(VtpSrc.Item2);
        }


        [Fact(DisplayName = "An instance of Kv should be created from ValueTuple that has Named Properties.")]
        [Trait(nameof(Kv), nameof(Kv.Create))]
        public void GetWithValueTupleWithNamedProperty()
        {
            (int k, string v) vtp = (100, "test");
            var kv = Kv.Create(vtp);

            kv.Key.Is(vtp.k);
            kv.Value.Is(vtp.v);
        }
    }


    /// <summary>
    /// Test for Kv&lt;K, V&gt;.Deconstruct.
    /// </summary>
    public class Kv_Deconstruct
    {
        [Fact(DisplayName = "The Deconstruct method should work.")]
        [Trait(nameof(Kv), nameof(Kv<string, int>.Deconstruct))]
        public void Deconstruct()
        {
            var (key, value) = KvSrc;
            key.Is(KvSrc.Key);
            value.Is(KvSrc.Value);
        }
    }


    /// <summary>
    /// Test for  Kv&lt;K, V&gt;.ToKeyValuePair
    /// </summary>
    public class Kv_ToKeyValuePair
    {
        [Fact(DisplayName = "It should return a new instance of KeyValurPair.")]
        [Trait(nameof(Kv), nameof(Kv<string, int>.ToKeyValuePair))]
        public void Get()
        {
            var kvp = KvSrc.ToKeyValuePair();
            kvp.IsInstanceOf<KeyValuePair<string, int>>();
            kvp.Key.Is(KvSrc.Key);
            kvp.Value.Is(KvSrc.Value);
        }
    }


    /// <summary>
    /// Test for  Kv&lt;K, V&gt;.ToTuple
    /// </summary>
    public class Kv_ToTuple
    {
        [Fact(DisplayName = "It should return a new instance of Tuple.")]
        [Trait(nameof(Kv), nameof(Kv<string, int>.ToTuple))]
        public void Get()
        {
            var tp = KvSrc.ToTuple();
            tp.IsInstanceOf<Tuple<string, int>>();
            tp.Item1.Is(KvSrc.Key);
            tp.Item2.Is(KvSrc.Value);
        }
    }


    /// <summary>
    /// Test for  Kv&lt;K, V&gt;.ToValueTuple
    /// </summary>
    public class Kv_ToValueTuple
    {
        [Fact(DisplayName = "It should return a new instance of ValueTuple.")]
        [Trait(nameof(Kv), nameof(Kv<string, int>.ToValueTuple))]
        public void Get()
        {
            var vtp = KvSrc.ToValueTuple();
            vtp.IsInstanceOf<(string, int)>();
            vtp.Item1.Is(KvSrc.Key);
            vtp.Item2.Is(KvSrc.Value);
        }
    }


    /// <summary>
    /// Test for ToKv.
    /// </summary>
    public class Kv_ToKvExtension
    {
        [Fact(DisplayName = "An instance of Kv should be created from KeyValuePair.")]
        [Trait(nameof(Kv), nameof(Kv.ToKv))]
        public void WithKeyValuePair()
        {
            var kv = KvpSrc.ToKv();
            kv.IsInstanceOf<Kv<double, decimal?>>();
            kv.Key.Is(KvpSrc.Key);
            kv.Value.Is(KvpSrc.Value);
        }


        [Fact(DisplayName = "An instance of Kv should be created from Tuple.")]
        [Trait(nameof(Kv), nameof(Kv.ToKv))]
        public void WithTuple()
        {
            var kv = TpSrc.ToKv();
            kv.IsInstanceOf<Kv<int, string>>();
            kv.Key.Is(TpSrc.Item1);
            kv.Value.Is(TpSrc.Item2);
        }


        [Fact(DisplayName = "An instance of Kv should be created from ValueTuple.")]
        [Trait(nameof(Kv), nameof(Kv.ToKv))]
        public void WithValueTuple()
        {
            var kv = VtpSrc.ToKv();
            kv.IsInstanceOf<Kv<string, string>>();
            kv.Key.Is(VtpSrc.Item1);
            kv.Value.Is(VtpSrc.Item2);
        }
    }


    /// <summary>
    /// Test for Kv&lt;K, V&gt;.Clone.
    /// </summary>
    public class Kv_Clone
    {
        [Fact(DisplayName = "The Clone() should return a instance that has same content, but reference is not equal.")]
        public void CloneShouldWork()
        {
            var kv = KvSrc.Clone();
            kv.IsNotSameReferenceAs(KvSrc);
            kv.IsStructuralEqual(KvSrc);
        }
    }
}