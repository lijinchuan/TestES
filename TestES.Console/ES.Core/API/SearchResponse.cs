using ES.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.API
{
    public class SearchResponse<T>
    {
        public class HitsArray
        {
            [JsonProperty("total")]
            public int Total
            {
                get;
                set;
            }

            [JsonProperty("max_score")]
            public double? MaxScore
            {
                get;
                set;
            }

            [JsonProperty("hits")]
            public Hit[] Hits
            {
                get;
                set;
            }
        }

        public class Hit
        {
            [JsonProperty("_index")]
            public string Index
            {
                get;
                set;
            }

            [JsonProperty("_type")]
            public string Type
            {
                get;
                set;
            }

            [JsonProperty("_id")]
            public string ID
            {
                get;
                set;
            }

            [JsonProperty("_score")]
            public double? Score
            {
                get;
                set;
            }

            [JsonProperty("_source")]
            public T Source
            {
                get;
                set;
            }
        }

        [JsonProperty("took")]
        public int Took
        {
            get;
            set;
        }

        [JsonProperty("timed_out")]
        public bool TimeOut
        {
            get;
            set;
        }

        [JsonProperty("_shards")]
        public Shards Shards
        {
            get;
            set;
        }

        [JsonProperty("hits")]
        public HitsArray Hits
        {
            get;
            set;
        }
    }
}
