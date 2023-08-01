using AutoMapper;
using FinancialGoals.Core.DTOs.Transaction;
using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, Core.DTOs.Transaction.TransactionToReturn>().ReverseMap();
        CreateMap<TransactionToCreate, Transaction>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}