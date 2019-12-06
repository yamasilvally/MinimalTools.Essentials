/*
 * OnDemandEvent
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System;

namespace Xunit
{
    /// <summary>任意のタイミングでイベントを発生させるクラス</summary>
    /// <typeparam name="T">引数の型</typeparam>
    public class OnDemandEvent<T>
    {
        /// <summary>constructor</summary>
        public OnDemandEvent() { }

        /// <summary>EventHandler&lt;T&gt;型のイベント</summary>
        public event EventHandler<T> GenericTestEvent;

        /// <summary>EventHandler型のイベント</summary>
        public event EventHandler TestEvent;

        /// <summary>Action&lt;T&gt;型汎用イベント</summary>
        public event Action<T> TestAction;

        /// <summary>EventHandler&lt;T&gt;型のイベントを発報する</summary>
        /// <param name="arg">イベント引数</param>
        public void Fire(T arg) => this.GenericTestEvent?.Invoke(this, arg);

        /// <summary>EventHandler型のイベントを発報する</summary>
        /// <param name="arg">イベント引数</param>
        public void Fire(EventArgs arg) => this.TestEvent?.Invoke(this, arg);

        /// <summary>EventHandler型のイベントをEventArgs.Emptyを引数にして発報する</summary>
        public void Fire() => this.Fire(EventArgs.Empty);

        /// <summary>Action&lt;T&gt;型汎用イベントを発報する</summary>
        /// <param name="arg">イベント引数</param>
        public void FireAction(T arg) => this.TestAction?.Invoke(arg);
    }
}