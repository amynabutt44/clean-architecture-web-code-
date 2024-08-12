using System;
using System.ComponentModel.DataAnnotations;

namespace domain.Models
{
    public class NotAbcAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var stringValue = value.ToString();
                if (stringValue.Equals("abc", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult("The category name cannot be 'abc' or 'ABC'.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
