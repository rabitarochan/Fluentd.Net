using System;
using System.Text.RegularExpressions;

namespace Fluentd.Net.Config
{
    public static class ConfigUtil
    {
        private static readonly Regex kbRegex = new Regex(@"([0-9]+)k", RegexOptions.IgnoreCase);
        private static readonly Regex mbRegex = new Regex(@"([0-9]+)m", RegexOptions.IgnoreCase);
        private static readonly Regex gbRegex = new Regex(@"([0-9]+)g", RegexOptions.IgnoreCase);
        private static readonly Regex tbRegex = new Regex(@"([0-9]+)t", RegexOptions.IgnoreCase);

        public static int ParseSizeValue(string value)
        {
            try {
                if (kbRegex.IsMatch(value)) {
                    return Convert.ToInt32(kbRegex.Match(value).Groups[1].Value) * 1024;
                }
                if (mbRegex.IsMatch(value)) {
                    return Convert.ToInt32(mbRegex.Match(value).Groups[1].Value) * 1024 * 1024;
                }
                if (gbRegex.IsMatch(value)) {
                    return Convert.ToInt32(gbRegex.Match(value).Groups[1].Value) * 1024 * 1024 * 1024;
                }
                if (tbRegex.IsMatch(value)) {
                    return Convert.ToInt32(tbRegex.Match(value).Groups[1].Value) * 1024 * 1024 * 1024 * 1024;
                }

                return Convert.ToInt32(value);
            } catch (FormatException) {
                throw new ConfigParseException(string.Format("cannot parse to size value ({0})", value));
            }
        }

        private static readonly Regex sRegex = new Regex(@"([0-9]+)s");
        private static readonly Regex mRegex = new Regex(@"([0-9]+)m");
        private static readonly Regex hRegex = new Regex(@"([0-9]+)h");
        private static readonly Regex dRegex = new Regex(@"([0-9]+)d");

        public static int ParseTimeValue(string value)
        {
            try {
                if (sRegex.IsMatch(value)) {
                    return Convert.ToInt32(sRegex.Match(value).Groups[1].Value);
                }
                if (mRegex.IsMatch(value)) {
                    return Convert.ToInt32(mRegex.Match(value).Groups[1].Value) * 60;
                }
                if (hRegex.IsMatch(value)) {
                    return Convert.ToInt32(hRegex.Match(value).Groups[1].Value) * 60 * 60;
                }
                if (dRegex.IsMatch(value)) {
                    return Convert.ToInt32(dRegex.Match(value).Groups[1].Value) * 24 * 60 * 60;
                }

                return Convert.ToInt32(value);
            } catch (FormatException) {
                throw new ConfigParseException(string.Format("cannot parse to time value ({0})", value));
            }
        }

        public static bool ParseBoolValue(string value)
        {
            var _ = value.ToLower();
            if (_ == "true" || _ == "yes") return true;
            if (_ == "false" || _ == "no") return false;

            throw new ConfigParseException(string.Format("cannot parse to bool value ({0})", value));
        }
    }
}
