using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core
{
    internal class IndexResponse
    {
        //{"_index":"cjzf.news","_type":"newsentity","_id":"AVUovjGkuxn7koQ8tpY0","_version":1,"_shards":{"total":2,"successful":1,"failed":0},"created":true}
        //{"_index":"cjzf.news","_type":"newsentity","_id":"1","_version":2,"_shards":{"total":2,"successful":1,"failed":0},"created":false}

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

        [JsonProperty("_index")]
        public string IndexName
        {
            get;
            set;
        }

        [JsonProperty("_type")]
        public string TypeName
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

        [JsonProperty("_version")]
        public string Version
        {
            get;
            set;
        }

        [JsonProperty("created")]
        public bool IsCreated
        {
            get;
            set;
        }

        [JsonProperty("_shards")]
        public Shards ShardsInfo
        {
            get;
            set;
        }
    }
}
