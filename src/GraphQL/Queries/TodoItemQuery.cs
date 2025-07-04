using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;

namespace GraphQL.Queries;

[QueryType]
public class TodoItemQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<TodoItem> GetTodoItems(ITodoItemRepository repository) => repository.GetAll();

    [UseSingleOrDefault]
    public IQueryable<TodoItem> GetTodoItem(int id, ITodoItemRepository repository) => repository.GetAll(id);
}