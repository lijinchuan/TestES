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

        private List<SearchCondition> _matchCollection = new List<SearchCondition>();
        [JsonIgnore]
        public List<SearchCondition> MatchCollection
        {
            get
            {
                return _matchCollection;
            }
        }

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(new StringWriter(sb));

            writer.WritePropertyName(this.Codition);
            writer.WriteStartObject();

            foreach(var m in _matchCollection)
            {
                writer.WritePropertyName(m.Codition);
                writer.WriteValue(m.Value);
            }

            writer.WriteEndObject();

            return sb.ToString();
        }
    }
}
