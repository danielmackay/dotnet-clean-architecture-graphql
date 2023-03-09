using CA.GraphQL.Domain.Entities;

namespace CA.GraphQL.Application.Common.Interfaces;

public interface ITodoItemRepository
{
    IQueryable<TodoItem> GetAll();

    IQueryable<TodoItem> GetAll(int todoItemId);
}
