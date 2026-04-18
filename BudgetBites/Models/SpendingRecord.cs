namespace BudgetBites.Models;

public class SpendingRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Note { get; set; } = string.Empty;
}