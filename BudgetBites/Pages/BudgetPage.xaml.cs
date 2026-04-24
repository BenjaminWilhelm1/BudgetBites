using BudgetBites.ViewModels;

namespace BudgetBites.Pages;

public partial class BudgetPage : ContentPage
{
    private readonly BudgetViewModel _viewModel;

    public BudgetPage(BudgetViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is BudgetViewModel vm)
            await vm.LoadItemsAsync();
    }
}