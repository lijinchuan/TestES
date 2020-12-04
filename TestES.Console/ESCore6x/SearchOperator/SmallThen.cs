using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchOperator
{
    public class SmallThen:SearchConditionBase
    {
        public SmallThen(object value = null) :
            base("lt")
        {
            Value = value;
        }
    }
}
