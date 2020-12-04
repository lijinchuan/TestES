using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class AdjustCourseLog
    {
        public int Studentid
        {
            get;
            set;
        }

        public int OldCourse
        {
            get;
            set;
        }

        public int OldSourceFlag
        {
            get;
            set;
        }

        public string subject
        {
            get;
            set;
        }

        public int Dept
        {
            get;
            set;
        }

        public string AdjustDate
        {
            get;
            set;
        }

        public string AdjsutTime
        {
            get;
            set;
        }

        public DateTime OpDateTime
        {
            get;
            set;
        }

        public int Opid
        {
            get;
            set;
        }
    }
}
