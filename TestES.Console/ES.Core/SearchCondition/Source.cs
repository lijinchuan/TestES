using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Source
    {
        private List<string> _items = new List<string>();

        public Source Add(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                _items.Add(item);
            }

            return this;
        }

        public Source AddMany(string[] items)
        {
            if (items == null)
                return this;

            foreach(var item in items)
            {
                Add(item);
            }

            return this;
        }

        internal void BuildString(JsonTextWriter writer)
        {
            writer.WritePropertyName("_source");
            writer.WriteStartArray();

            foreach(var item in _items)
            {
                writer.WriteValue(item);
            }

            writer.WriteEndArray();
        }
    }
}
