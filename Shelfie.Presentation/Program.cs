using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Services;
using Shelfie.Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IGebruikerRepository>(_ => new GebruikerRepository(connectionString));
builder.Services.AddScoped<IBoekRepository>(_ => new BoekRepository(connectionString));

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IBoekenkastService, BoekenkastService>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
    {
        opts.LoginPath = "/Account/Login";
    });

builder.Services.AddAuthorization();

// views
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();