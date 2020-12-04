using ES.Core6x.SearchCondition.Conditon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchCondition
{
    public class Filtered:ConditionBase
    {
        private Filter _filter;

        public Filter Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new Filter();

                return _filter;
            }
        }

        public Filtered()
            : base("filter")
        {

        }

        private Query _query;
        public void Query(Action<Query> query)
        {
            if(_query==null)
            {
                _query = new Query();
            }

            query(_query);
        }

        public override void BuildString(JsonTextWriter writer)
        {
            writer.WritePropertyName("filtered");
            writer.WriteStartObject();
            if(_filter!=null)
            {
                _filter.BuildString(writer);
            }

            if(_query!=null)
            {
                _query.BuildString(writer);
            }

            writer.WriteEndObject();
        }
    }
}
