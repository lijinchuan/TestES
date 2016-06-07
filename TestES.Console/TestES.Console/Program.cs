using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ljc.Com.NewsService.Entity;

namespace TestES.Console
{
    class Program
    {
        static string BaseUrl = "http://2.5.157.118:9200/";

        public static void AddNews()
        {
            var doc=new ES.Core.ESDocument<NewsEntity>();
            doc.Document=new NewsEntity{
                Cdate=DateTime.Now,
                Class="gjcj",
                Clicktime=1,
                Conkeywords="abc",
                Content="is my first news from gjcj",
                IsHtmlMaked=true,
                IsRead=true,
            };

            var documentID = new ES.Core.DocumentID();
            documentID.LongId = 1;
            doc.DocumentID = documentID;

            ES.Core.ESCore.Index<NewsEntity>("cjzf.news", doc);
        }

        static void Main(string[] args)
        {
            //var boo = ES.Core.ESCore.CreateIndex("test2");

            //var boo1 = ES.Core.ESCore.DeleteIndex("test2");

            AddNews();
        }
    }
}
