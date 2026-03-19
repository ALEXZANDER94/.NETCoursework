using System.ComponentModel.DataAnnotations;
using ECommerceDemo.Models;
using System.Numerics;

namespace ECommerceDemo.CustomValidators
{
    public class ListCountValidator : ValidationAttribute
    {
        public int Minimum { get; set; } = 0;
        public int? Maximum { get; set; }
        public string DefaultErrorMessage { get; set; } = "List Length must be at least {0}";
        public ListCountValidator() { }
        public ListCountValidator(int minimum)
        {
            Minimum = minimum;
        }
        public ListCountValidator(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                List<Product> List = (List<Product>) value;
                if (List.Count < Minimum)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }

                if (Maximum != null && List.Count > Maximum)
                {
                    return new ValidationResult(string.Format(this.ErrorMessage ?? "List Length must be at most {0}", Minimum));
                }
                return ValidationResult.Success;
            }
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must have at least {Minimum} entries";
        }
    }
}
