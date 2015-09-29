using System;
using System.Collections.Generic;
using System.IO;

namespace WordsProcessor.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                RunPart1();
                Console.WriteLine("\nPress 'Enter' to run part 2");
                Console.ReadLine();

                RunPart2();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("You see this error because I didn't spend enough time testing the application: {0}", ex);
            }
        }

        private static void RunPart1()
        {
            string fileLocation = GetFileLocation("part1fileinput.txt");

            var fileLoader = new FileWordsLoader(fileLocation);
            var wordsCounter = new WordsCounter(fileLoader);

            try
            {
                PrintPart1(wordsCounter.Count());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File was not found at location: {0}", fileLocation);
            }
        }

        private static void RunPart2()
        {
            string fileLocation = GetFileLocation("part2fileinput.txt");

            var fileLoader = new FileWordsLoader(fileLocation);
            var wordsFilter = new WordsFilter(fileLoader);
            List<string> filteredWords = null;
            try
            {
                filteredWords = wordsFilter.Filter();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File was not found at location: {0}", fileLocation);
            }

            foreach (var word in filteredWords)
            {
                Console.WriteLine(word);
            }
        }

        private static string GetFileLocation(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }

        private static void PrintPart1(SortedDictionary<string, int> toPrint)
        {
            var i = 0;
            foreach (var word in toPrint)
            {
                Console.WriteLine("{0} {1} {2}", word.Key, word.Key.Length < 7 ? "\t\t": "\t", word.Value);
                i++;

                if (i%50 != 0) continue;

                Console.WriteLine("----------------- Press 'Enter' to continue -----------------");
                Console.ReadLine();
            }
        }
    }
}