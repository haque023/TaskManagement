using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.IRepository;
using TaskManagementSystem.ViewModel;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.RegisterUserAsync(model);
                    return Ok(result);
                }
                return BadRequest("Some properties are not valid");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        [HttpPost]
        [Route("LoginUserAsync")]
        public async Task<IActionResult> LoginUserAsync(LoginDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.LoginUserAsync(model);
                    return Ok(result);
                }
                return BadRequest("Some properties are not valid");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
