using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingSystem.Demo;
using OnlineShoppingSystem.Models;

var builder = WebApplication.CreateBuilder(args);
var demoMode = builder.Configuration.GetValue<bool>("DEMO_MODE");
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    if (demoMode) options.UseSqlite(DemoDatabase.ConnectionString(builder.Configuration));
    else options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<ForwardedHeadersOptions>(options => {
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    if (demoMode) { options.KnownNetworks.Clear(); options.KnownProxies.Clear(); }
});
var app = builder.Build();
app.UseForwardedHeaders();
if (demoMode) {
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    if (!db.Products.Any()) {
        db.Products.AddRange(
            new Product { Name="Everyday Laptop", Description="Fictional 14-inch work laptop for browsing the sample catalogue.", Price=42990, Category="Electronics", ImageUrl="/images/laptop.svg" },
            new Product { Name="Wireless Headphones", Description="Sample over-ear headphones with comfortable cushions.", Price=2499, Category="Electronics", ImageUrl="/images/headphones.svg" },
            new Product { Name="Canvas Backpack", Description="Fictional everyday bag with a laptop compartment.", Price=1299, Category="Accessories", ImageUrl="/images/backpack.svg" },
            new Product { Name="Classic Watch", Description="Sample watch with a simple everyday dial.", Price=1899, Category="Accessories", ImageUrl="/images/watch.svg" },
            new Product { Name="Cotton T-Shirt", Description="Fictional casual cotton T-shirt in blue.", Price=599, Category="Fashion", ImageUrl="/images/shirt.svg" },
            new Product { Name="Desk Keyboard", Description="Sample compact keyboard for a tidy desk.", Price=1599, Category="Electronics", ImageUrl="/images/keyboard.svg" }
        );
        db.SaveChanges();
    }
}
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
if (!demoMode) app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapGet("/health", () => Results.Ok(new { status = "ok", demo = demoMode }));
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
