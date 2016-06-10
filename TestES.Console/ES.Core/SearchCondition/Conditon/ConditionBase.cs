using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition.Conditon
{
    public class ConditionBase
    {
        public string Condition;
        protected Dictionary<string, object> _namevalues=new Dictionary<string,object>();

        public ConditionBase(string condition)
        {
            Condition = condition;
        }

        public virtual ConditionBase Add(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                return this;
            _namevalues[name] = value;

            return this;
        }

        public virtual void BuildString(JsonTextWriter writer)
        {
            if (_namevalues.Count > 0)
            {
                foreach (var kv in _namevalues)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName(this.Condition);
                    writer.WriteStartObject();

                    writer.WritePropertyName(kv.Key);
                    writer.WriteValue(kv.Value);

                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
            }
            else
            {
                writer.WriteStartObject();
                writer.WritePropertyName(Condition);
                writer.WriteStartObject();
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
        }
    }
}
