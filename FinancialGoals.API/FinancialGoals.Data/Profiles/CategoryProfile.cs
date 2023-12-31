﻿using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Resolvers;
using FinancialGoals.Data.Resolvers.CategoryResolvers;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, Core.DTOs.Category.CategoryToReturn>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom<CategoryAmountResolver>())
            .ForMember(dest => dest.Image, opt => opt.MapFrom<CategoryImageResolver>())
            // .ForMember(dest => dest.FinancialAccountIds, opt => opt.MapFrom(x => x.FinancialAccounts.Select(x => x.FinancialAccountId)))
            .ReverseMap();

        CreateMap<CategoryToCreate, Category>()
            .ForMember(m => m.CategoryId, options => options.Ignore())
            .ForMember(m => m.Transactions, options => options.Ignore())
            .ForMember(m => m.TransactionType, options => options.MapFrom(src => TransactionType.Expense))
            .ForMember(dest => dest.ImageName, opt => opt.MapFrom<CategoryImageNameResolver>());
        
        CreateMap<CategoryToUpdate, Category>()
            //.ForMember(m => m.CategoryId, options => options.Ignore())
            .ForMember(m => m.Transactions, options => options.Ignore())
            .ForMember(dest => dest.ImageName, opt => opt.MapFrom<CategoryImageNameResolver>());
    }
}