using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsProcessor
{

    public class WordsCounter
    {
        private readonly IWordsLoader _wordsLoader;

        public WordsCounter(IWordsLoader wordsLoader)
        {
            _wordsLoader = wordsLoader;
        }

        /// <summary>
        /// Counts the occurrences of all words and sort them in alphabetical order.
        /// </summary>
        /// <returns>Returns occurrences of words sorted in alphabetical order.</returns>
        public SortedDictionary<string, int> Count()
        {
            var words = _wordsLoader.LoadWords();
            if (String.IsNullOrWhiteSpace(words))
            {
                return new SortedDictionary<string, int>();
            }

            var wordsArray = words.ReplacePunctuationWithBlankSpace().Split(' ');

            var sortedWords = new SortedDictionary<string, int>();

            // I could have used LINQ but that will make it difficult to read
            foreach (var word in wordsArray.Select(w => w.Trim()))
            {
                if (String.IsNullOrWhiteSpace(word)) continue;

                if (sortedWords.ContainsKey(word))
                {
                    sortedWords[word]++;
                }
                else
                {
                    sortedWords.Add(word, 1);
                }
            }

            return sortedWords;
        }
    }
}
