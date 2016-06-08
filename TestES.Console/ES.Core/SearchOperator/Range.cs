using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class Range : FilterCodition
    {
        public Range()
            : base("range")
        {

        }
    }
}
