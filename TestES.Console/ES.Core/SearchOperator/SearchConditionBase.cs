using LJC.FrameWork.Comm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.SearchOperator
{
    public class SearchConditionBase
    {
        public string Codition
        {
            get;
            set;
        }

        public SearchConditionBase(string condition = "query", object value = null)
        {
            this.Codition = condition;
            if (value != null)
            {
                Value = value;
            }
        }

        public object Value
        {
            get;
            set;
        }

        private List<SearchConditionBase> _filterCollection = new List<SearchConditionBase>();
        [JsonIgnore]
        public List<SearchConditionBase> FilterCollection
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

        public virtual void BuildQuery(JsonTextWriter jsonwriter)
        {
            //jsonwriter.WriteStartObject();
            jsonwriter.WritePropertyName(this.Codition);

            
            if (_filterCollection.Count > 0)
            {
                var booarray = (_filterCollection.GroupBy(p => p.Codition).Where(p => p.Count() > 1).Count() > 0);
                if (booarray)
                {
                    jsonwriter.WriteStartArray();
                }
                else
                {
                    jsonwriter.WriteStartObject();
                }

                

                foreach (var t in _filterCollection)
                {
                    t.BuildQuery(jsonwriter);
                }

                if (booarray)
                {
                    jsonwriter.WriteEndArray();
                }
                else
                {
                    jsonwriter.WriteEndObject();
                }
            }
            else
            {
                if (this.Value != null)
                    jsonwriter.WriteValue(this.Value);
                else
                    jsonwriter.WriteRawValue("{}");
            }

            //jsonwriter.WriteEndObject();
        }
    }
}
