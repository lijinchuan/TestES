using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class Properties
    {
        private List<Property> PPlist = new List<Property>();

        public Properties Property(string ppname, Action<Property> property)
        {
            if (string.IsNullOrWhiteSpace(ppname))
                throw new ArgumentNullException(ppname);

            Property pp = PPlist.FirstOrDefault(p => p._propertyName.Equals(ppname));
            if(pp==null)
            {
                pp = new Property().SetPropertyName(ppname);
            }

            property(pp);
            return this;
        }

        public void BuildString(JsonWriter writer)
        {

        }
    }
}
