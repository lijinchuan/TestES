using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchOperator
{
    public class GreaterThen:FilterCodition
    {
        public GreaterThen(object value = null)
            : base("gt")
        {
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
