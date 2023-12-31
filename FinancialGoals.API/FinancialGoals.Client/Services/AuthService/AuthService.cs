﻿using System.Net.Http.Json;
using FinancialGoals.Core.DTOs.User;
using FinancialGoals.Services;

namespace FinancialGoals.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<string>> Register(UserRegister request)
    {
        var result = await _httpClient.PostAsJsonAsync("https://localhost:7128/api/auth/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<string>> Login(UserLogin request)
    {
        var result = await _httpClient.PostAsJsonAsync("https://localhost:7128/api/auth/login", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
    {
        var result = await _httpClient.PostAsJsonAsync("https://localhost:7128/api/auth/change-password", request.Password);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }
}