using ES.Core.SearchCondition.Conditon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.SearchCondition
{
    public class MustNot
    {
        private Term _term;
        public void Term(Action<Term> term)
        {
            if (_term == null)
                _term = new Term();

            term(_term);
        }

        private Match _match;
        public void Match(Action<Match> match)
        {
            if (_match == null)
                _match = new Match();

            match(_match);
        }

        private Text _text;
        public MustNot Text(string name, string text)
        {
            if (_text != null)
                _text = new Text();
            _text.Add(name, text);
            return this;
        }

        private Prefix _prefix;
        public MustNot Prefix(string name, string word)
        {
            if (_prefix == null)
                _prefix = new Prefix();

            _prefix.Add(name, word);

            return this;
        }

        private Wildcard _wildcard;
        public MustNot WildCard(string name, string word)
        {
            if (_wildcard == null)
                _wildcard = new Wildcard();

            _wildcard.Add(name, word);
            return this;
        }


        internal void BuildString(JsonTextWriter writer)
        {
            writer.WritePropertyName("must_not");
            writer.WriteStartArray();

            if(_term!=null)
            {
                _term.BuildString(writer);
            }

            if(_match!=null)
            {
                _match.BuildString(writer);
            }

            if (_text != null)
            {
                _text.BuildString(writer);
            }

            if (_prefix != null)
            {
                _prefix.BuildString(writer);
            }

            if (_wildcard != null)
            {
                _wildcard.BuildString(writer);
            }

            writer.WriteEndArray();
        }
    }
}
