# Developer Setup Guide

## 1. Database Configuration

We use a template file to keep our local database credentials out of version control to maintain security.

1. Locate the `appsettings.example.json` file in the root of the repository (outside the `src` folder).
2. Copy this file into your `UI` project folder and rename the copy to exactly `appsettings.json`.
3. Open your new `appsettings.json` file and update the `DefaultConnection` string with your SQL Server instance name.
    - Example: `Server=.;Database=PharmacyDB;Trusted_Connection=True;TrustServerCertificate=True;`
4. In Visual Studio Solution Explorer, right-click your new `appsettings.json` file, select Properties, and set "Copy to Output Directory" to "Copy if newer".

## 2. Package Installation

You must ensure all required NuGet packages are installed and restored before running the application. 

Specifically, you must install the `Microsoft.Extensions.Configuration` and `Microsoft.Extensions.Configuration.Json` packages in the UI project so it can read the `appsettings.json` file. Additionally, ensure all other solution dependencies (such as Entity Framework Core) are fully restored.