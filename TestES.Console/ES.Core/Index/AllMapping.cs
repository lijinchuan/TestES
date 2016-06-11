using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class AllMapping
    {
        private bool _enabled = false;
        public void Enabled(bool boo)
        {
            _enabled = boo;
        }

        public void BuildString(JsonWriter writer)
        {

        }
    }
}
