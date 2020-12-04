using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.Index
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
                PPlist.Add(pp);
            }

            property(pp);
            return this;
        }

        internal void BuildString(JsonWriter writer)
        {
            writer.WritePropertyName("properties");

            writer.WriteStartObject();

            foreach(var pp in PPlist)
            {
                pp.BuildString(writer);
            }

            writer.WriteEndObject();
        }
    }
}
