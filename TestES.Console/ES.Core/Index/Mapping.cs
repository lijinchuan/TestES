using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class Mappings
    {
        private string _typename = string.Empty;
        public Mappings(string typename)
        {
            _typename = typename;
        }

        private Dictionary<string,MappingType> _mappintTypes;
        public Mappings Mapping(string typename,Action<MappingType> resource)
        {
            if (_mappintTypes == null)
            {
                _mappintTypes = new Dictionary<string, MappingType>();
            }

            if (!_mappintTypes.ContainsKey(typename))
            {
                _mappintTypes.Add(typename, new MappingType());
            }

            var maptype = _mappintTypes[typename];

            resource(maptype);

            return this;
        }

        internal void BuildString(JsonWriter writer)
        {

            writer.WritePropertyName("mappings");

            writer.WriteStartObject();

            //writer.WritePropertyName(_typename);
            //writer.WriteStartObject();

            if(_mappintTypes!=null)
            {
                foreach (var kv in _mappintTypes)
                {
                    writer.WritePropertyName(kv.Key);
                    writer.WriteStartObject();
                    kv.Value.BuildString(writer);
                    writer.WriteEndObject();
                }
            }
            //writer.WriteEndObject();
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
