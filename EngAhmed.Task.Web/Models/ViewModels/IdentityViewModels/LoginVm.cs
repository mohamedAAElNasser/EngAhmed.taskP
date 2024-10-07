using System.ComponentModel.DataAnnotations;

namespace EngAhmed.TaskP.Web.Models.ViewModels.IdentityViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string userName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string password { get; set; } = string.Empty;
    }
}
