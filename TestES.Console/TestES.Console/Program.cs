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
        static string BaseUrl = "http://2.5.157.189:9200/";

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
            s.Query(p => p.Filter(f => f.Bool(b => b.Must(m => m.Term(t => t.Add("address", "mill").Add("ss", "sfd"))
                .Range(r=>r.AddGT("g",1).AddLT("g",2).AddLT("x",10)))
                .Should(sx=>sx.Term(t=>t.Add("ff","ll")).Prefix("mm","我爱你"))
                ))//.Filter(f2=>f2.Range(r=>r.AddGT("r11",0).AddLTE("r11",10).AddGT("r12",11).AddLT("r13",19)))
                ).Source(s2=>s2.Add("haha").Add("ssssss"));
            var str = s.ToString();
        }

        static void Mapping()
        {
            Mappings mps = new Mappings("news");
            mps.Deafult(d => d.All(a => a.Enabled(true))).Resource(r => r.Property("p1", p1 => p1.SetIndex("idex1")).Property("p2",p2=>p2.SetStore(true)));
        }

        static void Main(string[] args)
        {
            

            //var boo = ES.Core.ESCore.CreateIndex("test2");

            //var boo1 = ES.Core.ESCore.DeleteIndex("test2");

            //AddNews();

            //GetNews();

            //Exsits();

            //MGet();

            //TestMOp();

            //Search2();

            Mapping();
        }
    }
}
