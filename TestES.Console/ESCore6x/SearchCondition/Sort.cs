using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchCondition
{
    public class Sort
    {
        private Dictionary<string, string> _sortList;

        public Sort Asc(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
               
            }
            if(_sortList==null)
                _sortList = new Dictionary<string, string>();
            _sortList[name] = "asc";

            return this;
        }

        public Sort Desc(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");

            }
            if(_sortList==null)
                _sortList = new Dictionary<string, string>();
            _sortList[name] = "desc";

            return this;
        }

        internal void BuildString(JsonTextWriter writer)
        {
            if (_sortList == null || _sortList.Count == 0)
                return;

            writer.WritePropertyName("sort");
            writer.WriteStartArray();
            foreach(var kv in _sortList)
            {
                writer.WriteStartObject();

                writer.WritePropertyName(kv.Key);
                writer.WriteStartObject();
                writer.WritePropertyName("order");
                writer.WriteValue(kv.Value);
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
