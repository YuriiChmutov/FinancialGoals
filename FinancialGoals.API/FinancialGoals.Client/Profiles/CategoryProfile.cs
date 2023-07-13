using AutoMapper;
using FinancialGoals.Core.DTOs.Category;

namespace FinancialGoals.Client.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryToReturn, CategoryToUpdate>();
    }
}