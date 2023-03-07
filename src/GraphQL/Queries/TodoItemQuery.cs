using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Queries;

[QueryType]
public class TodoItemQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoItem> GetTodoItems([Service(ServiceKind.Synchronized)] ApplicationDbContext dbContext) => dbContext.TodoItems.AsNoTracking();

    [UseSingleOrDefault]
    public IQueryable<TodoItem> GetTodoItem(int id, [Service(ServiceKind.Synchronized)] ApplicationDbContext dbContext) => dbContext.TodoItems.Where(ti => ti.Id == id);
}
