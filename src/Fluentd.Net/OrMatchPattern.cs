using System.Linq;

namespace Fluentd.Net
{
    internal class OrMatchPattern : IMatchPattern
    {
        private readonly IMatchPattern[] patterns; 

        public OrMatchPattern(params IMatchPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public bool IsMatch(string tag)
        {
            return patterns.Any(x => x.IsMatch(tag));
        }


        // internal

        public static IMatchPattern Create(params IMatchPattern[] patterns)
        {
            return new OrMatchPattern(patterns);
        }
    }
}
