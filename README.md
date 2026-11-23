# Online Shopping System

A runnable educational e-commerce application built with **ASP.NET Core 8 MVC, C#, Entity Framework Core, Bootstrap and session-based shopping carts**.

The normal local configuration uses **SQL Server**. The optional public-demo configuration uses a separate **SQLite** database with six fictional catalogue products, so the app can run on a free Linux Docker host without a paid SQL Server service.

## Try the demo

- Browse and search the catalogue, open product details, and add a product to the cart.
- Change its quantity, review totals, remove it, or open the checkout preview.
- Use **Manage Products** to create, edit and delete a fictional product; use the dashboard to inspect the count.
- No login is required in this educational demo. The existing product-management area is open for demonstration and is not a production authorization system.

**Checkout is a preview. This implementation does not create orders, charge a payment method, send messages, or provide order management.**

Public demo records are fictional and shared between visitors. Free-host filesystem changes reset when the service restarts, redeploys or spins down. Do not enter personal or payment information.

## Run locally with the prepared demo database

Install the .NET 8 SDK. From the repository root in PowerShell:

```powershell
$env:DEMO_MODE = "true"
dotnet run --project ./OnlineShoppingSystem/OnlineShoppingSystem.csproj --no-launch-profile --urls http://127.0.0.1:18223
```

Open **http://127.0.0.1:18223/**. On first demo startup, SQLite creates the schema and six sample products automatically. Its database defaults to the operating system's temporary directory; `DEMO_DB_PATH` can set another writable path. Demo mode uses `EnsureCreated` instead of the SQL Server-specific migration history.

## Run the original SQL Server configuration

Leave `DEMO_MODE` unset or set it to `false`. Configure `ConnectionStrings:DefaultConnection` in local configuration or the `ConnectionStrings__DefaultConnection` environment variable. Apply the included Entity Framework migrations to an authorized SQL Server database, then run the project normally. Do not commit real database passwords.

## Free Docker / Render demonstration

The root `Dockerfile` publishes the .NET 8 application, runs it as the non-root `app` user, enables `DEMO_MODE=true` and listens on **port 10000**. `/health` returns the application's health response. `render.yaml` specifies a **Free** Docker web service; no external database, paid disk or provider credential is required for the fictional demo.

If deploying through the Render dashboard, choose this repository's **master** branch, **Docker**, **Free**, and health-check path **/health**. Use the assigned HTTPS URL. A sleeping free instance may take time to wake on the first visit. This is a resettable portfolio demonstration rather than durable production hosting.

## Verified behavior

Local Release build succeeded. HTTP checks exercised catalogue seeding/search, session cart add/update/remove, the honest checkout preview, and product create/edit/delete with antiforgery validation. Temporary test records were removed. A cloud deployment must also be checked at its actual URL before being described as live.

## Source layout

```text
OnlineShoppingSystem/Controllers/  Product, cart, dashboard and management actions
OnlineShoppingSystem/Models/       EF entities and session cart records
OnlineShoppingSystem/Views/        MVC pages
OnlineShoppingSystem/Demo/         Optional demo database connection helper
OnlineShoppingSystem/wwwroot/      Local UI assets and fictional catalogue illustrations
Dockerfile                        Free-host demo image
render.yaml                       Free Render service configuration
```

Prepared by **Dhanish Kumar Maurya** for educational and portfolio use.
