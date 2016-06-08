using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class GreaterEquelThen:SearchCondition
    {
        public GreaterEquelThen(object value=null)
            : base("gte")
        {
            Value = value;
        }
    }
}
