using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class ShouldCondition:FilterCodition
    {
        public ShouldCondition()
            : base("should")
        {

        }
    }
}
