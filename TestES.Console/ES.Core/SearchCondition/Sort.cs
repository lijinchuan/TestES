using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class Sort
    {
        private Dictionary<string, string> _sortList = new Dictionary<string, string>();

        public void Asc(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
               
            }

            _sortList[name] = "asc";
        }

        public void Desc(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");

            }

            _sortList[name] = "desc";
        }
    }
}
