using CA.GraphQL.Domain.Entities;

namespace CA.GraphQL.Application.Common.Interfaces;

public interface ITodoListRepository
{
    IQueryable<TodoList> GetAll();
}
