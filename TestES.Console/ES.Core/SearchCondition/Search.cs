using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Search
    {
        private Query _query=null;
        public Search Query(Action<Query> query)
        {
            if (_query == null)
                _query = new Query();

            query(_query);

            return this;
        }

        private Source _source = null;

        public Search Source(Action<Source> source)
        {

            if (_source == null)
                _source = new Source();

            source(_source);

            return this;
        }

        private long _size=10;
        private Search Size(long sz)
        {
            _size = sz;

            return this;
        }

        private long _from = 0;
        public Search From(long from)
        {
            _from=from;

            return this;
        }

        internal void BuildString(JsonTextWriter writer)
        {
            writer.WriteStartObject();

            if(_query!=null)
            {
                _query.BuildString(writer);
            }

            if (_source != null)
            {
                _source.BuildString(writer);
            }

            writer.WritePropertyName("from");
            writer.WriteValue(_from);

            writer.WritePropertyName("size");
            writer.WriteValue(_size);

            writer.WriteEndObject();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            JsonTextWriter writer = new JsonTextWriter(new StringWriter(sb));

            BuildString(writer);

            return sb.ToString();
        }
    }
}
