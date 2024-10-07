using EngAhmed.TaskP.Application.Dto.DIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IAppService
{
    public interface IIdentityAppServiceAsync
    {
        
        Task<CustomTokenDto> Login(LoginDto model);
        
    }
}
