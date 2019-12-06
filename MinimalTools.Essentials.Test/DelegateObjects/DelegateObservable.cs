/*
 * Test for DelegateObservable
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
    /// Test for DelegateObservable.
    /// </summary>
    public class DelegateObservable_Test
    {
        const string TEST = "this is test";

        [Fact(DisplayName = "No exception should be thrown if delegate dose not set.")]
        [Trait(nameof(DelegateObservable<int>), "constructor")]
        public void NonParameterConstructor()
        {
            var observable = new DelegateObservable<int>();
            var disposer = observable.Subscribe(new DelegateObserver<int>());
            disposer.Dispose();
        }


        [Fact(DisplayName = "It should work when delegate set at construct.")]
        [Trait(nameof(DelegateObservable<int>), "constructor")]
        public void ConstructorWithParameter()
        {
            var subject = new DelegateObserver<int>();

            IDisposable exec(IObserver<int> o)
            {
                subject.Next += o.OnNext;
                subject.Error += o.OnError;
                subject.Completed += o.OnCompleted;

                return new DelegateDisposable(() =>
                {
                    subject.Next -= o.OnNext;
                    subject.Error -= o.OnError;
                    subject.Completed -= o.OnCompleted;
                });
            }

            var observable = new DelegateObservable<int>(exec);

            int next = 0;
            Exception exception = null;
            bool completed = false;

            var observer = new DelegateObserver<int>(i => next = i, eee => exception = eee, () => completed = true);
            var disposer = observable.Subscribe(observer);

            subject.OnNext(3);
            subject.OnError(new ArgumentNullException(TEST));
            subject.OnCompleted();

            // assert
            next.Is(3);
            exception.IsInstanceOf<ArgumentNullException>();
            (exception as ArgumentNullException)?.ParamName.Is(TEST);
            completed.IsTrue();

            next = 0;
            exception = null;
            completed = false;

            // release subscribe.
            disposer.Dispose();

            subject.OnNext(3);
            subject.OnError(new ArgumentOutOfRangeException(TEST));
            subject.OnCompleted();

            next.Is(0);
            exception.IsNull();
            completed.IsFalse();
        }


        [Fact(DisplayName = "Confirm registration and cancellation of multiple subscribers.")]
        [Trait(nameof(DelegateObservable<int>), nameof(DelegateObservable<int>.Subscribe))]
        public void ManySubscriber()
        {
            var subject = new DelegateObserver<int>();

            IDisposable exec(IObserver<int> o)
            {
                subject.Next += o.OnNext;
                subject.Error += o.OnError;
                subject.Completed += o.OnCompleted;

                return new DelegateDisposable(() =>
                {
                    subject.Next -= o.OnNext;
                    subject.Error -= o.OnError;
                    subject.Completed -= o.OnCompleted;
                });
            }

            var observable = new DelegateObservable<int>();
            observable.DelegateOfSubscribe = exec;

            int next1 = 0;
            Exception exception1 = null;
            bool completed1 = false;
            var observer1 = new DelegateObserver<int>(i => next1 = i, eee => exception1 = eee, () => completed1 = true);
            var disposer1 = observable.Subscribe(observer1);

            int next2 = 0;
            Exception exception2 = null;
            bool completed2 = false;
            var observer2 = new DelegateObserver<int>(i => next2 = i, eee => exception2 = eee, () => completed2 = true);
            var disposer2 = observable.Subscribe(observer2);

            // onNext
            subject.OnNext(3);
            next1.Is(3);
            next2.Is(3);

            // onError
            subject.OnError(new ArgumentOutOfRangeException(TEST));
            exception1.IsInstanceOf<ArgumentOutOfRangeException>();
            (exception1 as ArgumentOutOfRangeException)?.ParamName.Is(TEST);
            exception2.IsInstanceOf<ArgumentOutOfRangeException>();
            (exception2 as ArgumentOutOfRangeException)?.ParamName.Is(TEST);

            // onComplete
            subject.OnCompleted();
            completed1.IsTrue();
            completed2.IsTrue();

            // re-initialize
            next1 = 0;
            exception1 = null;
            completed1 = false;
            next2 = 0;
            exception2 = null;
            completed2 = false;

            // disposing only disposer1.
            disposer1.Dispose();

            subject.OnNext(6);
            next1.Is(0);
            next2.Is(6);

            subject.OnError(new NotSupportedException(TEST));
            exception1.IsNull();
            //
            exception2.IsInstanceOf<NotSupportedException>();
            (exception2 as NotSupportedException)?.Message.Is(TEST);

            subject.OnCompleted();
            completed1.IsFalse();
            completed2.IsTrue();
        }


        [Fact(DisplayName = "It is possible to reset null to delegate even after calling Subscribe. [Use carefully as it may leak memory]")]
        [Trait(nameof(DelegateObservable<int>), nameof(DelegateObservable<int>.Subscribe))]
        public void SetNull()
        {
            var subject = new DelegateObserver<int>();

            IDisposable exec(IObserver<int> o)
            {
                subject.Next += o.OnNext;
                subject.Error += o.OnError;
                subject.Completed += o.OnCompleted;

                return new DelegateDisposable(() =>
                {
                    subject.Next -= o.OnNext;
                    subject.Error -= o.OnError;
                    subject.Completed -= o.OnCompleted;
                });
            }

            var observable = new DelegateObservable<int>(exec);

            int next = 0;
            Exception exception = null;
            bool completed = false;

            var observer = new DelegateObserver<int>(i => next = i, eee => exception = eee, () => completed = true);
            var disposer = observable.Subscribe(observer);

            // Since it should be related to the subject, it should react even if replacing delegate.
            observable.DelegateOfSubscribe = null;

            subject.OnNext(6);
            next.Is(6);

            subject.OnError(new ArgumentNullException(TEST));
            exception.IsInstanceOf<ArgumentNullException>();
            (exception as ArgumentNullException)?.ParamName.Is(TEST);

            subject.OnCompleted();
            completed.IsTrue();
        }
    }
}