using ES.Core.SearchOperator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ES.Core
{
    public class SearchBuilder<T>
    {
        private StringBuilder sb = new StringBuilder();
        private void AnsyExpression(Expression expression,ref SearchCondition query)
        {
            if(query==null)
            {
                query=new SearchCondition("query");
            }

            if(expression is MemberExpression)
            {
                var memberexp = expression as MemberExpression;
                var nodetype = memberexp.NodeType;
                return;
            }

            if(expression is BinaryExpression)
            {
                SearchCondition condtion = null;
                var binexp = expression as BinaryExpression;

                if(!(binexp.Left is BinaryExpression))
                {
                    
                    string jsonname = string.Empty;
                    switch(binexp.NodeType)
                    {
                        case ExpressionType.GreaterThan:
                            {
                                condtion = new Range();
                            }
                            break;
                        case ExpressionType.Equal:
                            {
                                condtion = new TermCondition();
                            }
                            break;
                        case ExpressionType.NotEqual:
                            {
                                condtion = new MustNotCodition();
                                break;
                            }
                        default:
                            throw new NotSupportedException("不支持的操作：" + binexp.NodeType);
                    }

                    if(binexp.Left is MemberExpression)
                    {
                        var memberexp = binexp.Left as MemberExpression;
                        var jsonprop = (JsonPropertyAttribute)memberexp.Member.GetCustomAttributes(typeof(JsonPropertyAttribute), true).FirstOrDefault();
                        if (jsonprop != null)
                        {
                            jsonname = jsonprop.PropertyName;
                            
                        }
                        else
                        {
                            jsonname = memberexp.Member.Name;
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("表达式左侧不是属性");
                    }

                    if(binexp.Right is ConstantExpression)
                    {
                        var cst = binexp.Right as ConstantExpression;
                        if (condtion is Range)
                        {
                            (condtion as Range).FilterCollection.Add((FilterCodition)new GreaterThen(cst.Value));
                        }
                        else if (condtion is TermCondition)
                        {
                            var sc = new SearchCondition(jsonname);
                            sc.Value = cst.Value;
                            (condtion as TermCondition).FilterCollection.Add(sc);
                        }
                        else if (condtion is MustNotCodition)
                        {
                            var sc = new TermCondition();
                            var tem = new SearchCondition(jsonname);
                            tem.Value = cst.Value;
                            sc.FilterCollection.Add(tem);

                            (condtion as MustNotCodition).FilterCollection.Add(sc);
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("表达式的右侧一定是常数");
                    }

                    //jsonwriter.WriteRaw(condtion.ToString()+",");

                    query.FilterCollection.Add(condtion);
                    return;
                }

                SearchCondition group = new SearchCondition("bool");
                query.FilterCollection.Add(group);
                SearchCondition searchType = null;
                //包裹操作符
                switch (binexp.NodeType)
                {
                    case ExpressionType.AndAlso:
                        {
                            searchType = new MustCondition();
                            break;
                        }
                    case ExpressionType.OrElse:
                        {
                            searchType = new ShouldCondition();
                            break;
                        }
                    case ExpressionType.And:
                        {
                            break;
                        }
                    default:
                        throw new NotSupportedException("不支持的操作：" + binexp.NodeType);
                }

                group.FilterCollection.Add(searchType);

                AnsyExpression(binexp.Left,ref searchType);
                AnsyExpression(binexp.Right, ref searchType);
            }
        }

        public SearchBuilder<T> where(Expression<Func<T,bool>> predicate)
        {

            JsonTextWriter writer=new JsonTextWriter(new StringWriter(sb));
            SearchCondition query = null;
            AnsyExpression(predicate.Body,ref query);

            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
