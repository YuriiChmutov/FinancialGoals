﻿using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.DTOs.Transaction;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Resolvers;

namespace FinancialGoals.Data.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, Core.DTOs.Transaction.TransactionToReturn>()
            .ForMember(dest => dest.CategoryToReturn, opt => opt.MapFrom(src => src.Category))
            .ReverseMap();
        CreateMap<TransactionToCreate, Transaction>();

        CreateMap<TransactionToReturn, TransactionsDataDTO>()
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(x => x));

        CreateMap<TransactionToUpdate, Transaction>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.FinancialAccountId, opt => opt.Ignore())
            .ForMember(dest => dest.FinancialAccount, opt => opt.Ignore());
    }
}