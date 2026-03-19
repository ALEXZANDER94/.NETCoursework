using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.CustomValidators
{
    public class IsValidNumericValidator : ValidationAttribute
    {
        public string Type { get; set; } = "int";
        public string DefaultErrorMessage { get; set; } = "Value is not a valid numeric";

        public IsValidNumericValidator() { }
        public IsValidNumericValidator(string type)
        {
            Type = type;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null) 
            {
                
                if(Type == "int")
                {
                    int? numericValue = null;
                    numericValue = Convert.ToInt32(value);
                    if(numericValue != null)
                    {
                        return ValidationResult.Success;
                    }
                }
                if(Type == "double")
                {
                    double? numericValue = null;
                    numericValue = Convert.ToDouble(value);
                    if (numericValue != null)
                    {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);

            }
            return null;
        }
    }
}
