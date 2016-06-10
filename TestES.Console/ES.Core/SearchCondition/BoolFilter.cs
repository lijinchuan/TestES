using ES.Core.SearchCondition.Conditon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class BoolFilter
    {
        private Must _must;
        public BoolFilter Must(Action<Must> must)
        {
            if (_must == null)
                _must = new Must();

            must(_must);

            return this;
        }

        private MustNot _mustNot;

        public BoolFilter MustNot(Action<MustNot> mustnot)
        {
            if (_mustNot == null)
                _mustNot = new MustNot();

            mustnot(_mustNot);

            return this;
        }

        private Should _should;

        public BoolFilter Should(Action<Should> should)
        {
            if (_should == null)
                _should = new Should();

            should(_should);

            return this;
        }

        public void BuildString(JsonTextWriter writer)
        {
            writer.WritePropertyName("bool");

            writer.WriteStartObject();
            if(_must!=null)
            {
                _must.BuildString(writer);
            }

            if(_mustNot!=null)
            {
                _mustNot.BuildString(writer);
            }

            if(_should!=null)
            {
                _should.BuildString(writer);
            }

            writer.WriteEndObject();

        }
    }
}
