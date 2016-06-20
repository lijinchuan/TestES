using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Query
    {
        private Filtered _filtered;
        public Query Filter(Action<Filter> filter)
        {
            if(_filtered==null)
            {
                _filtered = new Filtered();
            }

            filter(_filtered.Filter);

            return this;
        }

        internal void BuildString(JsonTextWriter writer)
        {
            writer.WritePropertyName("query");
            writer.WriteStartObject();

            //if(_mutch!=null)
            //{
            //    _mutch.BuildString(writer);
            //}

            if(_filtered!=null)
            {
                _filtered.BuildString(writer);
            }

            writer.WriteEndObject();
        }
    }
}
