/*
 * ValueConvertExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimalTools.Extensions.Convert
{
    /// <summary>
    /// Extension methods for generating character strings or byte columns
    /// formatted from a value type.
    /// </summary>
    public static class ValueConvertExtensions
    {
        #region [ Converts DateTime to format string. (Commonly used formats in Japan.)]


        /// <summary>
        /// Converts DateTime to yyyyMMdd format string.
        /// </summary>
        /// <param name="dateTime">An instance of DateTime.</param>
        /// <returns>A formatted string.</returns>
        public static string ToYmd(this DateTime dateTime) => dateTime.ToString("yyyyMMdd");


        /// <summary>
        /// Converts DateTime to yyyyMMddHHmmss format string.
        /// </summary>
        /// <param name="dateTime">An instance of DateTime.</param>
        /// <returns>A formatted string.</returns>
        public static string ToYmdHms(this DateTime dateTime) => dateTime.ToString("yyyyMMddHHmmss");


        /// <summary>
        /// Converts DateTime to yyyyMMddHHmmssfff format string.
        /// </summary>
        /// <param name="dateTime">An instance of DateTime.</param>
        /// <returns>A formatted string.</returns>
        public static string ToTimestamp(this DateTime dateTime) => dateTime.ToString("yyyyMMddHHmmssfff");


        /// <summary>
        /// Converts DateTime to yyyy/MM/dd format string.
        /// </summary>
        /// <param name="dateTime">An instance of DateTime.</param>
        /// <returns>A formatted string.</returns>
        public static string ToDelimitedYmd(this DateTime dateTime) => dateTime.ToString("yyyy/MM/dd");


        /// <summary>
        /// Converts DateTime to yyyy/MM/dd HH:mm:ss format string.
        /// </summary>
        /// <param name="dateTime">An instance of DateTime.</param>
        /// <returns>A formatted string.</returns>
        public static string ToDelimitedYmdHms(this DateTime dateTime) => dateTime.ToString("yyyy/MM/dd HH:mm:ss");


        /// <summary>
        /// Converts DateTime to yyyy/MM/dd HH:mm:ss.fff format string.
        /// </summary>
        /// <param name="dateTime">An instance of DateTime.</param>
        /// <returns>A formatted string.</returns>
        public static string ToDelimitedTimestamp(this DateTime dateTime) => dateTime.ToString("yyyy/MM/dd HH:mm:ss.fff");


        /// <summary>
        /// Converts nullable DateTime to yyyyMMdd format string.
        /// </summary>
        /// <param name="dateTime">An instance of nullable DateTime.</param>
        /// <param name="defaultValue">
        /// An alternative string returned when <paramref name="dateTime"/> has no value.
        /// </param>
        /// <returns>
        /// if <paramref name="dateTime"/> has value, returns A formatted string, otherwise
        /// the alternative string specified in parameter.
        /// </returns>
        public static string ToYmd(this DateTime? dateTime, string defaultValue = "") => dateTime?.ToYmd() ?? defaultValue;


        /// <summary>
        /// Converts nullable DateTime to yyyyMMddHHmmss format string.
        /// </summary>
        /// <param name="dateTime">An instance of nullable DateTime.</param>
        /// <param name="defaultValue">
        /// An alternative string returned when <paramref name="dateTime"/> has no value.
        /// </param>
        /// <returns>
        /// if <paramref name="dateTime"/> has value, returns A formatted string, otherwise
        /// the alternative string specified in parameter.
        /// </returns>
        public static string ToYmdHms(this DateTime? dateTime, string defaultValue = "") => dateTime?.ToYmdHms() ?? defaultValue;


        /// <summary>
        /// Converts nullable DateTime to yyyyMMddHHmmssfff format string.
        /// </summary>
        /// <param name="dateTime">An instance of nullable DateTime.</param>
        /// <param name="defaultValue">
        /// An alternative string returned when <paramref name="dateTime"/> has no value.
        /// </param>
        /// <returns>
        /// if <paramref name="dateTime"/> has value, returns A formatted string, otherwise
        /// the alternative string specified in parameter.
        /// </returns>
        public static string ToTimestamp(this DateTime? dateTime, string defaultValue = "") => dateTime?.ToTimestamp() ?? defaultValue;


        /// <summary>
        /// Converts nullable DateTime to yyyy/MM/dd format string.
        /// </summary>
        /// <param name="dateTime">An instance of nullable DateTime.</param>
        /// <param name="defaultValue">
        /// An alternative string returned when <paramref name="dateTime"/> has no value.
        /// </param>
        /// <returns>
        /// if <paramref name="dateTime"/> has value, returns A formatted string, otherwise
        /// the alternative string specified in parameter.
        /// </returns>
        public static string ToDelimitedYmd(this DateTime? dateTime, string defaultValue = "") => dateTime?.ToDelimitedYmd() ?? defaultValue;


        /// <summary>
        /// Converts nullable DateTime to yyyy/MM/dd HH:mm:ss format string.
        /// </summary>
        /// <param name="dateTime">An instance of nullable DateTime.</param>
        /// <param name="defaultValue">
        /// An alternative string returned when <paramref name="dateTime"/> has no value.
        /// </param>
        /// <returns>
        /// if <paramref name="dateTime"/> has value, returns A formatted string, otherwise
        /// the alternative string specified in parameter.
        /// </returns>
        public static string ToDelimitedYmdHms(this DateTime? dateTime, string defaultValue = "") => dateTime?.ToDelimitedYmdHms() ?? defaultValue;


        /// <summary>
        /// Converts nullable DateTime to yyyy/MM/dd HH:mm:ss.fff format string.
        /// </summary>
        /// <param name="dateTime">An instance of nullable DateTime.</param>
        /// <param name="defaultValue">
        /// An alternative string returned when <paramref name="dateTime"/> has no value.
        /// </param>
        /// <returns>
        /// if <paramref name="dateTime"/> has value, returns A formatted string, otherwise
        /// the alternative string specified in parameter.
        /// </returns>
        public static string ToDelimitedTimestamp(this DateTime? dateTime, string defaultValue = "") => dateTime?.ToDelimitedTimestamp() ?? defaultValue;


        #endregion

        #region [ Hexadecimal character conversion of basic data type (value type). ]


        /// <summary>
        /// Converts a sbyte value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this sbyte v) => ((byte)v).ToHexadecimalString();


        /// <summary>
        /// Converts a byte value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this byte v) => v.ToString("X2");


        /// <summary>
        /// Converts  a short value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this short v) => ((ushort)v).ToHexadecimalString();


        /// <summary>
        /// Converts an ushort value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this ushort v) => v.ToString("X4");


        /// <summary>
        /// Converts an int value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this int v) => ((uint)v).ToHexadecimalString();


        /// <summary>
        /// Converts an uint value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this uint v) => v.ToString("X8");


        /// <summary>
        /// Converts a long value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this long v) => ((ulong)v).ToHexadecimalString();


        /// <summary>
        /// Converts an ulong value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalString(this ulong v) => v.ToString("X16");


        /// <summary>
        /// Converts a nullable sbyte value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this sbyte? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable byte value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this byte? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable short value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this short? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable ushort value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this ushort? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable int value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this int? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable uint value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this uint? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable long value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this long? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        /// <summary>
        /// Converts a nullable ulong value to hexadecimal representation string.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <param name="defaultValue">An alternative value returned when <paramref name="v"/> is null.</param>
        /// <returns>
        /// If <paramref name="v"/> is not null, returns the value, otherwise an alternative value specified in parameters.
        /// </returns>
        public static string ToHexadecimalString(this ulong? v, string defaultValue = "") => v?.ToHexadecimalString() ?? $"{defaultValue}";


        #endregion

        #region [ Byte array conversion of basic data type. (BigEndian fixed) ]


        /// <summary>The empty byte array</summary>
        static readonly byte[] emptyBytes = new byte[] { };


        /// <summary>
        /// Converts a ushort value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array.</returns>
        public static byte[] ToBigEndianBytes(this ushort v)
            => Enumerable.Range(0, 2).Select(i => (byte)(((0xFF00 >> 8 * i) & v) >> 8 * ((2 - 1) - i))).ToArray();

        /// <summary>
        /// Converts a short value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array.</returns>
        public static byte[] ToBigEndianBytes(this short v) => ((ushort)v).ToBigEndianBytes();


        /// <summary>
        /// Converts a uint value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array.</returns>
        public static byte[] ToBigEndianBytes(this uint v)
            => Enumerable.Range(0, 4).Select(i => (byte)(((0xFF000000 >> 8 * i) & v) >> 8 * ((4 - 1) - i))).ToArray();


        /// <summary>
        /// Converts a int value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array.</returns>
        public static byte[] ToBigEndianBytes(this int v) => ((uint)v).ToBigEndianBytes();


        /// <summary>
        /// Converts a ulong value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array.</returns>
        public static byte[] ToBigEndianBytes(this ulong v)
            => Enumerable.Range(0, 8).Select(i => (byte)(((0xFF00000000000000 >> 8 * i) & v) >> 8 * ((8 - 1) - i))).ToArray();


        /// <summary>
        /// Converts a long value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array.</returns>
        public static byte[] ToBigEndianBytes(this long v) => ((ulong)v).ToBigEndianBytes();


        /// <summary>
        /// Converts a nullable ushort value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array if <param name="v"> is not null, otherwise empty byte array.</returns>
        public static byte[] ToBigEndianBytes(this ushort? v) => v?.ToBigEndianBytes() ?? emptyBytes;


        /// <summary>
        /// Converts a nullable short value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array if <param name="v"> is not null, otherwise empty byte array.</returns>
        public static byte[] ToBigEndianBytes(this short? v) => v?.ToBigEndianBytes() ?? emptyBytes;


        /// <summary>
        /// Converts a nullable uint value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array if <param name="v"> is not null, otherwise empty byte array.</returns>
        public static byte[] ToBigEndianBytes(this uint? v) => v?.ToBigEndianBytes() ?? emptyBytes;


        /// <summary>
        /// Converts a nullable int value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array if <param name="v"> is not null, otherwise empty byte array.</returns>
        public static byte[] ToBigEndianBytes(this int? v) => v?.ToBigEndianBytes() ?? emptyBytes;


        /// <summary>
        /// Converts a nullable ulong value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array if <param name="v"> is not null, otherwise empty byte array.</returns>
        public static byte[] ToBigEndianBytes(this ulong? v) => v?.ToBigEndianBytes() ?? emptyBytes;


        /// <summary>
        /// Converts a nullable long value to Big-Endian byte array.
        /// </summary>
        /// <param name="v">A value to be converted.</param>
        /// <returns>The Big-Endian byte array if <param name="v"> is not null, otherwise empty byte array.</returns>
        public static byte[] ToBigEndianBytes(this long? v) => v?.ToBigEndianBytes() ?? emptyBytes;


        #endregion

        #region [ Hexadecimal string conversion of array. ]


        /// <summary>
        /// Considered the array of sbyte as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of sbyte.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<sbyte> array, string separator = "")
            => string.Join($"{separator}", (array ?? new sbyte[] { }).Select(v => v.ToHexadecimalString())) ?? string.Empty;


        /// <summary>
        /// Considered the array of byte as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of byte.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<byte> array, string separator = "")
            => string.Join($"{separator}", (array ?? new byte[] { }).Select(v => v.ToHexadecimalString())) ?? string.Empty;


        /// <summary>
        /// Considered the array of short as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of short.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<short> array, string separator = "")
            => string.Join($"{separator}", (array ?? new short[] { }).SelectMany(v => v.ToBigEndianBytes().Select(v2 => v2.ToHexadecimalString()))) ?? string.Empty;


        /// <summary>
        /// Considered the array of ushort as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of ushort.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<ushort> array, string separator = "")
            => string.Join($"{separator}", (array ?? new ushort[] { }).SelectMany(v => v.ToBigEndianBytes().Select(v2 => v2.ToHexadecimalString()))) ?? string.Empty;


        /// <summary>
        /// Considered the array of int as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of int.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<int> array, string separator = "")
            => string.Join($"{separator}", (array ?? new int[] { }).SelectMany(v => v.ToBigEndianBytes().Select(v2 => v2.ToHexadecimalString()))) ?? string.Empty;


        /// <summary>
        /// Considered the array of uint as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of uint.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<uint> array, string separator = "")
            => string.Join($"{separator}", (array ?? new uint[] { }).SelectMany(v => v.ToBigEndianBytes().Select(v2 => v2.ToHexadecimalString()))) ?? string.Empty;


        /// <summary>
        /// Considered the array of long as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of long.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<long> array, string separator = "")
            => string.Join($"{separator}", (array ?? new long[] { }).SelectMany(v => v.ToBigEndianBytes().Select(v2 => v2.ToHexadecimalString()))) ?? string.Empty;


        /// <summary>
        /// Considered the array of ulong as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of ulong.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<ulong> array, string separator = "")
            => string.Join($"{separator}", (array ?? new ulong[] { }).SelectMany(v => v.ToBigEndianBytes().Select(v2 => v2.ToHexadecimalString()))) ?? string.Empty;


        /// <summary>
        /// Considered the array of nullable sbyte as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable sbyte.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<sbyte?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new sbyte?[] { }).Select(v => v.ToHexadecimalString(nullByteReplacement))) ?? string.Empty;


        /// <summary>
        /// Considered the array of nullable byte as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable byte.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<byte?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new byte?[] { }).Select(v => v.ToHexadecimalString(nullByteReplacement))) ?? string.Empty;


        /// <summary>
        /// Considered the array of nullable short as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable short.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<short?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new short?[] { }).SelectMany(
                v => v?.ToBigEndianBytes()?.Select(b => b.ToHexadecimalString()) ?? Enumerable.Repeat(nullByteReplacement, 2)));


        /// <summary>
        /// Considered the array of nullable ushort as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable ushort.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<ushort?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new ushort?[] { }).SelectMany(
                v => v?.ToBigEndianBytes()?.Select(b => b.ToHexadecimalString()) ?? Enumerable.Repeat(nullByteReplacement, 2)));


        /// <summary>
        /// Considered the array of nullable int as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable int.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<int?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new int?[] { }).SelectMany(
                v => v?.ToBigEndianBytes()?.Select(b => b.ToHexadecimalString()) ?? Enumerable.Repeat(nullByteReplacement, 4)));


        /// <summary>
        /// Considered the array of nullable uint as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable uint.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<uint?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new uint?[] { }).SelectMany(
                v => v?.ToBigEndianBytes()?.Select(b => b.ToHexadecimalString()) ?? Enumerable.Repeat(nullByteReplacement, 4)));


        /// <summary>
        /// Considered the array of nullable long as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable long.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<long?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new long?[] { }).SelectMany(
                v => v?.ToBigEndianBytes()?.Select(b => b.ToHexadecimalString()) ?? Enumerable.Repeat(nullByteReplacement, 8)));


        /// <summary>
        /// Considered the array of nullable ulong as a byte array of BigEndian and convert it
        /// to a string of hexadecimal representation.
        /// </summary>
        /// <param name="array">The array of nullable ulong.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="nullByteReplacement">An alternative value replaced when a element of array is null.</param>
        /// <returns>A string represented hexadecimal.</returns>
        public static string ToHexadecimalStringAsBigEndianBytes(this IEnumerable<ulong?> array, string separator = "", string nullByteReplacement = "")
            => string.Join($"{separator}", (array ?? new ulong?[] { }).SelectMany(
                v => v?.ToBigEndianBytes()?.Select(b => b.ToHexadecimalString()) ?? Enumerable.Repeat(nullByteReplacement, 8)));


        #endregion
    }
}