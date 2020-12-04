using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchCondition
{
    public class Query
    {
        private BoolFilter _boolFilter;

        //private Filtered _filtered;
        //public Query Filter(Action<Filter> filter)
        //{
        //    if(_filtered==null)
        //    {
        //        _filtered = new Filtered();
        //    }

        //    filter(_filtered.Filter);

        //    return this;
        //}

        public Query Bool(Action<BoolFilter> filter)
        {
            if (_boolFilter == null)
            {
                _boolFilter = new BoolFilter();
            }

            filter(_boolFilter);

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

            //if(_filtered!=null)
            //{
            //    _filtered.BuildString(writer);
            //}

            if (_boolFilter != null)
            {
                _boolFilter.BuildString(writer);
            }

            writer.WriteEndObject();
        }
    }
}
