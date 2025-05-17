using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Server.Data.Validation
{
    public class TextOrNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Let [Required] handle null cases
                return ValidationResult.Success;
            }

            string name = value.ToString();

            // Check for empty or whitespace-only string
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult("Name cannot be empty or whitespace.");
            }

            name = name.Trim();
            // Updated Regex: Allows Arabic, English, numbers, and spaces
            string pattern = @"^[\p{L}0-9\s]+$"; // \p{L} matches any letter (including Arabic)

            if (Regex.IsMatch(name, pattern))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Name must contain only Arabic/English letters, numbers, and spaces.");
        }
    }
}
