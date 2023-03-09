using CA.GraphQL.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace CA.GraphQL.Application.Common.Interfaces;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetAll();
}
