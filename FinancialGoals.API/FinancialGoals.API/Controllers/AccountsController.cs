using FinancialGoals.Core.DTOs;
using FinancialGoals.Data.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Data.Repository.AccountService;
using Microsoft.AspNetCore.Authorization;

namespace FinancialGoals.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly FinancialDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;


        public AccountsController(
            FinancialDbContext context, 
            IAccountService accountService, 
            IMapper mapper)
        {
            _context = context;
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET: api/accounts
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<AccountToReturn>>> Get()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var accountsFromRepo = await _accountService.GetUserAccountsAsync(int.Parse(userId));
            var accountsFromRepo = await _accountService.GetUserAccountsAsync(5);

            var accountsToReturn = _mapper.Map<List<AccountToReturn>>(accountsFromRepo);
            return Ok(accountsToReturn);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.SecondName}"),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);

            //var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //if (result.Succeeded)
            //{
            //    var cookieValue = result.Principal?.Identities?.FirstOrDefault()?.AuthenticationType;
            //    //return Ok(cookieValue);
            //    var r = Request.Cookies[cookieValue];
            //    return Ok(r);
            //}

            //return BadRequest();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
