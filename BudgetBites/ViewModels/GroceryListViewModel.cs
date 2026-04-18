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

    public ObservableCollection<GroceryItem> Items { get; } = new();

    [ObservableProperty] private decimal estimatedTotal;
    [ObservableProperty] private bool isBusy;

    public GroceryListViewModel(GroceryRepository repository)
    {
        _repository = repository;
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
            CalculateTotal();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void CalculateTotal()
    {
        EstimatedTotal = Items.Where(i => !i.IsPurchased).Sum(i => i.TotalPrice);
    }

    [RelayCommand]
    private async Task TogglePurchasedAsync(GroceryItem item)
    {
        if (item is null) return;
        await _repository.TogglePurchasedAsync(item.Id);
        await LoadItemsAsync();
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