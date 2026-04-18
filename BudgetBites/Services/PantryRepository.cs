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
        try
        {
            if (!File.Exists(_filePath))
            {
                _items = new List<PantryItem>();
                return;
            }
            var json = await File.ReadAllTextAsync(_filePath);
            _items = JsonSerializer.Deserialize<List<PantryItem>>(json) ?? new List<PantryItem>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"PantryRepository.LoadAsync failed: {ex.Message}");
            _items = new List<PantryItem>();
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
            System.Diagnostics.Debug.WriteLine($"PantryRepository.SaveAsync failed: {ex.Message}");
        }
    }

    public async Task<List<PantryItem>> GetAllAsync()
    {
        await LoadAsync();
        return _items.ToList();
    }

    public async Task<PantryItem?> GetByIdAsync(string id)
    {
        await LoadAsync();
        return _items.FirstOrDefault(i => i.Id == id);
    }

    public async Task AddAsync(PantryItem item)
    {
        await LoadAsync();
        _items.Add(item);
        await SaveAsync();
    }

    public async Task UpdateAsync(PantryItem item)
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