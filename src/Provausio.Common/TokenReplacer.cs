using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Provausio.Common
{
    public class TokenReplacer
    {
        private readonly string _openingToken;
        private readonly string _closingToken;

        public TokenReplacer()
        {
            _openingToken = "{{";
            _closingToken = "}}";
        }

        public TokenReplacer(string openingToken, string closingToken)
        {
            if (string.IsNullOrEmpty(openingToken))
                throw new ArgumentException("Opening token cannot be null or empty", nameof(openingToken));

            if (string.IsNullOrEmpty(closingToken))
                throw new ArgumentException("Closing token cannot be null or empty", nameof(closingToken));

            _openingToken = Regex.Escape(openingToken);
            _closingToken = Regex.Escape(closingToken);
        }

        /// <summary>
        /// Scans an input string and returns all detected keys.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<string> FindTokens(string input)
        {
            var tokenPattern = $"{_openingToken}(\\w+){_closingToken}";
            return Regex.Matches(input, tokenPattern)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value);
        }

        /// <summary>
        /// Replaces all tokens within the provided text with the values provided.
        /// </summary>
        /// <param name="input">The input that will be processed.</param>
        /// <param name="resourceFile">The resource file that contains the final values that will be subbed in.</param>
        /// <returns></returns>
        public string Render(string input, IDictionary<string, string> resourceFile)
        {
            var foundTokens = FindTokens(input);
            var newRendering = input;
            foreach (var key in foundTokens)
            {
                if (!resourceFile.ContainsKey(key))
                    continue;

                var actualValue = resourceFile[key];

                var token = $"{_openingToken}{key}{_closingToken}";
                newRendering = new Regex(token).Replace(newRendering, actualValue);
            }

            return newRendering;
        }
    }
}
