# Live Online Shopping demonstration

**Open the demo:** https://dhanish-shopping-demo.onrender.com/

No account is required. Browse the six fictional catalogue products, search/open a product, add it to your visitor-session cart, change quantity, remove an item, and inspect the checkout preview. The **Manage Products** area supports educational product create/edit/delete operations; the dashboard shows the product count.

## What checkout does

Checkout displays a review of the cart. It does **not** create orders, process payments, send messages, or provide order management. This is explicitly labelled in the interface. The existing management area is public for the portfolio demonstration; it is not a production authorization system.

## Free hosting and sample data

- Render **Free** Docker web service, with `DEMO_MODE=true`.
- Optional SQLite database with six fictional products and local sample illustrations. Normal local SQL Server configuration remains available with demo mode disabled.
- Catalogue edits are shared between visitors. The shopping cart is scoped to the visitor's session.
- Free-host changes reset on service restart, redeploy or idle spin-down. Use fictional product data only; a sleeping instance may take time to start on the first request.

## Verified at the public HTTPS URL on 23 November 2026

**18 of 18 focused HTTP checks passed:** health/demo status, homepage/disclosure, six seeded products, a local illustration, search, details, empty session cart, add item, update quantity, invalid-quantity rejection, honest checkout preview, remove item, product create/edit/delete, GET deletion rejection, missing-antiforgery rejection, and no creation of the rejected product.

The temporary product was deleted and the test session's cart was emptied. These are focused workflow checks, not load testing or production security certification.

## Run the same demo locally

With the .NET 8 SDK installed, run from the repository root in PowerShell:

```powershell
$env:DEMO_MODE = "true"
dotnet run --project ./OnlineShoppingSystem/OnlineShoppingSystem.csproj --no-launch-profile --urls http://127.0.0.1:18223
```

Open http://127.0.0.1:18223/. The demo schema and fictional catalogue are initialized automatically. `DEMO_DB_PATH` can select another writable SQLite file; the default is the operating system's temporary directory.

For the normal SQL Server setup, leave `DEMO_MODE` unset/false, configure `ConnectionStrings:DefaultConnection`, and apply the included SQL Server migrations. Demo mode initializes its own SQLite schema instead of applying the SQL Server-specific migrations.
