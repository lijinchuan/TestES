using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Model
{
    public class AnalyzeTokenBag
    {
        [JsonProperty("tokens")]
        public AnalyzeToken[] Tokens
        {
            get;
            set;
        }

    }

    public class AnalyzeToken
    {
        [JsonProperty("token")]
        public string Token
        {
            get;
            set;
        }

        [JsonProperty("start_offset")]
        public int StartOffset
        {
            get;
            set;
        }

        [JsonProperty("end_offset")]
        public int EndOffset
        {
            get;
            set;
        }

        [JsonProperty("type")]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty("position")]
        public int Position
        {
            get;
            set;
        }
    }
}
