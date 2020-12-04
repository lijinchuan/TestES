using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x
{
    //{"_index":"cjzf.news","_type":"newsentity","_id":"1","_version":2,"found":true,"_source":{"cdate":"2016-06-07T10:45:19.6834623+08:00","mdate":"0001-01-01T00:00:00","title":null,"content":"is my first news from gjcj","class":"gjcj","source":null,"formurl":null,"keywords":null,"newsdate":"0001-01-01T00:00:00","isimp":0,"isvalid":false,"conkeywords":"abc11","id":0,"islist":false,"isread":true,"isreqest":false,"newswriter":null,"path":null,"power":0,"clicktime":1,"ishtmlmaked":true,"isnewskeyscollected":false,"oldcontent":null,"md5":"ec5020c125ddb26a5158abfcf440a107"}}

    internal class GetDocumentResponse<T>
    {
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
        public int Version
        {
            get;
            set;
        }

        [JsonProperty("found")]
        public bool Found
        {
            get;
            set;
        }

        [JsonProperty("_source")]
        public T Data
        {
            get;
            set;
        }
    }
}
