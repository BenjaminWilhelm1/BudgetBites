using BudgetBites.ViewModels;

namespace BudgetBites.Pages;

public partial class AddPantryItemPage : ContentPage
{
    public AddPantryItemPage(AddPantryItemViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}