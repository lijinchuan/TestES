using LJC.FrameWork.Comm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class SearchCondition
    {
        public string Codition
        {
            get;
            set;
        }

        public SearchCondition(string condition)
        {
            this.Codition = condition;
        }

        public object Value
        {
            get;
            set;
        }

        private List<SearchCondition> _filterCollection = new List<SearchCondition>();
        [JsonIgnore]
        public List<SearchCondition> FilterCollection
        {
            get
            {
                return _filterCollection;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(new StringWriter(sb));

            //writer.WriteStartObject();
            writer.WritePropertyName(this.Codition);

            if (_filterCollection.Count > 0)
            {
                writer.WriteStartObject();

                foreach (var t in _filterCollection)
                {
                    writer.WriteRaw(t.ToString());
                    writer.WriteRaw(",");
                }

                sb.Remove(sb.Length - 1, 1);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteValue(this.Value);
            }
            
            //writer.WriteEndObject();
            return sb.ToString();
        }
    }
}
