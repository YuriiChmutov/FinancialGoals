using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoals.Data.Repository.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly FinancialDbContext _context;

    public TransactionService(FinancialDbContext context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await _context.Transactions.Where(t => t.FinancialAccountId == accountId).ToListAsync();
    }

    public async Task<List<Transaction>> GetTransactionsByDateAsync(int accountId, DateTime dateStart, DateTime? dateEnd = null)
    {
        dateEnd ??= DateTime.Now;
        
        return await _context.Transactions
            .Where(t => t.FinancialAccountId == accountId &&
                        t.Date >= dateStart && t.Date <= dateEnd
            ).ToListAsync();
    }

    public decimal GetSpendAmountByCategory(int categoryId)
    {
        var amount = _context.Transactions
            .Where(t => t.CategoryId == categoryId)
            .Select(t => t.Amount)
            .Sum();
    
        return (decimal)amount;
    }

    public async Task<bool> TransactionExistsAsync(int id)
    {
        return await _context.Transactions.AnyAsync(t => t.TransactionId == id);
    }

    public async Task<Transaction?> GetTransactionAsync(int id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTransactionAsync(int id, Transaction transaction)
    {
        _context.Entry(transaction).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        
        // todo add nullable check
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}