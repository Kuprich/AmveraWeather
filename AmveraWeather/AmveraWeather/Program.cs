using AmveraWeather.Components;
using AmveraWeather.Data;
using AmveraWeather.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreeSql")));

builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await DbInitializer.InitializeDbAsync(appDbContext);
    }
    catch (Exception ex)
    {
        new Exception(ex.Message);
    }
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AmveraWeather.Client._Imports).Assembly);

app.Run();
