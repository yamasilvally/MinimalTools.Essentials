/*
 * InstantDisposable
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */
 
using System;

namespace Xunit
{
    /// <summary>IDisposableの簡易実装</summary>
    public class InstantDisposable : IDisposable
    {
        /// <summary>constructor</summary>
        public InstantDisposable() { }

        /// <summary>constructor</summary>
        /// <param name="action">Dispose()呼出時に実行されるアクション</param>
        public InstantDisposable(Action action) => this.DisposeAction = action;

        /// <summary>Dispose()呼出時に実行されるアクション</summary>
        public Action DisposeAction { get; set; }

        /// <summary>implements IDisposable.Dispose</summary>
        public void Dispose() => this.DisposeAction?.Invoke();
    }
}