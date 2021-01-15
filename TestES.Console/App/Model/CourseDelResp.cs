using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class CourseDelResp
    {
        public bool state { get; set; }
        public String error_code { get; set; }
        public String error_msg { get; set; }
        public bool data { get; set; }


    }
}
