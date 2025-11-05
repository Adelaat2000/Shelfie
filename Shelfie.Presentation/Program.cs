using Microsoft.AspNetCore.Authentication.Cookies;
using Shelfie.Domain.Interfaces;
using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Services;
using Shelfie.Dal;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Repos
builder.Services.AddScoped<IGebruikerRepository>(_ => new GebruikerRepository(connectionString));
builder.Services.AddScoped<IBoekRepository>(_ => new BoekRepository(connectionString));

// Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBoekenkastService, BoekenkastService>();
builder.Services.AddScoped<ISearchService, SearchService>();

// AUTHENTICATION
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
    {
        opts.LoginPath = "/Account/Login";
    });

// AUTHORIZATION
builder.Services.AddAuthorization();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// MUST be before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

// Default route → Register page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
