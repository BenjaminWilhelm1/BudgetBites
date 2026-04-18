namespace BudgetBites.Models;

public class PantryItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public string Category { get; set; } = "General";
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public DateTime? ExpirationDate { get; set; }
}