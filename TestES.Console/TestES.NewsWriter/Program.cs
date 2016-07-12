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
        static ES.Core.ESCore escore = new ES.Core.ESCore("http://ljcserver:9200/");
        static void Main(string[] args)
        {
                var maxid = DataContextMoudelFactory<NewsEntity>.GetDataContext("ConndbDB$CjzfDB").Max("id");
                //var maxid = 304734;
                List<NewsEntity> list;
                while ((list = DataContextMoudelFactory<NewsEntity>.GetDataContext("ConndbDB$CjzfDB")
                    .WhereSmallerEq(p => p.Id, maxid)
                    .WhereNotEq(p => p.Class, "公告")
                    .OrderByDesc(p => p.Id)
                    .Top(1000).ExecuteList()).Count > 0)
                {
                    maxid = list.Last().Id - 1;
                    try
                    {
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
                                    Class = news.Class,
                                    Title = news.Title,
                                    NewsDate = news.NewsDate,
                                    Source = news.Source,
                                    Content = news.Content,
                                    Id = news.Id,
                                };

                                doc.DocumentID = news.Id.ToString();
                                doc.DocumentType = "news";
                                doc.IndexName = "cjzf.news";
                                escore.Index<NewsEntity>(doc);

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
                }

            Console.Read();

        }

    }
}
