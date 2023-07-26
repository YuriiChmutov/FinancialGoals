using AutoMapper;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<FinancialAccount, AccountToReturn>()
            // .ForMember(m => m, options => options.Ignore())
            .ReverseMap();
    }
}