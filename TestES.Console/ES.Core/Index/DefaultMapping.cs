using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class DefaultMapping
    {
        private AllMapping _all = null;

        public DefaultMapping All(Action<AllMapping> allmapping)
        {
            if (_all == null)
                _all = new AllMapping();

            allmapping(_all);
            return this;
        }

        public void BuildString(JsonWriter writer)
        {

        }
    }
}
