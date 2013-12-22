using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fluentd.Net
{
    internal class GlobMatchPattern : IMatchPattern
    {
        private readonly Regex glob;

        public GlobMatchPattern(string pattern)
        {
            glob = CreateGlobRegex(pattern);
        }

        public bool IsMatch(string tag)
        {
            return glob.IsMatch(tag);
        }


        // internal

        public static IMatchPattern Create(string pattern)
        {
            return new GlobMatchPattern(pattern);
        }


        // private

        private Regex CreateGlobRegex(string pattern)
        {
            // TODO refactoring

            var stackStack = new Stack<Stack<StringBuilder>>();
            var regexStack = new Stack<StringBuilder>();
            regexStack.Push(new StringBuilder());
            var escape = false;
            var dot = false;

            var i = 0;
            while (i < pattern.Length) {
                var c = pattern.Substring(i, 1);

                if (escape) {
                    regexStack.Peek().Append(Regex.Escape(c));
                    escape = false;
                    i += 1;
                    continue;
                }

                if (Substring(pattern, i, 2) == "**") {
                    if (dot) {
                        regexStack.Peek().Append(@"(?![^\.])");
                        dot = false;
                    }
                    if (Substring(pattern, i + 2, 1) == ".") {
                        regexStack.Peek().Append(@"(?:.*\.|\A)");
                        i += 3;
                    } else {
                        regexStack.Peek().Append(@".*");
                        i += 2;
                    }
                    continue;
                }

                if (dot) {
                    regexStack.Peek().Append(@"\.");
                    dot = false;
                }

                if (c == @"\") {
                    escape = true;
                } else if (c == ".") {
                    dot = true;
                } else if (c == "*") {
                    regexStack.Peek().Append(@"[^\.]*");
                } else if (c == "{") {
                    stackStack.Push(new Stack<StringBuilder>());
                    regexStack.Push(new StringBuilder());
                } else if (c == "}" && stackStack.Count != 0) {
                    stackStack.Peek().Push(regexStack.Pop());
                    regexStack.Peek().Append("(" + string.Join("|", stackStack.Pop().Select(x => "(" + x.ToString() + ")")) + ")");
                } else if (c == "," && stackStack.Count != 0) {
                    stackStack.Peek().Push(regexStack.Pop());
                    regexStack.Push(new StringBuilder());
                } else if (Regex.IsMatch(c, @"[a-zA-Z0-9_]")) {
                    regexStack.Peek().Append(c);
                } else {
                    regexStack.Peek().Append(string.Format(@"\{0}", c));
                }

                i += 1;
            }

            if (stackStack.Count != 0) {
                stackStack.Peek().Push(regexStack.Pop());
                regexStack.Peek().Append("(" + string.Join("|", stackStack.Pop().Select(x => x.ToString())) + ")");
            }

            var regex = new Regex(@"\A" + regexStack.Pop() + @"\Z");
            return regex;
        }

        private string Substring(string s, int startIndex, int length)
        {
            if (startIndex > s.Length) return null;
            if ((startIndex + length) > s.Length) return s.Substring(startIndex);

            return s.Substring(startIndex, length);
        }
    }
}
