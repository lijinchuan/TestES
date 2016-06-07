using LJC.FrameWork.Comm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core
{
    public class ESDocument<T>
    {
        [JsonIgnore]
        public DocumentID DocumentID
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

        private string _documentType = null;
        [JsonIgnore]
        public string DocumentType
        {
            get
            {
                if (_documentType != null)
                    return _documentType;

                _documentType = typeof(T).Name.ToLower();

                return _documentType;
            }
        }

        public override string ToString()
        {
            return Document == null ? "{}" : Document.ToJson();
        }
    }
}
