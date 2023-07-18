using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers.CategoryResolvers;

public class CategoryImageNameResolver : IValueResolver<CategoryToCreate, Category, string>
{
    private readonly BlobStorageService _blobStorageService;

    public CategoryImageNameResolver(BlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    public string Resolve(CategoryToCreate source, Category destination, string destMember, ResolutionContext context)
    {
        var file = new MemoryFormFile(source.File.Name, source.File.Data);
        var imageName = _blobStorageService.UploadImageAsync(file, "categories", source.Name).Result;
        return imageName;
    }
}