using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoals.Data.Repository.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly FinancialDbContext _context;

    public CategoryService(FinancialDbContext context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(c => c.CategoryId == id);
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task AddCategoryAsync(Category category)
    {
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
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}