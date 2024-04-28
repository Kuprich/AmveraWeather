using AmveraWeather.Models;
using Microsoft.EntityFrameworkCore;

namespace AmveraWeather.Data;

public static class DbInitializer
{
    public static async Task InitializeDbAsync(AppDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        if (dbContext.Forecasts.Any()) return;

        await dbContext.AddRangeAsync(GetForecasts());
        await dbContext.SaveChangesAsync();
    }

    private static WeatherForecast[] GetForecasts()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();
    }
}
