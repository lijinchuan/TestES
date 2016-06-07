using LJC.FrameWork.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

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

            string data = string.Format("{{\"settings\":{{\"index\":{{\"number_of_shards\":\"{0}\",\"number_of_replicas\":\"{1}\"}}}}}}", number_of_shards, number_of_replicas);
            var httpresponse = esreqest.DoRequest(ESBaseUrl + indexname, data, WebRequestMethodEnum.PUT, false);

            if (!httpresponse.Successed)
                return false;

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

            if(!httpresponse.Successed)
            {
                return false;
            }

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
        public static string Index<T>(ESDocument<T> document)
        {
            if (document == null || document.IsEmptyDocument())
            {
                return string.Empty;
            }

            string url = ESBaseUrl + document.IndexName + "/" + document.DocumentType + "/" + (document.DocumentID??"");

            var httpresponse = esreqest.DoRequest(url, document.ToString(), document.DocumentID == null ? WebRequestMethodEnum.POST : WebRequestMethodEnum.PUT, false);

            if(!httpresponse.Successed)
            {
                return string.Empty;
            }

            var response = JsonHelper.JsonToEntity<IndexResponse>(httpresponse.ResponseContent);

            return response.ID;
        }


        public static ESDocument<T> Get<T>(string indexname,string typename,string documentid)
        {
            string url = ESBaseUrl + indexname + "/" + typename + "/" + documentid;

            var httpresponse = esreqest.DoRequest(url, string.Empty);

            if(!httpresponse.Successed)
            {
                return null;
            }

            var response = JsonHelper.JsonToEntity<GetDocumentResponse<T>>(httpresponse.ResponseContent);

            if (!response.Found)
                return null;

            ESDocument<T> doc=new ESDocument<T>();
            doc.Document=response.Data;
            doc.DocumentID=response.ID;
            doc.Version=response.Version;
            return doc;
        }

        public static T Get2<T>(string indexname, string typename, string documentid)
        {
            string url = ESBaseUrl + indexname + "/" + typename + "/" + documentid+"/_source";

            var httpresponse = esreqest.DoRequest(url, string.Empty);

            if(!httpresponse.Successed)
            {
                return default(T);
            }

            var response = JsonHelper.JsonToEntity<T>(httpresponse.ResponseContent);

            return response;
        }

        public static T Get3<T>(string indexname, string typename, string documentid,params Expression<Func<T,object>>[] predicate)
        {
            string url = ESBaseUrl + indexname + "/" + typename + "/" + documentid;

            if (predicate != null && predicate.Length > 0)
            {
                url += "?_source=";

                foreach (var colselector in predicate)
                {
                    url += JsonHelper.GetJsonTag(colselector) + ",";
                }
            }

            var httpresponse = esreqest.DoRequest(url, string.Empty);

            if (!httpresponse.Successed)
            {
                return default(T);
            }

            var response = JsonHelper.JsonToEntity<GetDocumentResponse<T>>(httpresponse.ResponseContent);

            return response.Data;
        }

        public static bool Exists(string indexname, string typename, string documentid)
        {
            string url = ESBaseUrl + indexname + "/" + typename + "/" + documentid;

            var httpresponse = esreqest.DoRequest(url, string.Empty, WebRequestMethodEnum.HEAD, false);

            if (!httpresponse.Successed)
            {
                throw httpresponse.ErrorMsg;
            }

            return httpresponse.Successed;
        }

        public static ESDocument<T>[] MGet<T>(string indexname, string typename, string[] documentid)
        {
            string url = ESBaseUrl + indexname + "/" + typename + "/_mget";

            var request = new
            {
                ids=documentid,
            };

            var data = JsonHelper.ToJson(request);

            //{"docs":[{"_index":"cjzf.news","_type":"newsentity","_id":"1","_version":2,"found":true,"_source":{"cdate":"2016-06-07T10:45:19.6834623+08:00","mdate":"0001-01-01T00:00:00","title":null,"content":"is my first news from gjcj","class":"gjcj","source":null,"formurl":null,"keywords":null,"newsdate":"0001-01-01T00:00:00","isimp":0,"isvalid":false,"conkeywords":"abc11","id":0,"islist":false,"isread":true,"isreqest":false,"newswriter":null,"path":null,"power":0,"clicktime":1,"ishtmlmaked":true,"isnewskeyscollected":false,"oldcontent":null,"md5":"ec5020c125ddb26a5158abfcf440a107"}},{"_index":"cjzf.news","_type":"newsentity","_id":"AVUop3rauxn7koQ8tpYk","_version":1,"found":true,"_source":{"cdate":"2016-06-07T10:19:30.9142936+08:00","mdate":"0001-01-01T00:00:00","title":null,"content":"is my first news from gjcj","class":"gjcj","source":null,"formurl":null,"keywords":null,"newsdate":"0001-01-01T00:00:00","isimp":0,"isvalid":false,"conkeywords":"abc","id":0,"islist":false,"isread":true,"isreqest":false,"newswriter":null,"path":null,"power":0,"clicktime":1,"ishtmlmaked":true,"isnewskeyscollected":false,"oldcontent":null,"md5":"ec5020c125ddb26a5158abfcf440a107"}},{"_index":"cjzf.news","_type":"newsentity","_id":"AVUoqgM6uxn7koQ8tpYm","_version":1,"found":true,"_source":{"cdate":"2016-06-07T10:21:53.935455+08:00","mdate":"0001-01-01T00:00:00","title":null,"content":"is my first news from gjcj","class":"gjcj","source":null,"formurl":null,"keywords":null,"newsdate":"0001-01-01T00:00:00","isimp":0,"isvalid":false,"conkeywords":"abc","id":0,"islist":false,"isread":true,"isreqest":false,"newswriter":null,"path":null,"power":0,"clicktime":1,"ishtmlmaked":true,"isnewskeyscollected":false,"oldcontent":null,"md5":"ec5020c125ddb26a5158abfcf440a107"}}]}
            var httpresponse = esreqest.DoRequest(url, data, WebRequestMethodEnum.POST);

            if (!httpresponse.Successed)
            {
                return null;
            }

            var response = JsonHelper.JsonToEntity<MGetDocumentResponse<T>>(httpresponse.ResponseContent);

            return response.Docs.Select(p=>new ESDocument<T>
                {
                    Document=p.Data,
                    Version=p.Version,
                    DocumentID=p.ID
                }).ToArray();
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="bulkopbuilder"></param>
        public static void BulkOperator(BulkOpBuilder bulkopbuilder)
        {
            if (bulkopbuilder == null)
                return;

            var url = ESBaseUrl + "_bulk";

            var httpresponse = esreqest.DoRequest(url, bulkopbuilder.ToString(), WebRequestMethodEnum.POST, false);
            if (!httpresponse.Successed)
                return;


        }
    }
}
