using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchOperator
{
    public class SmallEquelThen:SearchConditionBase
    {
        public SmallEquelThen(object value = null)
            : base("lte")
        {
            Value = value;
        }
    }
}
