using AutoMapper;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Resolvers.AccountResolvers;

namespace FinancialGoals.Data.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<FinancialAccount, AccountToReturn>()
            .ForMember(dest => dest.CurrencyInfo, opt => opt.MapFrom<AccountCurrencyResolver>())
            .ReverseMap();
        
        CreateMap<AccountToCreate, FinancialAccount>()
            .ConvertUsing<AccountCreateResolver>();
    }
}