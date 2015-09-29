namespace WordsProcessor
{
    /// <summary>
    /// Loader interface to allow to read text data.
    /// </summary>
    public interface IWordsLoader
    {
        /// <summary>
        /// Loads a text data.
        /// </summary>
        /// <returns>Returns text information.</returns>
        string LoadWords();
    }
}