namespace Provausio.Common
{
    public class RegexPattern
    {
        /// <summary>
        /// Alphanumeric with underscores
        /// </summary>

        public const string AlphaNumericWithUnderscore = @"^[a-zA-Z0-9\-_]{0,40}$";

        /// <summary>
        /// Locale (e.g. en-US)
        /// </summary>
        public const string Locale = @"[a-z]{2}-[A-Z]{2}";

        /// <summary>
        /// Digital Air Strike acceptable phone number format
        /// </summary>
        public const string DASPhoneUS = @" ^\+?\d{11}$";

        /// <summary>
        /// Email address (RFC 5322)
        /// </summary>
        public const string Email = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
    }
}
