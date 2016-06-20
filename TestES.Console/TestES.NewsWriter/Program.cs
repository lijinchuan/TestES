using Ljc.Com.NewsService.Entity;
using LJC.FrameWork.Data.QuickDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestES.NewsWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var list = DataContextMoudelFactory<NewsEntity>.GetDataContext("ConndbDB$CjzfDB")
                    .WhereBigerEq(p => p.Cdate, DateTime.Now.AddDays(-7)).Top(10000000).ExecuteList();

                Console.WriteLine("开始写入新闻：" + list.Count + "条");
                int count = 0;
                foreach (var news in list)
                {
                    count++;
                    try
                    {
                        var doc = new ES.Core.ESDocument<NewsEntity>();
                        doc.Document = new NewsEntity
                        {
                            Cdate = DateTime.Now,
                            Class = news.Class,
                            Clicktime = news.Clicktime,
                            Title = news.Title,
                            IsHtmlMaked = news.IsHtmlMaked,
                            IsRead = news.IsRead,
                            Formurl = news.Formurl,
                            NewsDate = news.NewsDate,
                            Source = news.Source,
                            Content=news.Content,
                        };

                        doc.DocumentID = news.Id.ToString();
                        doc.DocumentType = "news";
                        doc.IndexName = "cjzf.news";

                        ES.Core.ESCore.Index<NewsEntity>(doc);

                        Console.WriteLine("写入新闻成功,第" + count + "条,新闻ID：" + news.Id + "：" + news.Title);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("写入新闻失败,第" + count + "条," + news.Id + "：" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();

        }

    }
}
