using ES.Core6x.SearchCondition.Conditon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchCondition
{
    public class Prefix:ConditionBase
    {
        public Prefix()
            : base("prefix")
        {

        }
    }
}
