using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class MappingType
    {
        private Properties ppt;

        /// <summary>
        /// 关闭“动态修改索引”
        /// </summary>
        private bool? _dynamic;
        /// <summary>
        /// 
        /// </summary>
        private bool? _source;

        public MappingType SetDynamic(bool value)
        {
            _dynamic = value;

            return this;
        }

        /// <summary>
        /// 设置把内容保存在硬盘上或者只保存索引
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MappingType EnableSource(bool value)
        {
            _source = value;

            return this;
        }

        public MappingType Property(string ppname,Action<Property> property)
        {
            if (ppt == null)
                ppt = new Properties();

            ppt.Property(ppname, property);

            return this;
        }

        internal void BuildString(JsonWriter writer)
        {
            //writer.WritePropertyName("resource");

            //writer.WriteStartObject();

            if (_source != null)
            {
                writer.WritePropertyName("_source");
                writer.WriteStartObject();
                writer.WritePropertyName("enabled");
                writer.WriteValue(_source);
                writer.WriteEndObject();
            }


            if(_dynamic!=null)
            {
                writer.WritePropertyName("dynamic");
                writer.WriteValue(_dynamic);
            }

            if(ppt!=null)
            {
                ppt.BuildString(writer);
            }

            //writer.WriteEndObject();
        }
    }
}
