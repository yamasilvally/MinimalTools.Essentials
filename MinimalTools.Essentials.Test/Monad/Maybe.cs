/*
 * Test for Maybe
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Linq;
using MinimalTools.Monad;
using Xunit;
using static MinimalTools.Test.Monad.TestResource;

namespace MinimalTools.Test.Monad
{
    public class TestResource
    {
        public const string ReferenceString = "this is test";
        public const int ReferenceInt = 10;
        public const string AlternateString = "this is alternate";
        public const string ExceptionMessage = "exception message.";
    }

    #region [ Maybe.Of ]

    /// <summary>
    /// Test for Maybe.Of
    /// </summary>
    public class Maybe_Of
    {
        [Fact(DisplayName = "An instance of Maybe<string> that has value shuold be created from non-null string.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void WithStringValue()
        {
            var m = Maybe.Of(ReferenceString);

            m.IsInstanceOf<Maybe<string>>();
            m.HasValue.IsTrue();
            m.Value.Is(ReferenceString);
        }


        [Fact(DisplayName = "An instance of Maybe<string> that has value shuold be created from empty string.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void WithEmptyString()
        {
            var m = Maybe.Of(string.Empty);

            m.IsInstanceOf<Maybe<string>>();
            m.HasValue.IsTrue();
            m.Value.Is(string.Empty);
        }


        [Fact(DisplayName = "An instance of Maybe<string> that has no value shuold be created from null string.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void WithNullValue()
        {
            var m = Maybe.Of(null as string);
            m.IsInstanceOf<Maybe<string>>();
            m.HasValue.IsFalse();
        }


        [Fact(DisplayName = "An instance of Maybe<int> that has value shuold be created from int value.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void WithStruct()
        {
            var m = Maybe.Of(ReferenceInt);

            m.IsInstanceOf<Maybe<int>>();
            m.HasValue.IsTrue();
            m.Value.Is(ReferenceInt);
        }


        [Fact(DisplayName = "An instance of Maybe<int?> that has value shuold be created from non-null int? value.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void WithNullableStruct()
        {
            int? v = ReferenceInt;
            var m = Maybe.Of(v);

            m.IsInstanceOf<Maybe<int?>>();
            m.HasValue.IsTrue();
            m.Value.Is(v);
        }


        [Fact(DisplayName = "An instance of Maybe<int?> that has no value shuold be created from null int? value.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void WithNullableStructThatHasNoValue()
        {
            var m = Maybe.Of(null as int?);

            m.IsInstanceOf<Maybe<int?>>();
            m.HasValue.IsFalse();
        }


        [Fact(DisplayName = "An empty Maybe instance should be created, and that dose not evaluate the illigal expression of mapping.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void NotEvaluateExpression()
            => Maybe.Of<string>(null, false).Map(s => s.Substring(10, 11));


        [Fact(DisplayName = "Exception that occurred in delegate should be ignored if an instance of Maybe created without specifying ignore-exception flag.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void EvaluateExpressionButIgnoreException()
            => Maybe.Of<string>("aaa").Map(s => s.Substring(10, 11));


        [Fact(DisplayName = "Exception that occurred in delegate should be thrown if an instance of Maybe created with explicit specifying ignore-exception flag to false.")]
        [Trait(nameof(Maybe), nameof(Maybe.Of))]
        public void ThrowException()
            => Assert.ThrowsAny<Exception>(() => Maybe.Of<string>("aaa", false).Map(s => s.Substring(10, 11)));
    }

    #endregion

    #region [ Maybe.Empty ]

    /// <summary>
    /// Test for Maybe.Empty.
    /// </summary>
    public class Maybe_Empty
    {
        [Fact(DisplayName = "A Maybe<string> instance that has no value should be created.")]
        [Trait(nameof(Maybe), nameof(Maybe.Empty))]
        public void WithString()
            => Maybe.Empty<string>().HasValue.IsFalse();


        [Fact(DisplayName = "A Maybe<int> instance that has no value should be created.")]
        [Trait(nameof(Maybe), nameof(Maybe.Empty))]
        public void WithStruct()
            => Maybe.Empty<int>().HasValue.IsFalse();


        [Fact(DisplayName = "A Maybe<int?> instance that has no value should be created.")]
        [Trait(nameof(Maybe), nameof(Maybe.Empty))]
        public void WithNullable()
            => Maybe.Empty<int?>().HasValue.IsFalse();


        [Fact(DisplayName = "The empty maybe should not evaluate illegal expression of mapping.")]
        [Trait(nameof(Maybe), nameof(Maybe.Empty))]
        public void DoseNotEvaluate()
            => Maybe.Empty<string>().Map(s => s.Substring(10, 20));


        [Fact(DisplayName = "An instance of empty-Maybe should throw InvalidOperationException when value property is accessed.")]
        [Trait(nameof(Maybe), nameof(Maybe.Empty))]
        public void InvalidOperation()
            => Assert.Throws<InvalidOperationException>(() => { var x = Maybe.Empty<string>().Value; });
    }

    #endregion

    #region [ OrElse ]

    /// <summary>
    /// Test for Maybe.OrElse.
    /// </summary>
    public class Maybe_OrElse
    {
        [Fact(DisplayName = "Inner value should be returned if an instance of Maybe has value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrElse))]
        public void ReturnsInnerValue()
            => Maybe.Of(ReferenceString).OrElse(AlternateString).Is(ReferenceString);


        [Fact(DisplayName = "Inner value should be returned if an instance of Maybe has value, and ignore delegate in argument.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrElse))]
        public void ReturnsInnerValueNotGenerateValue()
            => Maybe.Of(ReferenceString).OrElse(() => AlternateString).Is(ReferenceString);


        [Fact(DisplayName = "Alternative value that specified in argument should be returned if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrElse))]
        public void ReturnsAlternateValue()
            => Maybe.Of(null as string).OrElse(AlternateString).Is(AlternateString);


        [Fact(DisplayName = "Alternative value that generated by delegate that specified in argument should be returned if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrElse))]
        public void ReturnsAlternateValueThatGenerated()
            => Maybe.Of(null as string).OrElse(() => AlternateString).Is(AlternateString);


        [Fact(DisplayName = "Null value that specified in argument should be returned if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrElse))]
        public void ReturnsNull()
            => Maybe.Of(null as string).OrElse(null as string).IsNull();


        [Fact(DisplayName = "ArgumentNullException should be thrown if supplier is null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrElse))]
        public void ThrowsException()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Of(null as string).OrElse(null as Func<string>))
                .ParamName.Is("supplier");
    }

    #endregion

    #region [ OrDefault ]

    /// <summary>
    /// Test for Maybe.OrDefault.
    /// </summary>
    public class Maybe_OrDefault
    {
        [Fact(DisplayName = "Inner value should be returned if an instance of Maybe has value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrDefault))]
        public void HasValue()
            => Maybe.Of(ReferenceString).OrDefault().Is(ReferenceString);


        [Fact(DisplayName = "The default value for each type should be returned if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrDefault))]
        public void NotHasValue()
            => Maybe.Empty<string>().OrDefault().IsNull();


        [Fact(DisplayName = "The default(int) should be returned if an instance of Maybe<int> has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrDefault))]
        public void IntDafalutIsZero()
            => Maybe.Empty<int>().OrDefault().Is(default(int));


        [Fact(DisplayName = "Null as int? should be returned if an instance of Maybe<int?> has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrDefault))]
        public void NullableIntDafalutIsNull()
            => Maybe.Empty<int?>().OrDefault().IsNull();
    }

    #endregion

    #region [ OrThrow ]

    /// <summary>
    /// Test of Maybe.OrThrow.
    /// </summary>
    public class Maybe_OrThrow
    {
        [Fact(DisplayName = "Inner value should be returned if an instance of Maybe has value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ReturnsValue()
            => Maybe.Of(ReferenceString).OrThrow(new Exception()).Is(ReferenceString);


        [Fact(DisplayName = "Inner value should be returned if an instance of Maybe has value, ignore exception-supplier.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ReturnsValueIgnoreExceptionSupplier()
            => Maybe.Of(ReferenceString).OrThrow(() => new Exception()).Is(ReferenceString);


        [Fact(DisplayName = "Exception that specified in argument should be thrown if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ThrowExceptionInArgument()
            => Assert.Throws<Exception>(() => Maybe.Of(null as string).OrThrow(new Exception(ExceptionMessage)))
                .Message.Is(ExceptionMessage);


        [Fact(DisplayName = "Exception that generated by exception-supplier that specified in argument should be thrown if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ThrowExceptionThatGeneratedByExceptionSupplier()
            => Assert.Throws<Exception>(() => Maybe.Of(null as string).OrThrow(() => new Exception(ExceptionMessage)))
                .Message.Is(ExceptionMessage);


        [Fact(DisplayName = "When exception that specified in argument is null, it should throw ArgumentNullException.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ExceptionIsNull()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Empty<string>().OrThrow(null as Exception))
                .ParamName.Is("exception");


        [Fact(DisplayName = "ArgumentNullException should be trownn if exception-supplier that specified in argument is null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ExceptionSupplierIsNull()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Empty<string>().OrThrow(null as Func<Exception>))
                .ParamName.Is("exceptionSupplier");


        [Fact(DisplayName = "NullReferenceException should be thrown if exception-supplier returns null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.OrThrow))]
        public void ExceptionSupplierReturnsNull()
            => Assert.Throws<NullReferenceException>(() => Maybe.Empty<string>().OrThrow(() => null as Exception));
    }

    #endregion

    #region [ Map ]

    /// <summary>
    /// Test for Maybe.Map.
    /// </summary>
    public class Maybe_Map
    {
        [Fact(DisplayName = "String to string mapping should success.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Map))]
        public void MapToSameType()
            => Maybe.Of(ReferenceString).Map(s => s + s).Value.Is(ReferenceString + ReferenceString);


        [Fact(DisplayName = "String to int mapping should success.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Map))]
        public void MapToAnotherType()
        {
            var m = Maybe.Of(ReferenceString).Map(s => s.Length);
            m.IsInstanceOf<Maybe<int>>();
            m.Value.Is(ReferenceString.Length);
        }


        [Fact(DisplayName = "Even if an exception occurred in the mapper, it should be ignored the exception and returned Empty-Maybe.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Map))]
        public void MapFailureReturnsEmpty()
            => Maybe.Of(ReferenceString).Map(s => s.Substring(1000, 0)).HasValue.IsFalse();


        [Fact(DisplayName = "Exception that occurred in the mapper should be thrown if 'ignore-exception' flag specified false.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Map))]
        public void MapFailureThrowsException()
            => Assert.ThrowsAny<Exception>(() => Maybe.Of(ReferenceString, false).Map(s => s.Substring(1000, 0)));


        [Fact(DisplayName = "ArgumentNullException should be thrown if the mapper is null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Map))]
        public void MapperIsNull()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Of(ReferenceString).Map(null as Func<string, string>))
                .ParamName.Is("mapper");
    }

    #endregion

    #region [ Filter ]

    /// <summary>
    /// Test for Maybe.Filter.
    /// </summary>
    public class Maybe_Filter
    {
        [Fact(DisplayName = "An instance of Maybe that has value should be returned if value matches the condition.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Filter))]
        public void Match()
            => Maybe.Of(ReferenceString).Filter(s => s.Length > 0).HasValue.IsTrue();


        [Fact(DisplayName = "An empty-Maybe should be returned if value dose not match the condition.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Filter))]
        public void Unmatch()
            => Maybe.Of(ReferenceString).Filter(s => s == "test").HasValue.IsFalse();


        [Fact(DisplayName = "An empty-Maybe should not evaluate the condition, and returns its own.")]
        [Trait(nameof(Maybe), nameof(Maybe<int>.Filter))]
        public void IgnoreCondition()
            => Maybe.Empty<int>().Filter(i => i % 2 == 0).HasValue.IsFalse();


        [Fact(DisplayName = "Even if an exception occurred in the predicate, it should ignore the exception and returns Empty-Maybe.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Filter))]
        public void FilterFailureReturnsEmpty()
            => Maybe.Of(ReferenceString).Filter(s => s.Substring(1000, 0) != "T").HasValue.IsFalse();


        [Fact(DisplayName = "When 'ignoreExceptions' flag specified true, it should throw the exception that occured in the predicate.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Filter))]
        public void FilterFailureThrowsException()
            => Assert.ThrowsAny<Exception>(() => Maybe.Of(ReferenceString, false).Filter(s => s.Substring(1000, 0) == "T"));


        [Fact(DisplayName = "ArgumentNullException should be thrown if the predicate is null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Filter))]
        public void MapperIsNull()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Of(ReferenceString).Filter(null as Func<string, bool>))
                .ParamName.Is("predicate");
    }

    #endregion

    #region [ Flatmap ]

    /// <summary>
    /// Test for Maybe.Flatmap.
    /// </summary>
    public class Maybe_Flatmap
    {
        [Fact(DisplayName = "Maybe<string> to Maybe<string> mapping should success.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Flatmap))]
        public void MapToSameType()
            => Maybe.Of(ReferenceString).Flatmap(s => Maybe.Of(s)).Value.Is(ReferenceString);


        [Fact(DisplayName = "Maybe<string> to Maybe<int> mapping should success.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Flatmap))]
        public void MapToAnotherType()
            => Maybe.Of(ReferenceString).Flatmap(s => Maybe.Of(s.Length)).Value.Is(ReferenceString.Length);


        [Fact(DisplayName = "Maybe<string> to Empty-Maybe mapping should success.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Flatmap))]
        public void ExchangeToEmptyMaybe()
            => Maybe.Of(ReferenceString).Flatmap(s => Maybe.Empty<int>()).HasValue.IsFalse();


        [Fact(DisplayName = "Even if an exception occurred in the mapper, it should ignore the exception and returns Empty-Maybe.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Flatmap))]
        public void MapFailureReturnsEmpty()
            => Maybe.Of(ReferenceString).Flatmap(s => Maybe.Of(s.Substring(1000, 0))).HasValue.IsFalse();


        [Fact(DisplayName = "When 'ignoreExceptions' flag specified true, it should throw the exception that occured in the mapper.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Flatmap))]
        public void MapFailureThrowsException()
            => Assert.ThrowsAny<Exception>(() => Maybe.Of(ReferenceString, false).Flatmap(s => Maybe.Of(s.Substring(1000, 0))));


        [Fact(DisplayName = "ArgumentNullException should be thrown if the mapper is null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.Flatmap))]
        public void MapperIsNull()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Of(ReferenceString).Flatmap(null as Func<string, Maybe<string>>))
                .ParamName.Is("mapper");
    }

    #endregion

    #region [ IfPresent ]

    /// <summary>
    /// Test for Maybe.IfPresent.
    /// </summary>
    public class Maybe_IfPresent
    {
        [Fact(DisplayName = "if an instance of Maybe has value, it should execute action that specified in argument.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresent))]
        public void ExecuteAction()
        {
            int count = 0;
            Maybe.Of(ReferenceString).IfPresent(s => count = s.Length);

            count.Is(ReferenceString.Length);
        }


        [Fact(DisplayName = "if an instance of Maybe has no value, it should not execute action that specified in argument.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresent))]
        public void NotExecuteAction()
        {
            int count = 1;
            Maybe.Empty<string>().IfPresent(s => count = s.Length);

            count.Is(1);
        }


        [Fact(DisplayName = "An illegal action should not be evaluated if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresent))]
        public void NotEvaluateAction()
            => Maybe.Empty<string>().IfPresent(s => s.Substring(1000, 0));


        [Fact(DisplayName = "ArgumentNullException should be thrown if action is null.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresent))]
        public void ThrowsArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => Maybe.Of(ReferenceString).IfPresent(null as Action<string>))
                .ParamName.Is("action");


        [Fact(DisplayName = "Null action should be ignored if an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresent))]
        public void IgnoreNullActionWhenMaybeIsEmpty()
            => Maybe.Empty<string>().IfPresent(null as Action<string>);
    }

    #endregion

    #region [ IfPresentThen ]

    /// <summary>
    /// Test for Maybe.IfPresentThen.
    /// </summary>
    public class Maybe_IfPresentThen
    {
        [Fact(DisplayName = "It should execute action specified in argument.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresentThen))]
        public void ExecuteThenAction()
        {
            int THEN = 0;
            int ELSE = 0;

            Maybe.Of(ReferenceString).IfPresentThen(s => THEN = s.Length).Else(() => ELSE = 1);

            THEN.Is(ReferenceString.Length);
            ELSE.Is(0);
        }


        [Fact(DisplayName = "It should execute action specified in argument for 'Else'.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresentThen))]
        public void ExecuteElseAction()
        {
            int THEN = 0;
            int ELSE = 0;
            int expect = -1;

            Maybe.Of(null as string).IfPresentThen(s => THEN = s.Length).Else(() => ELSE = expect);

            THEN.Is(0);
            ELSE.Is(expect);
        }


        [Fact(DisplayName = "No action should be executed if primary action is null and an instance of Maybe has a value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresentThen))]
        public void IgnoreNullAction()
        {
            int ELSE = int.MinValue;
            Maybe.Of(ReferenceString).IfPresentThen(null as Action<string>).Else(() => ELSE = int.MaxValue);
            ELSE.Is(int.MinValue);
        }


        [Fact(DisplayName = "No action should be executed if secondary action is null and an instance of Maybe has no value.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresentThen))]
        public void IgnoreNullActionForElse()
        {
            int THEN = int.MinValue;
            Maybe.Empty<string>().IfPresentThen(s => THEN = s.Length).Else(null as Action);
            THEN.Is(int.MinValue);
        }


        [Fact(DisplayName = "Nothing should happen even if any argument does not specified.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.IfPresentThen))]
        public void NoParameter()
            => Maybe.Of(ReferenceString).IfPresentThen().Else();
    }

    #endregion

    #region [ IEnumerable compatible ]

    /// <summary>
    /// Test for IEnumerable compatible.
    /// </summary>
    public class Maybe_IEnumerable
    {
        [Fact(DisplayName = "It should be possible to using as IEnumerable.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.GetEnumerator))]
        public void AsEnumerableContainsValue()
            => Maybe.Of(ReferenceString).Where(s => s.Length > 0).FirstOrDefault().Is(ReferenceString);


        [Fact(DisplayName = "If an instance of Maybe has no value, it should be just like a sequence that contain no element.")]
        [Trait(nameof(Maybe), nameof(Maybe<string>.GetEnumerator))]
        public void AsEnumerableContainsNoValue()
            => Maybe.Of(null as string).FirstOrDefault().IsNull();
    }

    #endregion

    #region [ ToMaybe ]

    /// <summary>
    /// Test for MaybeExtensions.ToMaybe.
    /// </summary>
    public class Maybe_ToMaybe
    {
        [Fact(DisplayName = "An instance of Maybe that has value should be created.")]
        [Trait(nameof(MaybeExtensions), nameof(MaybeExtensions.ToMaybe))]
        public void ExistValue()
        {
            var m = ReferenceString.ToMaybe();
            m.IsInstanceOf<Maybe<string>>();
            m.Value.Is(ReferenceString);
            m.Map(s => s.Substring(999)).HasValue.IsFalse();
        }


        [Fact(DisplayName = "An empty-Maybe should be created.")]
        [Trait(nameof(MaybeExtensions), nameof(MaybeExtensions.ToMaybe))]
        public void NotExistValue()
        {
            string source = null;
            var m = source.ToMaybe();
            m.IsInstanceOf<Maybe<string>>();
            m.HasValue.IsFalse();
            m.Map(s => s.Substring(999)).HasValue.IsFalse();
        }


        [Fact(DisplayName = "Exception should be thrown if 'ignoreException' flag specified false.")]
        [Trait(nameof(MaybeExtensions), nameof(MaybeExtensions.ToMaybe))]
        public void RaiseException()
            => Assert.ThrowsAny<Exception>(() => Maybe.Of(ReferenceString, false).Map(s => s.Substring(999)));
    }

    #endregion
}