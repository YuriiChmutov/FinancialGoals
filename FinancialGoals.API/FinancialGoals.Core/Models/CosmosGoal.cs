namespace FinancialGoals.Core.Models
{
    public class CosmosGoal
    {
        public int GoalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFinished { get; set; }
    }
}
