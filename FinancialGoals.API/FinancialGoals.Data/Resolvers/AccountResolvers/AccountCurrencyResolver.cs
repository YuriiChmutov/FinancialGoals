using AutoMapper;
using AutoMapper.Execution;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers.AccountResolvers;

public class AccountCurrencyResolver : 
    IValueResolver<FinancialAccount, AccountToReturn, CurrencyInfo>,
    IValueResolver<FinancialAccount, AccountToReturn, string>
{
    public CurrencyInfo Resolve(
        FinancialAccount source, 
        AccountToReturn destination, 
        CurrencyInfo destMember,
        ResolutionContext context)
    {
        return new CurrencyInfo(source.Currency);
    }

    public string Resolve(FinancialAccount source, AccountToReturn destination, string destMember, ResolutionContext context)
    {
        var currencyInfo = new CurrencyInfo(source.Currency);
        return $"Account # {source.Number} ({currencyInfo.CurrencySymbol}, {currencyInfo.Currency})";
    }
}