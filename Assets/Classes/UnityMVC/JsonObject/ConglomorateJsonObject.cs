using System;
using System.Collections.Generic;

namespace UnityMVC.JsonObject
{
    class ConglomorateJsonObject : IJsonObject
    {
        Dictionary<String, IJsonObject> children = new Dictionary<string, IJsonObject>();

        public void AddSource(String key, String source)
        {
            children.Add(key, new SimpleJsonObject(source));
        }

        public IJsonObject this[string key]
        {
            get
            {
                return children[key];
            }
        }

        public IJsonObject this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool AsBoolean
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int AsInt
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public float AsFloat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string AsString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public List<IJsonObject> AsList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<string, IJsonObject> AsDictionary
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
