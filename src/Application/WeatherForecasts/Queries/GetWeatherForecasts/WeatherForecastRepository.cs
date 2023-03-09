using CA.GraphQL.Application.Common.Interfaces;

namespace CA.GraphQL.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly string[] Summaries = new[] {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IEnumerable<WeatherForecast>> GetAll()
    {
        var rng = new Random();

        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });

        return Task.FromResult(result);
    }
}