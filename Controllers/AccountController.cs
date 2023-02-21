using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wms_api.DTO;
using wms_api.Helpers;

namespace wms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Create([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            var keyJwt = _configuration.GetValue<string>("KeyJwt");
            if (keyJwt == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server configuration error");
            }

            var user = new IdentityUser { UserName = userCredentialsDTO.Email, Email = userCredentialsDTO.Email };
            var result = await _userManager.CreateAsync(user, userCredentialsDTO.Password);

            if (result.Succeeded)
            {
                return JwtHelper.BuildToken(userCredentialsDTO, keyJwt);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login([FromBody] UserCredentialsDTO userCredentialsDTO)
        {            
            var keyJwt = _configuration.GetValue<string>("KeyJwt");
            if (keyJwt == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server configuration error");
            }

            var result = await _signInManager.PasswordSignInAsync(userCredentialsDTO.Email, userCredentialsDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return JwtHelper.BuildToken(userCredentialsDTO, keyJwt);
            }

            return BadRequest("The username or password is incorrect");
        }
    }
}
