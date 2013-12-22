using System.Linq;
using System.Text.RegularExpressions;

namespace Fluentd.Net
{
    internal class Match
    {
        private readonly IMatchPattern pattern;
        private readonly IOutputPlugin output;

        public Match(string patternString, IOutputPlugin output)
        {
            pattern = CreateMatchPattern(patternString);
            this.output = output;
        }


        // private

        private IMatchPattern CreateMatchPattern(string patternString)
        {
            var patterns = Regex.Split(patternString, @"\s+")
                                .Select(x => GlobMatchPattern.Create(x))
                                .ToArray();

            return (patterns.Length == 1) ? patterns[0] : OrMatchPattern.Create(patterns);
        }
    }
}
