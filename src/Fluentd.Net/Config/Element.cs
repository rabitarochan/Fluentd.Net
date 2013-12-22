using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Fluentd.Net.Config
{
    internal class Element : IElement
    {
        public string Name { get; private set; }
        public string Argument { get; private set; }
        public IReadOnlyDictionary<string, string> Attributes { get; private set; }
        public IReadOnlyList<IElement> Elements { get; private set; }

        public Element(string name, string argument, IDictionary<string, string> attributes, IList<IElement> elements)
        {
            Name = name;
            Argument = argument;
            Attributes = new ReadOnlyDictionary<string, string>(attributes);
            Elements = new ReadOnlyCollection<IElement>(elements);
        }

        public string this[string key]
        {
            get { return Attributes[key]; }
        }

        public bool ContainsKey(string key)
        {
            return Attributes.ContainsKey(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return Attributes.TryGetValue(key, out value);
        }

        public override string ToString()
        {
            return ElementUtil.ToString(this);
        }
    }

    internal static class ElementUtil
    {
        public static string ToString(this IElement element, int nest = 0)
        {
            var indent = string.Concat(Enumerable.Repeat("  ", nest));
            var nextIndent = indent + "  ";
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(element.Argument)) {
                sb.AppendFormat("{0}<{1}>{2}", indent, element.Name, Environment.NewLine);
            } else {
                sb.AppendFormat("{0}<{1} {2}>{3}", indent, element.Name, element.Argument, Environment.NewLine);
            }

            foreach (var attribute in element.Attributes) {
                sb.AppendFormat("{0}{1} {2}{3}", nextIndent, attribute.Key, attribute.Value, Environment.NewLine);
            }

            foreach (var elem in element.Elements) {
                sb.Append(elem.ToString(nest + 1));
            }

            sb.AppendFormat("{0}</{1}>{2}", nest, element.Name, Environment.NewLine);

            return sb.ToString();
        }
    }
}
