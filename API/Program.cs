using BLL_BuisnessLogicLayer;
using Microsoft.OpenApi.Models;
using Models;
using System.Text.Json.Serialization;
using Universe.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DbUniverse>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IService, Service>();
builder.Services.AddTransient<IService, Service>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Universe", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Universe");
    });
    app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
    app.UseRouting();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapRazorPages();
    });
}



/*app.UseHttpsRedirection();
app.UseStaticFiles();*/


app.Map("/Discoverer", discovererApp =>
{
    discovererApp.UseRouting();
    discovererApp.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages(); // Add this line to enable routing for Razor Pages
    });
});

app.MapControllers();

app.Run();
