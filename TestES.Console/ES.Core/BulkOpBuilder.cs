using LJC.FrameWork.Comm;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ES.Core
{
    public class BulkOpBuilder
    {
        public enum OpTypes
        {
            create,
            index,
            update,
            delete,
        }

        StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 文档不存在时创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public BulkOpBuilder Create<T>(ESDocument<T> doc)
        {
            if (doc != null)
            {
                if(string.IsNullOrWhiteSpace(doc.DocumentID))
                {
                    throw new ArgumentNullException("DocumentID");
                }

                sb.AppendFormat("{{ \"{0}\":  {{ \"_index\": \"{1}\", \"_type\": \"{2}\", \"_id\": \"{3}\" }}}}\n", OpTypes.create, doc.IndexName, doc.DocumentType, doc.DocumentID);
                sb.Append(doc.ToString() + "\n");
            }
            return this;
        }

        /// <summary>
        /// 创建或者替换，如果没有指定ID，则创建，指定了则替换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public BulkOpBuilder Index<T>(ESDocument<T> doc)
        {
            if (doc != null)
            {
                if(string.IsNullOrWhiteSpace(doc.DocumentID))
                {
                    sb.AppendFormat("{{ \"{0}\": {{ \"_index\": \"{1}\", \"_type\": \"{2}\" }}}}\n", OpTypes.index, doc.IndexName, doc.DocumentType);
                    sb.Append(doc.ToString() + "\n");
                }
                else
                {
                    sb.AppendFormat("{{ \"{0}\": {{ \"_index\": \"{1}\", \"_type\": \"{2}\",\"_id\":{3} }}}}\n", OpTypes.index, doc.IndexName, doc.DocumentType, doc.DocumentID);
                    sb.Append(doc.ToString() + "\n");
                }
            }
            return this;
        }

        /// <summary>
        /// 部分更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public BulkOpBuilder Update<T>(ESDocument<T> doc, params Expression<Func<T, object>>[] predicate)
        {
            if(doc!=null)
            {
                sb.AppendFormat("{{\"{0}\":{{ \"_index\":\"{1}\",\"_type\":\"{2}\",\"_id\":\"{3}\",\"_retry_on_conflict\":3}}}}\n", OpTypes.update, doc.IndexName, doc.DocumentType, doc.DocumentID);
                if(predicate==null||predicate.Length==0)
                {
                    sb.AppendFormat("{{\"doc\":{0}}}\n", doc.ToString());
                }
                else
                {
                    sb.Append("{{");

                    string membername = string.Empty;
                    using (Newtonsoft.Json.JsonTextWriter jw = new Newtonsoft.Json.JsonTextWriter(new StringWriter(sb)))
                    {
                        foreach (var selecter in predicate)
                        {
                            jw.WritePropertyName(JsonHelper.GetJsonTag(selecter,out membername));
                            var val = doc.Document.Eval(membername);
                            
                            jw.WriteValue(val);
                        }
                    }

                    sb.Append("}}\n");
                }
            }
            return this;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public BulkOpBuilder Delete<T>(ESDocument<T> doc)
        {
            if (doc != null)
            {
                sb.AppendFormat("{{ \"{0}\":  {{ \"_index\": \"{1}\", \"_type\": \"{2}\", \"_id\": \"{3}\" }}}}\n", OpTypes.delete, doc.IndexName, doc.DocumentType, doc.DocumentID);
            }
            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
