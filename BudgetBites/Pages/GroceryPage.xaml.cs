using BudgetBites.ViewModels;

namespace BudgetBites.Pages;

public partial class GroceryPage : ContentPage
{
    private readonly GroceryListViewModel _viewModel;

    public GroceryPage(GroceryListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadItemsAsync();
    }
}