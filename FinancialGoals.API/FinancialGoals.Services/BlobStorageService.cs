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

    public async Task<string> UploadImageAsync(IFormFile file, string filePath, string fileName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        // todo add validation if file with the same name already exists
        string imageName = $"{filePath}/{fileName}.png".ToLower().Replace(" ", string.Empty);
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

    public async Task<bool> DeleteImageAsync(string imageName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = containerClient.GetBlobClient(imageName);

        return await blobClient.DeleteIfExistsAsync();
    }
    
    public async Task<string> RenameImageAsync(string filePath, string currentImageName, string newImageName)
    {
        string fullCurrentImageName = $"{filePath}/{currentImageName}.png".ToLower().Replace(" ", string.Empty);
        string fullNewImageName = $"{filePath}/{newImageName}.png".ToLower().Replace(" ", string.Empty);
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient currentBlobClient = containerClient.GetBlobClient(fullCurrentImageName);
        BlobClient newBlobClient = containerClient.GetBlobClient(fullNewImageName);

        if (await currentBlobClient.ExistsAsync())
        {
            if (await newBlobClient.ExistsAsync())
            {
                // string uniqueNewImageName = GenerateUniqueImageName(newImageName);
                newBlobClient = containerClient.GetBlobClient(newImageName);
            }

            await newBlobClient.StartCopyFromUriAsync(currentBlobClient.Uri);

            await currentBlobClient.DeleteIfExistsAsync();

            return newBlobClient.Name;
        }

        return currentImageName;
    }
}