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
    private readonly PantryRepository _pantryRepo;

    public ObservableCollection<SpendingRecord> Records { get; } = new();

    [ObservableProperty] private decimal estimatedGroceryTotal;
    [ObservableProperty] private decimal spentThisMonth;
    [ObservableProperty] private decimal newAmount;
    [ObservableProperty] private string newNote = string.Empty;
    [ObservableProperty] private int purchasedItemCount;

    public BudgetViewModel(
        SpendingRepository spendingRepo,
        GroceryRepository groceryRepo,
        PantryRepository pantryRepo)
    {
        _spendingRepo = spendingRepo;
        _groceryRepo = groceryRepo;
        _pantryRepo = pantryRepo;
    }

    [RelayCommand]
    public async Task LoadItemsAsync()
    {
        var records = await _spendingRepo.GetAllAsync();

        Records.Clear();
        foreach (var record in records)
            Records.Add(record);

        var groceries = await _groceryRepo.GetAllAsync();

        EstimatedGroceryTotal = groceries
            .Where(g => !g.IsPurchased)
            .Sum(g => g.TotalPrice);

        PurchasedItemCount = groceries.Count(g => g.IsPurchased);

        var now = DateTime.Now;

        SpentThisMonth = records
            .Where(r => r.Date.Year == now.Year && r.Date.Month == now.Month)
            .Sum(r => r.Amount);
    }

    [RelayCommand]
    private async Task AddRecordAsync()
    {
        if (NewAmount <= 0)
        {
            await Shell.Current.DisplayAlert("Missing Amount", "Please enter a checkout total greater than $0.", "OK");
            return;
        }

        var record = new SpendingRecord
        {
            Amount = NewAmount,
            Date = DateTime.Now,
            Note = string.IsNullOrWhiteSpace(NewNote) ? "Grocery trip" : NewNote.Trim()
        };

        await _spendingRepo.AddAsync(record);

        var groceries = await _groceryRepo.GetAllAsync();
        var purchasedItems = groceries.Where(g => g.IsPurchased).ToList();

        foreach (var item in purchasedItems)
        {
            await _pantryRepo.AddOrUpdateAsync(item.Name, item.Quantity, item.Category);
            await _groceryRepo.DeleteAsync(item.Id);
        }

        NewAmount = 0;
        NewNote = string.Empty;

        await LoadItemsAsync();

        await Shell.Current.DisplayAlert(
            "Purchase Saved",
            "Your grocery trip was saved and purchased items were moved into the pantry.",
            "OK");
    }

    [RelayCommand]
    private async Task DeleteRecordAsync(SpendingRecord record)
    {
        if (record == null) return;

        await _spendingRepo.DeleteAsync(record.Id);
        await LoadItemsAsync();
    }
}