using System;
using System.Collections.Generic;
using System.IO;

namespace TurtleTime.Utils
{
    /// <summary>
    /// A class which represents a directory of strings loaded from a file.
    /// This is handy for multi-language purposes.
    /// </summary>
    class StringCollection
    {
        Dictionary<String, String> values = new Dictionary<string, string>();

        public StringCollection(String filename)
        {
            using (TextReader tr = new StreamReader(filename))
            {
                String s = tr.ReadLine();
                while (s != null && s.Length > 0)
                {
                    String[] splits = s.Split(':');
                    values.Add(splits[0].Trim(), splits[1].Trim());
                    s = tr.ReadLine();
                }
            }
        }

        public String this[String key, KeySuffix keySuffix]
        {
            get
            {
                String result;
                if (values.TryGetValue(AppendKeySuffix(key, keySuffix), out result))
                {
                    return result;
                }
                else
                {
                    return "[No entry found for \"" + AppendKeySuffix(key, keySuffix) + "\"]";
                }
            }
        }

        public enum KeySuffix
        {
            MENU, INFO
        }

        private static String AppendKeySuffix(String input, KeySuffix keySuffix)
        {
            return input + "_" + keySuffix.ToString().ToLower();
        }
    }
}
