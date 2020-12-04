using ES.Core6x.SearchCondition.Conditon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.SearchCondition
{
    public class Range:ConditionBase
    {
        public Range()
            : base("range")
        {

        }

        public override ConditionBase Add(string name, object value)
        {
            return this;
        }

        public Range AddGT(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");

            object val;
            if(this._namevalues.TryGetValue(name,out val))
            {
                var dic = (Dictionary<string, object>)val;
                dic["gt"] = value;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["gt"] = value;
                _namevalues[name] = dic;
            }

            return this;
        }

        public Range AddGTE(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");

            object val;
            if (this._namevalues.TryGetValue(name, out val))
            {
                var dic = (Dictionary<string, object>)val;
                dic["gte"] = value;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["gte"] = value;
                _namevalues[name] = dic;
            }

            return this;
        }

        public Range AddLT(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");
            object val;
            if (this._namevalues.TryGetValue(name, out val))
            {
                var dic = (Dictionary<string, object>)val;
                dic["lt"] = value;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["lt"] = value;
                _namevalues[name] = dic;
            }

            return this;
        }

        public Range AddLTE(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");

            object val;
            if (this._namevalues.TryGetValue(name, out val))
            {
                var dic = (Dictionary<string, object>)val;
                dic["lte"] = value;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["lte"] = value;
                _namevalues[name] = dic;
            }

            return this;
        }

        public override void BuildString(Newtonsoft.Json.JsonTextWriter writer)
        {
            if (this._namevalues.Count > 0)
            {
                foreach (var kv in this._namevalues)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName(Condition);

                    writer.WriteStartObject();

                    writer.WritePropertyName(kv.Key);

                    writer.WriteStartObject();
                    foreach (var kv2 in (Dictionary<string, object>)kv.Value)
                    {
                        writer.WritePropertyName(kv2.Key);
                        writer.WriteValue(kv2.Value);
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();

                    writer.WriteEndObject();
                }
            }
            else
            {

            }
        }
    }
}
