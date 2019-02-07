using System.Collections.Generic;
using System.Linq;
using Provausio.Common.Ext;

namespace Provausio.Common
{
    public class WordCloud : Dictionary<string, int>
    {
        /// <inheritdoc />
        /// <summary>
        /// Creates a word cloud consisting of words and their counts.
        /// </summary>
        /// <param name="content">The string content that will be evaluated.</param>
        public WordCloud(params string[] content)
            : this(new List<string>(), 3, content) { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a word cloud consisting of words and their counts.
        /// </summary>
        /// <param name="content">The string content that will be evaluated.</param>
        /// <param name="stopWords">A list of words that will be excluded from the cloud.</param>
        public WordCloud(IReadOnlyCollection<string> stopWords, params string[] content)
            : this(stopWords, 3, content) { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a word cloud consisting of words and their counts.
        /// </summary>
        /// <param name="content">The string content that will be evaluated.</param>
        /// <param name="stopWords">A list of words that will be excluded from the cloud.</param>
        /// <param name="wordLengthThreshold">The length of any particular word must be greater than this value to be included in the word cloud.</param>
        public WordCloud(IReadOnlyCollection<string> stopWords, int wordLengthThreshold, params string[] content)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var line in content)
            {
                var words = line
                    .ToLower()
                    .Split(' ')
                    .Where(word =>
                        !string.IsNullOrEmpty(word)
                        && word.Length > wordLengthThreshold
                        && !word.IsNumeric()
                        && !stopWords.Contains(word))
                    .ToList();

                foreach (var word in words)
                {
                    if (wordCounts.ContainsKey(word))
                    {
                        wordCounts[word]++;
                    }
                    else
                    {
                        wordCounts.Add(word, 1);
                    }
                }
            }

            foreach (var entry in wordCounts.OrderByDescending(kv => kv.Value).Take(50))
                Add(entry.Key, entry.Value);
        }
    }
}
