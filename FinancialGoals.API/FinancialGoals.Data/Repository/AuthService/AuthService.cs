using System.Security.Cryptography;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Repository.AuthService;

public class AuthService : IAuthService
{
    private readonly FinancialDbContext _context;

    public AuthService(FinancialDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        if (await UserExists(user.Email))
        {
            return new ServiceResponse<int>
            {
                Success = false,
                Message = "User already exists"
            };
        }
        
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new ServiceResponse<int>
        {
            Success = true,
            Message = $"User was registered successfully!",
            Data = user.UserId
        };
    }

    public async Task<bool> UserExists(string email) =>
        await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())) ? true : false;

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}