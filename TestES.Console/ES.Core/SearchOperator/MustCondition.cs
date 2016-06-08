using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class MustCondition:SearchCondition
    {
        public MustCondition()
            : base("must")
        {

        }
    }
}
