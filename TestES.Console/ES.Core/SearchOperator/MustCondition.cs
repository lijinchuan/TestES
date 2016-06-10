using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class MustCondition:SearchConditionBase
    {
        public MustCondition()
            : base("must")
        {

        }
    }
}
