using ES.Core6x.SearchOperator;
using LJC.FrameWork.Comm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ES.Core6x
{
    public class SearchBuilder<T>
    {
        private SearchConditionBase _query = null;
        private SearchConditionBase _lastquery = null;

        private void AnsyExpression(Expression expression, SearchConditionBase query)
        {
            if(query==null)
            {
                query = new SearchConditionBase("query");
            }

            if(expression is MemberExpression)
            {
                var memberexp = expression as MemberExpression;
                var nodetype = memberexp.NodeType;
                return;
            }

            if(expression is BinaryExpression)
            {
                SearchConditionBase condtion = null;
                var binexp = expression as BinaryExpression;

                if(!(binexp.Left is BinaryExpression))
                {
                    
                    string jsonname = string.Empty;
                    switch(binexp.NodeType)
                    {
                        case ExpressionType.GreaterThan:
                            {
                                condtion = new GreaterThen();
                            }
                            break;
                        case ExpressionType.GreaterThanOrEqual:
                            {
                                condtion = new GreaterEquelThen();
                            }
                            break;
                        case ExpressionType.LessThan:
                            {
                                condtion = new SmallThen();
                            }
                            break;
                        case ExpressionType.LessThanOrEqual:
                            {
                                condtion = new SmallEquelThen();
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
                        var conditionvalue = new SearchConditionBase(jsonname, cst.Value);

                        if (condtion is MustNotCodition)
                        {
                            var sc = new TermCondition();
                            sc.FilterCollection.Add(conditionvalue);

                            condtion.FilterCollection.Add(sc);
                        }
                        else
                        {
                            condtion.FilterCollection.Add(conditionvalue);
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("表达式的右侧一定是常数");
                    }

                    query.FilterCollection.Add(condtion);
                    return;
                }

                SearchConditionBase group = new SearchConditionBase("bool");
                query.FilterCollection.Add(group);
                SearchConditionBase searchType = null;
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

                AnsyExpression(binexp.Left,searchType);
                AnsyExpression(binexp.Right, searchType);
            }
        }

        public SearchBuilder<T> Filter(Expression<Func<T, bool>> predicate)
        {
            if(_query==null)
            {
                _query = new SearchConditionBase();
            }

            FilteredCondition filteredCondition = new FilteredCondition();
            FilterCodition filter = new FilterCodition();
            filteredCondition.FilterCollection.Add(filter);
            _query.FilterCollection.Add(filteredCondition);

            AnsyExpression(predicate.Body, filter);

            return this;
        }

        public SearchBuilder<T> Exists(Expression<Func<T, object>> predicate)
        {
            
            return this;
        }

        public SearchBuilder<T> Missing(Expression<Func<T,object>> predicate)
        {
            return this;
        }

        public SearchBuilder<T> Match()
        {
            if (_query == null)
            {
                _query = new SearchConditionBase();
            }

            MatchCondition mc = new MatchCondition();
            _query.FilterCollection.Add(mc);

            return this;
        }

        public SearchBuilder<T> Match<V>(Expression<Func<T,V>> predicate,V matchvalue)
        {
            if(_lastquery==null)
            {
                _lastquery = new SearchConditionBase();
            }

            var jsoname = JsonHelper.GetJsonTag(predicate);;
            if (_query == null)
            {
                _query = new SearchConditionBase();
            }

            MatchCondition mc = new MatchCondition();
            mc.FilterCollection.Add(new SearchConditionBase(jsoname, matchvalue));
            _query.FilterCollection.Add(mc);

            return this;
        }

        public void Clear()
        {
            _query = null;
            _lastquery = null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            JsonTextWriter writer = new JsonTextWriter(new StringWriter(sb));
            _query.BuildQuery(writer);

            return sb.ToString();
        }
    }
}
