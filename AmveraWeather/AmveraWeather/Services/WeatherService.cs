using AmveraWeather.Data;
using AmveraWeather.Models;
using Microsoft.EntityFrameworkCore;

namespace AmveraWeather.Services
{
    public class WeatherService(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<WeatherForecast>> GetForecastsAsync() => 
            await _dbContext.Forecasts.ToListAsync();
    }
}
