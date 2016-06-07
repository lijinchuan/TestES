using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core
{
    internal class BulkOpResponse
    {
        [JsonProperty("took")]
        public int Took
        {
            get;
            set;
        }

        [JsonProperty("errors")]
        public bool Errors
        {
            get;
            set;
        }
    }
}
