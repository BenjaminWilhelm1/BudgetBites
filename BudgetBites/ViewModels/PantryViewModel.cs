using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BudgetBites.Models;
using BudgetBites.Services;
using BudgetBites.Pages;

namespace BudgetBites.ViewModels;

public partial class PantryViewModel : ObservableObject
{
    private readonly PantryRepository _repository;

    public ObservableCollection<PantryItem> Items { get; } = new();

    [ObservableProperty] private bool isBusy;

    public PantryViewModel(PantryRepository repository)
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
            foreach (var item in items.OrderBy(i => i.Name))
                Items.Add(item);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task IncrementQuantityAsync(PantryItem item)
    {
        if (item is null) return;
        await _repository.UpdateQuantityAsync(item.Id, item.Quantity + 1);
        await LoadItemsAsync();
    }

    [RelayCommand]
    private async Task DecrementQuantityAsync(PantryItem item)
    {
        if (item is null) return;
        if (item.Quantity <= 1)
            await _repository.DeleteAsync(item.Id);
        else
            await _repository.UpdateQuantityAsync(item.Id, item.Quantity - 1);
        await LoadItemsAsync();
    }

    [RelayCommand]
    private async Task DeleteItemAsync(PantryItem item)
    {
        if (item is null) return;
        await _repository.DeleteAsync(item.Id);
        await LoadItemsAsync();
    }

    [RelayCommand]
    private async Task GoToAddItemAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddPantryItemPage));
    }
}