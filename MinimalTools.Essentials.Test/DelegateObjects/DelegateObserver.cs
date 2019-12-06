/*
 * Test for DelegateObserver
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using MinimalTools.DelegateObjects;
using Xunit;

namespace MinimalTools.Test.DelegateObjects
{
    /// <summary>
    /// Test for DelegateObserver
    /// </summary>
    public class DelegateObserver_Test
    {
        const string TEST = "this is test";

        [Fact(DisplayName = "No exception should be thrown if delegate dose not set.")]
        [Trait(nameof(DelegateObserver<int>), "constructor")]
        public void NoSpecifiedDelegates()
        {
            var testObservable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>();
            testObservable.Subscribe(observer);

            testObservable.OnNext(1);
            testObservable.OnError(new ArgumentException());
            testObservable.OnCompleted();
        }


        [Fact(DisplayName = "Specifying all delegate should work.")]
        [Trait(nameof(DelegateObserver<int>), "constructor")]
        public void SpecifyFull()
        {
            int t = 0;
            bool completed = false;
            Exception ee = null;

            var testObservable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>(i => t = i, exception => ee = exception, () => completed = true);
            testObservable.Subscribe(observer);

            testObservable.OnNext(1);
            t.Is(1);

            testObservable.OnError(new Exception());
            ee.IsNotNull();

            testObservable.OnCompleted();
            completed.IsTrue();
        }


        [Fact(DisplayName = "No exception should be thrown if delegate that null specified explicitly.")]
        [Trait(nameof(DelegateObserver<int>), "properties")]
        public void SetDelegatesToNull()
        {
            int t = 0;
            bool completed = false;
            Exception ee = null;

            var testObservable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>(i => t = i, exception => ee = exception, () => completed = true);
            testObservable.Subscribe(observer);

            observer.Next = null;
            observer.Error = null;
            observer.Completed = null;

            testObservable.OnNext(1);
            t.Is(0);

            testObservable.OnError(new Exception());
            ee.IsNull();

            testObservable.OnCompleted();
            completed.IsFalse();
        }


        [Fact(DisplayName = "'OnNext' method should work.")]
        [Trait(nameof(DelegateObserver<string>), nameof(DelegateObserver<string>.OnNext))]
        public void CallOnNext()
        {
            string test = null;
            var observer = new DelegateObserver<string>(s => test = s);

            observer.OnNext(TEST);
            test.Is(TEST);
        }


        [Fact(DisplayName = "'OnError' method should work.")]
        [Trait(nameof(DelegateObserver<string>), nameof(DelegateObserver<string>.OnError))]
        public void CallOnError()
        {
            Exception testExp = null;
            var observer = new DelegateObserver<string>(null, onError: exception => testExp = exception);

            observer.OnError(new NotImplementedException(TEST));
            testExp.IsInstanceOf<NotImplementedException>();
            (testExp as NotImplementedException)?.Message.Is(TEST);
        }


        [Fact(DisplayName = "'OnCompleted' method should work.")]
        [Trait(nameof(DelegateObserver<string>), nameof(DelegateObserver<string>.OnCompleted))]
        public void CallOnCompleted()
        {
            string completeMessage = null;
            var observer = new DelegateObserver<string>(null, onCompleted: () => completeMessage = TEST);

            observer.OnCompleted();
            completeMessage.Is(TEST);
        }
    }
}