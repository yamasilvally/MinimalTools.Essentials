/*
 * Test for EventExtensions
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using MinimalTools.Extensions.Events;
using Xunit;

namespace MinimalTools.Test.Extensions.Events
{
    #region [ WeakSubscribe (EventHandler) ]

    /// <summary>
    /// Test for Extensions.WeakSubscribe
    /// </summary>
    public class EventExtensions_WeakSubscribe
    {
        [Fact(DisplayName = "Subscriber should not receive notifications after explicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NormalRelease()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = 0;
            var dispo = supplier.WeakSubscribe<OnDemandEvent<int>>(
                (sup, hdr) => sup.TestEvent += hdr,
                (sup, hdr) => sup.TestEvent -= hdr,
                (s, e) => { reciever += 1; });

            supplier.Fire();
            reciever.Is(1);

            dispo.Dispose();
            supplier.Fire();
            reciever.Is(1);
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void ImplicitRelease()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = 0;
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = supplier.WeakSubscribe<OnDemandEvent<int>>(
                    (sup, hdr) => sup.TestEvent += hdr,
                    (sup, hdr) => sup.TestEvent -= hdr,
                    (s, e) => reciever += 1);

                supplier.Fire();
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute local method.
            exec();
            reciever.Is(1);

            supplier.Fire();
            reciever.Is(2);

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            supplier.Fire();

            // 'reciever' should stay '2', because subscription should be automatically released.
            reciever.Is(2);
        }


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                var supplier = new OnDemandEvent<int>();
                Action<OnDemandEvent<int>, EventHandler> addHandler = (s, h) => s.TestEvent += h;
                Action<OnDemandEvent<int>, EventHandler> removeHandler = (s, h) => s.TestEvent -= h;
                EventHandler handler = (s, e) => { };

                this.Add(null, addHandler, removeHandler, handler, "supplier");
                this.Add(supplier, null, removeHandler, handler, "addHandler");
                this.Add(supplier, addHandler, null, handler, "removeHandler");
                this.Add(supplier, addHandler, removeHandler, null, "handler");
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of parameters is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NullChecks(
            OnDemandEvent<int> supplier,
            Action<OnDemandEvent<int>, EventHandler> addHandler,
            Action<OnDemandEvent<int>, EventHandler> removeHandler,
            EventHandler handler,
            string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => supplier.WeakSubscribe(addHandler, removeHandler, handler))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ WeakSubscribe (EventHandler<T>) ]

    /// <summary>
    /// Test for EventExtensions.WeakSubscribe (EventHandler&gt;T&lt; version).
    /// </summary>
    public class EventExtensions_WeakSubscribe_Generic
    {
        [Fact(DisplayName = "Subscriber should not receive notifications after explicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NormalRelease()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = int.MinValue;
            string sender = null;
            var dispo = supplier.WeakSubscribe<OnDemandEvent<int>, int>(
                (sup, hdr) => sup.GenericTestEvent += hdr,
                (sup, hdr) => sup.GenericTestEvent -= hdr,
                (s, e) => { reciever = e; sender = s.GetType().Name; });

            supplier.Fire(1);
            reciever.Is(1);

            dispo.Dispose();
            supplier.Fire(3);
            reciever.Is(1);
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void ImplicitRelease()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = int.MinValue;
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = supplier.WeakSubscribe<OnDemandEvent<int>, int>(
                    (sup, hdr) => sup.GenericTestEvent += hdr,
                    (sup, hdr) => sup.GenericTestEvent -= hdr,
                    (s, e) => reciever = e);

                supplier.Fire(1);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute local method.
            exec();
            reciever.Is(1);

            supplier.Fire(2);
            reciever.Is(2);

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            supplier.Fire(3);

            // 'reciever' should stay '2', because subscription should be automatically released.
            reciever.Is(2);
        }


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                var supplier = new OnDemandEvent<int>();
                Action<OnDemandEvent<int>, EventHandler<int>> addHandler = (s, h) => s.GenericTestEvent += h;
                Action<OnDemandEvent<int>, EventHandler<int>> removeHandler = (s, h) => s.GenericTestEvent -= h;
                EventHandler<int> handler = (s, e) => { };

                this.Add(null, addHandler, removeHandler, handler, "supplier");
                this.Add(supplier, null, removeHandler, handler, "addHandler");
                this.Add(supplier, addHandler, null, handler, "removeHandler");
                this.Add(supplier, addHandler, removeHandler, null, "handler");
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of parameters is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NullChecks(
            OnDemandEvent<int> supplier,
            Action<OnDemandEvent<int>, EventHandler<int>> addHandler,
            Action<OnDemandEvent<int>, EventHandler<int>> removeHandler,
            EventHandler<int> handler,
            string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => supplier.WeakSubscribe(addHandler, removeHandler, handler))
                .ParamName.Is(expect);
    }

    #endregion

    #region [ WeakSubscribe (EventHandler with converter) ]

    /// <summary>
    /// Test for EventExtensions.WeakSubscribe (with conversion version).
    /// </summary>
    public class EventExtensions_WeakSubscribe_WithConverter
    {
        [Fact(DisplayName = "Notify should not reach after explicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NoConvert()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = 0;

            var dispo = supplier.WeakSubscribe<OnDemandEvent<int>, EventHandler>(
                f => (s, e) => f(s, e),
                (sup, hdr) => sup.TestEvent += hdr,
                (sup, hdr) => sup.TestEvent -= hdr,
                (s, e) => { reciever += 1; });

            supplier.Fire();
            reciever.Is(1);

            dispo.Dispose();
            supplier.Fire();
            reciever.Is(1);
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after explicit disposing.(with EventArgs conversion)")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NormalRelease()
        {
            var supplier = new OnDemandEvent<int>();
            string reciever = null;
            int count = 0;

            var dispo = supplier.WeakSubscribe<OnDemandEvent<int>, Action<string>>(
                f => (s, e) => f($"{++count}"),
                (sup, hdr) => sup.TestEvent += hdr,
                (sup, hdr) => sup.TestEvent -= hdr,
                e => { reciever = e; });

            supplier.Fire();
            reciever.Is(1.ToString());

            dispo.Dispose();
            supplier.Fire();
            reciever.Is(1.ToString());
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void ImplicitRelease()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = 0;
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = supplier.WeakSubscribe<OnDemandEvent<int>, EventHandler>(
                    f => (s, e) => f(s, e),
                    (sup, hdr) => sup.TestEvent += hdr,
                    (sup, hdr) => sup.TestEvent -= hdr,
                    (s, e) => reciever += 1);

                supplier.Fire();
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute local method.
            exec();
            reciever.Is(1);

            supplier.Fire();
            reciever.Is(2);

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            supplier.Fire();

            // 'reciever' should stay '2', because subscription should be automatically released.
            reciever.Is(2);
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicit disposing.(with EventArgs conversion)")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void ImplicitReleaseWithConvert()
        {
            var supplier = new OnDemandEvent<int>();
            string reciever = null;
            int count = 0;
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = supplier.WeakSubscribe<OnDemandEvent<int>, Action<string>>(
                    f => (s, e) => f($"{++count}"),
                    (sup, hdr) => sup.TestEvent += hdr,
                    (sup, hdr) => sup.TestEvent -= hdr,
                    e => reciever = e);

                supplier.Fire();
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute local method.
            exec();
            reciever.Is(1.ToString());

            supplier.Fire();
            reciever.Is(2.ToString());

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            supplier.Fire();

            // 'reciever' should stay '2', because subscription should be automatically released.
            reciever.Is(2.ToString());
        }


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                var supplier = new OnDemandEvent<int>();
                Func<Action<string>, EventHandler> converter = act => (s, e) => act(e.ToString());
                Action<OnDemandEvent<int>, EventHandler> addHandler = (s, h) => s.TestEvent += h;
                Action<OnDemandEvent<int>, EventHandler> removeHandler = (s, h) => s.TestEvent -= h;
                Action<string> handler = e => { };

                this.Add(null, converter, addHandler, removeHandler, handler, "supplier");
                this.Add(supplier, null, addHandler, removeHandler, handler, "converter");
                this.Add(supplier, converter, null, removeHandler, handler, "addHandler");
                this.Add(supplier, converter, addHandler, null, handler, "removeHandler");
                this.Add(supplier, converter, addHandler, removeHandler, null, "handler");
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of parameters is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NullChecks(
            OnDemandEvent<int> supplier,
            Func<Action<string>, EventHandler> converter,
            Action<OnDemandEvent<int>, EventHandler> addHandler,
            Action<OnDemandEvent<int>, EventHandler> removeHandler,
            Action<string> handler,
            string expect)
                => Assert.Throws<ArgumentNullException>(
                        () => supplier.WeakSubscribe<OnDemandEvent<int>, Action<string>>(
                            converter,
                            addHandler,
                            removeHandler,
                            handler))
                    .ParamName.Is(expect);
    }

    #endregion

    #region [ WeakSubscribe (EventHandler<T> with converter) ]

    /// <summary>
    /// Test for EventExtensions.WeakSubscribe (EventHander&gt;T&lt; with conversion version).
    /// </summary>
    public class EventExtensions_WeakSubscribe_Generic_WithConverter
    {
        [Fact(DisplayName = "Subscriber should not receive notifications after explicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NoConvert()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = int.MinValue;

            var dispo = supplier.WeakSubscribe<OnDemandEvent<int>, int, EventHandler<int>>(
                f => (s, e) => f(s, e),
                (sup, hdr) => sup.GenericTestEvent += hdr,
                (sup, hdr) => sup.GenericTestEvent -= hdr,
                (s, e) => { reciever = e; });

            supplier.Fire(1);
            reciever.Is(1);

            dispo.Dispose();
            supplier.Fire(3);
            reciever.Is(1);
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after explicit disposing.(with EventArgs conversion")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NormalRelease()
        {
            var supplier = new OnDemandEvent<int>();
            string reciever = null;

            var dispo = supplier.WeakSubscribe<OnDemandEvent<int>, int, Action<string>>(
                f => (s, e) => f(e.ToString()),
                (sup, hdr) => sup.GenericTestEvent += hdr,
                (sup, hdr) => sup.GenericTestEvent -= hdr,
                e => { reciever = e; });

            supplier.Fire(1);
            reciever.Is(1.ToString());

            dispo.Dispose();
            supplier.Fire(3);
            reciever.Is(1.ToString());
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicit disposing.")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void ImplicitRelease()
        {
            var supplier = new OnDemandEvent<int>();
            int reciever = int.MaxValue;
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = supplier.WeakSubscribe<OnDemandEvent<int>, int, EventHandler<int>>(
                    f => (s, e) => f(s, e),
                    (sup, hdr) => sup.GenericTestEvent += hdr,
                    (sup, hdr) => sup.GenericTestEvent -= hdr,
                    (s, e) => reciever = e);

                supplier.Fire(1);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute local method.
            exec();
            reciever.Is(1);

            supplier.Fire(2);
            reciever.Is(2);

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            supplier.Fire(3);

            // 'reciever' should stay '2', because subscription should be automatically released.
            reciever.Is(2);
        }


        [Fact(DisplayName = "Subscriber should not receive notifications after implicit disposing.(with event argument conversion)")]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void ImplicitReleaseWithConvert()
        {
            var supplier = new OnDemandEvent<int>();
            string reciever = null;
            WeakReference<IDisposable> weak = null;

            void exec()
            {
                var d = supplier.WeakSubscribe<OnDemandEvent<int>, int, Action<string>>(
                    f => (s, e) => f(e.ToString()),
                    (sup, hdr) => sup.GenericTestEvent += hdr,
                    (sup, hdr) => sup.GenericTestEvent -= hdr,
                    e => reciever = e);

                supplier.Fire(1);
                weak = new WeakReference<IDisposable>(d, true);
            };

            // execute local method.
            exec();
            reciever.Is(1.ToString());

            supplier.Fire(2);
            reciever.Is(2.ToString());

            // Force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure
            System.Threading.Tasks.Task.Delay(100).Wait();

            supplier.Fire(3);

            // 'reciever' should stay '2', because subscription should be automatically released.
            reciever.Is(2.ToString());
        }


        /// <summary>
        /// Test Data for null checking.
        /// </summary>
        class TestData003 : DataGenerator
        {
            public TestData003() : base()
            {
                OnDemandEvent<int> supplier = new OnDemandEvent<int>();
                Func<Action<string>, EventHandler<int>> converter = act => (s, e) => act(e.ToString());
                Action<OnDemandEvent<int>, EventHandler<int>> addHandler = (s, h) => s.GenericTestEvent += h;
                Action<OnDemandEvent<int>, EventHandler<int>> removeHandler = (s, h) => s.GenericTestEvent -= h;
                Action<string> handler = e => { };

                this.Add(null, converter, addHandler, removeHandler, handler, "supplier");
                this.Add(supplier, null, addHandler, removeHandler, handler, "converter");
                this.Add(supplier, converter, null, removeHandler, handler, "addHandler");
                this.Add(supplier, converter, addHandler, null, handler, "removeHandler");
                this.Add(supplier, converter, addHandler, removeHandler, null, "handler");
            }
        }


        [Theory(DisplayName = "ArgumentNullException should be thrown if one of the parameter is null.")]
        [ClassData(typeof(TestData003))]
        [Trait(nameof(EventExtensions), nameof(EventExtensions.WeakSubscribe))]
        public void NullChecks(
            OnDemandEvent<int> supplier,
            Func<Action<string>, EventHandler<int>> converter,
            Action<OnDemandEvent<int>, EventHandler<int>> addHandler,
            Action<OnDemandEvent<int>, EventHandler<int>> removeHandler,
            Action<string> handler,
            string expect)
            => Assert.Throws<ArgumentNullException>(
                    () => supplier.WeakSubscribe<OnDemandEvent<int>, int, Action<string>>(
                        converter,
                        addHandler,
                        removeHandler,
                        handler))
                .ParamName.Is(expect);
    }

    #endregion
}