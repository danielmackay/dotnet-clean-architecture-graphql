using CA.GraphQL.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using MediatR;

namespace GraphQL.Queries;

[QueryType]
public class WeatherForecastQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts([Service] ISender sender) => await sender.Send(new GetWeatherForecastsQuery());
}