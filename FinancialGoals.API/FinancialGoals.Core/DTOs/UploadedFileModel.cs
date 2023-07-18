namespace FinancialGoals.Core.DTOs;

public class UploadedFileModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public long Size { get; set; }
    public byte[] Data { get; set; }

}