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
        private bool? _sourceEnabled;
        private string[] _sourceExcludes = null;

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
            _sourceEnabled = value;

            return this;
        }

        public MappingType SourceExcludes(params string[] fields)
        {
            this._sourceExcludes = fields;

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


            writer.WritePropertyName("_source");
            writer.WriteStartObject();
            if (_sourceEnabled != null)
            {
                writer.WritePropertyName("enabled");
                writer.WriteValue(_sourceEnabled);
            }

            if (_sourceExcludes != null && _sourceExcludes.Length > 0)
            {
                writer.WritePropertyName("excludes");

                writer.WriteStartArray();
                foreach (var ex in _sourceExcludes)
                {
                    writer.WriteValue(ex);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndObject();


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
