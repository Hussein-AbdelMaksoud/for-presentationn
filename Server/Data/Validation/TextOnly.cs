using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class TextOnly : ValidationAttribute
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
        // Regex: Allows only Arabic letters, English letters, and spaces
        string pattern = @"^[a-zA-Z\u0600-\u06FF\s]+$";

        if (Regex.IsMatch(name, pattern))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Name must contain only Arabic or English letters and spaces.");
    }
}