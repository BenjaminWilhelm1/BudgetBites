using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BudgetBites.Models;
using BudgetBites.Services;

namespace BudgetBites.ViewModels;

public partial class AddPantryItemViewModel : ObservableObject
{
    private readonly PantryRepository _repository;

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private int quantity = 1;
    [ObservableProperty] private string category = "General";
    [ObservableProperty] private DateTime expirationDate = DateTime.Now.AddMonths(1);
    [ObservableProperty] private bool hasExpiration;

    public List<string> Categories { get; } = new()
    {
        "General", "Grains", "Canned", "Baking", "Spices", "Condiments", "Snacks", "Beverages", "Frozen"
    };

    public AddPantryItemViewModel(PantryRepository repository)
    {
        _repository = repository;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        var item = new PantryItem
        {
            Name = Name.Trim(),
            Quantity = Quantity,
            Category = Category,
            ExpirationDate = HasExpiration ? ExpirationDate : null
        };
        await _repository.AddAsync(item);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}