using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class AdjustCourseReq
    {
        public int ChannelOrigin { get; set; }
        public int? AdjustChannelOrigin { get; set; }
        public int CourseType { get; set; }
        public int admintype { get; set; }
        public int adminid { get; set; }
        public string adminarea { get; set; }
        public int yuyueId { get; set; }
        public int studentId { get; set; }
        public int contractDetailId { get; set; }
        public int deptId { get; set; }
        public int teacherId { get; set; }
        public string subject { get; set; }
        public string grade { get; set; }
        public Bookclassdatetime[] bookClassDateTimes { get; set; }
        public int status { get; set; }
        public int OpAdminId { get; set; }
        public int? OpAdminArea { get; set; }
        public int? CitySysCityId { get; set; }

        public class Bookclassdatetime
        {
            public string classDate { get; set; }
            public string classTime { get; set; }
            public float consumeCoefficient { get; set; }
        }


    }
}
