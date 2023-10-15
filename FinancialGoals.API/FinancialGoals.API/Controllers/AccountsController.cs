using FinancialGoals.Data.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Repository.AccountService;
using Microsoft.AspNetCore.Authorization;

namespace FinancialGoals.API.Controllers
{
    /// <summary>
    /// Actions with user's account for authorized users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        /// <summary>
        /// Gets the list of all authorized user's accounts
        /// </summary>
        /// <returns>The list of user's accounts</returns>
        // GET: api/accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountToReturn>>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountsFromRepo = await _accountService.GetUserAccountsAsync(int.Parse(userId));
            // var accountsFromRepo = await _accountService.GetUserAccountsAsync(5);

            var accountsToReturn = _mapper.Map<List<AccountToReturn>>(accountsFromRepo);
            return Ok(accountsToReturn);
        }

        /// <summary>
        /// Gets data of specific account
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>Data about specific account</returns>
        // GET api/accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountToReturn>> Get(int id)
        {
            var accountFromRepo = await _accountService.GetAccountAsync(id);
            return _mapper.Map<AccountToReturn>(accountFromRepo);
        }

        /// <summary>
        /// Creates an account for authorized user with specific currency.
        /// If the user already has an account with specific currency with the name "Account [Currency_X] N",
        /// then another one will be created and will have the name "Account [Currency_X] N+1"
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Accounts
        ///     {
        ///         "userId": 1,
        ///         "currencyType": 1
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Account data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(AccountToCreate request)
        {
            // var userId = 5;
            // request.UserId = userId;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountsFromRepo = await _accountService.GetUserAccountsAsync(int.Parse(userId));
            request.UserId = int.Parse(userId);
            
            var account = _mapper.Map<FinancialAccount>(request);

            // await _accountService.AddAccountAsync(account, userId);
            await _accountService.AddAccountAsync(account, int.Parse(userId));
            return Ok();
        }

        /// <summary>
        /// Updates the specific account. Not implemented yet. To think about blocking/disabling.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Removes user's account. Not implemented yet. To think about blocking/disabling.
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
