using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.CustomValidators
{
    public class DateTimeMinimumValidator : ValidationAttribute
    {
        public DateTime MinimumDateTime { get; set; }
        public string DefaultErrorMessage { get; set; } = "Value is not a valid DateTime";

        public DateTimeMinimumValidator() { }
        public DateTimeMinimumValidator(string minimumDateTime)
        {
            MinimumDateTime = DateTime.Parse(minimumDateTime);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null) 
            {
                DateTime? dateTimeValue = null;
                dateTimeValue = value as DateTime?;
                
                if (dateTimeValue != null && MinimumDateTime != null && dateTimeValue > MinimumDateTime)
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
            return $"{name} must be after {MinimumDateTime}";
        }
    }
}
