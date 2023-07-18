using AutoMapper;
using FinancialGoals.Core.DTOs;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers;

public class CategoryImageResolver : IValueResolver<Category, CategoryToReturn, ImageData>
{
    private readonly BlobStorageService _blobStorageService;

    public CategoryImageResolver(BlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }
    
    public ImageData Resolve(Category source, CategoryToReturn destination, ImageData destMember, ResolutionContext context)
    {
        var image = _blobStorageService.GetImageAsync(source.ImageName).Result;

        return new ImageData
        {
            ImageName = source.ImageName,
            ImageBytes = image
        };
    }
}