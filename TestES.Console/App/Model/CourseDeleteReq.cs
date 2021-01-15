using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class CourseDeleteReq
    {
        /// <summary>
        /// 默认为jr-gxh，jrzx表示精锐在线；
        /// </summary>
        public string Org_code
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人ID(*)
        /// </summary>
        public int OpAdminId
        {
            get;
            set;
        }

        /// <summary>
        /// 校区查看范围(*)
        /// </summary>
        public string OpAdminArea
        {
            get;
            set;
        }

        /// <summary>
        /// 城市化改革-排课城市
        /// </summary>
        public int? CitySysCityId
        {
            get;
            set;
        }

        /// <summary>
        /// 排课渠道来源：0：1对1线下排课；1：OMO线上直播课
        /// </summary>
        public int ChannelOrigin { get; set; }
        /// <summary>
        /// 校区ID
        /// </summary>
        public int deptId { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public int studentId { get; set; }

        /// <summary>
        /// 删除预约课ID集合
        /// </summary>
        public List<int> yuyueIds { get; set; }

        /// <summary>
        /// 合同明细ID
        /// </summary>
        public int HtDetailId { get; set; }

        /// <summary>
        /// 是否是批量删除
        /// </summary>
        public int isBatchDel { get; set; }

        /// <summary>
        /// 是否验证所有排课，false不会验证所有冲突
        /// </summary>
        public bool ValidAllErrors
        {
            get;
            set;
        }
    }
}
