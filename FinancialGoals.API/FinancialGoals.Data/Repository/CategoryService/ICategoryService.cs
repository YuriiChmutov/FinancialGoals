using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Repository.CategoryService;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();
    Task<bool> CategoryExistsAsync(int id);
    Task<Category?> GetCategoryAsync(int id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(int id, Category category);
    Task DeleteCategoryAsync(int id);
}