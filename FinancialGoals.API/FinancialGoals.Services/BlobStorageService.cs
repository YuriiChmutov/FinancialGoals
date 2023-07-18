using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace FinancialGoals.Services;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public BlobStorageService(BlobServiceClient blobServiceClient, string containerName)
    {
        _blobServiceClient = blobServiceClient;
        _containerName = containerName;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folderName, string fileName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        // todo add validation if file with the same name already exists
        string imageName = $"{folderName}/{fileName}.png".ToLower().Replace(" ", string.Empty);
        BlobClient blobClient = containerClient.GetBlobClient(imageName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
            });
        }

        return imageName;
    }

    public async Task<byte[]> GetImageAsync(string imageName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = containerClient.GetBlobClient(imageName);

        if (!await blobClient.ExistsAsync())
        {
            blobClient = containerClient.GetBlobClient("default.png");
        }

        var response = await blobClient.DownloadAsync();
        using (var memoryStream = new MemoryStream())
        {
            await response.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}