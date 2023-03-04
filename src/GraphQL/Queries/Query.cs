using CA.GraphQL.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Queries;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoItem> GetTodoItems([Service(ServiceKind.Synchronized)] ApplicationDbContext dbContext) => dbContext.TodoItems.AsNoTracking();

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoList> GetTodoLists([Service(ServiceKind.Synchronized)] ApplicationDbContext dbContext) => dbContext.TodoLists.AsNoTracking();

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts([Service] ISender sender) => await sender.Send(new GetWeatherForecastsQuery());
}
