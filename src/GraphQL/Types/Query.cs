using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Types;

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
}
