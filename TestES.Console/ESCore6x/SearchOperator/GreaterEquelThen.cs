using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchOperator
{
    public class GreaterEquelThen:SearchConditionBase
    {
        public GreaterEquelThen(object value=null)
            : base("gte")
        {
            Value = value;
        }
    }
}
