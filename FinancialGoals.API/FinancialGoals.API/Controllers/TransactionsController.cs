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

        // GET: api/Transactions
        [HttpGet("user-transactions/{page}")]
        public async Task<ActionResult<TransactionsDataDTO>> GetTransactions(int page)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactionsFromRepo = await _transactionService.GetTransactionsForUserAsync(int.Parse(userId), page);
            
            return Ok(transactionsFromRepo);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionToReturn>> GetTransaction(int id)
        {
            var transactionFromRepo = await _transactionService.GetTransactionAsync(id);

            if (transactionFromRepo == null) return NotFound();

            var transactionToReturn = _mapper.Map<TransactionToReturn>(transactionFromRepo);
            return transactionToReturn;
        }

        // // PUT: api/Transactions/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        // {
        //     if (id != transaction.TransactionId)
        //     {
        //         return BadRequest();
        //     }
        //
        //     var cate
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TransactionExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        
        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionToCreate request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            await _transactionService.AddTransactionAsync(transaction);

            var transactionToReturn = _mapper.Map<Transaction>(transaction);
            return Ok(transactionToReturn);
        }
        //
        // // DELETE: api/Transactions/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTransaction(int id)
        // {
        //     if (_context.Transactions == null)
        //     {
        //         return NotFound();
        //     }
        //     var transaction = await _context.Transactions.FindAsync(id);
        //     if (transaction == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Transactions.Remove(transaction);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // private bool TransactionExists(int id)
        // {
        //     return (_context.Transactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        // }
    }
}
