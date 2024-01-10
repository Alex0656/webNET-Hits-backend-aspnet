using lab1_backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace lab1_backend.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;

        public AuthController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> optAccess) // конструктор для приведения _context к актуальному состоянию
        {
            _userManager = user;
            _signInManager = signIn;
            _options = optAccess.Value;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(OtherParamUser paramUser)
        {
            var user = new IdentityUser { UserName = paramUser.UserName, Email = paramUser.Email };

            var result = await _userManager.CreateAsync(user, paramUser.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Gender", paramUser.Gender.ToString()));
                claims.Add(new Claim("IsAdmin", paramUser.IsAdmin.ToString()));
                claims.Add(new Claim("Name", paramUser.Name.ToString()));
                claims.Add(new Claim("BirthDate", paramUser.BirthDate.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, paramUser.Email));

                await _userManager.AddClaimsAsync(user, claims);
                var token = GetToken(user, claims);
                return Ok(token);
            }
            else 
            {
                return BadRequest();
            }

            
            
        }

        private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal)
        {
            var claims = prinicpal.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> SignIn(ParamUser paramUser)
        {
            var user = await _userManager.FindByNameAsync(paramUser.UserName);

            var result = await _signInManager.PasswordSignInAsync(user, paramUser.Password, false, false);

            if(result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                var token = GetToken(user, claims);

                return Ok(token);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("\"message\" : \"Logged Out\"");
        }
    }
}
