using Microsoft.AspNetCore.Components;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FinancialGoals.Client.Services;

public class ImageService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;

    public ImageService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task<string> UploadImageAsync(int categoryId, Stream imageStream, string fileName)
    {
        MultipartFormDataContent formDataContent = new MultipartFormDataContent();
        formDataContent.Add(new StreamContent(imageStream), "file", fileName);

        HttpResponseMessage response = await _httpClient.PostAsync($"api/categories/{categoryId}/image", formDataContent);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<byte[]> GetImageAsync(int categoryId)
    {
        // string imageUrl = _navigationManager.BaseUri + $"api/categories/{categoryId}/image";
        string imageUrl = "https://localhost:7128/" + $"api/categories/{categoryId}/image";

        _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
        {
            NoCache = true,
            NoStore = true
        };

        return await _httpClient.GetByteArrayAsync(imageUrl);
    }
}