using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityMVC
{
    /// <summary>
    /// Abstraction for a JSON object.
    /// </summary>
    interface IJsonObject
    {
        string AsString { get; }
        int AsInt { get; }
        float AsFloat { get; }
        bool AsBoolean { get; }
        List<IJsonObject> AsList { get; }
        Dictionary<string, IJsonObject> AsDictionary { get; }
        IJsonObject this[int index] { get; }
        IJsonObject this[string key] { get; }
    }
}
