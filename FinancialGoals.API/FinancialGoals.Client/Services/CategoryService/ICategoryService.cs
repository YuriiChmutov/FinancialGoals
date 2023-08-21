using FinancialGoals.Core.DTOs;
using FinancialGoals.Core.DTOs.Category;

namespace FinancialGoals.Client.CategoryService;

public interface ICategoryService
{
    event Action OnChange;
    List<CategoryToReturn> Categories { get; set; }
    Task GetCategories();
    Task<List<CategoryToReturn>> GetCategoriesForDropdown(int accountId);
    Task<CategoryToReturn> GetCategory(int categoryId);
    Task<bool> AddCategory(CategoryToCreate category, int accountId);
    CategoryToCreate CreateNewCategory();
    Task<bool> UpdateCategory(CategoryToUpdate category, int accountId);
    Task DeleteCategory(int id);
    Task<string> GetImageUrl();
    Task Clean();
}