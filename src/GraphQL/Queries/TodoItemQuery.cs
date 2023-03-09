using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;

namespace GraphQL.Queries;

[QueryType]
public class TodoItemQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoItem> GetTodoItems([Service(ServiceKind.Synchronized)] ITodoItemRepository repository) => repository.GetAll();

    [UseSingleOrDefault]
    public IQueryable<TodoItem> GetTodoItem(int id, [Service(ServiceKind.Synchronized)] ITodoItemRepository repository) => repository.GetAll(id);
}
