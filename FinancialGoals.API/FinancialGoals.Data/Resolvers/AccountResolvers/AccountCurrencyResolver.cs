using AutoMapper;
using AutoMapper.Execution;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers.AccountResolvers;

public class AccountCurrencyResolver : IValueResolver<FinancialAccount, AccountToReturn, CurrencyInfo>
{
    public CurrencyInfo Resolve(
        FinancialAccount source, 
        AccountToReturn destination, 
        CurrencyInfo destMember,
        ResolutionContext context)
    {
        return new CurrencyInfo(source.Currency);
    }
}