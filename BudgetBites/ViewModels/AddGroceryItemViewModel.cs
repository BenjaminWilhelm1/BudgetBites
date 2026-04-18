using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BudgetBites.Models;
using BudgetBites.Services;

namespace BudgetBites.ViewModels;

[QueryProperty(nameof(ItemId), "itemId")]
public partial class AddGroceryItemViewModel : ObservableObject
{
    private readonly GroceryRepository _repository;

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private int quantity = 1;
    [ObservableProperty] private decimal unitPrice;
    [ObservableProperty] private string category = "General";
    [ObservableProperty] private string itemId = string.Empty;
    [ObservableProperty] private string pageTitle = "Add Grocery Item";
    [ObservableProperty] private bool isEditing;

    public List<string> Categories { get; } = new()
    {
        "General", "Produce", "Dairy", "Meat", "Frozen", "Bakery", "Beverages", "Snacks", "Household"
    };

    public AddGroceryItemViewModel(GroceryRepository repository)
    {
        _repository = repository;
    }

    partial void OnItemIdChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _ = LoadItemAsync(value);
        }
    }

    private async Task LoadItemAsync(string id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item != null)
        {
            Name = item.Name;
            Quantity = item.Quantity;
            UnitPrice = item.UnitPrice;
            Category = item.Category;
            IsEditing = true;
            PageTitle = "Edit Grocery Item";
        }
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        if (IsEditing)
        {
            var item = await _repository.GetByIdAsync(ItemId);
            if (item != null)
            {
                item.Name = Name.Trim();
                item.Quantity = Quantity;
                item.UnitPrice = UnitPrice;
                item.Category = Category;
                await _repository.UpdateAsync(item);
            }
        }
        else
        {
            var item = new GroceryItem
            {
                Name = Name.Trim(),
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                Category = Category
            };
            await _repository.AddAsync(item);
        }

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}