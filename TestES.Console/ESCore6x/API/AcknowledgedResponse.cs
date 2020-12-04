using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x
{
    internal class AcknowledgedResponse
    {
        [JsonProperty("acknowledged")]
        public bool Acknowledged
        {
            get;
            set;
        }
    }
}
