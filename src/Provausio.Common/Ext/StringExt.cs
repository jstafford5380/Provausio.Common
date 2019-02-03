using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Provausio.Common.Ext
{
    public static class StringExt
    {
        /// <summary>
        /// Determines whether this instance is numeric.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool IsNumeric(this string target)
        {
            return target.Cast<char>().All(char.IsDigit);
        }

        /// <summary>
        /// Attempts to parse the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        public static T FindEnum<T>(this string target, bool ignoreCase = true)
            where T : struct
        {
            return (T) Enum.Parse(typeof(T), target, ignoreCase);
        }

        /// <summary>
        /// Attempts to parse the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="parameterName">The name of the parameter that was used with this extension.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        public static T FindEnum<T>(this string target, string parameterName, bool ignoreCase = true)
            where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), target, ignoreCase);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, parameterName, ex);
            }
        }

        /// <summary>
        /// Attempts to parse the enum. Returns true or false, indicating success or failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The string that will be parsed in to the specified type.</param>
        /// <param name="result">If successful, will contain the parsed result. If not successful, this will be equal to the default of T</param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool TryFindEnum<T>(this string target, out T result, bool ignoreCase = true)
            where T : struct
        {
            try
            {
                result = FindEnum<T>(target, ignoreCase);
                return true;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this object obj, IObjectStringFormatter formatter)
        {
            return formatter.ToString(obj);
        }

        /// <summary>
        /// Returns whether or not the target value is null, empty, or pure whitespace
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhitespace(this string target)
        {
            return string.IsNullOrEmpty(target) || string.IsNullOrWhiteSpace(target);
        }

        /// <summary>
        /// Reformats the input string to capitalize the first letter of every word.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string input)
        {
            var fixedWords = new List<string>();
            foreach (var word in input.Split(' '))
            {
                var modified = word.ToLower().ToCharArray();
                modified[0] = char.ToUpper(modified[0]);
                fixedWords.Add(new string(modified));
            }

            return string.Join(" ", fixedWords);
        }

        /// <summary>
        ///     Ensures that the input length is no longer than the specified maxLength. If it is longer, then 
        ///     it will be truncated to the maximum length. If ellipsis is enabled, then it will be added to the 
        ///     max length string. For example, if max length is 3 and the input is truncated, then the total result 
        ///     length will be 3 + 3 (ellipsis) or 6 in total.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="maxLength"></param>
        /// <param name="addEllipsis"></param>
        /// <returns></returns>
        public static string Truncate(this string input, int maxLength, bool addEllipsis = true)
        {
            if (input.Length <= maxLength)
                return input;

            var truncated = input.Substring(0, maxLength);
            if (addEllipsis)
                truncated += "...";

            return truncated;
        }

        /// <summary>
        /// Replaces tokens in the text with values provided by a resource file.
        /// </summary>
        /// <param name="input">The text that will be processed.</param>
        /// <param name="resourceFile">The resource file that contains the final values for each key.</param>
        /// <returns></returns>
        public static string RenderTokens(this string input, IDictionary<string, string> resourceFile)
        {
            var replacer = new TokenReplacer();
            return replacer.Render(input, resourceFile);
        }
    }

    public interface IObjectStringFormatter
    {
        string ToString(object input);
    }

    public class DefaultObjectStringFormatter : IObjectStringFormatter
    {
        public string ToString(object input)
        {
            return input.ToString();
        }
    }

    public static class MsWordCharacters
    {
        public const string SmartApostrophe = "[\u2018\u2019\u201A]";
        public const string SmartQuotes = "[\u201C\u201D\u201E]";
        public const string Ellipsis = "\u2026";
        public const string EmDash = "[\u2013\u2014]";
        public const string Circumflex = "\u02C6";
        public const string OpenAngleBracket = "\u2039";
        public const string CloseAngleBracket = "\u203A";
        public const string Space = "[\u02DC\u00A0]";
        
        /// <summary>
        /// Replaces MS Word "smart" characters with ASCII friendly characters. Returns new value.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceMsWordCharacters(this string text)
        {
            var output = text;
            // smart single quotes and apostrophe
            output = Regex.Replace(output, SmartApostrophe, "'");
            // smart double quotes
            output = Regex.Replace(output, SmartQuotes, "\"");
            // ellipsis
            output = Regex.Replace(output, Ellipsis, "...");
            // dashes
            output = Regex.Replace(output, EmDash, "-");
            // circumflex
            output = Regex.Replace(output, Circumflex, "^");
            // open angle bracket
            output = Regex.Replace(output, OpenAngleBracket, "<");
            // close angle bracket
            output = Regex.Replace(output, CloseAngleBracket, ">");
            // spaces
            output = Regex.Replace(output, Space, " ");

            return output;
        }
    }
}
