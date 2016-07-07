using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public enum PropertyType
    {
        STRING,
        DATE,
        integer,
        BOOLEAN,
        BINARY,
        IP,
        OBJECT,
        NESTED,
        GEO_POINT,
        GEO_SHAPE,
        COMPLETION,
    }
}
