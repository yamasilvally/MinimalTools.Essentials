/*
 * Test for DelegateDisposable
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTools.DelegateObjects;
using Xunit;

namespace MinimalTools.Test.DelegateObjects
{
    /// <summary>
    /// Test for DelegateDisposable.Dispose.null
    /// </summary>
    public class DelegateDisposable_Dispose
    {
        [Fact(DisplayName = "Calling Dispose should be accepted one time only even if multi times called.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public void Disposing()
        {
            int i = 0;
            var d = new DelegateDisposable();
            d.DisposingAction = () => { i++; };
            d.Dispose();
            i.Is(1);

            d.Dispose();
            i.Is(1);
        }


        [Fact(DisplayName = "Calling Dispose should be accepted one time only even if multi times called from multi threads.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public async void DisposingMulti()
        {
            int i = 0;
            var d = new DelegateDisposable(() => { i++; });

            List<Task> tasks = new List<Task>();
            for (int j = 0; j < 10; j++) tasks.Add(Task.Run(() => d.Dispose()));
            await Task.WhenAll(tasks);

            i.Is(1);
        }


        [Fact(DisplayName = "Nothing should happen if delegete of disposing action is not set.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public void NullDispose()
            => new DelegateDisposable().Dispose();


        [Fact(DisplayName = "Exception that occured in delegate of deisposing action should be ignored.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public void ThrowExceptionFromDelegate()
        {
            int i = 0;
            new DelegateDisposable(() => throw new Exception("test")).Dispose();
            i.Is(0);
        }


        [Fact(DisplayName = "Exception that occured in delegate of deisposing action for unmanaged resouces should be ignored.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public void ThrowExceptionFromDelegateForUnmanege()
        {
            int i = 0;
            new DelegateDisposable(() => i++, () => throw new Exception("test")).Dispose();
            i.Is(1);
        }


        [Fact(DisplayName = "[Test for Finalizer[1]] Only delegate of disposing action for unmanaged resources should be executed in Finalizer when Dispose was not called explicitly.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public void ImplicitFinalizerTest()
        {
            /*
             * I referred to the following.
             * https://www.inversionofcontrol.co.uk/unit-testing-finalizers-in-csharp/            
             */

            int managed = 0;
            int unmanaged = 0;
            WeakReference<DelegateDisposable> weak = null;

            // define an instance of DelegateDisposable in inner method.
            Action exec = () =>
            {
                var d = new DelegateDisposable();
                d.DisposingAction = () => { managed += 1; };
                d.UnmanagedDisposingAction = () => { unmanaged += 1; };
                weak = new WeakReference<DelegateDisposable>(d, true);    // I don't know why this WeakReference is neccesary...
            };

            // execute test.
            exec();

            // force GC.
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced); // just to be sure

            managed.Is(0);
            unmanaged.Is(1);
        }


        [Fact(DisplayName = "[Test for Finalizer[2]] If Dispose is explicitly called beforehand, Finalizer will not be executed.")]
        [Trait(nameof(DelegateDisposable), nameof(DelegateDisposable.Dispose))]
        public void ExplicitFinalizerTest()
        {
            /*
             * I referred to the following.
             * https://www.inversionofcontrol.co.uk/unit-testing-finalizers-in-csharp/            
             */

            int managed = 0;
            int unmanaged = 0;
            WeakReference<DelegateDisposable> weak = null;

            Action exec = () =>
            {
                var d = new DelegateDisposable(() => { managed += 1; }, () => { unmanaged += 1; });
                d.Dispose();
                weak = new WeakReference<DelegateDisposable>(d, true);
            };

            exec();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(0, GCCollectionMode.Forced);

            managed.Is(1);
            unmanaged.Is(1);
        }
    }
}