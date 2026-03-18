using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationsExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models
{
    public class Person : IValidatableObject
    {
        [Required]
        [Display(Name = "Person Name")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, space and dot (.)")]
        public string? PersonName { get; set; }

        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "{0} should contain 10 digits")]
        //[ValidateNever]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} should not be blank")]
        [Display(Name = "Re-enter Password")]
        [Compare("Password", ErrorMessage = "{0} must match {1}")]
        public string? ConfirmPassword { get; set; }

        public double? Price { get; set; }

        [MinimumYearValidator(2000, ErrorMessage = "Date of Birth should not be newere than Jan 01, {0}")]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? FromDate { get; set; }
        [DateRangeValidator("FromDate", ErrorMessage = "FromDate should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }

        public List<string?> Tags { get; set; } = new List<string?>();

        public override string ToString()
        {
            return $"Name: {PersonName} Email: {Email} Phone: {Phone} " +
                $"Password: {Password} ConfirmPassword: {ConfirmPassword} " +
                $"Price: {Price}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                yield return new ValidationResult(
                    "Either of Date of Birth or Age must be supplied", 
                    new[] { nameof(Age) }
                );
            }
        }
    }
}
