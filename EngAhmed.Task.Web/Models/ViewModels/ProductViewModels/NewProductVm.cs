using System.ComponentModel.DataAnnotations;

namespace EngAhmed.TaskP.Web.Models.ViewModels.ProductViewModels
{
    public class NewProductVm
    {

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        public decimal price { get; set; }
    }
}
