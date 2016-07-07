using ES.Core.SearchCondition.Conditon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Term:ConditionBase
    {
        public Term()
            : base("term")
        {

        }

        public override ConditionBase Add(string name, object value)
        {
            //return base.Add(name, value);
            if(this._namevalues.ContainsKey(name))
            {
                (_namevalues[name] as List<object>).Add(value);
            }
            else
            {
                var list = new List<object>();
                list.Add(value);
                _namevalues[name] = list;
            }

            return this;
        }

        public override void BuildString(Newtonsoft.Json.JsonTextWriter writer)
        {
            if (_namevalues.Count > 0)
            {
                foreach (var kv in _namevalues)
                {
                    var list = (List<object>)kv.Value;
                    
                    if (list.Count > 1)
                    {
                        //writer.WriteStartObject();
                        //writer.WritePropertyName(this.Condition+"s");
                        //writer.WriteStartObject();
                        //writer.WritePropertyName(kv.Key);
                        //writer.WriteStartArray();
                        //foreach(var it in list)
                        //{
                        //    writer.WriteValue(it);
                        //}
                        //writer.WriteEndArray();

                        foreach (var it in list)
                        {
                            writer.WriteStartObject();
                            writer.WritePropertyName(this.Condition);
                            writer.WriteStartObject();
                            writer.WritePropertyName(kv.Key);
                            writer.WriteValue(it);
                            writer.WriteEndObject();
                            writer.WriteEndObject();
                        }
                    }
                    else
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName(this.Condition);
                        writer.WriteStartObject();
                        writer.WritePropertyName(kv.Key);
                        writer.WriteValue(list[0]);
                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }
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
