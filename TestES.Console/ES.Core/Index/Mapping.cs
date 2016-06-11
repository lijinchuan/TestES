﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Core.Index
{
    public class Mappings
    {
        private string _typename = string.Empty;
        public Mappings(string typename)
        {
            _typename = typename;
        }

        private DefaultMapping _default;
        public Mappings Deafult(Action<DefaultMapping> defaultmapping)
        {
            if(_default==null)
            {
                _default = new DefaultMapping();
            }

            defaultmapping(_default);

            return this;
        }

        private MappingResource _resource;
        public Mappings Resource(Action<MappingResource> resource)
        {
            if (_resource == null)
                _resource = new MappingResource();

            resource(_resource);

            return this;
        }

        public void BuildString(JsonWriter writer)
        {

        }

    }
}
