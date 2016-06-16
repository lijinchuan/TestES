using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class IndexSetting
    {
        private int _number_of_shards = 5;
        private int _number_of_replicas = 1;

        public IndexSetting SetNumber_of_shards(int shards)
        {
            if (shards <= 0)
                return this;

            _number_of_shards = shards;

            return this;
        }

        public IndexSetting SetNumber_of_replicas(int replicas)
        {
            if (replicas <= 0)
                return this;

            _number_of_replicas = replicas;

            return this;
        }

        internal void BuildString(JsonWriter writer)
        {
            writer.WritePropertyName("settings");
            writer.WriteStartObject();

            writer.WritePropertyName("index");
            writer.WriteStartObject();

            writer.WritePropertyName("number_of_shards");
            writer.WriteValue(_number_of_shards);

            writer.WritePropertyName("number_of_replicas");
            writer.WriteValue(_number_of_replicas);

            writer.WriteEndObject();
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
