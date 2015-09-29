using System;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace WordsProcessor.Tests
{
    public class WordsFilterTest
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
            var wordsFilter = new WordsFilter(CreateWordsLoaderStub(val => ""));
            var words = wordsFilter.Filter();

            Assert.Equal(0, words.Count);
        }

        [Fact]
        public void NoWordFound_ShouldReturnZeroWords()
        {
            var wordsFilter = new WordsFilter(CreateWordsLoaderStub(val => "al, bums"));
            var words = wordsFilter.Filter();

            Assert.Equal(0, words.Count);
        }

        [Fact]
        public void WordFoundWithStartPosition_ShouldReturnOneWords()
        {
            var wordsFilter = new WordsFilter(CreateWordsLoaderStub(val => "al, bums, albums"));
            var words = wordsFilter.Filter();

            Assert.Equal(1, words.Count);
            Assert.Equal("albums", words[0]);
        }

        [Fact]
        public void WordFoundWithEndPosition_ShouldReturnOneWords()
        {
            var wordsFilter = new WordsFilter(CreateWordsLoaderStub(val => "bums, albums, al"));
            var words = wordsFilter.Filter();

            Assert.Equal(1, words.Count);
            Assert.Equal("albums", words[0]);
        }

        [Fact]
        public void MultipleWords_ShouldReturnAnArray()
        {
            var wordsFilter =
                new WordsFilter(
                    CreateWordsLoaderStub(
                        var =>
                            "al, albums, aver, bar, barely, be, befoul, bums, by, cat, con, convex, ely, foul, here, hereby, jig, jigsaw, or, saw, tail, tailor, vex, we, weaver"));
            var words = wordsFilter.Filter();

            Assert.Equal(8, words.Count);
            Assert.Contains("albums", words);
            Assert.Contains("barely", words);
            Assert.Contains("befoul", words);
            Assert.Contains("convex", words);
            Assert.Contains("hereby", words);
            Assert.Contains("jigsaw", words);
            Assert.Contains("tailor", words);
            Assert.Contains("weaver", words);
        }
    }
}