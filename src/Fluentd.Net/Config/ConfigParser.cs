using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Fluentd.Net.Config
{
    public class ConfigParser : IDisposable
    {
        private StreamReader reader;
        private int lineNumber;

        public ConfigParser(string configFilePath)
        {
            ConfigFilePath = configFilePath;
        }

        public string ConfigFilePath { get; private set; }

        public IElement Parse()
        {
            InitializeReader();
            lineNumber = 0;

            var element = Parse("ROOT", "");
            return element;
        }

        public void Dispose()
        {
            if (reader != null) reader.Dispose();
        }

        
        // private

        private void InitializeReader()
        {
            if (reader != null) reader.Dispose();
            reader = new StreamReader(ConfigFilePath);
        }

        private IElement Parse(string name, string argument)
        {
            var newAttribute = new Dictionary<string, string>();
            var newElements = new List<IElement>();

            while (!reader.EndOfStream) {
                lineNumber++;
                var line = TrimComment(reader.ReadLine());
                if (string.IsNullOrEmpty(line)) continue;

                if (IsElementOpenTag(line)) {
                    var elementInfo = GetElementInfo(line);
                    var element = Parse(elementInfo.Item1, elementInfo.Item2);
                    newElements.Add(element);
                } else if (line == string.Format(@"</{0}>", name)) {
                    break;
                } else if (IsAttributeLine(line)) {
                    var attributeInfo = GetAttributeInfo(line);
                    newAttribute.Add(attributeInfo.Item1, attributeInfo.Item2);
                } else {
                    var msg = string.Format("parse error at {0} line {1}", ConfigFilePath, lineNumber);
                    throw new ConfigParseException(msg);
                }
            }

            return new Element(name, argument, newAttribute, newElements);
        }


        // comment trim regex

        private static readonly Regex commentTrimmer = new Regex(@"\s*#.*$");
        
        private string TrimComment(string s)
        {
            return commentTrimmer.Replace(s, "");
        }

        
        // elements regex

        private static readonly Regex elementOpenTag = new Regex(@"^\<([a-zA-Z0-9_]+)\s*(.+?)?\>$");

        private bool IsElementOpenTag(string s)
        {
            return elementOpenTag.IsMatch(s);
        }

        private Tuple<string, string> GetElementInfo(string s)
        {
            var m = elementOpenTag.Match(s);
            var name = m.Groups[1].Value;
            var argument = "";

            if (m.Groups.Count == 3) {
                argument = m.Groups[2].Value;
            }

            return Tuple.Create(name, argument);
        }


        // attributes regex

        private static readonly Regex attributeLine = new Regex(@"^([a-zA-Z0-9_]+)\s*(.*)$");

        private bool IsAttributeLine(string s)
        {
            return attributeLine.IsMatch(s);
        }

        private Tuple<string, string> GetAttributeInfo(string s)
        {
            var m = attributeLine.Match(s);
            return Tuple.Create(m.Groups[1].Value, m.Groups[2].Value);
        }
    }
}
