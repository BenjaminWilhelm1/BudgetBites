using BudgetBites.Pages;

namespace BudgetBites;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddGroceryItemPage), typeof(AddGroceryItemPage));
        Routing.RegisterRoute(nameof(AddPantryItemPage), typeof(AddPantryItemPage));
    }
}