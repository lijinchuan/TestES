using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class GreaterThen:FilterCodition
    {
        public GreaterThen(object value)
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
