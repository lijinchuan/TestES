using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class MappingResource
    {
        private Property ppt;

        public MappingResource Property(Action<Property> property)
        {
            if (ppt == null)
                ppt = new Property();

            property(ppt);

            return this;
        }

        public void BuildString(JsonWriter writer)
        {

        }
    }
}
