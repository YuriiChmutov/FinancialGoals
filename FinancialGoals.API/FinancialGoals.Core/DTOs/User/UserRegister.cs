﻿using System.ComponentModel.DataAnnotations;
using FinancialGoals.Services;

namespace FinancialGoals.Core.DTOs.User;

public class UserRegister
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;
    [Required, Display(Name = "Last name")]
    public string SecondName { get; set; } = string.Empty;
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}