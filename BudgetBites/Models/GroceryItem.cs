namespace BudgetBites.Models;

public class GroceryItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string Category { get; set; } = "General";
    public bool IsPurchased { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;

    public decimal TotalPrice => Quantity * UnitPrice;
}