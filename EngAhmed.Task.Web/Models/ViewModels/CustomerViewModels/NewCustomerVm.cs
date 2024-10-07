using System.ComponentModel.DataAnnotations;

namespace EngAhmed.TaskP.Web.Models.ViewModels.CustomerViewModels
{
    public class NewCustomerVm
    {
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string name { get; set; } = string.Empty;
        
    }
}
