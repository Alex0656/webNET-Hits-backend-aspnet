using lab1_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Optivem.Framework.Core.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace lab1_backend.Controllers
{
    [Route("api/account/prifole")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> optAccess, UserDbContext context) // конструктор для приведения _context к актуальному состоянию
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signIn;
            _options = optAccess.Value;

        }

        [Authorize]
        [HttpGet]
        public  IActionResult GetProfile()
        {
            var usrId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usrEmail = this.User.FindFirstValue(ClaimTypes.Email);
            var usrName= this.User.FindFirstValue(ClaimTypes.Name);

            var userProfile =  _context.UserClaims.Where(c => c.UserId == usrId);
            
            List<MyPair> myPairList = new List<MyPair>();

            foreach(var node in userProfile)
            {
                var pair = new MyPair(node.ClaimType.ToString(), node.ClaimValue.ToString());
                myPairList.Add(pair);
            }
            var pairEmail = new MyPair("Email", usrEmail.ToString());
            myPairList.Add(pairEmail);
            var pairId = new MyPair("id", usrId.ToString());
            myPairList.Add(pairId);

            //   return this.Content(HttpStatusCode.OK,);
            return Ok(MyPair.ToJson("Профиль пользователя " + usrName, myPairList));
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


        [HttpPut]
        [Authorize]
        //[AllowAnonymous]
        //public async Task<IActionResult> UpdateProfile(OtherParamUser paramUser)
        public async Task<IActionResult> UpdateProfile(UserShortModel userFromParam)
        {

            IdentityUser userFromDb_byNameFromParam = await _userManager.FindByNameAsync(userFromParam.UserName);
            IdentityUser userFromDb_byNameFromSecuritySession = await _userManager.FindByNameAsync(this.User.Identity.Name);
            bool areUsersEquels = userFromDb_byNameFromSecuritySession.Equals(userFromDb_byNameFromParam);

            if (!areUsersEquels)
                return BadRequest("Изменять  UserName запрещено");

            if (userFromDb_byNameFromParam == null)
                return BadRequest("Вы не указали имя пользователя");


            IdentityResult delUserResult = await _userManager.DeleteAsync(userFromDb_byNameFromSecuritySession);
            if (!delUserResult.Succeeded)
                return BadRequest("у вас аккаунт не удаляется");

            var userCreated = new IdentityUser { UserName = userFromParam.UserName, Email = userFromParam.Email };
            var resultUserCreation = await _userManager.CreateAsync(userCreated, userFromParam.Password);

            if (!resultUserCreation.Succeeded)
                return BadRequest("Ваш аккаунт удален");

            await _signInManager.SignInAsync(userCreated, isPersistent: false);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Gender", userFromParam.Gender.ToString()));
            claims.Add(new Claim("IsAdmin", userFromParam.IsAdmin.ToString()));
            claims.Add(new Claim("Name", userFromParam.Name.ToString()));
            claims.Add(new Claim("BirthDate", userFromParam.BirthDate.ToString()));
            claims.Add(new Claim("NickName", userFromParam.NickName.ToString()));
            claims.Add(new Claim("Avatar", userFromParam.Avatar.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, userFromParam.Email));

            await _userManager.AddClaimsAsync(userCreated, claims);



            var token = GetToken(userCreated, claims); //Авторизирую ли я тут пользователя, или просто выдаю токен?
            return Ok(token);


        }

        [HttpPut("experements")]
        [Authorize]
        //[AllowAnonymous]
        //public async Task<IActionResult> UpdateProfile(OtherParamUser paramUser)
        public async Task<IActionResult> ExperementalUpdateProfile(MyUserProfile myUserProfile)
        {


            IdentityUser userFromDb_byNameFromSecuritySession = await _userManager.FindByNameAsync(this.User.Identity.Name);

            var user = _context.UserClaims.Where(p => p.UserId == userFromDb_byNameFromSecuritySession.Id).ToList();

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("Gender", myUserProfile.Gender.ToString()));
            claims.Add(new Claim("IsAdmin", myUserProfile.IsAdmin.ToString()));
            claims.Add(new Claim("Name", myUserProfile.Name.ToString()));
            claims.Add(new Claim("BirthDate", myUserProfile.BirthDate.ToString()));
            claims.Add(new Claim("NickName", myUserProfile.NickName.ToString()));
            claims.Add(new Claim("Avatar", myUserProfile.Avatar.ToString()));
          //  claims.Add(new Claim(ClaimTypes.Email, myUserProfile.Email)); //

            userFromDb_byNameFromSecuritySession.Email = myUserProfile.Email;

            await _context.SaveChangesAsync();

            await _userManager.AddClaimsAsync(userFromDb_byNameFromSecuritySession, claims);

            return Ok();


        }
    }
}

