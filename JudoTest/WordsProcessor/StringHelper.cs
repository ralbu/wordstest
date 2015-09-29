using System;
using System.Linq;
using System.Text;

namespace WordsProcessor
{
    public static class StringHelper
    {
        public static string RemovePunctuation(this string removeFrom)
        {
            return new String(removeFrom.ToCharArray().Where(c => !Char.IsPunctuation(c)).ToArray());
        }

        public static string ReplacePunctuationWithBlankSpace(this string replace)
        {
            var sb = new StringBuilder();
            foreach (var c in replace)
            {
                if (Char.IsPunctuation(c))
                {
                    sb.Append(" ");
                    continue;
                }
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}