# ShopInventory - Inventory Management App

A cross-platform inventory management application built with .NET MAUI 9.0, designed to help small businesses track their inventory, purchases, and sales with an intuitive dashboard.

## 🚀 Features

### Dashboard Overview

- **Monthly Statistics**: Track current month's inventory activities
- **Key Metrics Display**:
  - Total items purchased this month
  - Total items sold this month
  - Total amount spent on purchases (BDT)
  - Total revenue from sales (BDT)

### Inventory Management

- **Purchased Items Tracking**: Record and manage inventory purchases
- **Sold Items Tracking**: Track sales and revenue
- **Add/Edit Functionality**: Full CRUD operations for both purchased and sold items

### Data Persistence

- **SQLite Database**: Local data storage using SQLite
- **Asynchronous Operations**: Non-blocking database operations for smooth UI experience

## 🛠️ Technical Stack

### Framework & Platform

- **.NET 9.0**: Latest .NET framework
- **.NET MAUI**: Cross-platform UI framework
- **Target Platforms**:
  - Android (API 31+)

### Architecture

- **MVVM Pattern**: Model-View-ViewModel architecture

## Project Structure

### 📁 Root Directory

- **ShopInventory.csproj** - Main project file targeting .NET 9.0 with support for Android, iOS, MacCatalyst, and Windows platforms

### 📁 Platforms

Platform-specific entry points and configurations:

#### Android

- **MainActivity.cs** - Main activity for Android platform
- **MainApplication.cs** - Application entry point for Android

### 📁 Views

XAML pages for the user interface:

- **DashboardPage.xaml** - Main dashboard displaying monthly statistics
- **MainPage.xaml** - Application main page
- **PurchasedItemsPage.xaml** - List view for purchased items
- **SoldItemsPage.xaml** - List view for sold items
- **AddEditPurchasedItemPage.xaml** - Form for adding/editing purchased items
- **AddEditSoldItemPage.xaml** - Form for adding/editing sold items

### 📁 ViewModels

MVVM pattern view models (referenced in MauiProgram.cs):

- **MainPageViewModel**
- **DashboardViewModel**
- **PurchasedItemsViewModel**
- **SoldItemsViewModel**
- **AddEditPurchasedItemViewModel**
- **AddEditSoldItemViewModel**

### 📁 Services

Business logic and data services:

- **DatabaseService** - SQLite database operations

### 📁 Resources

Static resources for the application:

- **AppIcon/** - Application icon files
- **Splash/** - Splash screen assets
- **Images/** - Image resources
- **Fonts/** - Custom fonts
- **Raw/** - Raw assets
