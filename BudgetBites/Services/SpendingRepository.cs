using System.Text.Json;
using BudgetBites.Models;

namespace BudgetBites.Services;

public class SpendingRepository
{
    private readonly string _filePath;
    private List<SpendingRecord> _items = new();
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public SpendingRepository()
    {
        _filePath = Path.Combine(FileSystem.AppDataDirectory, "spending_records.json");
    }

    private async Task LoadAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                _items = new List<SpendingRecord>();
                return;
            }
            var json = await File.ReadAllTextAsync(_filePath);
            _items = JsonSerializer.Deserialize<List<SpendingRecord>>(json) ?? new List<SpendingRecord>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"SpendingRepository.LoadAsync failed: {ex.Message}");
            _items = new List<SpendingRecord>();
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
            System.Diagnostics.Debug.WriteLine($"SpendingRepository.SaveAsync failed: {ex.Message}");
        }
    }

    public async Task<List<SpendingRecord>> GetAllAsync()
    {
        await LoadAsync();
        return _items.OrderByDescending(r => r.Date).ToList();
    }

    public async Task AddAsync(SpendingRecord record)
    {
        await LoadAsync();
        _items.Add(record);
        await SaveAsync();
    }

    public async Task DeleteAsync(string id)
    {
        await LoadAsync();
        _items.RemoveAll(i => i.Id == id);
        await SaveAsync();
    }
}