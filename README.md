# 🛒 E-Commerce Web API (.NET 10)

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-10.0-3C8A3E?logo=nuget)
![SQLite](https://img.shields.io/badge/SQLite-003B57?logo=sqlite)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?logo=jsonwebtokens)

A robust, enterprise-grade E-Commerce backend API built entirely from scratch using the bleeding-edge **.NET 10 Preview**. This project demonstrates clean architecture principles, secure authentication, and complex database relationships.

## ✨ Features

* **🔐 Secure Authentication:** JWT (JSON Web Token) generation with industry-standard BCrypt password hashing.
* **👮 Role-Based Access Control (RBAC):** Strict separation between `Admin` (can create/manage products) and `Customer` (can browse and buy) roles.
* **🛍️ Smart Shopping Cart:** Persistent database cart that seamlessly merges duplicate items, tracks live inventory, and calculates running totals.
* **📦 Checkout & Order Management:** Automated checkout flow that validates warehouse stock, converts carts into permanent order receipts, and dynamically deducts inventory using optimized, direct-SQL commands.
* **🗄️ Relational Database:** Fully configured SQLite database using Entity Framework Core Code-First migrations.

## 🏗️ Architecture Layering

This project is separated into distinct layers to ensure maintainability and scalability:
1.  **API Layer (`ECommerce.API`):** Controllers, Swagger setup, and Dependency Injection.
2.  **Application Layer (`ECommerce.Application`):** Business logic (Services), Data Transfer Objects (DTOs), and Repository Interfaces.
3.  **Domain Layer (`ECommerce.Domain`):** Core entity models (`User`, `Product`, `Cart`, `Order`, etc.).
4.  **Infrastructure Layer (`ECommerce.Infrastructure`):** Entity Framework `AppDbContext` and concrete Repository implementations.

## 🚀 Getting Started

Follow these steps to get the API running locally on your machine.

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* Visual Studio Code or Visual Studio 2022+

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/Anan-kh666/E_Commerce_API.git](https://github.com/Anan-kh666/E_Commerce_API.git)
   cd E_Commerce_API
2.
Restore dependencies:
Bash
dotnet restore
Apply Database Migrations:
This will generate the ecommerce.db SQLite file and build all the tables.
Bash
dotnet ef database update --project ECommerce.Infrastructure --startup-project ECommerce.API
Run the API:
Bash
dotnet run --project ECommerce.API
Open Swagger UI:
Navigate to http://localhost:5116/swagger in your browser to interact with the API endpoints.
🧪 How to Test the API
To fully test the e-commerce lifecycle, follow this flow in Swagger:
Register a User: Use POST /api/Auth/register to create an account. (Note: Users default to the "Customer" role).
Log In: Use POST /api/Auth/login to get your JWT Token.
Authorize: Click the green Authorize button at the top of Swagger. Type Bearer  followed by a space, then paste your token. Click Authorize.
Create a Product: To do this, you must be an Admin! Open your ecommerce.db file in a SQLite Viewer, find your user in the Users table, and change your Role from Customer to Admin. Re-login to get a fresh token, and use POST /api/Products.
Shop: Use POST /api/Cart/items to add the product to your cart.
Checkout: Hit POST /api/Orders/checkout to convert your cart into a final receipt and deduct the stock!
🔮 Future Enhancements
[ ] Integrate Stripe API for real credit card processing.
[ ] Add pagination and search filtering to the Products endpoint.
[ ] Containerize the application using Docker.
[ ] Build a frontend UI (React/Blazor) to consume the API.

***