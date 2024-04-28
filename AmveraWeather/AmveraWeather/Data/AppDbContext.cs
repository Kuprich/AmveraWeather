using AmveraWeather.Models;
using Microsoft.EntityFrameworkCore;

namespace AmveraWeather.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> Forecasts { get; set; }
}
