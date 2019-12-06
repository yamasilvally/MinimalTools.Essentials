﻿/*
 * Maybe
 *
 * Copyright (c) 2018 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace MinimalTools.Monad
{
    /// <summary>
    /// Factory methods of Maybe.
    /// </summary>
    public class Maybe
    {
        /// <summary>
        /// Creates an instance of Maybe.
        /// </summary>
        /// <typeparam name="T">A type of the value to be stored.</typeparam>
        /// <param name="value">The value to be stored.</param>
        /// <param name="ignoreExceptions">
        /// whether to ignore exception when delegate executed by conversion method throws exception.
        /// </param>
        /// <returns>An instance of Maybe.</returns>
        public static Maybe<T> Of<T>(T value, bool ignoreExceptions = true)
            => new Maybe<T>(value, ignoreExceptions: ignoreExceptions);


        /// <summary>
        /// Creates an instance of Empty-Maybe that has no value.
        /// </summary>
        /// <typeparam name="T">A type of the value to be stored.</typeparam>
        /// <returns>An instance of Empty-Maybe that has no value.</returns>
        public static Maybe<T> Empty<T>() => new Maybe<T>(default(T), empty: true);
    }


    /// <summary>
    /// The struct that represent contexts that may have failed.
    /// </summary>
    /// <typeparam name="T">A type of the value to be stored.</typeparam>
    /// <remarks>
    /// This struct is same as Optional in Java.
    /// 
    /// Now that we can do the same thing with a combination of null condition operator and null coalesce operator,
    /// this struct has become meaningless.
    /// It is effective when you want to dare declaratively describe.
    /// 
    /// Since IEnumerable&lt;T&gt; is also implemented, it also functions as a container with
    /// zero or one element. This makes it possible to use it in the extension methods of System.Linq.
    /// 
    /// </remarks>
    public struct Maybe<T> : IEnumerable<T>
    {
        #region [ fields ]


        /// <summary>The value to be stored.</summary>
        readonly T value;


        /// <summary>Whether the value is stored.</summary>
        readonly bool hasValue;


        /// <summary>
        /// Whether to ignore exceptions when delegate executed by conversion method throws an exception.
        /// </summary>
        readonly bool ignore;


        #endregion

        #region [ constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> class.
        /// </summary>
        /// <param name="value">The value to be stored.</param>
        /// <param name="empty">If true, creates a instance without value.</param>
        /// <param name="ignoreExceptions">
        /// whether to ignore exceptions when delegate executed by conversion method throws an exception.
        /// </param>
        internal Maybe(T value, bool empty = false, bool ignoreExceptions = true)
        {
            if (!empty && value != null)
            {
                this.value = value;
                this.hasValue = true;
                this.ignore = ignoreExceptions;
            }
            else
            {
                this.value = default(T);
                this.hasValue = false;
                this.ignore = ignoreExceptions;
            }
        }


        #endregion

        #region [ properties ]


        /// <summary>Whether the value is stored.</summary>
        public bool HasValue => this.hasValue;


        /// <summary>The value.</summary>
        /// <exception cref="InvalidOperationException">no value exist.</exception>
        /// <remarks>
        /// If thera is no value, this method throws InvalidOperationException.
        /// </remarks>
        public T Value => this.hasValue ? this.value : throw new InvalidOperationException("no value exist.");


        #endregion

        #region [ methods ]


        /// <summary>
        /// Returns the value. If there is no value, returns an alternative value.
        /// </summary>
        /// <param name="alternativeValue">An alternative value.</param>
        /// <returns>The value.</returns>
        public T OrElse(T alternativeValue) => this.hasValue ? this.value : alternativeValue;


        /// <summary>
        /// Returns the value. If there is no value, returns an default value for each type.
        /// </summary>
        /// <returns>The value.</returns>
        public T OrDefault() => this.OrElse(default(T));


        /// <summary>
        /// Returns the value. If there is no value, returns an alternative value generated by <paramref name="supplier"/>.
        /// </summary>
        /// <param name="supplier">A delegate to generate alternative value.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">supplier</exception>
        public T OrElse(Func<T> supplier)
            => supplier != null ? this.hasValue ? this.value : supplier() : throw new ArgumentNullException(nameof(supplier));


        /// <summary>
        /// Returns the value. If there is no value, throws <paramref name="exception"/>.
        /// </summary>
        /// <typeparam name="X">A type of exception.</typeparam>
        /// <param name="exception">Exception thrown when there is no value.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">exception</exception>
        public T OrThrow<X>(X exception) where X : Exception
            => exception != null ? this.hasValue ? this.value : throw exception : throw new ArgumentNullException(nameof(exception));


        /// <summary>
        /// Returns the value. If there is no value, throws exception generated by <paramref name="exceptionSupplier"/>.
        /// </summary>
        /// <typeparam name="X">A type of exception.</typeparam>
        /// <param name="exceptionSupplier">A delegate to generate exception.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">exceptionSupplier</exception>
        public T OrThrow<X>(Func<X> exceptionSupplier) where X : Exception
            => exceptionSupplier != null ? this.hasValue ? this.value : throw exceptionSupplier() : throw new ArgumentNullException(nameof(exceptionSupplier));


        /// <summary>
        /// Do mapping.
        /// </summary>
        /// <typeparam name="S">The type of the result of the mapping function.</typeparam>
        /// <param name="mapper">A mapping function to apply to the value.</param>
        /// <returns>
        /// If a value is present, returns an instance containing the result of applying a mapping function
        /// to the value of this Maybe, otherwise an Empty-Maybe.
        /// </returns>
        /// <exception cref="ArgumentNullException">mapper</exception>
        public Maybe<S> Map<S>(Func<T, S> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            if (this.hasValue)
            {
                try
                {
                    return new Maybe<S>(mapper(this.value), ignoreExceptions: this.ignore);
                }
                catch
                {
                    if (!this.ignore) throw;
                }
            }

            return new Maybe<S>(default(S), empty: true);
        }


        /// <summary>
        /// Do Filtering.
        /// </summary>
        /// <param name="predicate">A predicate to apply to the value, if present.</param>
        /// <returns>
        /// If a value is present and the value matches the given predicate, return this instance, otherwise
        /// an Empty-Maybe.
        /// </returns>
        /// <exception cref="ArgumentNullException">predicate</exception>
        public Maybe<T> Filter(Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            if (this.hasValue)
            {
                try
                {
                    if (predicate(this.value)) return this;
                }
                catch
                {
                    if (!this.ignore) throw;
                }
            }

            return new Maybe<T>(default(T), empty: true);
        }


        /// <summary>
        /// Do mapping and flatten.
        /// </summary>
        /// <typeparam name="S">The type of the result of the mapping function.</typeparam>
        /// <param name="mapper">A mapping function to apply to the value.</param>
        /// <returns>
        /// If a value is present, an instance of Maybe&lt;S&gt;, otherwise an Empty-Maybe&lt;S&gt;.
        /// </returns>
        /// <exception cref="ArgumentNullException">mapper</exception>
        public Maybe<S> Flatmap<S>(Func<T, Maybe<S>> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            if (this.hasValue)
            {
                try
                {
                    return mapper(this.value);  // ignore-flag is not inherited
                }
                catch
                {
                    if (!this.ignore) throw;
                }
            }
            return new Maybe<S>(default(S), empty: true);
        }


        /// <summary>
        /// Performs action when there is a value.
        /// </summary>
        /// <param name="action">An action performed when there is a value.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        public void IfPresent(Action<T> action)
        {
            if (this.hasValue)
            {
                if (action == null) throw new ArgumentNullException(nameof(action));
                action(this.value);
            }
        }


        /// <summary>
        /// Performs action when there is a value.
        /// </summary>
        /// <param name="action">An action performed when there is a value.</param>
        /// <returns>
        /// A struct that invokes another action for when there is no value.
        /// </returns>
        public ElseSection IfPresentThen(Action<T> action = null)
        {
            if (this.hasValue)
            {
                action?.Invoke(this.value);
                return new ElseSection(false);
            }
            else
            {
                return new ElseSection(true);
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this.hasValue) yield return this.value;
            yield break;
        }


        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


        #endregion
    }


    /// <summary>
    /// A struct for return value of Maybe.IfPresentThen().
    /// </summary>
    public struct ElseSection
    {
        /// <summary>Whether invokes action.</summary>
        readonly bool canExecute;


        /// <summary>
        /// Initializes a new instance of the <see cref="ElseSection"/> class.
        /// </summary>
        /// <param name="canExecute">Whether invokes action.</param>
        internal ElseSection(bool canExecute) => this.canExecute = canExecute;


        /// <summary>
        /// Performs action when there is no value.
        /// </summary>
        /// <param name="action">An action performed when there is no value.</param>
        public void Else(Action action = null)
        {
            if (this.canExecute) action?.Invoke();
        }
    }

    /// <summary>
    /// An extension method for Maybe.
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        /// Converts to instance of Maybe
        /// </summary>
        /// <typeparam name="T">The type of a value to be stored.</typeparam>
        /// <param name="value">A value to be stored.</param>
        /// <param name="ignoreExceptions">
        /// Whether to ignore exceptions when delegate executed by conversion method throws an exception.
        /// </param>
        /// <returns>An instance of Maybe.</returns>
        public static Maybe<T> ToMaybe<T>(this T value, bool ignoreExceptions = true)
             => Maybe.Of(value, ignoreExceptions);
    }
}