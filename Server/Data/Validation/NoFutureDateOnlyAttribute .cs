using System.ComponentModel.DataAnnotations;



public class NoFutureDateOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date > today)
            {
                return new ValidationResult($"{validationContext.DisplayName} cannot be a future date.");
            }
        }

        return ValidationResult.Success;
    }
}