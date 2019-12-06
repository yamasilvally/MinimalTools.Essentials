/*
 * DataGeneratorBase
 *
 * Copyright (c) 2019 Takahisa YAMASHIGE
 *
 * This software is released under the MIT License.
 * https://opensource.org/licenses/mit-license.php
 */

using System.Collections;
using System.Collections.Generic;

namespace Xunit
{
    /// <summary>ClassData用基底クラス</summary>
    /// <typeparam name="object[]">引数の配列</typeparam>
    public class DataGenerator : IEnumerable<object[]>
    {
        /// <summary>constructor</summary>
        public DataGenerator() { }

        /// <summary>テストデータ格納用</summary>
        protected IList<object[]> Data { get; } = new List<object[]>();

        /// <summary>テストデータを可変長引数で登録する</summary>
        /// <param name="parameters">可変長引数</param>
        /// <remarks>コンストラクタから、1行分ごとに渡す想定</remark>
        public virtual void Add(params object[] parameters)
        {
            if(parameters != null) this.Data.Add(parameters);
        }

        /// <summary>implements IEnumerable&lt;T&gt;.GetEnumerator</summary>
        /// <returns>IEnumerator&lt;object[]&gt;</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var o in this.Data) yield return o;
            yield break;
        }

        /// <summary>implements IEnumerabl.GetEnumerator</summary>
        /// <returns>IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}