using AutoMapper;
using FinancialGoals.Core.DTOs.Transaction;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using FinancialGoals.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FinancialGoals.Data.Repository.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly FinancialDbContext _context;
    private readonly IMapper _mapper;

    public TransactionService(FinancialDbContext context, IMapper mapper)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
        _mapper = mapper;
    }

    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await _context.Transactions
            //.Include(t => t.Category)
            .Where(t => t.FinancialAccountId == accountId)
            .ToListAsync();
    }

    public async Task<TransactionsDataDTO> GetTransactionsForUserAsync(int userId, int page)
    {
        var pageResults = 3f;
        var data = await _context.Transactions
            .Include(t => t.FinancialAccount)
            .Where(t => t.FinancialAccount.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        var pageCount = Math.Ceiling(data.Count / pageResults);

        var transactions = data.Skip((page - 1) * (int) pageResults).Take((int) pageResults).ToList();

        return new TransactionsDataDTO
        {
            Transactions = _mapper.Map<List<TransactionToReturn>>(transactions),
            Pages = (int) pageCount,
            CurrentPage = page
        };
    }
    
    public async Task<TransactionsDataDTO> GetTransactionsForUserByAccountAsync(int userId, int accountId, int page)
    {
        var pageResults = 5f;
        var data = await _context.Transactions
            .Include(t => t.FinancialAccount)
            .Include(t => t.Category)
            .Where(t => t.FinancialAccount.UserId == userId && t.FinancialAccountId == accountId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
        
        var pageCount = Math.Ceiling(data.Count / pageResults);

        var transactions = data.Skip((page - 1) * (int) pageResults).Take((int) pageResults).ToList();
        
        return new TransactionsDataDTO
        {
            Transactions = _mapper.Map<List<TransactionToReturn>>(transactions),
            Pages = (int) pageCount,
            CurrentPage = page
        };
    }

    public async Task<List<Transaction>> GetTransactionsByDateAsync(int accountId, DateTime dateStart, DateTime? dateEnd = null)
    {
        dateEnd ??= DateTime.Now;
        
        return await _context.Transactions
            .Where(t => t.FinancialAccountId == accountId &&
                        t.Date >= dateStart && t.Date <= dateEnd
            ).ToListAsync();
    }

    public async Task<Dictionary<string, double>> GetTransactionsSpendsForEachMonthPerYear(int accountId, int year)
    {
        var monthlyExpenses =
            await _context.Transactions
                .Where(x => x.FinancialAccountId == accountId &&
                            x.Type == TransactionType.Expense &&
                            x.Date.Year == year)
                .GroupBy(x => x.Date.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    TotalAmount = group.Sum(x => x.Amount)
                })
                .ToListAsync();

        var result = new Dictionary<string, double>();
        var cultureInfo = new CultureInfo("en-US");

        for (int month = 1; month <= 12; month++)
        {
            //var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            var monthName = cultureInfo.DateTimeFormat.GetAbbreviatedMonthName(month);
            var expensesForMonth = monthlyExpenses.FirstOrDefault(x => x.Month == month);
            result.Add(monthName, expensesForMonth?.TotalAmount ?? 0);
        }

        return result;
    }

    public decimal GetSpendAmountByCategory(int categoryId, DateTime? date = null)
    {
        if (!date.HasValue)
        {
            date = DateTime.Now;
        }
        var amount = _context.Transactions
            .Where(t => t.CategoryId == categoryId && t.Date.Month == date.Value.Month)
            .Select(t => t.Amount)
            .Sum();
    
        return (decimal)amount;
    }

    public async Task<List<ExpensesPerMonthByCategoryDTO>> GetExpensesAmountByCategoryPerMonth(int accountId, int month, int year)
    {
        var data = await _context.Transactions
            .Where(x => x.Date.Month == month && x.Date.Year == year && x.FinancialAccountId == accountId)
            .GroupBy(x => x.CategoryId)
            .Select(group => new ExpensesPerMonthByCategoryDTO
            {
                CategoryId = group.Key,
                CategoryName = group.FirstOrDefault()!.Category.Name,
                Amount = (decimal)group.Sum(x => x.Amount),
                Color = group.FirstOrDefault()!.Category.Color
            })
            .ToListAsync();
    
        return data;
    }

    public async Task<bool> TransactionExistsAsync(int id)
    {
        return await _context.Transactions.AnyAsync(t => t.TransactionId == id);
    }

    public async Task<Transaction?> GetTransactionAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.TransactionId == id);
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTransactionAsync(int id, Transaction transaction)
    {
        var transactionFromDb = await _context.Transactions.FindAsync(id);

        if (transactionFromDb != null)
        {
            transactionFromDb.Amount = transaction.Amount;
            transactionFromDb.CategoryId = transaction.CategoryId;
            transactionFromDb.Description = transaction.Description;
            transactionFromDb.Type = transaction.Type;

            //_context.Entry(transaction).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTransactionAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        
        // todo add nullable check
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}