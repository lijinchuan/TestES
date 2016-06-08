using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class SmallEquelThen:SearchCondition
    {
        public SmallEquelThen(object value = null)
            : base("lte")
        {
            Value = value;
        }
    }
}
