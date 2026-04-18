# BudgetBites 

> A .NET MAUI mobile app that helps college students and young adults take control of grocery spending — without the spreadsheets.

BudgetBites keeps your grocery list, pantry inventory, and spending history all in one place, stored entirely on-device so no account or internet connection is required.

---

## Features

### Grocery List
- Add items with name, quantity, category, and estimated price
- Toggle items between **needed** and **purchased** as you shop
- Auto-calculated estimated total updates in real time

### Pantry Tracker
- Track what you already have at home
- Quick increment/decrement controls to adjust quantities on the fly

### Budget & Spending
- See your estimated grocery total before you head to the store
- Submit actual receipt amounts after checkout
- Browse past spending with a running monthly total

---

## Tech Stack

| Category | Technology |
|---|---|
| Framework | .NET MAUI (.NET 8) |
| Architecture | MVVM |
| MVVM Toolkit | CommunityToolkit.Mvvm |
| Local Storage | JSON via System.Text.Json |
| Design Pattern | Repository pattern |
| Source Control | GitHub |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (v17.8+) with the **.NET MAUI workload** installed
- Android/iOS emulator or a physical device

### Running the App

```bash
# Clone the repo
git clone https://github.com/your-username/BudgetBites.git
cd BudgetBites

# Restore dependencies
dotnet restore

# Run on Android emulator
dotnet build -t:Run -f net8.0-android

# Run on iOS simulator (macOS only)
dotnet build -t:Run -f net8.0-ios
```

Or open `BudgetBites.sln` in Visual Studio and press **F5** with your target device selected.

---

## Project Structure

```
BudgetBites/
├── Models/
│   ├── GroceryItem.cs          # Item name, quantity, category, price
│   ├── PantryItem.cs           # Home inventory item
│   └── SpendingRecord.cs       # Individual checkout record
│
├── Services/
│   ├── GroceryRepository.cs    # Read/write grocery list JSON
│   ├── PantryRepository.cs     # Read/write pantry JSON
│   └── SpendingRepository.cs   # Read/write spending history JSON
│
├── ViewModels/
│   ├── GroceryListViewModel.cs
│   ├── PantryViewModel.cs
│   └── BudgetViewModel.cs
│
├── Views/
│   ├── HomePage.xaml
│   ├── GroceryListPage.xaml
│   ├── AddGroceryItemPage.xaml
│   ├── PantryPage.xaml
│   ├── AddPantryItemPage.xaml
│   └── BudgetPage.xaml
│
├── Resources/
├── App.xaml
├── AppShell.xaml
└── MauiProgram.cs
```

---

## Architecture

BudgetBites follows a clean MVVM structure with a repository layer for data access. Each feature is a self-contained vertical slice that a single team member owns end to end.

```
View (XAML)
  └── binds to ViewModel (CommunityToolkit.Mvvm)
        └── calls Repository / Service
              └── reads/writes JSON on local device storage
```

- **Models** — plain C# classes representing data (items, records)
- **Services** — repositories that serialize/deserialize JSON to the app's local storage directory
- **ViewModels** — business logic, commands, and observable properties via `CommunityToolkit.Mvvm`
- **Views** — XAML pages bound to their respective ViewModels; no logic lives here

Data never leaves the device. All JSON files are written to the app's sandboxed storage folder via `FileSystem.AppDataDirectory`.

---

## Data Storage

Each feature persists its data to a separate JSON file:

| File | Contents |
|---|---|
| `grocery_items.json` | Current grocery list |
| `pantry_items.json` | Pantry inventory |
| `spending_records.json` | Checkout history |

Files are created automatically on first launch and survive app restarts.

---

## Team

| Feature | Owner |
|---|---|
| Grocery List | — |
| Pantry Tracker | — |
| Budget & Spending | — |

---

## Contributing

1. Branch off `main` using the convention `feature/your-feature-name`
2. Keep changes scoped to your vertical slice (Models → Services → ViewModel → View)
3. Open a pull request and request a review before merging

---
