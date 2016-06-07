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

            ES.Core.ESCore.Index<NewsEntity>(doc);
        }

        public static void GetNews()
        {
            ES.Core.ESCore.Get3<NewsEntity>("cjzf.news", "newsentity", "1", p => p.NewsDate, p => p.Title, p => p.Content);
        }

        public static void Exsits()
        {
            var boo = ES.Core.ESCore.Exists("cjzf.news", "newsentity", "2");
        }


        public static void MGet()
        {
            var docs = ES.Core.ESCore.MGet<NewsEntity>("cjzf.news", "newsentity", new[] { "1", "AVUop3rauxn7koQ8tpYk", "AVUoqgM6uxn7koQ8tpYm" });
        }

        static void TestMOp()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 100000; i++)
            {
                ES.Core.BulkOpBuilder bb = new ES.Core.BulkOpBuilder();
                bb.Update<NewsEntity>(new ES.Core.ESDocument<NewsEntity>
                {
                    DocumentID = "1",
                    Document = new NewsEntity
                    {
                        Content = "test",
                        Title = "title",
                        NewsDate = DateTime.Now,
                        IsList = true,
                    },
                    IndexName = "xxx",
                }, p => p.Title, p => p.Content, p => p.NewsDate, p => p.IsList);

                string b = bb.ToString();
            }
            sw.Stop();

            var el = sw.ElapsedMilliseconds;
        }

        static void Main(string[] args)
        {
            //var boo = ES.Core.ESCore.CreateIndex("test2");

            //var boo1 = ES.Core.ESCore.DeleteIndex("test2");

            //AddNews();

            //GetNews();

            //Exsits();

            //MGet();

            TestMOp();
        }
    }
}
