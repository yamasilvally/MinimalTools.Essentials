/*
 * Test for ValueConvertExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using MinimalTools.Extensions.Convert;
using Xunit;

namespace MinimalTools.Test.Extensions.Convert
{
    #region [ DateTime format ]

    /// <summary>
    /// Test for DateTime extensions of ValueConvertExtensions.
    /// </summary>
    public class ValueConvertExtensions_ToXXX
    {
        DateTime Now = DateTime.Now;

        string Ymd;
        string DelimitedYmd;
        string YmdHms;
        string DelimitedYmdHms;
        string Timestp;
        string DelimitedTimestp;


        /// <summary>
        /// Set up.
        /// </summary>
        public ValueConvertExtensions_ToXXX()
        {
            this.Ymd = $"{Now.Year:D4}{Now.Month:D2}{Now.Day:D2}";
            this.YmdHms = $"{Now.Year:D4}{Now.Month:D2}{Now.Day:D2}{Now.Hour:D2}{Now.Minute:D2}{Now.Second:D2}";
            this.Timestp = $"{Now.Year:D4}{Now.Month:D2}{Now.Day:D2}{Now.Hour:D2}{Now.Minute:D2}{Now.Second:D2}{Now.Millisecond:D3}";

            this.DelimitedYmd = $"{Now.Year:D4}/{Now.Month:D2}/{Now.Day:D2}";
            this.DelimitedYmdHms = $"{Now.Year:D4}/{Now.Month:D2}/{Now.Day:D2} {Now.Hour:D2}:{Now.Minute:D2}:{Now.Second:D2}";
            this.DelimitedTimestp = $"{Now.Year:D4}/{Now.Month:D2}/{Now.Day:D2} {Now.Hour:D2}:{Now.Minute:D2}:{Now.Second:D2}.{Now.Millisecond:D3}";
        }

        #region [ YMD ]

        [Fact(DisplayName = "DateTime should be formatted to [yyyyMMdd].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToYmd))]
        public void DateTimeToYmd() => this.Now.ToYmd().Is(this.Ymd);


        [Fact(DisplayName = "DateTime? should be formatted to [yyyyMMdd].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToYmd))]
        public void NullableDateTimeToYmd() => (new DateTime?(this.Now)).ToYmd().Is(this.Ymd);


        [Fact(DisplayName = "DateTime? without value should be string.Empty.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToYmd))]
        public void NullToYmd() => (null as DateTime?).ToYmd().Is(string.Empty);


        [Fact(DisplayName = "DateTime should be formatted to [yyyy/MM/dd].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedYmd))]
        public void DateTimeToYmdWithSlash() => this.Now.ToDelimitedYmd().Is(this.DelimitedYmd);


        [Fact(DisplayName = "DateTime? should be formatted to [yyyy/MM/dd].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedYmd))]
        public void NullableDateTimeToYmdWithSlash() => (new DateTime?(this.Now)).ToDelimitedYmd().Is(this.DelimitedYmd);


        [Fact(DisplayName = "DateTime? without value should be string.Empty.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedYmd))]
        public void NullToYmdWithSlash() => (null as DateTime?).ToDelimitedYmd().Is(string.Empty);

        #endregion

        #region [ YMDHMS ]

        [Fact(DisplayName = "DateTime should be formatted to [yyyyMMddHHmmss].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToYmdHms))]
        public void DateTimeToYmdHms() => this.Now.ToYmdHms().Is(this.YmdHms);


        [Fact(DisplayName = "DateTime? should be formatted to [yyyyMMddHHmmss].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToYmdHms))]
        public void NullableDateTimeToYmdHms() => (new DateTime?(this.Now)).ToYmdHms().Is(this.YmdHms);


        [Fact(DisplayName = "DateTime? without value should be string.Empty.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToYmdHms))]
        public void NullToYmdHms() => (null as DateTime?).ToYmdHms().Is(string.Empty);


        [Fact(DisplayName = "DateTime should be formatted to [yyyy/MM/dd HH:mm:ss].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedYmdHms))]
        public void DateTimeToYmdHmsWithSlash() => this.Now.ToDelimitedYmdHms().Is(this.DelimitedYmdHms);


        [Fact(DisplayName = "DateTime? should be formatted to [yyyy/MM/dd HH:mm:ss].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedYmdHms))]
        public void NullableDateTimeToYmdHmsWithSlash() => (new DateTime?(this.Now)).ToDelimitedYmdHms().Is(this.DelimitedYmdHms);


        [Fact(DisplayName = "DateTime? without value should be string.Empty.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedYmdHms))]
        public void NullToYmdHmsWithSlash() => (null as DateTime?).ToDelimitedYmdHms().Is(string.Empty);

        #endregion

        #region [ TIMESTAMP ]

        [Fact(DisplayName = "DateTime should be formatted to [yyyyMMddHHmmssfff].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToTimestamp))]
        public void DateTimeToTmstmp() => this.Now.ToTimestamp().Is(this.Timestp);


        [Fact(DisplayName = "DateTime? should be formatted to [yyyyMMddHHmmssfff].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToTimestamp))]
        public void NullableDateTimeToTmstamp() => (new DateTime?(this.Now)).ToTimestamp().Is(this.Timestp);


        [Fact(DisplayName = "DateTime? without value should be string.Empty.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToTimestamp))]
        public void NullToTmstamp() => (null as DateTime?).ToYmdHms().Is(string.Empty);


        [Fact(DisplayName = "DateTime should be formatted to [yyyy/MM/dd HH:mm:ss.fff].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedTimestamp))]
        public void DateTimeToTmstampWithSlash() => this.Now.ToDelimitedTimestamp().Is(this.DelimitedTimestp);


        [Fact(DisplayName = "DateTime? should be formatted to [yyyy/MM/dd HH:mm:ss.fff].")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedTimestamp))]
        public void NullableDateTimeToTmstampWithSlash() => (new DateTime?(this.Now)).ToDelimitedTimestamp().Is(this.DelimitedTimestp);


        [Fact(DisplayName = "DateTime? without value should be string.Empty.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToDelimitedTimestamp))]
        public void NullToTmstampsWithSlash() => (null as DateTime?).ToDelimitedTimestamp().Is(string.Empty);

        #endregion
    }

    #endregion

    #region [ ToHexadecimalString ]

    /// <summary>
    /// Test for ValueConvertExtensions.ToHexadecimalString.
    /// </summary>
    public class ValueConvertExtensions_ToHexadecimalString
    {
        [Fact(DisplayName = "A sbyte value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Sbyte()
        {
            sbyte x = 10;
            var replace = "XXX";
            var expect = x.ToString("X2");

            x.ToHexadecimalString().Is(expect);

            sbyte? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "A byte value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Byte()
        {
            byte x = 128;
            var replace = "XXX";
            var expect = x.ToString("X2");

            x.ToHexadecimalString().Is(expect);

            byte? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "A short value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Short()
        {
            short x = -128;
            var replace = "XXX";
            var expect = x.ToString("X4");

            x.ToHexadecimalString().Is(expect);

            short? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "An ushort value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Ushort()
        {
            ushort x = 511;
            var replace = "XXX";
            var expect = x.ToString("X4");

            x.ToHexadecimalString().Is(expect);

            ushort? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "An int value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Int()
        {
            int x = -768;
            var replace = "XXX";
            var expect = x.ToString("X8");

            x.ToHexadecimalString().Is(expect);

            int? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "An uint value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Uint()
        {
            uint x = 768;
            var replace = "XXX";
            var expect = x.ToString("X8");

            x.ToHexadecimalString().Is(expect);

            uint? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "A long value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Long()
        {
            long x = -70000;
            var replace = "XXX";
            var expect = x.ToString("X16");

            x.ToHexadecimalString().Is(expect);

            long? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }


        [Fact(DisplayName = "An ulong value should be a hexadecimal-string.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalString))]
        public void Ulong()
        {
            ulong x = 0xFFFF0000FFFF0000;
            var replace = "XXX";
            var expect = x.ToString("X16");

            x.ToHexadecimalString().Is(expect);

            ulong? nx = x;
            nx.ToHexadecimalString().Is(expect);
            nx.ToHexadecimalString(replace).Is(expect);

            nx = null;
            nx.ToHexadecimalString().Is(string.Empty);
            nx.ToHexadecimalString(replace).Is(replace);
        }
    }

    #endregion

    #region [ ToBigEndianBytes ]

    /// <summary>
    /// Test for ValueConvertExtensions.ToBigEndianBytes
    /// </summary>
    public class ValueConvertExtensions_ToBigEndianBytes
    {
        [Fact(DisplayName = "An ushort value should be big-endian byte array.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToBigEndianBytes))]
        public void UShort()
        {
            ushort x = 0xFFFA;
            var expect = new byte[] { 0xFF, 0xFA, };

            x.ToBigEndianBytes().Is(expect);

            ushort? nx = x;
            nx.ToBigEndianBytes().Is(expect);

            nx = null;
            nx.ToBigEndianBytes().Is(new byte[] { });
        }


        [Fact(DisplayName = "A short value should be big-endian byte array.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToBigEndianBytes))]
        public void Short()
        {
            short x = -32768;
            var expect = new byte[] { 0x80, 0x00, };

            x.ToBigEndianBytes().Is(expect);

            short? nx = x;
            nx.ToBigEndianBytes().Is(expect);

            nx = null;
            nx.ToBigEndianBytes().Is(new byte[] { });
        }


        [Fact(DisplayName = "An int value should be big-endian byte array.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToBigEndianBytes))]
        public void Int()
        {
            int x = -1;
            var expect = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, };

            x.ToBigEndianBytes().Is(expect);

            int? nx = x;
            nx.ToBigEndianBytes().Is(expect);

            nx = null;
            nx.ToBigEndianBytes().Is(new byte[] { });
        }


        [Fact(DisplayName = "An uint value should be big-endian byte array.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToBigEndianBytes))]
        public void UInt()
        {
            uint x = 0xFFFF0000;
            var expect = new byte[] { 0xFF, 0xFF, 0x00, 0x00, };

            x.ToBigEndianBytes().Is(expect);

            uint? nx = x;
            nx.ToBigEndianBytes().Is(expect);

            nx = null;
            nx.ToBigEndianBytes().Is(new byte[] { });
        }


        [Fact(DisplayName = "A long value should be big-endian byte array.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToBigEndianBytes))]
        public void Long()
        {
            long x = -2;
            var expect = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFE, };

            x.ToBigEndianBytes().Is(expect);

            long? nx = x;
            nx.ToBigEndianBytes().Is(expect);

            nx = null;
            nx.ToBigEndianBytes().Is(new byte[] { });
        }


        [Fact(DisplayName = "An ulong value should be big-endian byte array.")]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToBigEndianBytes))]
        public void ULong()
        {
            ulong x = 0x1122334455667788;
            var expect = new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

            x.ToBigEndianBytes().Is(expect);

            ulong? nx = x;
            nx.ToBigEndianBytes().Is(expect);

            nx = null;
            nx.ToBigEndianBytes().Is(new byte[] { });
        }
    }

    #endregion

    #region [ ToHexadecimalStringAsBigEndianBytes ]

    /// <summary>
    /// Test for ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes.
    /// </summary>
    public class ValueConvertExtensions_ToHexadecimalStringAsBigEndianBytes
    {
        #region [ sbyte ]

        /// <summary>Test data supplier for sbyte.</summary>
        class TestData001 : DataGenerator
        {
            /// <summary>constructor</summary>
            public TestData001()
            {
                sbyte[] array = new sbyte[] { 1, 127, -1, -128, 0 };
                this.Add(array, false, null, "017FFF8000");
                this.Add(array, true, null, "017FFF8000");
                this.Add(array, true, string.Empty, "017FFF8000");
                this.Add(array, true, "/", "01/7F/FF/80/00");
                this.Add(array, true, "  ", "01  7F  FF  80  00");

                array = new sbyte[] { 127, };
                this.Add(array, false, null, "7F");
                this.Add(array, true, null, "7F");
                this.Add(array, true, string.Empty, "7F");
                this.Add(array, true, "/", "7F");
                this.Add(array, true, "  ", "7F");

                array = new sbyte[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The sbyte array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void SbyteArray(IEnumerable<sbyte> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ byte ]

        /// <summary>
        /// Test Data for byte.
        /// </summary>
        class TestData002 : DataGenerator
        {
            /// <summary>
            /// constructor
            ///</summary>
            public TestData002()
            {
                byte[] array = new byte[] { 1, 127, 0xFF, 0x80, 0 };
                this.Add(array, false, null, "017FFF8000");
                this.Add(array, true, null, "017FFF8000");
                this.Add(array, true, string.Empty, "017FFF8000");
                this.Add(array, true, "/", "01/7F/FF/80/00");
                this.Add(array, true, "  ", "01  7F  FF  80  00");

                array = new byte[] { 127, };
                this.Add(array, false, null, "7F");
                this.Add(array, true, null, "7F");
                this.Add(array, true, string.Empty, "7F");
                this.Add(array, true, "/", "7F");
                this.Add(array, true, "  ", "7F");

                array = new byte[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The byte array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void ByteArray(IEnumerable<byte> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ short ]

        /// <summary>
        /// Test Data for short.
        /// </summary>
        class TestData003 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData003()
            {
                short[] array = new short[] { 0, 1, 32767, -32768, -1, };
                this.Add(array, false, null, "000000017FFF8000FFFF");
                this.Add(array, true, null, "000000017FFF8000FFFF");
                this.Add(array, true, string.Empty, "000000017FFF8000FFFF");
                this.Add(array, true, "/", "00/00/00/01/7F/FF/80/00/FF/FF");
                this.Add(array, true, "  ", "00  00  00  01  7F  FF  80  00  FF  FF");

                array = new short[] { 127, };
                this.Add(array, false, null, "007F");
                this.Add(array, true, null, "007F");
                this.Add(array, true, string.Empty, "007F");
                this.Add(array, true, "/", "00/7F");
                this.Add(array, true, "  ", "00  7F");

                array = new short[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The short array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void ShortArray(IEnumerable<short> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ ushort ]

        /// <summary>
        /// Test Data for ushort.
        /// </summary>
        class TestData004 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData004()
            {
                ushort[] array = new ushort[] { 0, 1, 32767, 0x8000, 0xFFFF, };
                this.Add(array, false, null, "000000017FFF8000FFFF");
                this.Add(array, true, null, "000000017FFF8000FFFF");
                this.Add(array, true, string.Empty, "000000017FFF8000FFFF");
                this.Add(array, true, "/", "00/00/00/01/7F/FF/80/00/FF/FF");
                this.Add(array, true, "  ", "00  00  00  01  7F  FF  80  00  FF  FF");

                array = new ushort[] { 127, };
                this.Add(array, false, null, "007F");
                this.Add(array, true, null, "007F");
                this.Add(array, true, string.Empty, "007F");
                this.Add(array, true, "/", "00/7F");
                this.Add(array, true, "  ", "00  7F");

                array = new ushort[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The ushort array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void UShortArray(IEnumerable<ushort> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ int ]

        /// <summary>
        /// Test Data for int.
        /// </summary>
        class TestData005 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData005()
            {
                int[] array = new int[] { 0, 1, 0x7FFFFFFF, -0x80000000, -1 };
                this.Add(array, false, null, "00000000000000017FFFFFFF80000000FFFFFFFF");
                this.Add(array, true, null, "00000000000000017FFFFFFF80000000FFFFFFFF");
                this.Add(array, true, string.Empty, "00000000000000017FFFFFFF80000000FFFFFFFF");
                this.Add(array, true, "/", "00/00/00/00/00/00/00/01/7F/FF/FF/FF/80/00/00/00/FF/FF/FF/FF");
                this.Add(array, true, "  ", "00  00  00  00  00  00  00  01  7F  FF  FF  FF  80  00  00  00  FF  FF  FF  FF");

                array = new int[] { 127, };
                this.Add(array, false, null, "0000007F");
                this.Add(array, true, null, "0000007F");
                this.Add(array, true, string.Empty, "0000007F");
                this.Add(array, true, "/", "00/00/00/7F");
                this.Add(array, true, "  ", "00  00  00  7F");

                array = new int[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The int array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData005))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void IntArray(IEnumerable<int> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ uint ]

        /// <summary>
        /// Test Data for uint.
        /// </summary>
        class TestData006 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData006()
            {
                uint[] array = new uint[] { 0, 1, 0x7FFFFFFF, 0x80000000, 0xFFFFFFFF };
                this.Add(array, false, null, "00000000000000017FFFFFFF80000000FFFFFFFF");
                this.Add(array, true, null, "00000000000000017FFFFFFF80000000FFFFFFFF");
                this.Add(array, true, string.Empty, "00000000000000017FFFFFFF80000000FFFFFFFF");
                this.Add(array, true, "/", "00/00/00/00/00/00/00/01/7F/FF/FF/FF/80/00/00/00/FF/FF/FF/FF");
                this.Add(array, true, "  ", "00  00  00  00  00  00  00  01  7F  FF  FF  FF  80  00  00  00  FF  FF  FF  FF");

                array = new uint[] { 127, };
                this.Add(array, false, null, "0000007F");
                this.Add(array, true, null, "0000007F");
                this.Add(array, true, string.Empty, "0000007F");
                this.Add(array, true, "/", "00/00/00/7F");
                this.Add(array, true, "  ", "00  00  00  7F");

                array = new uint[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The uint array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData006))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void UIntArray(IEnumerable<uint> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ long ]

        /// <summary>Test Data for long.</summary>
        class TestData007 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData007()
            {
                long[] array = new long[] { 0, 1, -1 };
                this.Add(array, false, null, "00000000000000000000000000000001FFFFFFFFFFFFFFFF");
                this.Add(array, true, null, "00000000000000000000000000000001FFFFFFFFFFFFFFFF");
                this.Add(array, true, string.Empty, "00000000000000000000000000000001FFFFFFFFFFFFFFFF");
                this.Add(array, true, "/", "00/00/00/00/00/00/00/00/00/00/00/00/00/00/00/01/FF/FF/FF/FF/FF/FF/FF/FF");
                this.Add(array, true, "__", "00__00__00__00__00__00__00__00__00__00__00__00__00__00__00__01__FF__FF__FF__FF__FF__FF__FF__FF");

                array = new long[] { 127, };
                this.Add(array, false, null, "000000000000007F");
                this.Add(array, true, null, "000000000000007F");
                this.Add(array, true, string.Empty, "000000000000007F");
                this.Add(array, true, "/", "00/00/00/00/00/00/00/7F");
                this.Add(array, true, "  ", "00  00  00  00  00  00  00  7F");

                array = new long[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The long array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData007))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void LongArray(IEnumerable<long> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion

        #region [ ulong ]

        /// <summary>
        /// Test Data for ulong.
        /// </summary>
        class TestData008 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData008()
            {
                ulong[] array = new ulong[] { 0, 1, 0xFFFFFFFFFFFFFFFF };
                this.Add(array, false, null, "00000000000000000000000000000001FFFFFFFFFFFFFFFF");
                this.Add(array, true, null, "00000000000000000000000000000001FFFFFFFFFFFFFFFF");
                this.Add(array, true, string.Empty, "00000000000000000000000000000001FFFFFFFFFFFFFFFF");
                this.Add(array, true, "/", "00/00/00/00/00/00/00/00/00/00/00/00/00/00/00/01/FF/FF/FF/FF/FF/FF/FF/FF");
                this.Add(array, true, "__", "00__00__00__00__00__00__00__00__00__00__00__00__00__00__00__01__FF__FF__FF__FF__FF__FF__FF__FF");

                array = new ulong[] { 127, };
                this.Add(array, false, null, "000000000000007F");
                this.Add(array, true, null, "000000000000007F");
                this.Add(array, true, string.Empty, "000000000000007F");
                this.Add(array, true, "/", "00/00/00/00/00/00/00/7F");
                this.Add(array, true, "  ", "00  00  00  00  00  00  00  7F");

                array = new ulong[] { };
                this.Add(array, false, null, string.Empty);
                this.Add(array, true, null, string.Empty);
                this.Add(array, true, string.Empty, string.Empty);
                this.Add(array, true, "/", string.Empty);
                this.Add(array, true, "  ", string.Empty);

                this.Add(null, false, null, string.Empty);
                this.Add(null, true, null, string.Empty);
                this.Add(null, true, string.Empty, string.Empty);
                this.Add(null, true, "/", string.Empty);
                this.Add(null, true, "  ", string.Empty);
            }
        }


        [Theory(DisplayName = "The ulong array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData008))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void ULongArray(IEnumerable<long> array, bool useSepalator, string sepalator, string expect)
        {
            if (useSepalator)
            {
                array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
            }
            else
            {
                array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
            }
        }

        #endregion
    }

    #endregion

    #region [ ToHexadecimalStringAsBigEndianBytes[nullable array] ]

    /// <summary>
    /// Test for ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes
    /// </summary>
    public class ValueConvertExtensions_ToHexadecimalStringAsBigEndianBytesForNullables
    {
        /// <summary>single asterisk</summary>
        const string SA = "*";
        /// <summary>double asterisk</summary>
        const string DA = "**";
        /// <summary>slash</summary>
        const string SL = "/";
        /// <summary>double space</summary>
        const string DS = "  ";
        /// <summary>string empty</summary>
        const string EP = "";
        /// <summary>null</summary>
        const string NL = null;

        #region [ sbyte? ]

        /// <summary>Test data supplier for sbyte?.</summary>
        class TestData001 : DataGenerator
        {
            /// <summary>constructor</summary>
            public TestData001()
            {
                sbyte?[] array = new sbyte?[] { 1, 127, null, -1, -128, 0 };

                this.Add(array, false, NL, false, NL, "017FFF8000");

                this.Add(array, false, NL, true, NL, "017FFF8000");
                this.Add(array, false, NL, true, EP, "017FFF8000");
                this.Add(array, false, NL, true, SA, "017F*FF8000");
                this.Add(array, false, NL, true, DA, "017F**FF8000");

                this.Add(array, true, NL, false, NL, "017FFF8000");
                this.Add(array, true, EP, false, NL, "017FFF8000");
                this.Add(array, true, SL, false, NL, "01/7F//FF/80/00");
                this.Add(array, true, DS, false, NL, "01  7F    FF  80  00");

                this.Add(array, true, NL, true, NL, "017FFF8000");
                this.Add(array, true, NL, true, EP, "017FFF8000");
                this.Add(array, true, NL, true, SA, "017F*FF8000");
                this.Add(array, true, NL, true, DA, "017F**FF8000");

                this.Add(array, true, EP, true, NL, "017FFF8000");
                this.Add(array, true, EP, true, EP, "017FFF8000");
                this.Add(array, true, EP, true, SA, "017F*FF8000");
                this.Add(array, true, EP, true, DA, "017F**FF8000");

                this.Add(array, true, SL, true, NL, "01/7F//FF/80/00");
                this.Add(array, true, SL, true, EP, "01/7F//FF/80/00");
                this.Add(array, true, SL, true, SA, "01/7F/*/FF/80/00");
                this.Add(array, true, SL, true, DA, "01/7F/**/FF/80/00");

                this.Add(array, true, DS, true, NL, "01  7F    FF  80  00");
                this.Add(array, true, DS, true, EP, "01  7F    FF  80  00");
                this.Add(array, true, DS, true, SA, "01  7F  *  FF  80  00");
                this.Add(array, true, DS, true, DA, "01  7F  **  FF  80  00");


                array = new sbyte?[] { 127, };
                this.Add(array, false, NL, false, NL, "7F");

                this.Add(array, false, NL, true, NL, "7F");
                this.Add(array, false, NL, true, EP, "7F");
                this.Add(array, false, NL, true, SA, "7F");
                this.Add(array, false, NL, true, DA, "7F");

                this.Add(array, true, NL, false, NL, "7F");
                this.Add(array, true, EP, false, NL, "7F");
                this.Add(array, true, SL, false, NL, "7F");
                this.Add(array, true, DA, false, NL, "7F");

                this.Add(array, true, NL, true, NL, "7F");
                this.Add(array, true, NL, true, EP, "7F");
                this.Add(array, true, NL, true, SA, "7F");
                this.Add(array, true, NL, true, DA, "7F");

                this.Add(array, true, EP, true, NL, "7F");
                this.Add(array, true, EP, true, EP, "7F");
                this.Add(array, true, EP, true, SA, "7F");
                this.Add(array, true, EP, true, DA, "7F");

                this.Add(array, true, SL, true, NL, "7F");
                this.Add(array, true, SL, true, EP, "7F");
                this.Add(array, true, SL, true, SA, "7F");
                this.Add(array, true, SL, true, DA, "7F");

                this.Add(array, true, DS, true, NL, "7F");
                this.Add(array, true, DS, true, EP, "7F");
                this.Add(array, true, DS, true, SA, "7F");
                this.Add(array, true, DS, true, DA, "7F");


                array = new sbyte?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, SA);
                this.Add(array, false, NL, true, DA, DA);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DA, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, SA);
                this.Add(array, true, NL, true, DA, DA);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, SA);
                this.Add(array, true, EP, true, DA, DA);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, SA);
                this.Add(array, true, SL, true, DA, DA);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, SA);
                this.Add(array, true, DS, true, DA, DA);

                array = new sbyte?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The sbyte? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData001))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void SbyteArray(
            IEnumerable<sbyte?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ byte? ]

        /// <summary>
        /// Test Data for byte?.
        /// </summary>
        class TestData002 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData002()
            {
                byte?[] array = new byte?[] { 1, 127, null, 0xFF, 0x80, 0 };

                this.Add(array, false, NL, false, NL, "017FFF8000");

                this.Add(array, false, NL, true, NL, "017FFF8000");
                this.Add(array, false, NL, true, EP, "017FFF8000");
                this.Add(array, false, NL, true, SA, "017F*FF8000");
                this.Add(array, false, NL, true, DA, "017F**FF8000");

                this.Add(array, true, NL, false, NL, "017FFF8000");
                this.Add(array, true, EP, false, NL, "017FFF8000");
                this.Add(array, true, SL, false, NL, "01/7F//FF/80/00");
                this.Add(array, true, DS, false, NL, "01  7F    FF  80  00");

                this.Add(array, true, NL, true, NL, "017FFF8000");
                this.Add(array, true, NL, true, EP, "017FFF8000");
                this.Add(array, true, NL, true, SA, "017F*FF8000");
                this.Add(array, true, NL, true, DA, "017F**FF8000");

                this.Add(array, true, EP, true, NL, "017FFF8000");
                this.Add(array, true, EP, true, EP, "017FFF8000");
                this.Add(array, true, EP, true, SA, "017F*FF8000");
                this.Add(array, true, EP, true, DA, "017F**FF8000");

                this.Add(array, true, SL, true, NL, "01/7F//FF/80/00");
                this.Add(array, true, SL, true, EP, "01/7F//FF/80/00");
                this.Add(array, true, SL, true, SA, "01/7F/*/FF/80/00");
                this.Add(array, true, SL, true, DA, "01/7F/**/FF/80/00");

                this.Add(array, true, DS, true, NL, "01  7F    FF  80  00");
                this.Add(array, true, DS, true, EP, "01  7F    FF  80  00");
                this.Add(array, true, DS, true, SA, "01  7F  *  FF  80  00");
                this.Add(array, true, DS, true, DA, "01  7F  **  FF  80  00");


                array = new byte?[] { 127, };
                this.Add(array, false, NL, false, NL, "7F");

                this.Add(array, false, NL, true, NL, "7F");
                this.Add(array, false, NL, true, EP, "7F");
                this.Add(array, false, NL, true, SA, "7F");
                this.Add(array, false, NL, true, DA, "7F");

                this.Add(array, true, NL, false, NL, "7F");
                this.Add(array, true, EP, false, NL, "7F");
                this.Add(array, true, SL, false, NL, "7F");
                this.Add(array, true, DA, false, NL, "7F");

                this.Add(array, true, NL, true, NL, "7F");
                this.Add(array, true, NL, true, EP, "7F");
                this.Add(array, true, NL, true, SA, "7F");
                this.Add(array, true, NL, true, DA, "7F");

                this.Add(array, true, EP, true, NL, "7F");
                this.Add(array, true, EP, true, EP, "7F");
                this.Add(array, true, EP, true, SA, "7F");
                this.Add(array, true, EP, true, DA, "7F");

                this.Add(array, true, SL, true, NL, "7F");
                this.Add(array, true, SL, true, EP, "7F");
                this.Add(array, true, SL, true, SA, "7F");
                this.Add(array, true, SL, true, DA, "7F");

                this.Add(array, true, DS, true, NL, "7F");
                this.Add(array, true, DS, true, EP, "7F");
                this.Add(array, true, DS, true, SA, "7F");
                this.Add(array, true, DS, true, DA, "7F");


                array = new byte?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, SA);
                this.Add(array, false, NL, true, DA, DA);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DA, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, SA);
                this.Add(array, true, NL, true, DA, DA);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, SA);
                this.Add(array, true, EP, true, DA, DA);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, SA);
                this.Add(array, true, SL, true, DA, DA);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, SA);
                this.Add(array, true, DS, true, DA, DA);

                array = new byte?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The byte? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData002))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void ByteArray(
            IEnumerable<byte?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ short? ]

        /// <summary>
        /// Test Data for short?.
        /// </summary>
        class TestData003 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData003()
            {
                short?[] array = new short?[] { 1, 127, null, -1, -32768, 0 };

                this.Add(array, false, NL, false, NL, "0001007FFFFF80000000");

                this.Add(array, false, NL, true, NL, "0001007FFFFF80000000");
                this.Add(array, false, NL, true, EP, "0001007FFFFF80000000");
                this.Add(array, false, NL, true, SA, "0001007F**FFFF80000000");
                this.Add(array, false, NL, true, DA, "0001007F****FFFF80000000");

                this.Add(array, true, NL, false, NL, "0001007FFFFF80000000");
                this.Add(array, true, EP, false, NL, "0001007FFFFF80000000");
                this.Add(array, true, SL, false, NL, "00/01/00/7F///FF/FF/80/00/00/00");
                this.Add(array, true, DS, false, NL, "00  01  00  7F      FF  FF  80  00  00  00");

                this.Add(array, true, NL, true, NL, "0001007FFFFF80000000");
                this.Add(array, true, NL, true, EP, "0001007FFFFF80000000");
                this.Add(array, true, NL, true, SA, "0001007F**FFFF80000000");
                this.Add(array, true, NL, true, DA, "0001007F****FFFF80000000");

                this.Add(array, true, EP, true, NL, "0001007FFFFF80000000");
                this.Add(array, true, EP, true, EP, "0001007FFFFF80000000");
                this.Add(array, true, EP, true, SA, "0001007F**FFFF80000000");
                this.Add(array, true, EP, true, DA, "0001007F****FFFF80000000");

                this.Add(array, true, SL, true, NL, "00/01/00/7F///FF/FF/80/00/00/00");
                this.Add(array, true, SL, true, EP, "00/01/00/7F///FF/FF/80/00/00/00");
                this.Add(array, true, SL, true, SA, "00/01/00/7F/*/*/FF/FF/80/00/00/00");
                this.Add(array, true, SL, true, DA, "00/01/00/7F/**/**/FF/FF/80/00/00/00");

                this.Add(array, true, DS, true, NL, "00  01  00  7F      FF  FF  80  00  00  00");
                this.Add(array, true, DS, true, EP, "00  01  00  7F      FF  FF  80  00  00  00");
                this.Add(array, true, DS, true, SA, "00  01  00  7F  *  *  FF  FF  80  00  00  00");
                this.Add(array, true, DS, true, DA, "00  01  00  7F  **  **  FF  FF  80  00  00  00");


                array = new short?[] { 127, };
                this.Add(array, false, NL, false, NL, "007F");

                this.Add(array, false, NL, true, NL, "007F");
                this.Add(array, false, NL, true, EP, "007F");
                this.Add(array, false, NL, true, SA, "007F");
                this.Add(array, false, NL, true, DA, "007F");

                this.Add(array, true, NL, false, NL, "007F");
                this.Add(array, true, EP, false, NL, "007F");
                this.Add(array, true, SL, false, NL, "00/7F");
                this.Add(array, true, DS, false, NL, "00  7F");

                this.Add(array, true, NL, true, NL, "007F");
                this.Add(array, true, NL, true, EP, "007F");
                this.Add(array, true, NL, true, SA, "007F");
                this.Add(array, true, NL, true, DA, "007F");

                this.Add(array, true, EP, true, NL, "007F");
                this.Add(array, true, EP, true, EP, "007F");
                this.Add(array, true, EP, true, SA, "007F");
                this.Add(array, true, EP, true, DA, "007F");

                this.Add(array, true, SL, true, NL, "00/7F");
                this.Add(array, true, SL, true, EP, "00/7F");
                this.Add(array, true, SL, true, SA, "00/7F");
                this.Add(array, true, SL, true, DA, "00/7F");

                this.Add(array, true, DS, true, NL, "00  7F");
                this.Add(array, true, DS, true, EP, "00  7F");
                this.Add(array, true, DS, true, SA, "00  7F");
                this.Add(array, true, DS, true, DA, "00  7F");


                array = new short?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, "**");
                this.Add(array, false, NL, true, DA, "****");

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, "/");
                this.Add(array, true, DS, false, NL, "  ");

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, "**");
                this.Add(array, true, NL, true, DA, "****");

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, "**");
                this.Add(array, true, EP, true, DA, "****");

                this.Add(array, true, SL, true, NL, "/");
                this.Add(array, true, SL, true, EP, "/");
                this.Add(array, true, SL, true, SA, "*/*");
                this.Add(array, true, SL, true, DA, "**/**");

                this.Add(array, true, DS, true, NL, "  ");
                this.Add(array, true, DS, true, EP, "  ");
                this.Add(array, true, DS, true, SA, "*  *");
                this.Add(array, true, DS, true, DA, "**  **");

                array = new short?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The short? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void ShortArray(
            IEnumerable<short?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ ushort? ]

        /// <summary>
        /// Test Data for ushort?.
        /// </summary>
        class TestData004 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData004()
            {
                ushort?[] array = new ushort?[] { 1, 127, null, 0xFFFF, 0x8000, 0 };

                this.Add(array, false, NL, false, NL, "0001007FFFFF80000000");

                this.Add(array, false, NL, true, NL, "0001007FFFFF80000000");
                this.Add(array, false, NL, true, EP, "0001007FFFFF80000000");
                this.Add(array, false, NL, true, SA, "0001007F**FFFF80000000");
                this.Add(array, false, NL, true, DA, "0001007F****FFFF80000000");

                this.Add(array, true, NL, false, NL, "0001007FFFFF80000000");
                this.Add(array, true, EP, false, NL, "0001007FFFFF80000000");
                this.Add(array, true, SL, false, NL, "00/01/00/7F///FF/FF/80/00/00/00");
                this.Add(array, true, DS, false, NL, "00  01  00  7F      FF  FF  80  00  00  00");

                this.Add(array, true, NL, true, NL, "0001007FFFFF80000000");
                this.Add(array, true, NL, true, EP, "0001007FFFFF80000000");
                this.Add(array, true, NL, true, SA, "0001007F**FFFF80000000");
                this.Add(array, true, NL, true, DA, "0001007F****FFFF80000000");

                this.Add(array, true, EP, true, NL, "0001007FFFFF80000000");
                this.Add(array, true, EP, true, EP, "0001007FFFFF80000000");
                this.Add(array, true, EP, true, SA, "0001007F**FFFF80000000");
                this.Add(array, true, EP, true, DA, "0001007F****FFFF80000000");

                this.Add(array, true, SL, true, NL, "00/01/00/7F///FF/FF/80/00/00/00");
                this.Add(array, true, SL, true, EP, "00/01/00/7F///FF/FF/80/00/00/00");
                this.Add(array, true, SL, true, SA, "00/01/00/7F/*/*/FF/FF/80/00/00/00");
                this.Add(array, true, SL, true, DA, "00/01/00/7F/**/**/FF/FF/80/00/00/00");

                this.Add(array, true, DS, true, NL, "00  01  00  7F      FF  FF  80  00  00  00");
                this.Add(array, true, DS, true, EP, "00  01  00  7F      FF  FF  80  00  00  00");
                this.Add(array, true, DS, true, SA, "00  01  00  7F  *  *  FF  FF  80  00  00  00");
                this.Add(array, true, DS, true, DA, "00  01  00  7F  **  **  FF  FF  80  00  00  00");


                array = new ushort?[] { 127, };
                this.Add(array, false, NL, false, NL, "007F");

                this.Add(array, false, NL, true, NL, "007F");
                this.Add(array, false, NL, true, EP, "007F");
                this.Add(array, false, NL, true, SA, "007F");
                this.Add(array, false, NL, true, DA, "007F");

                this.Add(array, true, NL, false, NL, "007F");
                this.Add(array, true, EP, false, NL, "007F");
                this.Add(array, true, SL, false, NL, "00/7F");
                this.Add(array, true, DS, false, NL, "00  7F");

                this.Add(array, true, NL, true, NL, "007F");
                this.Add(array, true, NL, true, EP, "007F");
                this.Add(array, true, NL, true, SA, "007F");
                this.Add(array, true, NL, true, DA, "007F");

                this.Add(array, true, EP, true, NL, "007F");
                this.Add(array, true, EP, true, EP, "007F");
                this.Add(array, true, EP, true, SA, "007F");
                this.Add(array, true, EP, true, DA, "007F");

                this.Add(array, true, SL, true, NL, "00/7F");
                this.Add(array, true, SL, true, EP, "00/7F");
                this.Add(array, true, SL, true, SA, "00/7F");
                this.Add(array, true, SL, true, DA, "00/7F");

                this.Add(array, true, DS, true, NL, "00  7F");
                this.Add(array, true, DS, true, EP, "00  7F");
                this.Add(array, true, DS, true, SA, "00  7F");
                this.Add(array, true, DS, true, DA, "00  7F");


                array = new ushort?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, "**");
                this.Add(array, false, NL, true, DA, "****");

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, "/");
                this.Add(array, true, DS, false, NL, "  ");

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, "**");
                this.Add(array, true, NL, true, DA, "****");

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, "**");
                this.Add(array, true, EP, true, DA, "****");

                this.Add(array, true, SL, true, NL, "/");
                this.Add(array, true, SL, true, EP, "/");
                this.Add(array, true, SL, true, SA, "*/*");
                this.Add(array, true, SL, true, DA, "**/**");

                this.Add(array, true, DS, true, NL, "  ");
                this.Add(array, true, DS, true, EP, "  ");
                this.Add(array, true, DS, true, SA, "*  *");
                this.Add(array, true, DS, true, DA, "**  **");

                array = new ushort?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The ushort? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData004))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void UShortArray(
            IEnumerable<ushort?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ int? ]

        /// <summary>
        /// Test Data for int?.
        /// </summary>
        class TestData005 : DataGenerator
        {
            /// <summary>  
            /// constructor
            /// </summary>
            public TestData005()
            {
                int?[] array = new int?[] { 1, 0x11223344, null, int.MaxValue, 0 };

                this.Add(array, false, NL, false, NL, "00000001112233447FFFFFFF00000000");

                this.Add(array, false, NL, true, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, false, NL, true, EP, "00000001112233447FFFFFFF00000000");
                this.Add(array, false, NL, true, SA, "0000000111223344****7FFFFFFF00000000");
                this.Add(array, false, NL, true, DA, "0000000111223344********7FFFFFFF00000000");

                this.Add(array, true, NL, false, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, EP, false, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, SL, false, NL, "00/00/00/01/11/22/33/44/////7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, DS, false, NL, "00  00  00  01  11  22  33  44          7F  FF  FF  FF  00  00  00  00");

                this.Add(array, true, NL, true, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, NL, true, EP, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, NL, true, SA, "0000000111223344****7FFFFFFF00000000");
                this.Add(array, true, NL, true, DA, "0000000111223344********7FFFFFFF00000000");

                this.Add(array, true, EP, true, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, EP, true, EP, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, EP, true, SA, "0000000111223344****7FFFFFFF00000000");
                this.Add(array, true, EP, true, DA, "0000000111223344********7FFFFFFF00000000");

                this.Add(array, true, SL, true, NL, "00/00/00/01/11/22/33/44/////7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, SL, true, EP, "00/00/00/01/11/22/33/44/////7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, SL, true, SA, "00/00/00/01/11/22/33/44/*/*/*/*/7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, SL, true, DA, "00/00/00/01/11/22/33/44/**/**/**/**/7F/FF/FF/FF/00/00/00/00");

                this.Add(array, true, DS, true, NL, "00  00  00  01  11  22  33  44          7F  FF  FF  FF  00  00  00  00");
                this.Add(array, true, DS, true, EP, "00  00  00  01  11  22  33  44          7F  FF  FF  FF  00  00  00  00");
                this.Add(array, true, DS, true, SA, "00  00  00  01  11  22  33  44  *  *  *  *  7F  FF  FF  FF  00  00  00  00");
                this.Add(array, true, DS, true, DA, "00  00  00  01  11  22  33  44  **  **  **  **  7F  FF  FF  FF  00  00  00  00");


                array = new int?[] { 127, };
                this.Add(array, false, NL, false, NL, "0000007F");

                this.Add(array, false, NL, true, NL, "0000007F");
                this.Add(array, false, NL, true, EP, "0000007F");
                this.Add(array, false, NL, true, SA, "0000007F");
                this.Add(array, false, NL, true, DA, "0000007F");

                this.Add(array, true, NL, false, NL, "0000007F");
                this.Add(array, true, EP, false, NL, "0000007F");
                this.Add(array, true, SL, false, NL, "00/00/00/7F");
                this.Add(array, true, DS, false, NL, "00  00  00  7F");

                this.Add(array, true, NL, true, NL, "0000007F");
                this.Add(array, true, NL, true, EP, "0000007F");
                this.Add(array, true, NL, true, SA, "0000007F");
                this.Add(array, true, NL, true, DA, "0000007F");

                this.Add(array, true, EP, true, NL, "0000007F");
                this.Add(array, true, EP, true, EP, "0000007F");
                this.Add(array, true, EP, true, SA, "0000007F");
                this.Add(array, true, EP, true, DA, "0000007F");

                this.Add(array, true, SL, true, NL, "00/00/00/7F");
                this.Add(array, true, SL, true, EP, "00/00/00/7F");
                this.Add(array, true, SL, true, SA, "00/00/00/7F");
                this.Add(array, true, SL, true, DA, "00/00/00/7F");

                this.Add(array, true, DS, true, NL, "00  00  00  7F");
                this.Add(array, true, DS, true, EP, "00  00  00  7F");
                this.Add(array, true, DS, true, SA, "00  00  00  7F");
                this.Add(array, true, DS, true, DA, "00  00  00  7F");


                array = new int?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, "****");
                this.Add(array, false, NL, true, DA, "********");

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, "///");
                this.Add(array, true, DS, false, NL, "      ");

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, "****");
                this.Add(array, true, NL, true, DA, "********");

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, "****");
                this.Add(array, true, EP, true, DA, "********");

                this.Add(array, true, SL, true, NL, "///");
                this.Add(array, true, SL, true, EP, "///");
                this.Add(array, true, SL, true, SA, "*/*/*/*");
                this.Add(array, true, SL, true, DA, "**/**/**/**");

                this.Add(array, true, DS, true, NL, "      ");
                this.Add(array, true, DS, true, EP, "      ");
                this.Add(array, true, DS, true, SA, "*  *  *  *");
                this.Add(array, true, DS, true, DA, "**  **  **  **");

                array = new int?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The int? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData005))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void IntArray(
            IEnumerable<int?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ uint? ]

        /// <summary>
        /// Test Data for uint?.
        /// </summary>
        class TestData006 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData006()
            {
                uint?[] array = new uint?[] { 1, 0x11223344, null, int.MaxValue, 0 };

                this.Add(array, false, NL, false, NL, "00000001112233447FFFFFFF00000000");

                this.Add(array, false, NL, true, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, false, NL, true, EP, "00000001112233447FFFFFFF00000000");
                this.Add(array, false, NL, true, SA, "0000000111223344****7FFFFFFF00000000");
                this.Add(array, false, NL, true, DA, "0000000111223344********7FFFFFFF00000000");

                this.Add(array, true, NL, false, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, EP, false, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, SL, false, NL, "00/00/00/01/11/22/33/44/////7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, DS, false, NL, "00  00  00  01  11  22  33  44          7F  FF  FF  FF  00  00  00  00");

                this.Add(array, true, NL, true, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, NL, true, EP, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, NL, true, SA, "0000000111223344****7FFFFFFF00000000");
                this.Add(array, true, NL, true, DA, "0000000111223344********7FFFFFFF00000000");

                this.Add(array, true, EP, true, NL, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, EP, true, EP, "00000001112233447FFFFFFF00000000");
                this.Add(array, true, EP, true, SA, "0000000111223344****7FFFFFFF00000000");
                this.Add(array, true, EP, true, DA, "0000000111223344********7FFFFFFF00000000");

                this.Add(array, true, SL, true, NL, "00/00/00/01/11/22/33/44/////7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, SL, true, EP, "00/00/00/01/11/22/33/44/////7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, SL, true, SA, "00/00/00/01/11/22/33/44/*/*/*/*/7F/FF/FF/FF/00/00/00/00");
                this.Add(array, true, SL, true, DA, "00/00/00/01/11/22/33/44/**/**/**/**/7F/FF/FF/FF/00/00/00/00");

                this.Add(array, true, DS, true, NL, "00  00  00  01  11  22  33  44          7F  FF  FF  FF  00  00  00  00");
                this.Add(array, true, DS, true, EP, "00  00  00  01  11  22  33  44          7F  FF  FF  FF  00  00  00  00");
                this.Add(array, true, DS, true, SA, "00  00  00  01  11  22  33  44  *  *  *  *  7F  FF  FF  FF  00  00  00  00");
                this.Add(array, true, DS, true, DA, "00  00  00  01  11  22  33  44  **  **  **  **  7F  FF  FF  FF  00  00  00  00");


                array = new uint?[] { 127, };
                this.Add(array, false, NL, false, NL, "0000007F");

                this.Add(array, false, NL, true, NL, "0000007F");
                this.Add(array, false, NL, true, EP, "0000007F");
                this.Add(array, false, NL, true, SA, "0000007F");
                this.Add(array, false, NL, true, DA, "0000007F");

                this.Add(array, true, NL, false, NL, "0000007F");
                this.Add(array, true, EP, false, NL, "0000007F");
                this.Add(array, true, SL, false, NL, "00/00/00/7F");
                this.Add(array, true, DS, false, NL, "00  00  00  7F");

                this.Add(array, true, NL, true, NL, "0000007F");
                this.Add(array, true, NL, true, EP, "0000007F");
                this.Add(array, true, NL, true, SA, "0000007F");
                this.Add(array, true, NL, true, DA, "0000007F");

                this.Add(array, true, EP, true, NL, "0000007F");
                this.Add(array, true, EP, true, EP, "0000007F");
                this.Add(array, true, EP, true, SA, "0000007F");
                this.Add(array, true, EP, true, DA, "0000007F");

                this.Add(array, true, SL, true, NL, "00/00/00/7F");
                this.Add(array, true, SL, true, EP, "00/00/00/7F");
                this.Add(array, true, SL, true, SA, "00/00/00/7F");
                this.Add(array, true, SL, true, DA, "00/00/00/7F");

                this.Add(array, true, DS, true, NL, "00  00  00  7F");
                this.Add(array, true, DS, true, EP, "00  00  00  7F");
                this.Add(array, true, DS, true, SA, "00  00  00  7F");
                this.Add(array, true, DS, true, DA, "00  00  00  7F");


                array = new uint?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, "****");
                this.Add(array, false, NL, true, DA, "********");

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, "///");
                this.Add(array, true, DS, false, NL, "      ");

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, "****");
                this.Add(array, true, NL, true, DA, "********");

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, "****");
                this.Add(array, true, EP, true, DA, "********");

                this.Add(array, true, SL, true, NL, "///");
                this.Add(array, true, SL, true, EP, "///");
                this.Add(array, true, SL, true, SA, "*/*/*/*");
                this.Add(array, true, SL, true, DA, "**/**/**/**");

                this.Add(array, true, DS, true, NL, "      ");
                this.Add(array, true, DS, true, EP, "      ");
                this.Add(array, true, DS, true, SA, "*  *  *  *");
                this.Add(array, true, DS, true, DA, "**  **  **  **");

                array = new uint?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The uint? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData006))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void UIntArray(
            IEnumerable<uint?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ long? ]

        /// <summary>
        /// Test Data for long?.
        /// </summary>
        class TestData007 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData007()
            {
                long?[] array = new long?[] { 1, 0x1122334455667788, null, long.MaxValue, long.MinValue };

                this.Add(array, false, NL, false, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, false, NL, true, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, false, NL, true, EP, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, false, NL, true, SA, "00000000000000011122334455667788********7FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, false, NL, true, DA, "00000000000000011122334455667788****************7FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, true, NL, false, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, false, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, SL, false, NL, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/////////7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, DS, false, NL, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88                  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");

                this.Add(array, true, NL, true, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, NL, true, EP, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, NL, true, SA, "00000000000000011122334455667788********7FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, NL, true, DA, "00000000000000011122334455667788****************7FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, true, EP, true, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, true, EP, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, true, SA, "00000000000000011122334455667788********7FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, true, DA, "00000000000000011122334455667788****************7FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, true, SL, true, NL, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/////////7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, SL, true, EP, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/////////7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, SL, true, SA, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/*/*/*/*/*/*/*/*/7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, SL, true, DA, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/**/**/**/**/**/**/**/**/7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");

                this.Add(array, true, DS, true, NL, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88                  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");
                this.Add(array, true, DS, true, EP, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88                  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");
                this.Add(array, true, DS, true, SA, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88  *  *  *  *  *  *  *  *  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");
                this.Add(array, true, DS, true, DA, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88  **  **  **  **  **  **  **  **  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");


                array = new long?[] { 127, };
                this.Add(array, false, NL, false, NL, "000000000000007F");

                this.Add(array, false, NL, true, NL, "000000000000007F");
                this.Add(array, false, NL, true, EP, "000000000000007F");
                this.Add(array, false, NL, true, SA, "000000000000007F");
                this.Add(array, false, NL, true, DA, "000000000000007F");

                this.Add(array, true, NL, false, NL, "000000000000007F");
                this.Add(array, true, EP, false, NL, "000000000000007F");
                this.Add(array, true, SL, false, NL, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, DS, false, NL, "00  00  00  00  00  00  00  7F");

                this.Add(array, true, NL, true, NL, "000000000000007F");
                this.Add(array, true, NL, true, EP, "000000000000007F");
                this.Add(array, true, NL, true, SA, "000000000000007F");
                this.Add(array, true, NL, true, DA, "000000000000007F");

                this.Add(array, true, EP, true, NL, "000000000000007F");
                this.Add(array, true, EP, true, EP, "000000000000007F");
                this.Add(array, true, EP, true, SA, "000000000000007F");
                this.Add(array, true, EP, true, DA, "000000000000007F");

                this.Add(array, true, SL, true, NL, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, SL, true, EP, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, SL, true, SA, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, SL, true, DA, "00/00/00/00/00/00/00/7F");

                this.Add(array, true, DS, true, NL, "00  00  00  00  00  00  00  7F");
                this.Add(array, true, DS, true, EP, "00  00  00  00  00  00  00  7F");
                this.Add(array, true, DS, true, SA, "00  00  00  00  00  00  00  7F");
                this.Add(array, true, DS, true, DA, "00  00  00  00  00  00  00  7F");


                array = new long?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, "********");
                this.Add(array, false, NL, true, DA, "****************");

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, "///////");
                this.Add(array, true, DS, false, NL, "              ");

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, "********");
                this.Add(array, true, NL, true, DA, "****************");

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, "********");
                this.Add(array, true, EP, true, DA, "****************");

                this.Add(array, true, SL, true, NL, "///////");
                this.Add(array, true, SL, true, EP, "///////");
                this.Add(array, true, SL, true, SA, "*/*/*/*/*/*/*/*");
                this.Add(array, true, SL, true, DA, "**/**/**/**/**/**/**/**");

                this.Add(array, true, DS, true, NL, "              ");
                this.Add(array, true, DS, true, EP, "              ");
                this.Add(array, true, DS, true, SA, "*  *  *  *  *  *  *  *");
                this.Add(array, true, DS, true, DA, "**  **  **  **  **  **  **  **");

                array = new long?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The long? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData007))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void LongArray(
            IEnumerable<long?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion

        #region [ ulong? ]

        /// <summary>
        /// Test Data for ulong?.
        /// </summary>
        class TestData008 : DataGenerator
        {
            /// <summary>
            /// constructor
            /// </summary>
            public TestData008()
            {
                ulong?[] array = new ulong?[] { 1, 0x1122334455667788, null, long.MaxValue, (ulong)long.MaxValue + 1 };

                this.Add(array, false, NL, false, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, false, NL, true, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, false, NL, true, EP, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, false, NL, true, SA, "00000000000000011122334455667788********7FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, false, NL, true, DA, "00000000000000011122334455667788****************7FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, true, NL, false, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, false, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, SL, false, NL, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/////////7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, DS, false, NL, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88                  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");

                this.Add(array, true, NL, true, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, NL, true, EP, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, NL, true, SA, "00000000000000011122334455667788********7FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, NL, true, DA, "00000000000000011122334455667788****************7FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, true, EP, true, NL, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, true, EP, "000000000000000111223344556677887FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, true, SA, "00000000000000011122334455667788********7FFFFFFFFFFFFFFF8000000000000000");
                this.Add(array, true, EP, true, DA, "00000000000000011122334455667788****************7FFFFFFFFFFFFFFF8000000000000000");

                this.Add(array, true, SL, true, NL, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/////////7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, SL, true, EP, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/////////7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, SL, true, SA, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/*/*/*/*/*/*/*/*/7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");
                this.Add(array, true, SL, true, DA, "00/00/00/00/00/00/00/01/11/22/33/44/55/66/77/88/**/**/**/**/**/**/**/**/7F/FF/FF/FF/FF/FF/FF/FF/80/00/00/00/00/00/00/00");

                this.Add(array, true, DS, true, NL, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88                  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");
                this.Add(array, true, DS, true, EP, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88                  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");
                this.Add(array, true, DS, true, SA, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88  *  *  *  *  *  *  *  *  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");
                this.Add(array, true, DS, true, DA, "00  00  00  00  00  00  00  01  11  22  33  44  55  66  77  88  **  **  **  **  **  **  **  **  7F  FF  FF  FF  FF  FF  FF  FF  80  00  00  00  00  00  00  00");


                array = new ulong?[] { 127, };
                this.Add(array, false, NL, false, NL, "000000000000007F");

                this.Add(array, false, NL, true, NL, "000000000000007F");
                this.Add(array, false, NL, true, EP, "000000000000007F");
                this.Add(array, false, NL, true, SA, "000000000000007F");
                this.Add(array, false, NL, true, DA, "000000000000007F");

                this.Add(array, true, NL, false, NL, "000000000000007F");
                this.Add(array, true, EP, false, NL, "000000000000007F");
                this.Add(array, true, SL, false, NL, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, DS, false, NL, "00  00  00  00  00  00  00  7F");

                this.Add(array, true, NL, true, NL, "000000000000007F");
                this.Add(array, true, NL, true, EP, "000000000000007F");
                this.Add(array, true, NL, true, SA, "000000000000007F");
                this.Add(array, true, NL, true, DA, "000000000000007F");

                this.Add(array, true, EP, true, NL, "000000000000007F");
                this.Add(array, true, EP, true, EP, "000000000000007F");
                this.Add(array, true, EP, true, SA, "000000000000007F");
                this.Add(array, true, EP, true, DA, "000000000000007F");

                this.Add(array, true, SL, true, NL, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, SL, true, EP, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, SL, true, SA, "00/00/00/00/00/00/00/7F");
                this.Add(array, true, SL, true, DA, "00/00/00/00/00/00/00/7F");

                this.Add(array, true, DS, true, NL, "00  00  00  00  00  00  00  7F");
                this.Add(array, true, DS, true, EP, "00  00  00  00  00  00  00  7F");
                this.Add(array, true, DS, true, SA, "00  00  00  00  00  00  00  7F");
                this.Add(array, true, DS, true, DA, "00  00  00  00  00  00  00  7F");


                array = new ulong?[] { null, };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, "********");
                this.Add(array, false, NL, true, DA, "****************");

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, "///////");
                this.Add(array, true, DS, false, NL, "              ");

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, "********");
                this.Add(array, true, NL, true, DA, "****************");

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, "********");
                this.Add(array, true, EP, true, DA, "****************");

                this.Add(array, true, SL, true, NL, "///////");
                this.Add(array, true, SL, true, EP, "///////");
                this.Add(array, true, SL, true, SA, "*/*/*/*/*/*/*/*");
                this.Add(array, true, SL, true, DA, "**/**/**/**/**/**/**/**");

                this.Add(array, true, DS, true, NL, "              ");
                this.Add(array, true, DS, true, EP, "              ");
                this.Add(array, true, DS, true, SA, "*  *  *  *  *  *  *  *");
                this.Add(array, true, DS, true, DA, "**  **  **  **  **  **  **  **");

                array = new ulong?[] { };
                this.Add(array, false, NL, false, NL, EP);

                this.Add(array, false, NL, true, NL, EP);
                this.Add(array, false, NL, true, EP, EP);
                this.Add(array, false, NL, true, SA, EP);
                this.Add(array, false, NL, true, DA, EP);

                this.Add(array, true, NL, false, NL, EP);
                this.Add(array, true, EP, false, NL, EP);
                this.Add(array, true, SL, false, NL, EP);
                this.Add(array, true, DS, false, NL, EP);

                this.Add(array, true, NL, true, NL, EP);
                this.Add(array, true, NL, true, EP, EP);
                this.Add(array, true, NL, true, SA, EP);
                this.Add(array, true, NL, true, DA, EP);

                this.Add(array, true, EP, true, NL, EP);
                this.Add(array, true, EP, true, EP, EP);
                this.Add(array, true, EP, true, SA, EP);
                this.Add(array, true, EP, true, DA, EP);

                this.Add(array, true, SL, true, NL, EP);
                this.Add(array, true, SL, true, EP, EP);
                this.Add(array, true, SL, true, SA, EP);
                this.Add(array, true, SL, true, DA, EP);

                this.Add(array, true, DS, true, NL, EP);
                this.Add(array, true, DS, true, EP, EP);
                this.Add(array, true, DS, true, SA, EP);
                this.Add(array, true, DS, true, DA, EP);


                this.Add(null, false, NL, false, NL, EP);

                this.Add(null, false, NL, true, NL, EP);
                this.Add(null, false, NL, true, EP, EP);
                this.Add(null, false, NL, true, SA, EP);
                this.Add(null, false, NL, true, DA, EP);

                this.Add(null, true, NL, false, NL, EP);
                this.Add(null, true, EP, false, NL, EP);
                this.Add(null, true, SL, false, NL, EP);
                this.Add(null, true, DS, false, NL, EP);

                this.Add(null, true, NL, true, NL, EP);
                this.Add(null, true, NL, true, EP, EP);
                this.Add(null, true, NL, true, SA, EP);
                this.Add(null, true, NL, true, DA, EP);

                this.Add(null, true, EP, true, NL, EP);
                this.Add(null, true, EP, true, EP, EP);
                this.Add(null, true, EP, true, SA, EP);
                this.Add(null, true, EP, true, DA, EP);

                this.Add(null, true, SL, true, NL, EP);
                this.Add(null, true, SL, true, EP, EP);
                this.Add(null, true, SL, true, SA, EP);
                this.Add(null, true, SL, true, DA, EP);

                this.Add(null, true, DS, true, NL, EP);
                this.Add(null, true, DS, true, EP, EP);
                this.Add(null, true, DS, true, SA, EP);
                this.Add(null, true, DS, true, DA, EP);
            }
        }


        [Theory(DisplayName = "The ulong? array should be converted to hexadecimal-string array that ordered by big-endian.")]
        [ClassData(typeof(TestData008))]
        [Trait(nameof(ValueConvertExtensions), nameof(ValueConvertExtensions.ToHexadecimalStringAsBigEndianBytes))]
        public void ULongArray(
            IEnumerable<ulong?> array,
             bool useSepalator,
             string sepalator,
             bool useReplace,
             string replace,
             string expect)
        {
            if (useSepalator)
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator, replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes(sepalator).Is(expect);
                }
            }
            else
            {
                if (useReplace)
                {
                    array.ToHexadecimalStringAsBigEndianBytes(nullByteReplacement: replace).Is(expect);
                }
                else
                {
                    array.ToHexadecimalStringAsBigEndianBytes().Is(expect);
                }
            }
        }

        #endregion
    }

    #endregion
}