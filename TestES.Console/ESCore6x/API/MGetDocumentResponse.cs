using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x
{
    internal class MGetDocumentResponse<T>
    {
        [JsonProperty("docs")]
        public GetDocumentResponse<T>[] Docs
        {
            get;
            set;
        }
    }
}
