using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class DelCourseLog
    {
        /// <summary>
        /// 排课渠道来源：0：1对1线下排课；1：OMO线上直播课
        /// </summary>
        public int ChannelOrigin { get; set; }

        /// <summary>
        /// 操作人ID(*)
        /// </summary>
        public int OpAdminId
        {
            get;
            set;
        }

        public DateTime OpDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 校区ID
        /// </summary>
        public int deptId { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public int studentId { get; set; }

        /// <summary>
        /// 删除预约课ID
        /// </summary>
        public int yuyueId { get; set; }

        /// <summary>
        /// 合同明细ID
        /// </summary>
        public int HtDetailId { get; set; }
    }
}
