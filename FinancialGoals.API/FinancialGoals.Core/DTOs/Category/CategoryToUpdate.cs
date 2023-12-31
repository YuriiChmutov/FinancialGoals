﻿using System.ComponentModel.DataAnnotations;

namespace FinancialGoals.Core.DTOs.Category;

public class CategoryToUpdate
{
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Please enter {0}")]
    public string Name { get; set; }
    [Range(0, Double.MaxValue, ErrorMessage = "The {0} field must be greater than or equal to {1}.")]
    public decimal Limit { get; set; }
    public UploadedFileModel File { get; set; }
    public CategoryWhatWasChanged Changed { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string Color { get; set; } = String.Empty;
}