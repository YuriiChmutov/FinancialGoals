using AutoMapper;
using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, Core.DTOs.Transaction.TransactionToReturn>().ReverseMap();
    }
}