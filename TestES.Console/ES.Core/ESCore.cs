using LJC.FrameWork.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJC.FrameWork.Comm;

namespace ES.Core
{
    public class ESCore
    {
        private static string ESBaseUrl = "http://2.5.157.118:9200/";
        private static HttpRequestEx esreqest = new HttpRequestEx();

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="indexname"></param>
        /// <param name="number_of_shards"></param>
        /// <param name="number_of_replicas"></param>
        /// <returns></returns>
        public static bool CreateIndex(string indexname,int number_of_shards=5,int number_of_replicas=1)
        {
            if (string.IsNullOrWhiteSpace(indexname))
                throw new ArgumentNullException("indexname");

            //{"settings":{"index":{"number_of_shards":"5","number_of_replicas":"1"}}}:
            string data = string.Format("{{\"settings\":{{\"index\":{{\"number_of_shards\":\"{0}\",\"number_of_replicas\":\"{1}\"}}}}}}", number_of_shards, number_of_replicas);
            var httpresponse = esreqest.DoRequest(ESBaseUrl + indexname, data, WebRequestMethodEnum.PUT, false);
            //{"acknowledged":true}

            var response = JsonHelper.JsonToEntity<AcknowledgedResponse>(httpresponse.ResponseContent);

            return response.Acknowledged;
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="indexname"></param>
        /// <returns></returns>
        public static bool DeleteIndex(string indexname)
        {
            if (string.IsNullOrWhiteSpace(indexname))
                throw new ArgumentNullException("indexname");

            var httpresponse = esreqest.DoRequest(ESBaseUrl + indexname, string.Empty, WebRequestMethodEnum.DELETE, false);

            var response = JsonHelper.JsonToEntity<AcknowledgedResponse>(httpresponse.ResponseContent);

            return response.Acknowledged;
        }

        public static void GetESStatus()
        {

        }

        /// <summary>
        /// 索引文档
        /// </summary>
        /// <param name="document"></param>
        public static void Index<T>(string indexname,ESDocument<T> document)
        {
            if (document == null || document.IsEmptyDocument())
            {
                return;
            }

            string url = ESBaseUrl + indexname + "/" + document.DocumentType + "/" + (document.DocumentID.IsEmpty() ? "" : document.DocumentID.ToString());

            var httpresponse = esreqest.DoRequest(url, document.ToString(), WebRequestMethodEnum.PUT, false);
        }
    }
}
