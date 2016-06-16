using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public enum WellknownAnalyzer
    {
        standard,
        /// <summary>
        /// whitespace 分词器，只通过空格来分割文本
        /// </summary>
        whitespace,
        simple,
        english,
        ik,
    }
}
