/*
 * EnumerableExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;
using MinimalTools.DelegateObjects;

namespace MinimalTools.Extensions.Linq
{
    /// <summary>
    /// Extension methods for IEnumerable&lt;T&gt.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Apply the action for each elements.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="action">The action to be applied to each element.</param>
        public static void Foreach<T>(this IEnumerable<T> sequence, Action<T> action)
            => sequence.Foreach(v => true, action);


        /// <summary>
        /// Apply the action for each elements that match the condition.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate for judging whether to match.</param>
        /// <param name="action">The action to be applied to each element.</param>
        /// <exception cref="ArgumentNullException">sequence or predicate or action</exception> 
        public static void Foreach<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, Action<T> action)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (action == null) throw new ArgumentNullException(nameof(action));

            exec();

            void exec()
            {
                foreach (var v in sequence) if (predicate(v)) action(v);
            }
        }


        /// <summary>
        /// Apply the action for each elements.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="action">The action to be applied to each element. The second argument is as index.</param>
        public static void Foreach<T>(this IEnumerable<T> sequence, Action<T, int> action)
            => sequence.Foreach(v => true, action);


        /// <summary>
        /// Apply the action for each elements that match the condition.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate for judging whether to match.</param>
        /// <param name="action">The action to be applied to each element. The second argument is as index.</param>
        /// <exception cref="ArgumentNullException">source or predicate or action</exception>
        /// <remarks>
        /// This method is same as <code>foreach(var x in source.Where(predicate).Select((v, i) => new {v, i}) action(c, i))</code>.
        /// Attention is required since index becomes serial number after filtering.
        /// </remarks>
        public static void Foreach<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, Action<T, int> action)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (action == null) throw new ArgumentNullException(nameof(action));

            exec();

            void exec()
            {
                int i = 0;
                foreach (var v in sequence) if (predicate(v)) action(v, i++);
            }
        }


        /// <summary>
        /// Apply the action for each elements.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="action">A action that be applied to elements.</param>
        /// <returns>The sequence same as source sequence.</returns>
        /// <remarks>
        /// This is same as follow.
        /// <code>.Select(a => { action(a); return a;})</code>
        /// </remarks>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> sequence, Action<T> action) => sequence.Do(t => true, action);


        /// <summary>Apply the action for elements those satisfy the condition.</summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate that judge whether to apply action to the element.</param>
        /// <param name="action">A action that be applied to elements.</param>
        /// <returns>The sequence same as source sequence.</returns>
        /// <remarks>
        /// Action is applied to those that satisfy the condition, but do not narrow down and connect later.
        /// For example, if three of the 10 elements satisfy the condition,
        /// Processing only applies to three elements, but all subsequent 10 flows.
        /// </remarks>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, Action<T> action)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return exec();

            IEnumerable<T> exec()
            {
                foreach (var t in sequence)
                {
                    if (predicate(t)) action(t);
                    yield return t;
                }
                yield break;
            }
        }


        /// <summary>
        /// Removes the element that is null.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The sequence after removing null.</returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            return exec();

            IEnumerable<T> exec() => sequence.Where(v => v != null);
        }


        /// <summary>
        /// Converts sequence to dictionary safely. When the key is already exist, updates the value that corresponding to the key.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <typeparam name="TKey">A type of the key of dictionary.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="keySelector">A delegate to convert for each element to the key of dictionary.</param>
        /// <returns>An instance of dictionary.</returns>
        public static IDictionary<TKey, T> ToDictionarySafelyUpdate<T, TKey>(
            this IEnumerable<T> sequence,
            Func<T, TKey> keySelector) => sequence.ToDictionarySafely(keySelector, v => v, updateValueIfKeyAlreadyExists: true);


        /// <summary>
        /// Converts sequence to dictionary safely. When the key is already exist, updates the value that corresponding to the key.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <typeparam name="TKey">A type of the key of dictionary.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="keySelector">A delegate to convert for each element to the key of dictionary.</param>
        /// <param name="valueSelector">A delegate to convert for each element to the value of dictionary.</param>
        /// <returns>An instance of dictionary.</returns>
        public static IDictionary<TKey, TValue> ToDictionarySafelyUpdate<T, TKey, TValue>(
            this IEnumerable<T> sequence,
            Func<T, TKey> keySelector,
            Func<T, TValue> valueSelector) => sequence.ToDictionarySafely(keySelector, valueSelector, updateValueIfKeyAlreadyExists: true);


        /// <summary>
        /// Converts sequence to dictionary safely.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <typeparam name="TKey">A type of the key of dictionary.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="keySelector">A delegate to convert for each element to the key of dictionary.</param>
        /// <param name="updateValueIfKeyAlreadyExists">The strategy of update when the key already exists. The default is false, that means no updating the value.</param>
        /// <returns>An instance of dictionary.</returns>
        public static IDictionary<TKey, T> ToDictionarySafely<T, TKey>(
            this IEnumerable<T> sequence,
            Func<T, TKey> keySelector,
            bool updateValueIfKeyAlreadyExists = false) => sequence.ToDictionarySafely(keySelector, v => v, updateValueIfKeyAlreadyExists);


        /// <summary>
        /// Converts sequence to dictionary safely.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <typeparam name="TKey">A type of the key of dictionary.</typeparam>
        /// <typeparam name="TValue">A type of the value of dictionary.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="keySelector">A delegate to convert for each element to the key of dictionary.</param>
        /// <param name="valueSelector">A delegate to convert for each element to the value of dictionary.</param>
        /// <param name="updateValueIfKeyAlreadyExists">The strategy of update when the key already exists. The default is false, that means no updating the value.</param>
        /// <returns>An instance of dictionary.</returns>
        /// <exception cref="ArgumentNullException">sequence or keySelector or valueSelector</exception>
        public static IDictionary<TKey, TValue> ToDictionarySafely<T, TKey, TValue>(
            this IEnumerable<T> sequence,
            Func<T, TKey> keySelector,
            Func<T, TValue> valueSelector,
            bool updateValueIfKeyAlreadyExists = false)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (valueSelector == null) throw new ArgumentNullException(nameof(valueSelector));

            return exec();

            IDictionary<TKey, TValue> exec()
            {
                var dict = new Dictionary<TKey, TValue>();
                foreach (var item in sequence)
                {
                    var key = keySelector(item);
                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, valueSelector(item));
                    }
                    else if (updateValueIfKeyAlreadyExists)
                    {
                        dict[key] = valueSelector(item);
                    }
                }
                return dict;
            }
        }


        /// <summary>
        /// Buffers elements of the sequence by the specified number.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="count">The count of elements those to be buffered.</param>
        /// <returns>New sequence that include segmented sequence.</returns>
        /// <exception cref="ArgumentNullException">sequence</exception>
        public static IEnumerable<IEnumerable<T>> Buffer<T>(this IEnumerable<T> sequence, int count)
        {
            return sequence != null ? exec() : throw new ArgumentNullException(nameof(sequence));

            IEnumerable<IEnumerable<T>> exec()
            {
                var bufferCount = count < 1 ? 1 : count;

                // We can do it in a loop of {yeild return source.Take(bufferCount) ; source = source.Skip(bufferCount);} ,but it's inefficient.

                var buffer = new List<T>(bufferCount);
                foreach (var item in sequence)
                {
                    buffer.Add(item);
                    if (buffer.Count >= bufferCount)
                    {
                        yield return buffer.ToArray();
                        buffer.Clear();
                    }
                }

                if (buffer.Any()) yield return buffer.ToArray();
            }
        }


        /// <summary>
        /// Buffers elements that match the condition.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate that judge matching the condition.</param>
        /// <returns>New sequence that include segmented sequence.</returns>
        /// <exception cref="ArgumentNullException">sequence or predicate</exception>
        public static IEnumerable<IEnumerable<T>> BufferWhere<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return exec();

            IEnumerable<IEnumerable<T>> exec()
            {
                var buffer = new List<T>();

                foreach (var item in sequence)
                {
                    if (predicate(item))
                    {
                        buffer.Add(item);
                    }
                    else
                    {
                        if (buffer.Any())
                        {
                            yield return buffer.ToArray();
                            buffer.Clear();
                        }
                    }
                }

                if (buffer.Any()) yield return buffer.ToArray();

                yield break;
            }
        }


        /// <summary>
        /// Buffers elements while the condition is satisfied compared with the previous element.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate that judge matching the condition.</param>
        /// <returns>New sequence that include segmented sequence.</returns>
        /// <exception cref="ArgumentNullException">source or predicate</exception>
        /// <remarks>It may be useful when you want to do something like code break for a sorted sequence.</remarks>
        public static IEnumerable<IEnumerable<T>> BufferWhile<T>(this IEnumerable<T> sequence, Func<T, T, bool> predicate)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return exec();

            IEnumerable<IEnumerable<T>> exec()
            {
                var buffer = new List<T>();
                var isFirst = true;
                T prev = default(T);

                foreach (var item in sequence)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else if (!predicate(prev, item) && buffer.Any())
                    {
                        yield return buffer.ToArray();
                        buffer.Clear();
                    }

                    buffer.Add(item);
                    prev = item;
                }

                if (buffer.Any()) yield return buffer.ToArray();

                yield break;
            }
        }


        /// <summary>
        /// Buffers the elements alternating consecutive parts that do not satisfy 
        /// the condition and continuous parts that does not satisfy.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate that judge matching the condition.</param>
        /// <returns>New sequence that include segmented sequence.</returns>
        /// <exception cref="ArgumentNullException">sequence or predicate</exception>
        public static IEnumerable<IEnumerable<T>> BufferAlternative<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return exec();

            IEnumerable<IEnumerable<T>> exec()
            {
                var buffer = new List<T>();
                bool? prevCondition = null;
                foreach (var item in sequence)
                {
                    var currentCondition = predicate(item);
                    if (prevCondition.HasValue && prevCondition.Value != predicate(item))
                    {
                        yield return buffer.ToArray();
                        buffer.Clear();
                    }

                    buffer.Add(item);
                    prevCondition = currentCondition;
                }

                if (buffer.Any()) yield return buffer.ToArray();

                yield break;
            }
        }


        /// <summary>
        /// Concatenates any elements those specified in variable length parameters to sequence.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="values">Elements that to be concatinated the sequence.</param>
        /// <returns>The sequence that concatenated elements those specified in variable length parameters.</returns>
        /// <exception cref="ArgumentNullException">sequence</exception>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> sequence, params T[] values)
        {
            return sequence != null ? exec() : throw new ArgumentNullException(nameof(sequence));

            IEnumerable<T> exec()
            {
                foreach (var item in sequence) yield return item;
                if (values != null) foreach (var item in values) yield return item;
                yield break;
            }
        }


        /// <summary>
        /// Returns all elements equal to the maximum value.
        /// </summary>
        /// <typeparam name="T">A type of elements before convertion.</typeparam>
        /// <typeparam name="S">A type of elements after convertion.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="selector">A delegate that returns the element that compares the maximum value.</param>
        /// <returns>The sequence that contains only elements equal to the maximum value.</returns>
        /// <exception cref="ArgumentNullException">sequence or selector</exception>
        public static IEnumerable<S> Maxes<T, S>(this IEnumerable<T> sequence, Func<T, S> selector) where S : IComparable<S>
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return exec();

            IEnumerable<S> exec() => sequence.Select(t => selector(t)).Where(s => sequence.Max(selector).CompareTo(s) == 0);
        }


        /// <summary>
        /// Returns all elements equal to the maximum value.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The sequence that contains only elements equal to the maximum value.</returns>
        public static IEnumerable<T> Maxes<T>(this IEnumerable<T> sequence) where T : IComparable<T> => sequence.Maxes(t => t);


        /// <summary>
        /// Returns all elements equal to the minimum value.
        /// </summary>
        /// <typeparam name="T">A type of elements before convertion.</typeparam>
        /// <typeparam name="S">A type of elements after convertion.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="selector">A delegate that returns the element that compares the minimum value.</param>
        /// <returns>The sequence that contains only elements equal to the minimum value.</returns>
        /// <exception cref="ArgumentNullException">sequence or selector</exception>
        public static IEnumerable<S> Mins<T, S>(this IEnumerable<T> sequence, Func<T, S> selector) where S : IComparable<S>
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return exec();

            IEnumerable<S> exec() => sequence.Select(t => selector(t)).Where(s => sequence.Min(selector).CompareTo(s) == 0);
        }


        /// <summary>
        /// Returns all elements equal to the minimum value.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The sequence that contains only elements equal to the minimum value.</returns>
        public static IEnumerable<T> Mins<T>(this IEnumerable<T> sequence) where T : IComparable<T> => sequence.Mins(t => t);


        /// <summary>
        /// Zip to the longer one. Supplement with the default value for missing parts.
        /// </summary>
        /// <typeparam name="TFirst">A type of elements of first sequence.</typeparam>
        /// <typeparam name="TSecond">A type of elements of second sequence.</typeparam>
        /// <typeparam name="TResult">A type of elements of zipped sequence.</typeparam>
        /// <param name="sequence">The first sequence.</param>
        /// <param name="secondSequence">The second sequence.</param>
        /// <param name="resultSelector">A delegate that select the element of zipped sequence.</param>
        /// <returns>The zipped sequence.</returns>
        /// <exception cref="ArgumentNullException">sequence or secondSource or resultSelector</exception>
        public static IEnumerable<TResult> FullZip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> sequence,
            IEnumerable<TSecond> secondSequence,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (secondSequence == null) throw new ArgumentNullException(nameof(secondSequence));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return exec();

            IEnumerable<TResult> exec()
            {
                using (var s1 = sequence.GetEnumerator())
                using (var s2 = secondSequence.GetEnumerator())
                {
                    bool hasS1, hasS2;
                    while ((hasS1 = s1.MoveNext()) | (hasS2 = s2.MoveNext()))
                    {
                        yield return resultSelector(
                            hasS1 ? s1.Current : default(TFirst),
                            hasS2 ? s2.Current : default(TSecond));
                    }
                }

                yield break;
            }
        }


        /// <summary>
        /// Short cuts of string.join.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="separator">Delimiter, default is comma. When null is specified, it is replaced with string.Empty.</param>
        /// <returns>A joined string.</returns>
        /// <exception cref="ArgumentNullException">sequence</exception>
        public static string ConcatWith<T>(this IEnumerable<T> sequence, string separator = ",")
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            return string.Join(string.IsNullOrEmpty(separator) ? string.Empty : separator, sequence);
        }


        /// <summary>
        /// Short cuts of string.join with format.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="format">A format specification string.</param>
        /// <param name="separator">Delimiter, default is comma. When null is specified, it is replaced with string.Empty.</param>
        /// <param name="provider">A instance of implementation fo IFormatProvider.</param>
        /// <returns>A joined string.</returns>
        /// <exception cref="ArgumentNullException">sequence</exception>
        public static string ConcatWith<T>(
            this IEnumerable<T> sequence,
            string format,
            string separator = ",",
            IFormatProvider provider = null) where T : IFormattable
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            return ConcatWith(sequence.Select(c => c.ToString(format, provider)), separator);
        }


        /// <summary>
        /// Converts to Set.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">A delegate that to select element to be included to Set.</param>
        /// <returns>An instance of implementation fo ISet&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">sequence or predicate</exception>
        public static ISet<T> ToSet<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return new HashSet<T>(sequence.Where(predicate));
        }


        /// <summary>
        /// Converts to Set.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="comparer">Implementation of customized equality comparison.</param>
        /// <param name="predicate">A delegate that to select element to be included to Set.</param>
        /// <returns>An instance of implementation fo ISet&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">
        /// sequence
        /// or
        /// predicate
        /// or
        /// comparer
        /// </exception>
        public static ISet<T> ToSet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer, Func<T, bool> predicate)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            return new HashSet<T>(sequence.Where(predicate), comparer);
        }


        /// <summary>
        /// Converts to Set.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns>An instance of implementation fo ISet&lt;T&gt;.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> sequence) => sequence.ToSet(t => true);


        /// <summary>
        /// Converts to Set.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="comparer">Implementation of customized equality comparison.</param>
        /// <returns>An instance of implementation fo ISet&lt;T&gt;.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer) => sequence.ToSet(comparer, t => true);


        /// <summary>
        /// Left outer join.
        /// </summary>
        /// <typeparam name="TOuter">A type of elements of outer sequence.</typeparam>
        /// <typeparam name="TInner">A type of elements of inner sequence.</typeparam>
        /// <typeparam name="TKey">A type of the key using to join.</typeparam>
        /// <typeparam name="TResult">A type of the elements of new sequence that joined two sequences.</typeparam>
        /// <param name="outer">The outer sequence.</param>
        /// <param name="inner">The inner sequence.</param>
        /// <param name="outerKeySelector">A delegate that select the key that to be used to join of the outer sequence.</param>
        /// <param name="innerKeySelector">A delegate that select the key that to be used to join of the inner sequence.</param>
        /// <param name="resultSelector">A delegate that select the element of new sequence that joined two sequences.</param>
        /// <returns>New sequence that joined two sequences.</returns>
        /// <exception cref="ArgumentNullException">
        /// outer or inner or outerKeySelector or innerKeySelector or resultSelector
        /// </exception>
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return exec();

            IEnumerable<TResult> exec() =>
                outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (o, ary) => new { o, ary })
                .SelectMany(ann => ((ann.ary?.Any()) ?? false)
                    ? ann.ary.Select(i => resultSelector(ann.o, i))
                    : new TResult[] { resultSelector(ann.o, default(TInner)) });
        }


        /// <summary>
        /// Left outer join.
        /// </summary>
        /// <typeparam name="TOuter">A type of elements of outer sequence.</typeparam>
        /// <typeparam name="TInner">A type of elements of inner sequence.</typeparam>
        /// <typeparam name="TKey">A type of the key using to join.</typeparam>
        /// <typeparam name="TResult">A type of the elements of new sequence that joined two sequences.</typeparam>
        /// <param name="outer">The outer sequence.</param>
        /// <param name="inner">The inner sequence.</param>
        /// <param name="outerKeySelector">A delegate that select the key that to be used to join of the outer sequence.</param>
        /// <param name="innerKeySelector">A delegate that select the key that to be used to join of the inner sequence.</param>
        /// <param name="resultSelector">A delegate that select the element of new sequence that joined two sequences.</param>
        /// <param name="comparer">An instance of a class for comparing equality.</param>
        /// <returns>New sequence that joined two sequences.</returns>
        /// <exception cref="ArgumentNullException">
        /// outer or inner or outerKeySelector or innerKeySelector or resultSelector or comparer
        /// </exception>
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            return exec();
            IEnumerable<TResult> exec() =>
                outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (o, ary) => new { o, ary }, comparer)
                .SelectMany(ann => ((ann.ary?.Any()) ?? false)
                    ? ann.ary.Select(i => resultSelector(ann.o, i))
                    : new TResult[] { resultSelector(ann.o, default(TInner)) });
        }


        /// <summary>
        /// Inner join with arbitrary conditions, not keys.
        /// </summary>
        /// <typeparam name="TOuter">A type of elements of outer sequence.</typeparam>
        /// <typeparam name="TInner">A type of elements of inner sequence.</typeparam>
        /// <typeparam name="TResult">A type of the elements of new sequence that joined two sequences.</typeparam>
        /// <param name="outer">The outer sequence.</param>
        /// <param name="inner">The inner sequence.</param>
        /// <param name="predicate">A delegate that jadge whether to join.</param>
        /// <param name="resultSelector">A delegate that select the element of new sequence that joined two sequences.</param>
        /// <returns>New sequence that joined two sequences.</returns>
        /// <exception cref="ArgumentNullException">outer or inner or predicate or resultSelector</exception>
        /// <remarks>
        /// Be careful to use that if one or both sequences are too long, serious performance problems will result.
        /// </remarks>
        public static IEnumerable<TResult> LooseJoin<TOuter, TInner, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TInner, bool> predicate,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return exec();

            IEnumerable<TResult> exec() => outer.SelectMany(o => inner.Where(i => predicate(o, i)).Select(i => resultSelector(o, i)));
        }


        /// <summary>
        /// Left outer join with arbitrary conditions, not keys.
        /// </summary>
        /// <typeparam name="TOuter">A type of elements of outer sequence.</typeparam>
        /// <typeparam name="TInner">A type of elements of inner sequence.</typeparam>
        /// <typeparam name="TResult">A type of the elements of new sequence that joined two sequences.</typeparam>
        /// <param name="outer">The outer sequence.</param>
        /// <param name="inner">The inner sequence.</param>
        /// <param name="predicate">A delegate that jadge whether to join.</param>
        /// <param name="resultSelector">A delegate that select the element of new sequence that joined two sequences.</param>
        /// <returns>New sequence that joined two sequences.</returns>
        /// <exception cref="ArgumentNullException">outer or inner or predicate or resultSelector</exception>
        /// <remarks>
        /// Be careful to use that if one or both sequences are too long, serious performance problems will result.
        /// </remarks>
        public static IEnumerable<TResult> LooseLeftJoin<TOuter, TInner, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TInner, bool> predicate,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return exec();
            IEnumerable<TResult> exec() =>
                outer
                .Select(o => new { o, ary = inner.Where(i => predicate(o, i)) })
                .SelectMany(
                    ann => ((ann.ary?.Any()) ?? false)
                    ? ann.ary.Select(i => resultSelector(ann.o, i))
                    : new TResult[] { resultSelector(ann.o, default(TInner)) });
        }


        /// <summary>
        /// Gets the number of elements using Count property when the original collection
        /// implements ICollection or IReadOnlyCollection&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The number of count of collecton.</returns>
        /// <remarks>Since Linq's Count() seems to correspond, it may not need it.</remarks>
        public static int BestEffortCount<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));

            if (sequence is System.Collections.ICollection ic) return ic.Count;
            else if (sequence is System.Collections.Generic.IReadOnlyCollection<T> rc) return rc.Count;
            else return sequence.Count();
        }


        /// <summary>
        /// Distinct taking selector as argument.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <typeparam name="R">A type of element to be compared.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="selector">A delegate to select the element that be compared.</param>
        /// <returns>The sequence that exceped duplication.</returns>
        /// <exception cref="ArgumentNullException">
        /// source
        /// or
        /// selector
        /// </exception>
        public static IEnumerable<T> Distinct<T, R>(this IEnumerable<T> sequence, Func<T, R> selector)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return exec();

            IEnumerable<T> exec() =>
                sequence.Distinct(new DelegateEqualityComparer<T>(
                    (x, y) => selector(x).Equals(selector(y)), x => selector(x).GetHashCode()));
        }


        /// <summary>
        /// Repeat a sequence Infinitely.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns>After the last element of the sequence is returned, repeat from the beginning of the sequence.</returns>
        /// <remarks>
        /// Use it with caution as it will be an infinite loop.
        /// Use an upper limit with the Take () method, or zip with a finite sequence.
        /// </remarks>
        public static IEnumerable<T> Infinitely<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));

            return exec();

            IEnumerable<T> exec()
            {
                if (sequence.Any()) while (true) foreach (var x in sequence) yield return x;
            }
        }

        /// <summary>
        /// Repeat a sequence.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="repeatTimes">The repeating times</param>
        /// <returns>After the last element of the sequence is returned, repeat from the beginning of the sequence by specified repeating times.</returns>
        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int repeatTimes)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));

            return exec();

            IEnumerable<T> exec()
            {
                for (int i = 0; i < repeatTimes; i++) foreach (var x in sequence) yield return x;
            }
        }
    }
}