using System.Text.Json;
using BudgetBites.Models;

namespace BudgetBites.Services;

public class PantryRepository
{
    private readonly string _filePath;
    private List<PantryItem> _items = new();
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public PantryRepository()
    {
        _filePath = Path.Combine(FileSystem.AppDataDirectory, "pantry_items.json");
    }

    private async Task LoadAsync()
    {
        if (!File.Exists(_filePath))
        {
            _items = new List<PantryItem>();
            return;
        }

        var json = await File.ReadAllTextAsync(_filePath);
        _items = JsonSerializer.Deserialize<List<PantryItem>>(json) ?? new List<PantryItem>();
    }

    private async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_items, _jsonOptions);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task<List<PantryItem>> GetAllAsync()
    {
        await LoadAsync();
        return _items.ToList();
    }

    public async Task AddAsync(PantryItem item)
    {
        await LoadAsync();
        _items.Add(item);
        await SaveAsync();
    }

    public async Task AddOrUpdateAsync(string name, int quantity, string category = "General")
    {
        await LoadAsync();

        var existing = _items.FirstOrDefault(i =>
            i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (existing == null)
        {
            _items.Add(new PantryItem
            {
                Name = name,
                Quantity = quantity,
                Category = category
            });
        }
        else
        {
            existing.Quantity += quantity;
        }

        await SaveAsync();
    }

    public async Task DeleteAsync(string id)
    {
        await LoadAsync();
        _items.RemoveAll(i => i.Id == id);
        await SaveAsync();
    }

    public async Task UpdateQuantityAsync(string id, int newQuantity)
    {
        await LoadAsync();

        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            item.Quantity = Math.Max(0, newQuantity);
            await SaveAsync();
        }
    }
}