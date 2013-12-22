using System.Collections.Generic;

namespace Fluentd.Net.Config
{

    public interface IElement
    {
        string Name { get; }
        string Argument { get; }
        IReadOnlyDictionary<string, string> Attributes { get; }
        IReadOnlyList<IElement> Elements { get; }

        string this[string key] { get; }

        bool ContainsKey(string key);

        bool TryGetValue(string key, out string value);

        string ToString();
    }

}