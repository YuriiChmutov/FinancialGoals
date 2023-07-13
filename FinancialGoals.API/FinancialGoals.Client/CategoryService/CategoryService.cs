﻿using System.Net;
using System.Net.Http.Json;
using FinancialGoals.Core.DTOs;
using FinancialGoals.Core.DTOs.Category;

namespace FinancialGoals.Client.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _http;

    public CategoryService(HttpClient http)
    {
        _http = http;
    }

    public List<CategoryToReturn> Categories { get; set; }
    public event Action OnChange;
    
    public async Task GetCategories()
    {
        Categories = await _http.GetFromJsonAsync<List<CategoryToReturn>>("https://localhost:7128/api/Categories");
    }

    public Task<CategoryToReturn> GetCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddCategory(CategoryToCreate newCategory)
    {
        var response = await _http.PostAsJsonAsync("https://localhost:7128/api/Categories", newCategory);
        await GetCategories();
        OnChange.Invoke();

        return response.StatusCode == HttpStatusCode.Created ? true : false;
    }

    public CategoryToCreate CreateNewCategory()
    {
        var newCategory = new CategoryToCreate();
        // Categories.Add(newCategory);
        // OnChange.Invoke();
        return newCategory;
    }

    public async Task<bool> UpdateCategory(CategoryToUpdate categoryModified)
    {
        var response = await _http.PutAsJsonAsync($"https://localhost:7128/api/Categories/{categoryModified.CategoryId}", categoryModified);
        await GetCategories();
        OnChange.Invoke();

        return response.StatusCode == HttpStatusCode.OK ? true: false;
    }

    public async Task DeleteCategory(int id)
    {
        await _http.DeleteAsync($"https://localhost:7128/api/Categories/{id}");
        await GetCategories();
        OnChange.Invoke();
    }
}