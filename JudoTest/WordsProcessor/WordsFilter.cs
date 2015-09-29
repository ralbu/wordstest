using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsProcessor
{
    public class WordsFilter
    {
        private readonly IWordsLoader _wordsLoader;

        public WordsFilter(IWordsLoader wordsLoader)
        {
            _wordsLoader = wordsLoader;
        }

        /// <summary>
        /// Filters the words which are composed of two concatenated words from the same source.
        /// </summary>
        /// <returns>Returns a list of filtered words.</returns>
        public List<string> Filter()
        {
            var words = _wordsLoader.LoadWords();

            if (String.IsNullOrWhiteSpace(words))
            {
                return new List<string>();
            }

            var resultWords = new List<string>();

            var wordsArray = words.ReplacePunctuationWithBlankSpace().Split(' ').Select(w => w.Trim()).ToArray();

            foreach (var firstPart in wordsArray)
            {
                if (String.IsNullOrWhiteSpace(firstPart)) continue;

                if (IgnoreWord(firstPart)) continue;

                foreach (var secondPart in wordsArray)
                {
                    if (String.IsNullOrWhiteSpace(secondPart)) continue;

                    if (IgnoreWord(secondPart)) continue;

                    var fullWord = String.Concat(firstPart, secondPart);
                    if (wordsArray.Contains(fullWord))
                    {
                        resultWords.Add(fullWord);
                        break;
                    }
                }
            }

            return resultWords;
        }

        private bool IgnoreWord(string word)
        {
            const int WORD_LENGTH_PER_SPEC = 6;

            return word.Length == WORD_LENGTH_PER_SPEC;
        }
    }
}