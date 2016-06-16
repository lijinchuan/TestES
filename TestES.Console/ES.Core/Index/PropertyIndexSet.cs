using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    /// <summary>
    /// 可选值为analyzed(默认)和no，如果是字段是字符串类型的，则可以是not_analyzed.
    /// Index表示该字段是否索引，如果index为no那个analyzer设为啥也没用。
    /// </summary>
    public enum PropertyIndexSet
    {
        _default,
        /// <summary>
        /// 首先分析这个字符串，然后索引。换言之，以全文形式索引此字段。
        /// </summary>
        analyzed,
        /// <summary>
        /// 索引这个字段，使之可以被搜索，但是索引内容和指定值一样。不分析此字段。
        /// </summary>
        not_analyzed,
        /// <summary>
        /// 不索引这个字段。这个字段不能为搜索到。
        /// </summary>
        no,
    }
}
