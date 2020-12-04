using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class AdjustCourseResp
    {

        public Validresult[] validResult { get; set; }
        public bool state { get; set; }
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public bool data { get; set; }

        public class Validresult
        {
            public string sdate { get; set; }
            public string stime { get; set; }
            public bool pass { get; set; }
            public string failReason { get; set; }
        }

    }
}
