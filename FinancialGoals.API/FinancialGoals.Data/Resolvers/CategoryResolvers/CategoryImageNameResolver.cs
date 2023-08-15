using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Repository.CategoryService;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers.CategoryResolvers;

public class CategoryImageNameResolver : 
    IValueResolver<CategoryToCreate, Category, string>, 
    IValueResolver<CategoryToUpdate, Category, string>
{
    private readonly BlobStorageService _blobStorageService;
    private readonly ICategoryService _categoryService;

    private const string DefaultCategoryImageName = "default.png";

    public CategoryImageNameResolver(BlobStorageService blobStorageService, ICategoryService categoryService)
    {
        _blobStorageService = blobStorageService;
        _categoryService = categoryService;
    }

    public string Resolve(CategoryToCreate source, Category destination, string destMember, ResolutionContext context)
    {
        var imageName = string.Empty;
        if (source.File != null)
        {
            var file = new MemoryFormFile(source.File.Name, source.File.Data);
            imageName = _blobStorageService.UploadImageAsync(file, $"categories/{source.FilePath}", source.Name).Result;
        }
        else
        {
            imageName = DefaultCategoryImageName;
        }
        return imageName;
    }
    
    public string Resolve(CategoryToUpdate source, Category destination, string destMember, ResolutionContext context)
    {
        var file = new MemoryFormFile(source.File.Name, source.File.Data);
        if (source.Changed is {Name: false, Image: true} || (source.Changed is {Name: true, Image: true}))
        {
            var oldCategory = _categoryService.GetCategoryAsync(source.CategoryId).Result;
            if (oldCategory.ImageName != DefaultCategoryImageName)
            {
                var isDeleted = _blobStorageService.DeleteImageAsync(oldCategory.ImageName).Result;
            }
            var imageName = _blobStorageService.UploadImageAsync(file, $"categories/{source.FilePath}", source.Name).Result;
            return imageName;
        }

        if (source.Changed is {Name: true, Image: false})
        {
            var oldCategory = _categoryService.GetCategoryAsync(source.CategoryId).Result;
            var newImageName = 
                _blobStorageService.RenameImageAsync($"categories/{source.FilePath}", oldCategory.Name, source.Name).Result;
            return newImageName;
        }
        
        return $"categories/{source.FilePath}/{source.Name}.png".ToLower().Replace(" ", string.Empty);
    }
}