using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Dto.DCustomer
{
    public class NewCustomerDto
    {
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
