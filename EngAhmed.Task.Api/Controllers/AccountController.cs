using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Dto.DIdentity;
using Microsoft.AspNetCore.Mvc;

namespace EngAhmed.TaskP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityAppServiceAsync _ser;

        public AccountController(IIdentityAppServiceAsync ser)
        {
            _ser = ser;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ser.Login(loginDto);

                if (!result.IsAuthenticated)
                    return Unauthorized(result.Message); 

                return Ok(result); 
            }
            return BadRequest("User Name or Password  Invalid ");
        }
        }
}
