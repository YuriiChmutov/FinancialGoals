﻿namespace FinancialGoals.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}