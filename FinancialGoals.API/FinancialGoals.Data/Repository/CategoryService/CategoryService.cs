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

    public async Task<List<Category>> GetCategoriesByUserIdAsync(int userId, int financialAccountId)
    {
        var data = await _context.FinancialAccounts
            .Include(acc => acc.Categories)
            .FirstOrDefaultAsync(x => x.FinancialAccountId == financialAccountId && x.UserId == userId);

        return data != null ? data.Categories : new List<Category>();
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
    
    public async Task AddCategoryByUserIdAsync(Category category, int userId)
    {
        var account = await _context.FinancialAccounts
            .FirstOrDefaultAsync(x => x.UserId == userId && x.FinancialAccountId == 2); // todo: add financialAccountId
        category.FinancialAccounts.Add(account);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    // public async Task UpdateCategoryByUserIdAsync(int categoryId, Category category, int userId)
    // {
    //     
    // }

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
}