using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Repository.CategoryService;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();
    Task<List<Category>> GetCategoriesByUserIdAsync(int userId);
    Task<List<Category>> GetCategoriesForAccountAsync(int financialAccountId);
    Task<bool> CategoryExistsAsync(int id);
    Task<Category?> GetCategoryAsync(int id);
    Task AddCategoryAsync(Category category);
    Task AddCategoryForAccountAsync(Category category, int userId, int accountId);
    Task UpdateCategoryAsync(int id, Category category);
    // Task UpdateCategoryByUserIdAsync(int categoryId, Category category, int userId);
    Task DeleteCategoryAsync(int id);
    Task<List<Category>> GetDefaultCategories();
}