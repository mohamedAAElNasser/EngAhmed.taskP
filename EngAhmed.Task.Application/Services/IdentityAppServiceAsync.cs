using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Dto.DIdentity;
using EngAhmed.TaskP.TaskIdentity.Extends;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EngAhmed.TaskP.Application.Services
{
    public class IdentityAppServiceAsync : IIdentityAppServiceAsync
       
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityAppServiceAsync(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        #region Login
        public async Task<CustomTokenDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new CustomTokenDto() { Message = "User Name or Password  Invalid" };
            }



            var claims = new List<Claim>
    {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        
    };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(claims, true);



            return new CustomTokenDto()
            {
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                IsAuthenticated = true,
                Expiration = token.ValidTo,
                UserId = user.Id,


            };

        }
        #endregion

        #region Generate JWT Token

        private JwtSecurityToken GetToken(List<Claim> authClaims, bool rememberMe)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = jwtSettings["Secret"];

            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException(nameof(secret), "JWT Secret cannot be null or empty.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenValidity = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(3);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: authClaims,
                expires: tokenValidity,
                signingCredentials: creds
            );
        }

        #endregion
    }
}
