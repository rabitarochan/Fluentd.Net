namespace Fluentd.Net.Config
{
    public static class ElementExtensions
    {
        public static int ParseSizeValue(this IElement element, string key)
        {
            return ConfigUtil.ParseSizeValue(element[key]);
        }

        public static int ParseTimeValue(this IElement element, string key)
        {
            return ConfigUtil.ParseTimeValue(element[key]);
        }

        public static bool ParseBoolValue(this IElement element, string key)
        {
            return ConfigUtil.ParseBoolValue(element[key]);
        }
    }
}
