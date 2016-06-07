using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core
{
    public struct DocumentID
    {
        private long _longid;
        public long LongId
        {
            get
            {
                return _longid;
            }
            set
            {
                if (_uuid != null)
                    throw new NotSupportedException("uuid has valued");
                _longid = value;
            }
        }


        private string _uuid;
        public string UUID
        {
            get
            {
                return _uuid;
            }
            set
            {
                if (_longid != 0L)
                    throw new NotImplementedException("longid has valued");
                _uuid = value;
            }
        }

        public bool IsEmpty()
        {
            return _uuid == null && _longid == long.MinValue;
        }

        public override string ToString()
        {
            if (_longid != 0L)
                return _longid.ToString();

            if (_uuid != null)
                return _uuid;

            return string.Empty;
        }
    }
}
