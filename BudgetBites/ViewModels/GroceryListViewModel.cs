using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BudgetBites.Models;
using BudgetBites.Services;
using BudgetBites.Pages;

namespace BudgetBites.ViewModels;

public partial class GroceryListViewModel : ObservableObject
{
    private readonly GroceryRepository _repository;
    private readonly PantryRepository _pantryRepository;
    private readonly SpendingRepository _spendingRepository;

    public ObservableCollection<GroceryItem> Items { get; } = new();

    [ObservableProperty] private decimal estimatedTotal;
    [ObservableProperty] private decimal purchasedTotal;
    [ObservableProperty] private int purchasedItemCount;
    [ObservableProperty] private bool isBusy;

    public GroceryListViewModel(
        GroceryRepository repository,
        PantryRepository pantryRepository,
        SpendingRepository spendingRepository)
    {
        _repository = repository;
        _pantryRepository = pantryRepository;
        _spendingRepository = spendingRepository;
    }

    [RelayCommand]
    public async Task LoadItemsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var items = await _repository.GetAllAsync();

            Items.Clear();
            foreach (var item in items.OrderByDescending(i => i.DateAdded))
                Items.Add(item);

            CalculateTotals();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void CalculateTotals()
    {
        EstimatedTotal = Items.Where(i => !i.IsPurchased).Sum(i => i.TotalPrice);
        PurchasedTotal = Items.Where(i => i.IsPurchased).Sum(i => i.TotalPrice);
        PurchasedItemCount = Items.Count(i => i.IsPurchased);
    }

    [RelayCommand]
    private async Task TogglePurchasedAsync(GroceryItem item)
    {
        if (item is null) return;

        await _repository.TogglePurchasedAsync(item.Id);
        await LoadItemsAsync();
    }

    [RelayCommand]
    private async Task CheckoutPurchasedItemsAsync()
    {
        var purchasedItems = Items.Where(i => i.IsPurchased).ToList();

        if (!purchasedItems.Any())
        {
            await Shell.Current.DisplayAlert(
                "No Purchased Items",
                "Check off at least one grocery item before checking out.",
                "OK");
            return;
        }

        bool confirm = await Shell.Current.DisplayAlert(
            "Checkout Items",
            $"Move {purchasedItems.Count} purchased item(s) to pantry and add ${PurchasedTotal:F2} to spending?",
            "Yes",
            "Cancel");

        if (!confirm) return;

        foreach (var item in purchasedItems)
        {
            await _pantryRepository.AddOrUpdateAsync(item.Name, item.Quantity, item.Category);
            await _repository.DeleteAsync(item.Id);
        }

        await _spendingRepository.AddAsync(new SpendingRecord
        {
            Amount = PurchasedTotal,
            Date = DateTime.Now,
            Note = "Grocery checkout"
        });

        await LoadItemsAsync();

        await Shell.Current.DisplayAlert(
            "Checkout Complete",
            "Purchased items were moved to your pantry and your spending total was updated.",
            "OK");
    }

    [RelayCommand]
    private async Task DeleteItemAsync(GroceryItem item)
    {
        if (item is null) return;

        await _repository.DeleteAsync(item.Id);
        await LoadItemsAsync();
    }

    [RelayCommand]
    private async Task GoToAddItemAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddGroceryItemPage));
    }

    [RelayCommand]
    private async Task EditItemAsync(GroceryItem item)
    {
        if (item is null) return;

        await Shell.Current.GoToAsync($"{nameof(AddGroceryItemPage)}?itemId={item.Id}");
    }
}