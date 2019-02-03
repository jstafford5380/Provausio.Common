using System.ComponentModel.DataAnnotations;

namespace Provausio.Common.Localization
{
    public class StringResourceInfo
    {
        [Required]
        [RegularExpression(RegexPattern.AlphaNumericWithUnderscore, ErrorMessage = "Keys can only be alphanumeric or underscore.")]
        public string Key { get; set; }

        [Required]
        [RegularExpression(RegexPattern.Locale, ErrorMessage = "Incorrect locale format (e.g. en-US)")]
        public string Culture { get; set; }

        [Required]
        [StringLength(5000)]
        public string Value { get; set; }

        [Required]
        [RegularExpression(RegexPattern.AlphaNumericWithUnderscore, ErrorMessage = "Context keys can only be alphanumeric or underscore.")]
        public string Context { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
