using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Queries;

[QueryType]
public class TodoListQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoList> GetTodoLists([Service(ServiceKind.Synchronized)] ApplicationDbContext dbContext) => dbContext.TodoLists.AsNoTracking();
}
