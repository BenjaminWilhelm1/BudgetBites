using BudgetBites.ViewModels;

namespace BudgetBites.Pages;

public partial class AddGroceryItemPage : ContentPage
{
    public AddGroceryItemPage(AddGroceryItemViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}