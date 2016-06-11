using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class MappingResource
    {
        private Properties ppt;

        /// <summary>
        /// 关闭“动态修改索引”
        /// </summary>
        public bool? _dynamic;

        public MappingResource SetDynamic(bool value)
        {
            _dynamic = value;

            return this;
        }

        public MappingResource Property(string ppname,Action<Property> property)
        {
            if (ppt == null)
                ppt = new Properties();

            ppt.Property(ppname, property);

            return this;
        }

        public void BuildString(JsonWriter writer)
        {
            writer.WritePropertyName("resource");

            writer.WriteStartObject();

            if(_dynamic!=null)
            {
                writer.WritePropertyName("dynamic");
                writer.WriteValue(_dynamic);
            }

            if(ppt!=null)
            {
                ppt.BuildString(writer);
            }

            writer.WriteEndObject();
        }
    }
}
