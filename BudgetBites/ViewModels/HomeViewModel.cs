using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BudgetBites.Services;

namespace BudgetBites.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly GroceryRepository _groceryRepo;
    private readonly SpendingRepository _spendingRepo;
    private readonly PantryRepository _pantryRepo;

    [ObservableProperty] private int groceryItemCount;
    [ObservableProperty] private decimal estimatedGroceryTotal;
    [ObservableProperty] private decimal spentThisMonth;
    [ObservableProperty] private int pantryItemCount;

    public HomeViewModel(GroceryRepository groceryRepo, SpendingRepository spendingRepo, PantryRepository pantryRepo)
    {
        _groceryRepo = groceryRepo;
        _spendingRepo = spendingRepo;
        _pantryRepo = pantryRepo;
    }

    [RelayCommand]
    public async Task LoadDashboardAsync()
    {
        var groceries = await _groceryRepo.GetAllAsync();
        GroceryItemCount = groceries.Count(g => !g.IsPurchased);
        EstimatedGroceryTotal = groceries.Where(g => !g.IsPurchased).Sum(g => g.TotalPrice);

        var pantry = await _pantryRepo.GetAllAsync();
        PantryItemCount = pantry.Count;

        var spending = await _spendingRepo.GetAllAsync();
        var now = DateTime.Now;
        SpentThisMonth = spending
            .Where(r => r.Date.Year == now.Year && r.Date.Month == now.Month)
            .Sum(r => r.Amount);
    }

    [RelayCommand]
    private async Task GoToGroceryAsync() => await Shell.Current.GoToAsync("//grocery");

    [RelayCommand]
    private async Task GoToPantryAsync() => await Shell.Current.GoToAsync("//pantry");

    [RelayCommand]
    private async Task GoToBudgetAsync() => await Shell.Current.GoToAsync("//budget");
}