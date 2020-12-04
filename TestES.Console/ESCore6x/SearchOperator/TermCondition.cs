using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchOperator
{
    public class TermCondition:FilterCodition
    {
        public TermCondition()
            : base("term")
        {

        }
    }
}
