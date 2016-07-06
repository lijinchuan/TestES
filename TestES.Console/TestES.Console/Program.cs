using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ljc.Com.NewsService.Entity;
using ES.Core.SearchCondition;
using ES.Core.Index;
using LJC.FrameWork.Comm;

namespace TestES.Console
{
    class Program
    {
        static ES.Core.ESCore escore = new ES.Core.ESCore("http://2.5.158.163:8080/el/");

        public static void AddNews()
        {
            var doc=new ES.Core.ESDocument<NewsEntity>();
            doc.Document=new NewsEntity{
                Cdate=DateTime.Now,
                Class="gjcj",
                Clicktime=1,
                Title="英国脱欧公投悬念重重 高盛：如何交易英镑？",
                Content=@"FX168财经报社(香港)讯 随着英国6月23日脱欧公投日的临近，近期英镑汇率波动明显加剧，高盛集团(Goldman Sachs)在最新的报告中对于围绕公投结果如何交易英镑向投资者给出建议。
      以下是高盛分析师Silvia Ardagna、Robin Brooks和Michael Cahill所撰报告的主要内容：
      跟所有主要货币一样，上周五(6月3日)异常糟糕的非农报告之后，英镑出现上涨，但涨势不及其它主要货币。英镑兑十国集团(G10)货币(除美元之外)出现贬值，这一趋势在大约10天前就变得愈发明显，当时对英国公投结果的民调再度显示，脱欧阵营和留欧阵营将势均力敌。",
                IsHtmlMaked=true,
                IsRead=true,
            };

            doc.DocumentID = "284266";
            doc.IndexName = "cjzf.news";

            escore.Index<NewsEntity>(doc);
        }

        public static void GetNews()
        {
            escore.Get3<NewsEntity>("cjzf.news", "newsentity", "1", p => p.NewsDate, p => p.Title, p => p.Content);
        }

        public static void Exsits()
        {
            var boo = escore.Exists("cjzf.news", "newsentity", "2");
        }


        public static void MGet()
        {
            var docs = escore.MGet<NewsEntity>("cjzf.news", "newsentity", new[] { "1", "AVUop3rauxn7koQ8tpYk", "AVUoqgM6uxn7koQ8tpYm" });
        }

        static void TestMOp()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 100000; i++)
            {
                ;
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

        static void Search()
        {
            //var sb = new ES.Core.SearchBuilder<NewsEntity>();
            //var str = sb.Filter(p => p.Class == "x" && p.Clicktime > 11).ToString();

            //var x=new ES.Core.SearchCondition.Query().Filtered.Filter;
            //var str = sb.Filter(p => p.Class == "x" && p.Clicktime > 11&&p.Content=="xxxxxx").ToString();

            var sb2=new ES.Core.QueryBuilder<NewsEntity>();
            var str2=sb2.Filter(p => p.Class == "x" && p.Clicktime > 11&&p.Content=="xxxxxx").ToString();
        }

        static void Search2()
        {
            ES.Core.SearchCondition.Search s = new Search();
            s.Query(q => q.Filter(f => f.Bool(b => b.Must(m => m.Match(t=>t.Add("class","国际财经")))))).From(0).Size(10);
            

            var str = s.ToString();

            var result = escore.Search<NewsEntity>(s);
        }

        static void Search3()
        {
            ES.Core.SearchCondition.Search s = new Search();
            s.Query(q => q.Filter(f => f.Bool(b => b.Must(m => m.Term(t => t.Add("title", "万科").Add("title", "公告")))))).From(0).Size(10);


            var str = s.ToString();

            var result = escore.Search<NewsEntity>(s);
        }

        static void Mapping()
        {
            Mappings mps = new Mappings();
            mps.Mapping("news",r => r.Property("p1", p1 => p1.SetIndex(PropertyIndexSet.not_analyzed)).Property("p2",p2=>p2.SetStore(true)));
        }

        static void Main(string[] args)
        {
            //var boo1 = ES.Core.ESCore.DeleteIndex("cjzf.news");

            //var boo = ES.Core.ESCore.CreateIndex("cjzf.news", new IndexSetting(), new Mappings()
            //    .Mapping("news",r => r.EnableSource(true).Property("content", p => p.SetAnalyzer("ik").SetType(PropertyType.STRING))
            //    .Property("class", p => p.SetIndex(PropertyIndexSet.not_analyzed).SetType(PropertyType.STRING))
            //    .Property("title", p => p.SetType(PropertyType.STRING).SetAnalyzer("ik"))
            //    .Property("source", p => p.SetType(PropertyType.STRING).SetAnalyzer("ik"))));

            //var boo = ES.Core.ESCore.CreateIndex("test2");

            //var boo1 = ES.Core.ESCore.DeleteIndex("test2");

            //AddNews();

            //GetNews();

            //Exsits();

            //MGet();

            //TestMOp();

            Search3();

            //Mapping();
        }
    }
}
