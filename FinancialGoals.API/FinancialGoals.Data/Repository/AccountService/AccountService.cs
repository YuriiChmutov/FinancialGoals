using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using FinancialGoals.Services;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoals.Data.Repository.AccountService;

public class AccountService : IAccountService
{
    private readonly FinancialDbContext _context;
    private readonly BlobStorageService _blobStorageService;

    public AccountService(FinancialDbContext context, BlobStorageService blobStorageService)
    {
        _context = context;
        _blobStorageService = blobStorageService;
    }

    public async Task<List<FinancialAccount>> GetUserAccountsAsync(int userId)
    {
        return await _context.FinancialAccounts
            .Include(x => x.Categories)
            .Where(acc => acc.UserId == userId).ToListAsync();
    }

    public async Task<bool> AccountExistsAsync(int accountId)
    {
        return await _context.FinancialAccounts
            .AnyAsync(acc => acc.FinancialAccountId == accountId);
    }

    public async Task<FinancialAccount?> GetAccountAsync(int accountId)
    {
        return await _context.FinancialAccounts
            .FirstOrDefaultAsync(acc => acc.FinancialAccountId == accountId);
    }

    public async Task AddAccountAsync(FinancialAccount account, int userId)
    {
        account.UserId = userId;
        _context.FinancialAccounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAccountAsync(int accountId, FinancialAccount account)
    {
        _context.Entry(account).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await _context.FinancialAccounts.FindAsync(accountId);
        _context.FinancialAccounts.Remove(account);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetAmountOfUsersCurrencyAccounts(int userId, CurrencyType currencyType)
    {
        return _context.FinancialAccounts.Count(x => x.UserId == userId && x.Currency == currencyType);
    }
}