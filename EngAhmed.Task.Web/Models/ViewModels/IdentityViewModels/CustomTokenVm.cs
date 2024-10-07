namespace EngAhmed.TaskP.Web.Models.ViewModels.IdentityViewModels
{
    public class CustomTokenVm
    {
        public string userId { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        public DateTime expiration { get; set; }
    }
}
