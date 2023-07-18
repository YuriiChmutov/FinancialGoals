using Microsoft.AspNetCore.Http;

namespace FinancialGoals.Services;

public class MemoryFormFile : IFormFile
{
    private readonly string _fileName;
    private readonly byte[] _data;

    public MemoryFormFile(string fileName, byte[] data)
    {
        _fileName = fileName;
        _data = data;
    }

    public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{_fileName}\"";

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public string ContentType { get; } = "application/octet-stream";

    public IHeaderDictionary Headers { get; } = new HeaderDictionary();

    public long Length => _data.Length;

    public string Name { get; } = "file";
    public string FileName { get; }

    public Stream OpenReadStream()
    {
        return new MemoryStream(_data);
    }

    public void CopyTo(Stream target)
    {
        throw new NotImplementedException();
    }
}