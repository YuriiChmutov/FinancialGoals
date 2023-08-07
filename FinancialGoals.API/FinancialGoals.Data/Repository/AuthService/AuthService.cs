using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AutoMapper;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Data.Repository.AccountService;
using FinancialGoals.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinancialGoals.Data.Repository.AuthService;

public class AuthService : IAuthService
{
    private readonly FinancialDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public AuthService(FinancialDbContext context, IConfiguration configuration, IMapper mapper, IAccountService accountService)
    {
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
        _accountService = accountService;
    }

    public async Task<ServiceResponse<string>> Register(User user, string password)
    {
        if (await UserExists(user.Email))
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "User already exists"
            };
        }
        
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        var account = _mapper.Map<FinancialAccount>(new AccountToCreate
        {
            CurrencyType = CurrencyType.UAH,
            UserId = user.UserId
        });
        account.User = user;
        account.Categories = new List<Category>();
        
        var defaultCategories = new List<Category>
        {
            new Category
            {
                Name = "Income",
                Default = true,
                ImageName = "income.png",
                Limit = 0,
                TransactionType = TransactionType.Income
            },
            new Category
            {
                Name = "Shopping",
                Default = true,
                ImageName = "shopping-basket.png",
                Limit = 0,
                TransactionType = TransactionType.Expense
            }
        };
        
        account.Categories.AddRange(defaultCategories);
        _context.Users.Add(user);
        _context.FinancialAccounts.Add(account);
        
        await _context.SaveChangesAsync();

        return new ServiceResponse<string>
        {
            Success = true,
            Message = $"User was registered successfully!",
            Data = CreateToken(user)
        };
    }

    public async Task<bool> UserExists(string email) =>
        await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())) ? true : false;

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found";
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Wrong password";
        }
        else
        {
            response.Success = true;
            response.Data = CreateToken(user);
        }
        
        return response;
    }

    public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "User not found."
            };
        }
        
        CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Success = true,
            Message = "Password has been changed."
        };
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.SecondName}"),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmc = new HMACSHA512(passwordSalt);
        var computedHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}