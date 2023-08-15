using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FinancialGoals.Core.DTOs.Category;

public class CategoryToCreate
{
    [Required(ErrorMessage = "Please enter {0}")]
    public string Name { get; set; }
    [Range(0, Double.MaxValue, ErrorMessage = "The {0} field must be greater than or equal to {1}.")]
    public decimal Limit { get; set; } = 0;
    public UploadedFileModel? File { get; set; }
}