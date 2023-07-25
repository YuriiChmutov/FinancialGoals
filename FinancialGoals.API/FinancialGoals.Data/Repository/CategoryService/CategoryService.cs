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

    public async Task<List<Category>> GetCategoriesByUserIdAsync(int userId)
    {
        var userCategories = await _context.Users
            .Include(u => u.Categories)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        return userCategories.Categories;
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
        var user = await _context.Users.FindAsync(userId);
        category.Users.Add(user);
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
}