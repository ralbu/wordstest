using System;
using System.Linq;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace WordsProcessor.Tests
{
    public class WordCounterTest
    {
        private static IWordsLoader CreateWordsLoaderStub(Func<CallInfo, string> returnValue)
        {
            var wordsLoaderStub = Substitute.For<IWordsLoader>();
            wordsLoaderStub.LoadWords().Returns(returnValue);

            return wordsLoaderStub;
        }

        [Fact]
        public void EmptyArray_ShouldReturnZeroWords()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => ""));

            var words = wordsCounter.Count();

            Assert.Equal(0, words.Count);
        }

        [Fact]
        public void BlankSpaces_ShouldBe_Ignored()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "a    b   a    "));

            var words = wordsCounter.Count();

            Assert.Equal(2, words.Count);
        }

        [Fact]
        public void TwoSameWords_ShouldReturnCount1()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "one one"));

            var words = wordsCounter.Count();
            Assert.Equal(1, words.Count);
        }

        [Fact]
        public void TwoSameWords_ShouldReturnTwoOccurences()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "word word"));

            var words = wordsCounter.Count();
            Assert.Equal(2, words["word"]);
        }

        [Fact]
        public void DifferentWords_ShouldReturnOccurences()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "word test word word test"));

            var words = wordsCounter.Count();
            Assert.Equal(3, words["word"]);
            Assert.Equal(2, words["test"]);
        }

        [Fact]
        public void Words_ShouldSortOccurences()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "word test word word test apple"));

            var words = wordsCounter.Count();

            Assert.Equal("apple", words.Keys.ElementAt(0));
            Assert.Equal("test", words.Keys.ElementAt(1));
            Assert.Equal("word", words.Keys.ElementAt(2));
        }

        [Fact]
        public void WordsWithPunctuation_ShouldNotTakePunctuationsIntoAccount()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "a word, word a, (word."));

            var words = wordsCounter.Count();

            Assert.Equal(3, words["word"]);
        }

        [Fact]
        public void WordsWithPunctuationAndNoSpaces_ShouldNotTakePunctuationsIntoAccount()
        {
            var wordsCounter = new WordsCounter(CreateWordsLoaderStub(val => "a word,word a,(word.word"));

            var words = wordsCounter.Count();

            Assert.Equal(4, words["word"]);
        }

    }
}