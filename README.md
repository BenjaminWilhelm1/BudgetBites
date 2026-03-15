# BudgetBites

A .NET MAUI mobile app that helps college students and young adults manage grocery spending. Track grocery lists, manage your pantry inventory, and keep an eye on spending history, all stored locally on-device with JSON.

## Features

- **Grocery List**  Add items with name, quantity, category, and estimated price. Toggle items between needed and purchased. Auto-calculated estimated total.
- **Pantry Tracker**  Keep track of what you have at home with quick increment/decrement quantity controls.
- **Budget & Spending** View your estimated grocery total, submit checkout receipt amounts, and browse past spending with a running monthly total.

## Tech Stack

| Category | Technology |
|---|---|
| Framework | .NET MAUI (.NET 8) |
| Architecture | MVVM |
| MVVM Toolkit | CommunityToolkit.Mvvm |
| Local Storage | JSON via System.Text.Json |
| Design Pattern | Repository pattern |
| Source Control | GitHub |

## Project Structure

```
BudgetBites/
├── Models/
│   ├── GroceryItem.cs
│   ├── PantryItem.cs
│   └── SpendingRecord.cs
├── Services/
│   ├── GroceryRepository.cs
│   ├── PantryRepository.cs
│   └── SpendingRepository.cs
├── ViewModels/
│   ├── GroceryListViewModel.cs
│   ├── PantryViewModel.cs
│   └── BudgetViewModel.cs
├── Views/
│   ├── HomePage.xaml
│   ├── GroceryListPage.xaml
│   ├── AddGroceryItemPage.xaml
│   ├── PantryPage.xaml
│   ├── AddPantryItemPage.xaml
│   └── BudgetPage.xaml
├── Resources/
├── App.xaml
├── AppShell.xaml
└── MauiProgram.cs
```


## Architecture

The app follows MVVM with a clean separation of concerns:

- **Models** define the data structures (items, records)
- **Services** handle reading/writing JSON files to local app storage
- **ViewModels** contain the business logic and commands using CommunityToolkit.Mvvm
- **Views** are the XAML UI pages bound to their respective ViewModels

Each feature (Grocery, Pantry, Budget) is a self-contained vertical slice that one team member owns end to end.
