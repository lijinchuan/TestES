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
        internal bool? _store;
        public Property SetStore(bool store)
        {
            _store = store;
            return this;
        }


        [JsonProperty("ignore_malformed")]
        internal bool? _ignore_malformed;

        public Property SetIgnore_malformed(bool ignore_malformed)
        {
            _ignore_malformed = ignore_malformed;
            return this;
        }

        /// <summary>
        /// 可选值为analyzed(默认)和no，如果是字段是字符串类型的，则可以是not_analyzed.
        /// Index表示该字段是否索引，如果index为no那个analyzer设为啥也没用。
        /// </summary>
        [JsonProperty("index")]
        internal string _index = "analyzed";

        public Property SetIndex(string index)
        {
            _index = index;
            return this;
        }

        /// <summary>
        /// 默认为1，定义了文档中该字段的重要性，越高越重要
        /// </summary>
        [JsonProperty("boost")]
        internal int? _boost;

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
        internal bool? _include_in_all;

        public Property SetInclude_in_all(bool include_in_all)
        {
            _include_in_all = include_in_all;
            return this;
        }

        /// <summary>
        /// 字段项分词的设置对应Lucene里面的Analyzer
        /// </summary>
        [JsonProperty("analyzer")]
        private string _analyzer;

        public Property SetAnalyzer(string analyzer)
        {
            this._analyzer = analyzer;
            return this;
        }

        /// <summary>
        /// 指的是索引过程中采用的分词器
        /// </summary>
        [JsonProperty("index_analyzer")]
        private string _index_analyzer;

        public Property SetIndex_analyzer(string value)
        {
            _index_analyzer = value;
            return this;
        }

        /// <summary>
        /// 指的是检索过程中采用的分词器
        /// </summary>
        [JsonProperty("search_analyzer")]
        private string _search_analyzer;

        public Property SetSearch_analyzer(string value)
        {
            _search_analyzer = value;
            return this;
        }

        public void BuildString(JsonWriter writer)
        {
            if (string.IsNullOrWhiteSpace("_propertyName"))
                throw new Exception("_propertyName不能为空");

            writer.WritePropertyName(this._propertyName);
            writer.WriteStartObject();

            if(_index!=null)
            {
                writer.WritePropertyName("index");
                writer.WriteValue(_index);
            }

            if(this._boost!=null)
            {
                writer.WritePropertyName("boost");
                writer.WriteValue(_boost);
            }

            if(this._store!=null)
            {
                writer.WritePropertyName("store");
                writer.WriteValue(_store);
            }

            if(this._null_value!=null)
            {
                writer.WritePropertyName("null_value");
                writer.WriteValue(_null_value);
            }

            if(this._ignore_malformed!=null)
            {
                writer.WritePropertyName("ignore_malformed");
                writer.WriteValue(_ignore_malformed);
            }

            if(this._include_in_all!=null)
            {
                writer.WritePropertyName("include_in_all");
                writer.WriteValue(_include_in_all);
            }

            writer.WritePropertyName("type");
            writer.WriteValue(_propertyType.ToString().ToLower());

            if(this._index_analyzer!=null)
            {
                writer.WritePropertyName("index_analyzer");
                writer.WriteValue(_index_analyzer);
            }

            if(this._search_analyzer!=null)
            {
                writer.WritePropertyName("search_analyzer");
                writer.WriteValue(_search_analyzer);
            }

            if(this._analyzer!=null)
            {
                writer.WritePropertyName("analyzer");
                writer.WriteValue(_analyzer);
            }

            writer.WriteEndObject();
        }
    }
}
