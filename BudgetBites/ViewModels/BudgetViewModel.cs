using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BudgetBites.Models;
using BudgetBites.Services;

namespace BudgetBites.ViewModels;

public partial class BudgetViewModel : ObservableObject
{
    private readonly SpendingRepository _spendingRepo;
    private readonly GroceryRepository _groceryRepo;

    public ObservableCollection<SpendingRecord> Records { get; } = new();

    [ObservableProperty] private decimal estimatedGroceryTotal;
    [ObservableProperty] private decimal spentThisMonth;
    [ObservableProperty] private decimal newAmount;
    [ObservableProperty] private string newNote = string.Empty;

    public BudgetViewModel(SpendingRepository spendingRepo, GroceryRepository groceryRepo)
    {
        _spendingRepo = spendingRepo;
        _groceryRepo = groceryRepo;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var records = await _spendingRepo.GetAllAsync();
        Records.Clear();
        foreach (var r in records)
            Records.Add(r);

        var groceries = await _groceryRepo.GetAllAsync();
        EstimatedGroceryTotal = groceries.Where(g => !g.IsPurchased).Sum(g => g.TotalPrice);

        var now = DateTime.Now;
        SpentThisMonth = records
            .Where(r => r.Date.Year == now.Year && r.Date.Month == now.Month)
            .Sum(r => r.Amount);
    }

    [RelayCommand]
    private async Task AddRecordAsync()
    {
        if (NewAmount <= 0) return;
        var record = new SpendingRecord
        {
            Amount = NewAmount,
            Date = DateTime.Now,
            Note = string.IsNullOrWhiteSpace(NewNote) ? "Grocery run" : NewNote.Trim()
        };
        await _spendingRepo.AddAsync(record);
        NewAmount = 0;
        NewNote = string.Empty;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task DeleteRecordAsync(SpendingRecord record)
    {
        if (record is null) return;
        await _spendingRepo.DeleteAsync(record.Id);
        await LoadAsync();
    }
}