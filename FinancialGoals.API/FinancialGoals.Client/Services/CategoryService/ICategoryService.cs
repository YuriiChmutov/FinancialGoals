using FinancialGoals.Core.DTOs;
using FinancialGoals.Core.DTOs.Category;

namespace FinancialGoals.Client.CategoryService;

public interface ICategoryService
{
    event Action OnChange;
    List<CategoryToReturn> Categories { get; set; }
    Task GetCategories();
    Task<CategoryToReturn> GetCategory(int categoryId);
    Task<bool> AddCategory(CategoryToCreate category);
    CategoryToCreate CreateNewCategory();
    Task<bool> UpdateCategory(CategoryToUpdate category);
    Task DeleteCategory(int id);
}