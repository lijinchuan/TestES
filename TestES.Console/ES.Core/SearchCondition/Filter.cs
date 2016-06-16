using ES.Core.SearchCondition.Conditon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Filter
    {
        private BoolFilter _bool;

        public Filter Bool(Action<BoolFilter> boolfilter)
        {
            if (_bool == null)
                _bool = new BoolFilter();

            boolfilter(_bool);

            return this;
        }

        internal void BuildString(JsonTextWriter writer)
        {
            writer.WritePropertyName("filter");
            bool isempty = true;
            if(_bool!=null)
            {
                isempty = false;
                writer.WriteStartObject();
                _bool.BuildString(writer);
                writer.WriteEndObject();
            }
            

            if(isempty)
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }
    }
}
