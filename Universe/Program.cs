using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using System.Configuration;
using Universe.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using BLL;
using Universe.Models.galaxy;
using Universe.Models.discoverer;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

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
        builder.Services.AddTransient<ISpaceObjectService<Galaxy>, SpaceObjectService<Galaxy>>();
        builder.Services.AddTransient<ISpaceObjectService<Discoverer>, SpaceObjectService<Discoverer>>();
        builder.Services.AddTransient<ISpaceObjectService<Planet>, SpaceObjectService<Planet>>();
        builder.Services.AddTransient<ISpaceObjectService<Ship>, SpaceObjectService<Ship>>();
        builder.Services.AddTransient<ISpaceObjectService<Star>, SpaceObjectService<Star>>();
        builder.Services.AddTransient<ISpaceObjectService<StarSystem>, SpaceObjectService<StarSystem>>();


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