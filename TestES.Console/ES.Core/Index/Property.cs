using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class Property
    {
        internal string _propertyName;

        public Property SetPropertyName(string ppname)
        {
            _propertyName = ppname;
            return this;
        }

        [JsonProperty("type")]
        internal PropertyType _propertyType = PropertyType.STRING;

        public Property SetType(PropertyType type)
        {
            _propertyType = type;
            return this;
        }

        /// <summary>
        /// 可选值为yes或no，指定该字段的原始值是否被写入索引中，默认为no，即结果中不能返回该字段。
        /// </summary>
        [JsonProperty("store")]
        internal bool _store;
        public Property SetStore(bool store)
        {
            _store = store;
            return this;
        }


        [JsonProperty("ignore_malformed")]
        internal bool _ignore_malformed;

        public Property SetIgnore_malformed(bool ignore_malformed)
        {
            _ignore_malformed = ignore_malformed;
            return this;
        }

        /// <summary>
        /// 可选值为analyzed(默认)和no，如果是字段是字符串类型的，则可以是not_analyzed.
        /// </summary>
        [JsonProperty("index")]
        internal string _index = "no";

        public Property SetIndex(string index)
        {
            _index = index;
            return this;
        }

        /// <summary>
        /// 默认为1，定义了文档中该字段的重要性，越高越重要
        /// </summary>
        [JsonProperty("boost")]
        internal int _boost = 1;

        public Property SetBoost(int boost)
        {
            _boost = boost;
            return this;
        }

        /// <summary>
        /// 如果一个字段为null值(空数组或者数组都是null值)的话不会被索引及搜索到，null_value参数可以显示替代null values为指定值，这样使得字段可以被搜索到。
        /// </summary>
        [JsonProperty("null_value")]
        internal object _null_value;

        public Property SetNull_value(object nullvalue)
        {

            _null_value = nullvalue;
            return this;
        }

        /// <summary>
        /// 指定该字段是否应该包括在_all字段里头，默认情况下都会包含。
        /// </summary>
        [JsonProperty("include_in_all")]
        internal bool _include_in_all;

        public Property SetInclude_in_all(bool include_in_all)
        {
            _include_in_all = include_in_all;
            return this;
        }

        public void BuildString(JsonWriter writer)
        {

        }
    }
}
