/*
 * Test for ObsavableExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using MinimalTools.DelegateObjects;
using MinimalTools.Extensions.Reactive;
using Xunit;
using static MinimalTools.Test.Extensions.Reactive.TestResource;

namespace MinimalTools.Test.Extensions.Reactive
{
    class TestResource
    {
        public static readonly Exception E = new Exception();
    }

    #region [ AsWeakObservable ]

    /// <summary>
    /// Test for ObservableExtensions.AsWeakObservable (extension for IObsavable&gt;T&lt;).
    /// </summary>
    public class ObservableExtensions_AsWeakObservable
    {
        [Fact(DisplayName = "Even if garbage collection is done, subscriber should receive notifications continue unless unsubscribed.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsWeakObservable))]
        public void KeepReferences()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            var dispose = observable.AsWeakObservable().Subscribe(observer);

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced);  // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(9);    // to be notified

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after explicitly unsubscribe.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsWeakObservable))]
        public void ExplicitRelease()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var dispose = observable.AsWeakObservable().Subscribe(new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; }));

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // unsubscribe
            dispose.Dispose();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IDisposable.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsWeakObservable))]
        public void ImplicitReleaseByLostIDisposable()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var ovserver = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = observable.AsWeakObservable().Subscribe(ovserver);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure.
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should receive notifications even if lost reference to IObserver<T>.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsWeakObservable))]
        public void ImplicitReleaseByLostObserver()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            IDisposable disposable = null;
            WeakReference<IObserver<int>> weak = null;

            void exec()
            {
                var o = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
                disposable = observable.AsWeakObservable().Subscribe(o);
                weak = new WeakReference<IObserver<int>>(o, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            // It should be notified. Because reference of IDisposable is alive and not unsubscribe automatically,
            // and so subscription is valid. Reference of IObserver<T> is kept internally, and was not collected by
            // the garbage collector.
            observable.OnNext(6);
            onnext.Is(9);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObservable<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsWeakObservable))]
        public void NullObservable()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IObservable<int>).AsWeakObservable().Subscribe(new DelegateObserver<int>(i => { })))
                .ParamName.Is("source");


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObserver<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsWeakObservable))]
        public void NullObserver()
            => Assert.Throws<ArgumentNullException>(
                    () => new OnDemandObservable<int>().AsWeakObservable().Subscribe(null as IObserver<int>))
                .ParamName.Is("observer");
    }

    #endregion

    #region [ AsVeryWeakObservable ]

    /// <summary>
    /// Test for ObservableExtensions.AsVeryWeakObservable (extension for IObsavable&gt;T&lt;).
    /// </summary>
    public class ObservableExtensions_AsVeryWeakObservable
    {
        [Fact(DisplayName = "Even if garbage collection is done, subscriber should receive notifications continue unless unsubscribed.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsVeryWeakObservable))]
        public void KeepReferences()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            var dispose = observable.AsVeryWeakObservable().Subscribe(observer);

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced);
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(9);   // to be notified

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after explicitly unsubscribe.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsVeryWeakObservable))]
        public void ExplicitRelease()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var dispose = observable.AsVeryWeakObservable()
                .Subscribe(new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; }));

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // disposing to unsubscribe
            dispose.Dispose();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IDisposable.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsVeryWeakObservable))]
        public void ImplicitReleaseByLostIDisposable()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var ovserver = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = observable.AsVeryWeakObservable().Subscribe(ovserver);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IObserver<T>.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsVeryWeakObservable))]
        public void ImplicitReleaseByLostObserver()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            IDisposable disposable = null;
            WeakReference<IObserver<int>> weak = null;

            void exec()
            {
                var o = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
                disposable = observable.AsVeryWeakObservable().Subscribe(o);
                weak = new WeakReference<IObserver<int>>(o, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            Assert.NotNull(e);

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObservable<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsVeryWeakObservable))]
        public void NullObservable()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IObservable<int>)
                        .AsVeryWeakObservable().Subscribe(new DelegateObserver<int>(i => { })))
                .ParamName.Is("source");


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObserver<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.AsVeryWeakObservable))]
        public void NullObserver()
            => Assert.Throws<ArgumentNullException>(
                    () => new OnDemandObservable<int>()
                        .AsVeryWeakObservable().Subscribe(null as IObserver<int>))
                .ParamName.Is("observer");
    }

    #endregion

    #region [ WeakSubscribe ]

    /// <summary>
    /// Test for ObservableExtensions.WeakSubscribe (extension for IObsavable&gt;T&lt;).
    /// </summary>
    public class ObservableExtensions_WeakSubscribe
    {
        [Fact(DisplayName = "Even if garbage collection is done, notifications should continue unless unsubscribed.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.WeakSubscribe))]
        public void KeepReferences()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            var dispose = observable.WeakSubscribe(observer);

            // observer = null;

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure.
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(9);    // it should be notified.

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after explicitly unsubscribe.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.WeakSubscribe))]
        public void ExplicitRelease()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var dispose = observable
                .WeakSubscribe(new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; }));

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // disposing to unsubscribe
            dispose.Dispose();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IDisposable.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.WeakSubscribe))]
        public void ImplicitReleaseByLostIDisposable()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var ovserver = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = observable.WeakSubscribe(ovserver);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IObserver<T>.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.WeakSubscribe))]
        public void ImplicitReleaseByLostObserver()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            IDisposable disposable = null;
            WeakReference<IObserver<int>> weak = null;

            void exec()
            {
                var o = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
                disposable = observable.WeakSubscribe(o);
                weak = new WeakReference<IObserver<int>>(o, true);
            };

            // execute inner method
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(9);    // subscriber should recieve notification because a reference of IObserver<T> is kept internally.

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObservable<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.WeakSubscribe))]
        public void NullObservable()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IObservable<int>).WeakSubscribe(new DelegateObserver<int>(i => { })))
                .ParamName.Is("source");


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObserver<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.WeakSubscribe))]
        public void NullObserver()
            => Assert.Throws<ArgumentNullException>(
                    () => new OnDemandObservable<int>().WeakSubscribe(null as IObserver<int>))
                .ParamName.Is("observer");
    }

    #endregion

    #region [ VeryWeakSubscribe ]

    /// <summary>
    /// Test for ObservableExtensions.VeryWeakSubscribe (extension for IObservable&gt;T&lt;).
    /// </summary>
    public class ObservableExtensions_VeryWeakSubscribe
    {
        [Fact(DisplayName = "Even if garbage collection is done, notifications should continue unless unsubscribed.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.VeryWeakSubscribe))]
        public void KeepReferences()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var observer = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            var dispose = observable.VeryWeakSubscribe(observer);

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(9);    // it should be notified.

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after explicitly unsubscribe.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.VeryWeakSubscribe))]
        public void ExplicitRelease()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var dispose = observable.VeryWeakSubscribe(new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; }));

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // disposing to unsubscribe.
            dispose.Dispose();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IDisposable.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.VeryWeakSubscribe))]
        public void ImplicitReleaseByLostIDisposable()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            var ovserver = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = observable.VeryWeakSubscribe(ovserver);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicitly unsubscribe by losing reference to IObserver<T>.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.VeryWeakSubscribe))]
        public void ImplicitReleaseByLostObserver()
        {
            int onnext = 0;
            Exception e = null;
            bool isCompleted = false;

            var observable = new OnDemandObservable<int>();
            IDisposable disposable = null;
            WeakReference<IObserver<int>> weak = null;

            void exec()
            {
                var o = new DelegateObserver<int>(i => onnext += i, ex => e = ex, () => { isCompleted = true; });
                disposable = observable.VeryWeakSubscribe(o);
                weak = new WeakReference<IObserver<int>>(o, true);
            };

            // execute inner method.
            exec();

            observable.OnNext(3);
            onnext.Is(3);

            observable.OnError(E);
            e.IsNotNull();

            observable.OnCompleted();
            isCompleted.IsTrue();

            // reset values.
            e = null;
            isCompleted = false;

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            observable.OnNext(6);
            onnext.Is(3);    // it should not be notified.

            observable.OnError(E);
            e.IsNull();

            observable.OnCompleted();
            isCompleted.IsFalse();
        }


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObservable<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.VeryWeakSubscribe))]
        public void NullObservable()
            => Assert.Throws<ArgumentNullException>(
                    () => (null as IObservable<int>).VeryWeakSubscribe(new DelegateObserver<int>(i => { })))
                .ParamName.Is("source");


        [Fact(DisplayName = "ArgumentNullException should be thrown if reference of IObserver<T> is null.")]
        [Trait(nameof(ObservableExtensions), nameof(ObservableExtensions.VeryWeakSubscribe))]
        public void NullObserver()
            => Assert.Throws<ArgumentNullException>(
                    () => new OnDemandObservable<int>().VeryWeakSubscribe(null as IObserver<int>))
                .ParamName.Is("observer");
    }

    #endregion
}