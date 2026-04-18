using BudgetBites.ViewModels;

namespace BudgetBites.Pages;

public partial class PantryPage : ContentPage
{
    private readonly PantryViewModel _viewModel;

    public PantryPage(PantryViewModel viewModel)
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