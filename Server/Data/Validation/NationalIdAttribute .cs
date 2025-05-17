using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class NationalIdAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("National ID is required.");
        }

        string nationalId = value.ToString();

        // Ensure the National ID consists of exactly 14 digits
        if (!Regex.IsMatch(nationalId, @"^\d{14}$"))
        {
            return new ValidationResult("National ID must be exactly 14 digits long.");
        }

        // Validate that the first digit is 2, 3 (born in Egypt) or 0 (born abroad)
        char firstDigit = nationalId[0];

        if (firstDigit != '2' && firstDigit != '3' && firstDigit != '0')
        {
            return new ValidationResult("Invalid National ID. It must start with 2, 3, or 0 for those born abroad.");
        }

        return ValidationResult.Success;
    }
}