using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpsideDownKittenGenerator.Shared;
using UpsideDownKittenGenerator.Shared.Models;

namespace UpsideDownKittenGenerator.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public UsersController(JwtConfig jwtConfig, UserManager<IdentityUser> userManager)
        {
            _jwtConfig = jwtConfig;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>() { "Invalid authentication request" }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (isCorrect)
                {
                    var jwtToken = existingUser.GenerateJwtToken(_jwtConfig.Secret);

                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>() { "Invalid authentication request" }
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>() { "Invalid payload" }
            });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>() { "Email already exist" }
                    });
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Email };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = newUser.GenerateJwtToken(_jwtConfig.Secret);

                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }

                return new JsonResult(new RegistrationResponse()
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()
                }
                    )
                { StatusCode = 500 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>() { "Invalid payload" }
            });
        }
    }
}
