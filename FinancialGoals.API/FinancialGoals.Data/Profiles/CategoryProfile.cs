﻿using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Resolvers;

namespace FinancialGoals.Data.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, Core.DTOs.Category.CategoryToReturn>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom<CategoryAmountResolver>())
            .ForMember(dest => dest.Image, opt => opt.MapFrom<CategoryImageResolver>())
            .ReverseMap();

        CreateMap<CategoryToCreate, Category>()
            .ForMember(m => m.CategoryId, options => options.Ignore())
            .ForMember(m => m.Transactions, options => options.Ignore());
        
        CreateMap<CategoryToUpdate, Category>()
            //.ForMember(m => m.CategoryId, options => options.Ignore())
            .ForMember(m => m.Transactions, options => options.Ignore());
    }
}