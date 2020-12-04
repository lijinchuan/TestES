using LJC.FrameWork.Comm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core6x
{
    public class ESDocument<T>
    {
        public ESDocument()
        {

        }

        public ESDocument(string index, string type, T document)
        {
            IndexName = index;
            DocumentType = type;
            Document = document;
        }

        [JsonProperty("_index")]
        public string IndexName
        {
            get;
            set;
        }

        private string _documentType = null;
        [JsonProperty("_type")]
        public string DocumentType
        {
            get
            {
                if (_documentType != null)
                    return _documentType;

                _documentType = typeof(T).Name.ToLower();

                return _documentType;
            }
            set
            {
                _documentType = value;
            }
        }

        [JsonProperty("_id")]
        public string DocumentID
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

        public T Document
        {
            get;
            set;
        }

        public bool IsEmptyDocument()
        {
            return object.Equals(Document, default(T));
        }

        public override string ToString()
        {
            return Document == null ? "{}" : Document.ToJson();
        }
    }
}
