using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.Core.Dtos.Idenitiy;
using TestApp.Services.Idenitiy.Interfaces;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserServices _userService;

        public AccountController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto) => Ok(await _userService.Login(loginDto));

    }
}