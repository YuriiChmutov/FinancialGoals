using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using FinancialGoals.Services;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoals.Data.Repository.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly FinancialDbContext _context;
    private readonly BlobStorageService _blobStorageService;

    public CategoryService(FinancialDbContext context, BlobStorageService blobStorageService)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
        _blobStorageService = blobStorageService;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<List<Category>> GetCategoriesForAccountAsync(int financialAccountId)
    {
        var data = await _context.FinancialAccounts
            .Include(acc => acc.Categories)
            .FirstOrDefaultAsync(x => x.FinancialAccountId == financialAccountId);

        return data != null ? data.Categories : new List<Category>();
    }
    
    public async Task<List<Category>> GetCategoriesByUserIdAsync(int userId)
    {
        return await _context.Categories
            .Include(c => c.FinancialAccounts
                .Where(x => x.UserId == userId)).ToListAsync();
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(c => c.CategoryId == id);
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }
    
    public async Task AddCategoryForAccountAsync(Category category, int userId, int accountId)
    {
        var account = await _context.FinancialAccounts
            .FirstOrDefaultAsync(x => x.UserId == userId && x.FinancialAccountId == accountId);
        category.FinancialAccounts.Add(account);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        await _blobStorageService.DeleteImageAsync(category.ImageName);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetDefaultCategories()
    {
        return await _context.Categories.Where(c => c.Default).ToListAsync();
    }

    public async Task<List<Category>> CreateDefaultCategories(int accountId)
    {
        return new List<Category>();
    }
}