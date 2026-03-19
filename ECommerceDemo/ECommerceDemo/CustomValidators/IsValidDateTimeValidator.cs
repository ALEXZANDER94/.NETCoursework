using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.CustomValidators
{
    public class IsValidDateTimeValidator : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Value is not a valid DateTime";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null) 
            {
                DateTime? dateTimeValue = null;
                dateTimeValue = value as DateTime?;
                if (dateTimeValue != null)
                {
                    return ValidationResult.Success;
                }
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);

            }
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} is not a valid DateTime";
        }
    }
}
