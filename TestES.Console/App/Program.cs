using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ES.Core6x;
using App.Model;
using LJC.FrameWork.Comm;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<AdjustCourseLog> adjustCourseLogs = new List<AdjustCourseLog>();
            var regex = new Regex(@"参数：\{""req"":(.*)\}[\r\n]?.*结果：(.*)[\r\n]?.*");
            for (DateTime dt = DateTime.Parse("2020-10-26"); dt <= DateTime.Parse("2020-11-30"); dt = dt.AddDays(1))
            {
                var index = $"upc-course-public-api-{dt:yyyy.MM.dd}";
                ESCore eSCore = new ESCore($"http://192.168.100.92:9200/");
                //ESCore eSCore = new ESCore("http://192.168.100.92:9200");
                var search = new ES.Core6x.SearchCondition.Search();
                var ts1 = LJC.FrameWork.Comm.DateTimeHelper.GetTimeStamp(DateTime.Parse("2020-11-01"));
                var ts2 = LJC.FrameWork.Comm.DateTimeHelper.GetTimeStamp(DateTime.Parse("2020-12-01"));
                search.Query(q => q.Bool(t => t.Must(m => m.Match(te => te.Add("message", "adjustcourse")))
                //.Must(m=>m.Term(te=>te.Add("type", "upc-course-public-api")))
                //.Must(m => m.Range(r => r.AddGT("log_timestamp", ts1).AddLT("log_timestamp", ts2)))
                )).Size(10000);
                //ES.Core6x.SearchBuilder<string> sb = new SearchBuilder<string>();

                var resp = eSCore.Search<LogInfo>(index, search);

                foreach(var item in resp.Hits.Hits)
                {
                    var m = regex.Match(item.Source.message);
                    if (m.Success)
                    {
                        var adjustcourseresp = JsonUtil<AdjustCourseResp>.Deserialize(m.Groups[2].Value);

                        if (adjustcourseresp.data)
                        {
                            var adjustcoursereq = JsonUtil<AdjustCourseReq>.Deserialize(m.Groups[1].Value);

                            adjustCourseLogs.Add(new AdjustCourseLog
                            {
                                OldSourceFlag = adjustcoursereq.ChannelOrigin,
                                AdjustDate = adjustcoursereq.bookClassDateTimes[0].classDate,
                                AdjsutTime = adjustcoursereq.bookClassDateTimes[0].classTime,
                                Dept = adjustcoursereq.deptId,
                                OldCourse = adjustcoursereq.yuyueId,
                                Opid = adjustcoursereq.adminid,
                                Studentid = adjustcoursereq.studentId,
                                OpDateTime =LJC.FrameWork.Comm.DateTimeHelper.FromTimeStamp(item.Source.log_timestamp),
                                subject = adjustcoursereq.subject
                            });
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (var log in adjustCourseLogs)
            {
                if (sb.Length > 0)
                {
                    sb.Append("union all ");
                }
                sb.AppendLine($"select {log.Dept} dept,'{log.subject}' subject,{log.OldSourceFlag} sf,{log.OldCourse} yuyueid,{log.Studentid} sid,'{log.AdjustDate}' adjustdate,'{log.AdjsutTime}' adjusttime,{log.Opid} opid,'{log.OpDateTime:yyyy-MM-dd HH:mm:ss}' opdate");
            }

            var sql = sb.ToString();
        }
    }
}
