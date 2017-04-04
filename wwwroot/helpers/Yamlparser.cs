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
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    // Read the stream to a string, and write the string to the console.
                    char ch;
                    string line;
                    int cur = -1;
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        ch = (char)sr.Read();
                        if (ch == '-' && (char)sr.Peek() != '-')
                        {
                            cur++;
                            data.Add(new Dictionary<string, string>());
                        }
                        line = sr.ReadLine();
                        var keyval = toKeyValue(line);
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

        private KeyValuePair<string, string> toKeyValue(string s)
        {
            // var strKeyVal = Regex.Replace(s, @"\s+", "").Split(new char[] { ':' }, 2);
            var strKeyVal = s.Split(new char[] { ':' }, 2);
            return new KeyValuePair<string, string>(Regex.Replace(strKeyVal[0], @"\s+", ""), strKeyVal[1]);
        }


    }

    public class YamlNode
    {

    }
}