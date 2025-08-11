# WPF Generic DevExpress Grid (EF Core + SQLite)

A simple WPF application demonstrating a **reusable Generic GridControl** using **DevExpress** for WPF, backed by **Entity Framework Core** and **SQLite**.  
The grid can display any entity type by simply setting its `EntityType` property.  
It auto-generates columns and loads data directly from the database.

## Features
- **Generic GridControl** – works with any entity type
- **DevExpress WPF** – rich data grid UI
- **EF Core + SQLite** – lightweight, easy to use
- **Auto-generate columns** from entity properties
- **Self-contained data loading** – no ViewModel code required for binding
- **Optional AutoLoad** – loads on control initialization

## Requirements
- [.NET 6.0+ or .NET Framework with EF Core](https://dotnet.microsoft.com/)
- [DevExpress WPF Components](https://www.devexpress.com/products/net/controls/wpf/)
- SQLite

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/<your-username>/WpfGenericDevExpressGrid-EFCore.git
cd WpfGenericDevExpressGrid-EFCore
