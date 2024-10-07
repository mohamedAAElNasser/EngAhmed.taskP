using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Dto.DProduct
{
    public class NewProductDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        public decimal Price { get; set; }
    }
}
