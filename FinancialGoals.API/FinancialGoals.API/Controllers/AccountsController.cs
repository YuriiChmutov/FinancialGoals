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
using FinancialGoals.Core.Models;
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
        public async Task<ActionResult<AccountToReturn>> Get(int id)
        {
            var accountFromRepo = await _accountService.GetAccountAsync(id);
            return _mapper.Map<AccountToReturn>(accountFromRepo);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AccountToCreate request)
        {
            var userId = 5;
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var accountsFromRepo = await _accountService.GetUserAccountsAsync(int.Parse(userId));
            // request.UserId = int.Parse(userId);
            request.UserId = userId;
            
            var account = _mapper.Map<FinancialAccount>(request);

            await _accountService.AddAccountAsync(account, userId);
            // await _accountService.AddAccountAsync(account, int.Parse(userId));
            return Ok();
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
