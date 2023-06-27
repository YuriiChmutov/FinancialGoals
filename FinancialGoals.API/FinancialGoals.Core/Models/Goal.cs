namespace FinancialGoals.Core.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string Name { get; set; }
        public double TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<FinancialAccount> FinancialAccounts { get; set; }
        public bool IsFinished { get; set; }
    }
}
