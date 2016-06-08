using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class MustNotCodition:FilterCodition
    {
        public MustNotCodition()
            : base("must_not")
        {
        }


    }
}
