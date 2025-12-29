using SuperShop.Components;
using SuperShop.Models;
using SuperShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

//builder.Services.AddScoped<ProductService>(sp =>
//{
//    string conn = "Server=.;Database=Multi_Shop;Trusted_Connection=True;"; // আপনার connection string বা অন্য string
//    return new ProductService(conn);
//});


//builder.Services.AddScoped<SuperAdminService>();
//builder.Services.AddScoped<ProductService>();
//builder.Services.AddScoped<PurchaseService>();
//builder.Services.AddScoped<SalesService>();
//builder.Services.AddScoped<StockService>();
//builder.Services.AddScoped<StaffService>();
//builder.Services.AddScoped<POSService>();
//builder.Services.AddScoped<AccountingService>();
//builder.Services.AddScoped<UserService>();

builder.Services.AddSingleton(new SuperAdminService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new SubscriptionService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new ProductService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new PurchaseService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new SalesService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new StockService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new StaffService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new POSService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(new AccountingService(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddSingleton(new UserService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<UserService>();





//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
