using ES.Core.SearchCondition;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ES.Core
{
    public class QueryBuilder<T>
    {
        private Query _query = null;

        private void AnsyExpression(Expression expression, Query query)
        {
            if(expression is LambdaExpression)
            {
                expression = (expression as LambdaExpression).Body;
            }

            if (expression is BinaryExpression)
            {
                var binexp = expression as BinaryExpression;

                if (binexp.Left is MemberExpression)
                {
                    string jsonname = string.Empty;
                    switch (binexp.NodeType)
                    {
                        case ExpressionType.GreaterThan:
                            {

                            }
                            break;
                        case ExpressionType.GreaterThanOrEqual:
                            {

                            }
                            break;
                        case ExpressionType.LessThan:
                            {

                            }
                            break;
                        case ExpressionType.LessThanOrEqual:
                            {

                            }
                            break;
                        case ExpressionType.Equal:
                            {
     
                            }
                            break;
                        case ExpressionType.NotEqual:
                            {

                                break;
                            }
                        default:
                            throw new NotSupportedException("不支持的操作：" + binexp.NodeType);
                    }

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

                    if (binexp.Right is ConstantExpression)
                    {
                        var cst = binexp.Right as ConstantExpression;
                       
                    }
                    else
                    {
                        throw new NotSupportedException("表达式的右侧一定是数值");
                    }

                    return;
                }

              
                //包裹操作符
                switch (binexp.NodeType)
                {
                    case ExpressionType.AndAlso:
                        {
    
                            break;
                        }
                    case ExpressionType.OrElse:
                        {
      
                            break;
                        }
                    case ExpressionType.And:
                        {
                            break;
                        }
                    default:
                        throw new NotSupportedException("不支持的操作：" + binexp.NodeType);
                }

                AnsyExpression(binexp.Left, null);
                AnsyExpression(binexp.Right, null);
            }
        }


        public QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)
        {
            AnsyExpression(predicate.Body, _query);
            return this;
        }
    }
}
