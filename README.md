# Online Shopping System

## Overview

Online Shopping System is a web-based e-commerce application developed using ASP.NET Core MVC and SQL Server. The application allows users to browse products, view product details, add items to a shopping cart, and place orders. It also provides an admin panel for managing products and monitoring system data.

## Features

### Customer Features

* View all products
* Product details page
* Search products
* Add products to cart
* Update cart quantity
* Remove products from cart
* Checkout functionality

### Admin Features

* Admin Dashboard
* Add Product
* Edit Product
* Delete Product
* Product Management
* Product Count Dashboard
* Order Management

## Technologies Used

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server
* Bootstrap 5
* HTML
* CSS
* JavaScript

## Database

The project uses SQL Server with Entity Framework Core Code First Migrations.

### Main Tables

* Products
* Orders
* Carts

## Project Structure

```text
Controllers/
Models/
Views/
wwwroot/
 └── uploads/
Data/
```

## Installation

1. Clone the repository

```bash
git clone <repository-url>
```

2. Open the project in Visual Studio

3. Update connection string in appsettings.json

4. Run migrations

```powershell
Add-Migration InitialCreate
Update-Database
```

5. Run the project


## Learning Outcomes

* ASP.NET Core MVC Architecture
* Entity Framework Core
* SQL Server Integration
* CRUD Operations
* Session Management
* File Upload Handling
* E-Commerce Application Development

## Author

Dhanish Kumar Maurya

## License

This project is created for educational and portfolio purposes.
