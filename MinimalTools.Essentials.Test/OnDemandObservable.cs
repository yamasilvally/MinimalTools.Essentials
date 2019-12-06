/*
 * OnDemandObservable
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */
 
using System;

namespace Xunit
{
    /// <summary>任意のタイミングで値を通知するObservable</summary>
    /// <typeparam name="T">通知する値の型</typeparam>
    /// <remarks>
    /// IObserver<typeparam name="T"/>も実装しているので
    /// RxのSubjectのような使い方もできる
    /// </remarks>
    public class OnDemandObservable<T> : IObservable<T>, IObserver<T>
    {
        /// <summary>constructor</summary>
        public OnDemandObservable() { }

        /// <summary>OnNextの登録口</summary>
        Action<T> OnNextAction;

        /// <summary>OnErrorの登録口</summary>
        Action<Exception> OnErrorAction;

        /// <summary>OnCompletedの登録口</summary>
        Action OnCompletedAction;

        /// <summary>購読者を登録する</summary>
        /// <param name="observer">購読者</param>
        /// <returns>購読解除用オブジェクト</returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            var d = new InstantDisposable();
            if (observer == null) return d;

            this.OnNextAction += observer.OnNext;
            this.OnErrorAction += observer.OnError;
            this.OnCompletedAction += observer.OnCompleted;

            d.DisposeAction = () =>
            {
                this.OnNextAction -= observer.OnNext;
                this.OnErrorAction -= observer.OnError;
                this.OnCompletedAction -= observer.OnCompleted;
            };

            return d;
        }

        /// <summary>値を通知する</summary>
        /// <param name="arg">通知する値</param>
        public void OnNext(T arg) => this.OnNextAction?.Invoke(arg);

        /// <summary>エラーを通知する</summary>
        /// <param name="e">例外</param>
        public void OnError(Exception e) => this.OnErrorAction?.Invoke(e);

        /// <summary>完了を通知する</summary>
        public void OnCompleted() => this.OnCompletedAction?.Invoke();
    }
}