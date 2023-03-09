using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;

namespace CA.GraphQL.Infrastructure.Persistence.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TodoListRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TodoList> GetAll() => _dbContext.TodoLists;
}
