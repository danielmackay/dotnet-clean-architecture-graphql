using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace GraphQL.Queries;

[QueryType]
public class WeatherForecastQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts([Service] IWeatherForecastRepository repository) => await repository.GetAll();
}