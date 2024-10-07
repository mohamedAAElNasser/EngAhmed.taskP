

namespace EngAhmed.TaskP.Application.Dto.DIdentity
{
    public class CustomTokenDto
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
