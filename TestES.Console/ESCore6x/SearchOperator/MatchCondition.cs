using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchOperator
{
    public class MatchCondition:SearchConditionBase
    {
        public MatchCondition()
            : base("match")
        {

        }

        public override void BuildQuery(JsonTextWriter jsonwriter)
        {
            if (this.FilterCollection.Count == 0)
                this.Codition = "match_all";
            else
                this.Codition = "match";

            base.BuildQuery(jsonwriter);
        }
    }
}
