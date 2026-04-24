# BudgetBites

BudgetBites is a cross-platform mobile application built using .NET MAUI and C#. It helps users manage grocery shopping, track pantry items, and monitor spending — all in one simple app.

---

## Project Overview

Many people, especially college students, overspend on groceries because they:
- Don’t know what they already have
- Don’t estimate costs before checkout
- Don’t track spending consistently

BudgetBites solves this by combining:
- Grocery list management
- Pantry tracking
- Budget and spending tracking

---

## Features

### Grocery List
- Add, edit, and delete grocery items  
- Set quantity and estimated price  
- Mark items as **Purchased**  
- View a running estimated total  

### Pantry Tracker
- Automatically stores purchased items  
- Merge duplicate items instead of creating duplicates  
- Adjust quantities with simple controls  

### Budget & Spending
- Log grocery checkout totals  
- Track total spending by month  
- View simple spending history  

### Checkout Flow (Core Feature)
- Mark items as purchased in the Grocery List  
- Tap **“Items Purchased”**  
- Items are:
  - Moved to Pantry  
  - Removed from Grocery List  
  - Added to Spending records  

---

## Technology Stack

- Framework: .NET MAUI  
- Language: C#  
- UI: XAML  
- Architecture: MVVM (Model-View-ViewModel)  
- Data Storage: Local JSON (System.Text.Json)  
- Design Pattern: Repository Pattern  
- Version Control: GitHub  

---

## Project Structure

BudgetBites/

Models/
- GroceryItem.cs  
- PantryItem.cs  
- SpendingRecord.cs  

ViewModels/
- GroceryListViewModel.cs  
- PantryViewModel.cs  
- BudgetViewModel.cs  

Services/
- GroceryRepository.cs  
- PantryRepository.cs  
- SpendingRepository.cs  

Pages/
- GroceryPage.xaml  
- PantryPage.xaml  
- BudgetPage.xaml  

AppShell.xaml  

---

## How to Run the App

1. Clone the repository:
git clone https://github.com/BenjaminWilhelm1/BudgetBites

2. Open the project in Visual Studio 2022  

3. Select a target device:
- Windows Machine  
- Android Emulator  

4. Click Run  

---

## Team Members & Contributions

### Macy Debord 
- Project planning and documentation  
- Wireframes and UI/UX design  
- Navigation flow and layout improvements  
- Final UI polish and feature integration  
- Implemented final updates including checkout flow improvements  

### Benjamin Wilhelm
- Implemented core application functionality  
- Built Grocery List feature (add/edit/delete/toggle purchased)  
- Implemented Pantry and Budget base functionality  
- Developed initial data structure and application logic  

---

## User Guide

1. Open the app and navigate to the Grocery List  
2. Add items with quantity and price  
3. Mark items as Purchased  
4. Tap “Items Purchased”  
5. Items will:
   - Move to Pantry  
   - Update spending totals  
6. View updates in Pantry and Budget pages  

---

## Challenges & Solutions

Challenge: Learning .NET MAUI  
Solution: Built the app incrementally and followed MVVM structure  

Challenge: Data persistence  
Solution: Used JSON-based local storage with repositories  

Challenge: Feature integration  
Solution: Connected Grocery → Pantry → Budget through checkout flow  

---

## Future Improvements

- Low stock notifications  
- Spending charts and analytics  
- Category filtering  
- Cloud sync  

---

## Conclusion

BudgetBites provides a simple and effective way to manage grocery shopping and spending. By combining multiple features into one application, it improves organization, reduces overspending, and creates a better user experience.

---
