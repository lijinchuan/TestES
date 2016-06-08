using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class MatchCondition:SearchCondition
    {
        public MatchCondition()
            : base("match")
        {

        }
    }
}
