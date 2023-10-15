using System.Security.Claims;
using AutoMapper;
using FinancialGoals.Core.DTOs.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using FinancialGoals.Data.Repository.TransactionService;
using Microsoft.AspNetCore.Authorization;

namespace FinancialGoals.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all user's transactions. Without connection with accounts.
        /// Returns specific page - skips some amount of transactions.
        /// Page has data about 4 transactions.
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns></returns>
        // GET: api/Transactions/user-transactions/1
        [HttpGet("user-transactions/{page}")]
        public async Task<ActionResult<TransactionsDataDTO>> GetTransactions(int page)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactionsFromRepo = await _transactionService.GetTransactionsForUserAsync(int.Parse(userId), page);
            
            return Ok(transactionsFromRepo);
        }

        /// <summary>
        /// Gets all user's transactions for specific account.
        /// Returns specific page - skips some amount of transactions.
        /// Page has data about 4 transactions.
        /// </summary>
        /// <param name="accountId">User account id</param>
        /// <param name="page">Page number</param>
        /// <returns></returns>
        // GET: api/Transactions/user-transactions/1/1
        [HttpGet("user-transactions/{accountId}/{page}")]
        public async Task<ActionResult<TransactionsDataDTO>> GetTransactions(int accountId, int page)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactionsFromRepo = await _transactionService.GetTransactionsForUserByAccountAsync(int.Parse(userId), accountId, page);
            
            return Ok(transactionsFromRepo);
        }

        /// <summary>
        /// Gets all user's transactions for specific account in specific year.
        /// Returns specific page - skips some amount of transactions.
        /// Page has data about 4 transactions.
        /// </summary>
        /// <param name="accountId">User account id</param>
        /// <param name="year">Specific year</param>
        /// <returns></returns>
        [HttpGet("account-transactions/{accountId}/{year}")]
        public async Task<ActionResult<Dictionary<string, double>>> GetTransactionsSpendsForEachMonthPerYear(int accountId, int year)
        {
            var data = 
                await _transactionService.GetTransactionsSpendsForEachMonthPerYear(accountId, year);
            return Ok(data);
        }

        /// <summary>
        /// Gets all user's transactions for specific account in specific year and month.
        /// Returns specific page - skips some amount of transactions.
        /// Page has data about 4 transactions.
        /// </summary>
        /// <param name="accountId">User account id</param>
        /// <param name="year">Specific year</param>
        /// <param name="month">Specific month</param>
        /// <returns></returns>
        [HttpGet("account-transactions-per-month/{accountId}/{year}/{month}")]
        public async Task<ActionResult<List<ExpensesPerMonthByCategoryDTO>>> GetExpensesAmountByCategoryPerMonth(
            int accountId, int year, int month)
        {
            var data = await _transactionService.GetExpensesAmountByCategoryPerMonth(accountId, month, year);
            return Ok(data);
        }

        /// <summary>
        /// Gets data about specific transaction.
        /// </summary>
        /// <param name="id">Transaction id</param>
        /// <returns></returns>
        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionToReturn>> GetTransaction(int id)
        {
            var transactionFromRepo = await _transactionService.GetTransactionAsync(id);

            if (transactionFromRepo == null) return NotFound();

            var transactionToReturn = _mapper.Map<TransactionToReturn>(transactionFromRepo);
            return transactionToReturn;
        }
        
        /// <summary>
        /// Creates new transaction
        /// </summary>
        /// <remarks>
        /// Simple request:
        ///
        ///     POST api/Transactions
        ///     {
        ///         "amount": 499,
        ///         "type": 0,
        ///         "date": "2023-10-10T18:18:46.989Z",
        ///         "description": "I bought food",
        ///         "financialAccountId": 1,
        ///         "categoryId": 4
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Transaction data</param>
        /// <returns></returns>
        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionToCreate request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            await _transactionService.AddTransactionAsync(transaction);

            var transactionToReturn = _mapper.Map<Transaction>(transaction);
            return Ok(transactionToReturn);
        }

        /// <summary>
        /// Updates transaction
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{accountId}/{id}")]
        public async Task<ActionResult> PutTransaction(int accountId, int id, TransactionToUpdate request)
        {
            var transactionToUpdate = _mapper.Map<Transaction>(request);

            await _transactionService.UpdateTransactionAsync(id, transactionToUpdate);

            return Ok();
        }

        /// <summary>
        /// Removes transaction. No soft delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
