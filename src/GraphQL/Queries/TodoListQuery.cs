using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Queries;

[QueryType]
public class TodoListQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoList> GetTodoLists([Service]ApplicationDbContext dbContext) => dbContext.TodoLists.AsNoTracking();
}