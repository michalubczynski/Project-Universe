using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using System.Configuration;
using Universe.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using BLL;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.json");
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddDbContext<DbUniverse>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cs"), b=> b.MigrationsAssembly("UI_Universe")));
        builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
        builder.Services.AddTransient<IGalaxyService, GalaxyService>();


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

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}