using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x.Model
{
    public class Shards
    {
        [JsonProperty("total")]
        public int Total
        {
            get;
            set;
        }

        [JsonProperty("successful")]
        public int SuccessFul
        {
            get;
            set;
        }

        [JsonProperty("failed")]
        public int Failed
        {
            get;
            set;
        }
    }
}
