using System;

namespace Fluentd.Net.Config
{
    public class ConfigParseException : Exception
    {
        public ConfigParseException(string message) : base(message)
        {}
    }
}
