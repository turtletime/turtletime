using System.Collections.Generic;
using SimpleJSON;

namespace UnityMVC.JsonObject
{
    class SimpleJsonObject : IJsonObject
    {
        JSONNode underlyingNode;

        public SimpleJsonObject(string source)
        {
            underlyingNode = JSONNode.Parse(source);
        }

        private SimpleJsonObject(JSONNode underlyingNode)
        {
            this.underlyingNode = underlyingNode;
        }

        public IJsonObject this[string key]
        {
            get
            {
                JSONNode child = underlyingNode[key];
                if (child == null)
                {
                    return null;
                }
                return new SimpleJsonObject(child);
            }
        }

        public IJsonObject this[int index]
        {
            get
            {
                JSONNode child = underlyingNode[index];
                if (child == null)
                {
                    return null;
                }
                return new SimpleJsonObject(child);
            }
        }

        public bool AsBoolean
        {
            get
            {
                return underlyingNode.AsBool;
            }
        }

        public float AsFloat
        {
            get
            {
                return underlyingNode.AsFloat;
            }
        }

        public int AsInt
        {
            get
            {
                return underlyingNode.AsInt;
            }
        }

        public string AsString
        {
            get
            {
                return underlyingNode.Value;
            }
        }

        public IEnumerable<IJsonObject> Children
        {
            get
            {
                foreach (JSONNode child in underlyingNode.Childs)
                {
                    yield return new SimpleJsonObject(child);
                }
            }
        }
    }
}
