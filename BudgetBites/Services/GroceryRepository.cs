using System.Text.Json;
using BudgetBites.Models;

namespace BudgetBites.Services;

public class GroceryRepository
{
    private readonly string _filePath;
    private List<GroceryItem> _items = new();
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public GroceryRepository()
    {
        _filePath = Path.Combine(FileSystem.AppDataDirectory, "grocery_items.json");
    }

    private async Task LoadAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                _items = new List<GroceryItem>();
                return;
            }
            var json = await File.ReadAllTextAsync(_filePath);
            _items = JsonSerializer.Deserialize<List<GroceryItem>>(json) ?? new List<GroceryItem>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"GroceryRepository.LoadAsync failed: {ex.Message}");
            _items = new List<GroceryItem>();
        }
    }

    private async Task SaveAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_items, _jsonOptions);
            await File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"GroceryRepository.SaveAsync failed: {ex.Message}");
        }
    }

    public async Task<List<GroceryItem>> GetAllAsync()
    {
        await LoadAsync();
        return _items.ToList();
    }

    public async Task<GroceryItem?> GetByIdAsync(string id)
    {
        await LoadAsync();
        return _items.FirstOrDefault(i => i.Id == id);
    }

    public async Task AddAsync(GroceryItem item)
    {
        await LoadAsync();
        _items.Add(item);
        await SaveAsync();
    }

    public async Task UpdateAsync(GroceryItem item)
    {
        await LoadAsync();
        var idx = _items.FindIndex(i => i.Id == item.Id);
        if (idx >= 0)
        {
            _items[idx] = item;
            await SaveAsync();
        }
    }

    public async Task DeleteAsync(string id)
    {
        await LoadAsync();
        _items.RemoveAll(i => i.Id == id);
        await SaveAsync();
    }

    public async Task TogglePurchasedAsync(string id)
    {
        await LoadAsync();
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            item.IsPurchased = !item.IsPurchased;
            await SaveAsync();
        }
    }
}