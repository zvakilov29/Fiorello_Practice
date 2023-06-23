using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZV_Fiorello.DAL;
using ZV_Fiorello.Extensions;
using ZV_Fiorello.Models;
using ZV_Fiorello.Services.ProductServ;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();
builder.Services.AddApplicationDbContext(configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddCustomIdentity();
builder.Services.AddCustomSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
