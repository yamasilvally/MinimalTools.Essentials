/*
 * DelegateDisposable
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;

namespace MinimalTools.DelegateObjects
{
    /// <summary>
    /// A class that made IDisposable implementable with delegate.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class DelegateDisposable : IDisposable
    {
        #region [ fields ]


        /// <summary>The flag to judge whether Dispose() has been called.</summary>
        bool disposedValue = false;


        /// <summary>An object for lock.</summary>
        readonly object o = new object();


        #endregion

        #region [ constructors & finalizer ]


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateDisposable"/> class.
        /// </summary>
        public DelegateDisposable() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateDisposable"/> class.
        /// </summary>
        /// <param name="disposingAction">A delegate that performed when invokes Dispose() method.</param>
        public DelegateDisposable(Action disposingAction) => this.DisposingAction = disposingAction;


        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateDisposable"/> class.
        /// </summary>
        /// <param name="disposingAction">A delegate that performed when invokes Dispose() method.</param>
        /// <param name="unmanagedDisposingAction">
        /// A delegate that release unmanaged resources,performed when invokes Dispose() method.
        /// </param>
        public DelegateDisposable(Action disposingAction, Action unmanagedDisposingAction) : this(disposingAction)
            => this.UnmanagedDisposingAction = unmanagedDisposingAction;


        /// <summary>
        /// Finalizes an instance of the <see cref="DelegateDisposable"/> class.
        /// </summary>
        ~DelegateDisposable() => this.Dispose(false);


        #endregion

        #region [ properties ]


        /// <summary>
        /// A delegate that performed when invokes Dispose() method.
        /// </summary>
        public Action DisposingAction { get; set; }


        /// <summary>
        /// A delegate that release unmanaged resources, performed when invokes Dispose() method.
        /// </summary>
        public Action UnmanagedDisposingAction { get; set; }


        #endregion

        #region [ methods ]


        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (o)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        // managed
                        try
                        {
                            this.DisposingAction?.Invoke();
                        }
                        catch
                        {
                            // nop
                        }
                    }

                    // unmanaged
                    try
                    {
                        this.UnmanagedDisposingAction?.Invoke();
                    }
                    catch
                    {
                        // nop
                    }

                    this.disposedValue = true;
                }
            }
        }


        #endregion
    }
}