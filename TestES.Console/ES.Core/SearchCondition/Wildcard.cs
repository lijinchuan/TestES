using ES.Core.SearchCondition.Conditon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Wildcard:ConditionBase
    {
        public Wildcard()
            : base("wildcard")
        {

        }
    }
}
