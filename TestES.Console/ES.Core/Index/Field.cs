using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class Field
    {
        private string _fieldname;
        public Field(string fieldname)
        {
            this._fieldname = fieldname;
        }

        private PropertyType _property=PropertyType.STRING;
        public Field SetType(PropertyType pptype)
        {
            _property = pptype;
            return this;
        }

        private string _analyzer;
        public Field SetAnalyzer(string analyzer)
        {
            _analyzer = analyzer;
            return this;
        }
    }
}
