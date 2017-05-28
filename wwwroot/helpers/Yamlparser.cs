using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace YamlParser
{
    public class Parser
    {
        public List<Dictionary<string, string>> Parse(string path)
        {
            var data = new List<Dictionary<string, string>>();

            try
            {
                using (var sr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    var cur = -1;
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var ch = (char) sr.Read();
                        if (ch == '-' && (char) sr.Peek() != '-')
                        {
                            cur++;
                            data.Add(new Dictionary<string, string>());
                        }
                        var line = sr.ReadLine();
                        var keyval = ToKeyValue(line);
                        if (!data[cur].ContainsKey(keyval.Key))
                            data[cur].Add(keyval.Key, keyval.Value);
                    }
                    sr.DiscardBufferedData();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return data;
        }

        private static KeyValuePair<string, string> ToKeyValue(string s)
        {
            var strKeyVal = s.Split(new char[] {':'}, 2);
            return new KeyValuePair<string, string>(Regex.Replace(strKeyVal[0], @"\s+", ""), strKeyVal[1]);
        }
    }
}