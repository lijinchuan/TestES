using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class LogInfo
    {
        public string version { get; set; }
        public long log_timestamp { get; set; }
        public string logger_name { get; set; }
        public object exception { get; set; }
        public DateTime timestamp { get; set; }
        public string app_id { get; set; }
        public string message { get; set; }
        public string host_name { get; set; }
        public string level { get; set; }
        public string type { get; set; }
    }
}
