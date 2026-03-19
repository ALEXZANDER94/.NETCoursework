using ECommerceDemo.CustomValidators;
using ECommerceDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Models
{
    public class Order : IValidatableObject
    {
        [BindNever]
        public int? OrderNo { get; set; }
        [IsValidDateTimeValidator(ErrorMessage = "Order Date is not a valid DateTime")]
        [DateTimeMinimumValidator("2000-01-01")]
        [Required(ErrorMessage = "Order Date is Required")]
        public DateTime OrderDate { get; set; }
        [IsValidNumericValidator("double", ErrorMessage = "Order Invoice Price is not a valid number")]
        [Required(ErrorMessage = "Order Invoice Price is Required")]
        public double InvoicePrice { get; set; }

        [ListCountValidator(1, ErrorMessage = "Product List must have at least one entry")]
        [Required(ErrorMessage = "Order Products is Required")]
        public List<Product> Products { get; set; } = new List<Product>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            double TotalPrice = 0;
            Products.ForEach(product => {
                TotalPrice += product.Price * product.Quantity;
            });
            if (TotalPrice != InvoicePrice)
            {
                yield return new ValidationResult(
                    "The Sum of the price(s) of the Products must match the Invoice Price",
                        new[] { nameof(InvoicePrice) }
                );
            }
        }
    }
}
