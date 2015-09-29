using System.IO;

namespace WordsProcessor
{
    /// <summary>
    /// Reads a text file and load the data.
    /// </summary>
    public class FileWordsLoader : IWordsLoader
    {
        private readonly string _location;

        public FileWordsLoader(string location)
        {
            _location = location;
        }

        public string LoadWords()
        {
            var word = File.ReadAllText(_location);

            return word;
        }
    }
}