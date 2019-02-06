using System.Security.Claims;
using System.Threading.Tasks;
using Lab3.API.Auth;
using Lab3.API.Data;
using Lab3.API.Helpers;
using Lab3.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Lab3.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<ApplicationUser> userManager,
          IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(userViewModel.Email, userViewModel.Password);
            if (identity == null)
            {
                return BadRequest("Identity not found!");
            }

            // Geting user role.
            var user = await _userManager.FindByNameAsync(userViewModel.Email);
            var role = await _userManager.GetRolesAsync(user);

            // Generating JWT.
            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, userViewModel.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented }, role[0]);

            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
