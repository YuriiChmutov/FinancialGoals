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

    public CategoryImageNameResolver(BlobStorageService blobStorageService, ICategoryService categoryService)
    {
        _blobStorageService = blobStorageService;
        _categoryService = categoryService;
    }

    public string Resolve(CategoryToCreate source, Category destination, string destMember, ResolutionContext context)
    {
        var file = new MemoryFormFile(source.File.Name, source.File.Data);
        var imageName = _blobStorageService.UploadImageAsync(file, "categories", source.Name).Result;
        return imageName;
    }
    
    public string Resolve(CategoryToUpdate source, Category destination, string destMember, ResolutionContext context)
    {
        var file = new MemoryFormFile(source.File.Name, source.File.Data);
        if ((!source.Changed.Name && source.Changed.Image) || (source.Changed is {Name: true, Image: true}))
        {
            var oldCategory = _categoryService.GetCategoryAsync(source.CategoryId).Result;
            var isDeleted = _blobStorageService.DeleteImageAsync(oldCategory.ImageName).Result;
            var imageName = _blobStorageService.UploadImageAsync(file, "categories", source.Name).Result;
            return imageName;
        }

        if (source.Changed.Name && !source.Changed.Image)
        {
            var oldCategory = _categoryService.GetCategoryAsync(source.CategoryId).Result;
            var newImageName = 
                _blobStorageService.RenameImageAsync("categories", oldCategory.Name, source.Name).Result;
            return newImageName;
        }
        
        return $"categories/{source.Name}.png".ToLower().Replace(" ", string.Empty);;
    }
}