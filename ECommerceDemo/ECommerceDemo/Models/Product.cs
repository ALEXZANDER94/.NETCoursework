using System.ComponentModel.DataAnnotations;
using ECommerceDemo.CustomValidators;

namespace ECommerceDemo.Models
{
    public class Product
    {
        [IsValidNumericValidator("int", ErrorMessage = "Product Code is not a valid number")]
        [Required(ErrorMessage = "Product Code is Required")]
        public int ProductCode { get; set; }
        [IsValidNumericValidator("double", ErrorMessage = "Product Price is not a valid number")]
        [Required(ErrorMessage = "Product Price is Required")]
        public double Price { get; set; }

        [IsValidNumericValidator(ErrorMessage = "Product Quantity is not a valid number")]
        [Required(ErrorMessage = "Product Quantity is Required")]
        public int Quantity { get; set; }
    }
}
