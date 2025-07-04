using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;

namespace GraphQL.Queries;

[QueryType]
public class TodoItemQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoItem> GetTodoItems([Service] ApplicationDbContext dbContext) => dbContext.TodoItems;

    [UseSingleOrDefault]
    public IQueryable<TodoItem> GetTodoItem(int id, [Service]ApplicationDbContext dbContext) => dbContext.TodoItems
        .Where(x => x.Id == id);
}