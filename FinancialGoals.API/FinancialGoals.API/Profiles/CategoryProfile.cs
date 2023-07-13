using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;

namespace FinancialGoals.API.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, Core.DTOs.Category.CategoryToReturn>().ReverseMap();

        CreateMap<CategoryToCreate, Category>()
            .ForMember(m => m.CategoryId, options => options.Ignore())
            .ForMember(m => m.Transactions, options => options.Ignore());
        
        CreateMap<CategoryToUpdate, Category>()
            //.ForMember(m => m.CategoryId, options => options.Ignore())
            .ForMember(m => m.Transactions, options => options.Ignore());
    }
}